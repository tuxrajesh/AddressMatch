using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using AddressMatch.DataBase;
using AddressMatch.Entities;

namespace AddressMatch.Services
{
    /// <summary>
    /// Address Match Service Layer Code
    /// </summary>
    [Serializable]
    public class MatchDataService
    {
        public Database database { get; set; }

        #region [Separate Matches]

        private Collection<StandardAddress> stateAndZipMatches { get; set; }
        //private Collection<StandardAddress> cityStateMatches { get; set; }
        //private Collection<StandardAddress> streetNumberMatches { get; set; }
        //private Collection<StandardAddress> streetNameMatches { get; set; }

        #endregion

        private Collection<StandardAddress> icombinedResult { get; set; }

        /// <summary>
        /// Create a Data Service layer with the specified connection string.
        /// </summary>
        /// <param name="connectionString">Connection String</param>
        public MatchDataService(string connectionString)
        {
            database = new Database(connectionString);
        }

        /// <summary>
        /// Get Matching address from Database
        /// </summary>
        /// <param name="addressToBeStandardized">Address to be Standardized</param>
        /// <returns>Matching Addresses</returns>
        public Collection<StandardAddress> GetMatchingAddressFromDB(StandardAddress addressToBeStandardized)
        {
            // Get Matches by Address Parts.
            stateAndZipMatches = MatchesByStateAndZip(addressToBeStandardized);

            // TODO: get by fields and combine
            //streetNumberMatches = MatchesByDBField("primary_high_address", addressToBeStandardized.PrimaryHighAddress);
            //streetNameMatches = MatchesByDBField("street_name", addressToBeStandardized.StreetName);
            
            // Filter results using Levenshtein Distance.
            icombinedResult = GetCorrectedAddresses(stateAndZipMatches, addressToBeStandardized);

            return icombinedResult;
        }

        /// <summary>
        /// Get Standard Street Suffix
        /// </summary>
        /// <param name="streetSuffix">Entered Street Suffix</param>
        /// <returns>Standard Street Suffix</returns>
        public string GetStandardStreetSuffix(string streetSuffix)
        {
            string lstrQuery = "select standard_abbr from sgs_usps_std_suffix where common_usage = '"+ streetSuffix +"'";
            
            DataSet ldsData = database.ExecuteSelect(lstrQuery);

            if (ldsData != null && ldsData.Tables[0] != null)
            {
                return ldsData.Tables[0].Rows[0][0].ToString();
            }
            return string.Empty;
        }

        #region [Private Methods]

        /// <summary>
        /// Match by State Code and Zip Code
        /// </summary>
        /// <param name="addressToBeStandardized">Address to be Standardized</param>
        /// <returns>State Zip Matches.</returns>
        private Collection<StandardAddress> MatchesByStateAndZip(StandardAddress addressToBeStandardized)
        {
            // Select query to be used.
            string lstrQuery = string.Empty;

            // Get matches by State.
            if (!string.IsNullOrEmpty(addressToBeStandardized.StateCode) && !string.IsNullOrEmpty(addressToBeStandardized.ZipCode))
                lstrQuery = "select * from sgs_usps_file_address with(nolock) where state_code = '" + addressToBeStandardized.StateCode + "' and zipcode = '" + addressToBeStandardized.ZipCode + "'";
            else if (!string.IsNullOrEmpty(addressToBeStandardized.ZipCode))
                lstrQuery = "select * from sgs_usps_file_address with(nolock) where zipcode = '" + addressToBeStandardized.ZipCode + "'";
            else if (!string.IsNullOrEmpty(addressToBeStandardized.StateCode))
                lstrQuery = "select * from sgs_usps_file_address with(nolock) where state_code = '" + addressToBeStandardized.StateCode + "'";
            else
                return null;

            DataSet ldsMatches = database.ExecuteSelect(lstrQuery);
            return GetFileAddressFromDataSet(ldsMatches);
        }

        /// <summary>
        /// Match by DB Field
        /// </summary>
        /// <param name="astrDBField">DB Field</param>
        /// <param name="astrValue">Value</param>
        /// <returns>Address Match by Field</returns>
        private Collection<StandardAddress> MatchesByDBField(string astrDBField, string astrValue)
        {
            // Select query to be used.
            string lstrQuery = string.Empty;

            // Get matches by State.
            if (!string.IsNullOrEmpty(astrDBField) && !string.IsNullOrEmpty(astrValue))
                lstrQuery = "select * from sgs_usps_file_address with(nolock) where " + astrDBField + " like '%" + astrValue + "%'";
            else
                return null;

            DataSet ldsMatches = database.ExecuteSelect(lstrQuery);
            return GetFileAddressFromDataSet(ldsMatches);
        }

        /// <summary>
        /// Get File Address from DataSet
        /// </summary>
        /// <param name="adsFileAddresses">File Address DataSet</param>
        /// <returns>Collection of File Addresses</returns>
        private Collection<StandardAddress> GetFileAddressFromDataSet(DataSet adsFileAddresses)
        {
            Collection<StandardAddress> lFileAddresses = new Collection<StandardAddress>();

            foreach (DataRow ldrRow in adsFileAddresses.Tables[0].Rows)
            {
                StandardAddress lFileAddress = new StandardAddress();
                lFileAddress.UspsFileAddressId = ldrRow["usps_file_address_id"] != DBNull.Value ? Convert.ToInt32(ldrRow["usps_file_address_id"]) : 0;
                lFileAddress.DetailCode = ldrRow["detail_code"] != DBNull.Value ? ldrRow["detail_code"].ToString() : string.Empty;
                lFileAddress.ActionCode = ldrRow["action_code"] != DBNull.Value ? ldrRow["action_code"].ToString() : string.Empty;
                lFileAddress.ZipCode = ldrRow["zipcode"] != DBNull.Value ? ldrRow["zipcode"].ToString() : string.Empty;
                lFileAddress.UpdateKeyNumber = ldrRow["update_key_number"] != DBNull.Value ? ldrRow["update_key_number"].ToString() : string.Empty;
                lFileAddress.RecordType = ldrRow["record_type"] != DBNull.Value ? ldrRow["record_type"].ToString() : string.Empty;
                lFileAddress.CarrierRouteId = ldrRow["carrier_route_id"] != DBNull.Value ? ldrRow["carrier_route_id"].ToString() : string.Empty;
                lFileAddress.StreetPreDirection = ldrRow["street_pre_direction"] != DBNull.Value ? ldrRow["street_pre_direction"].ToString() : string.Empty;
                lFileAddress.StreetName = ldrRow["street_name"] != DBNull.Value ? ldrRow["street_name"].ToString() : string.Empty;
                lFileAddress.StreetSuffix = ldrRow["street_suffix"] != DBNull.Value ? ldrRow["street_suffix"].ToString() : string.Empty;
                lFileAddress.StreetPostDirection = ldrRow["street_post_direction"] != DBNull.Value ? ldrRow["street_post_direction"].ToString() : string.Empty;
                lFileAddress.PrimaryLowAddress = ldrRow["primary_low_address"] != DBNull.Value ? ldrRow["primary_low_address"].ToString() : string.Empty;
                lFileAddress.PrimaryHighAddress = ldrRow["primary_high_address"] != DBNull.Value ? ldrRow["primary_high_address"].ToString() : string.Empty;
                lFileAddress.PrimaryOddEvenBothFlag = ldrRow["primary_odd_even_both_flag"] != DBNull.Value ? ldrRow["primary_odd_even_both_flag"].ToString() : string.Empty;
                lFileAddress.BuildingOrFirmName = ldrRow["building_or_firm_name"] != DBNull.Value ? ldrRow["building_or_firm_name"].ToString() : string.Empty;
                lFileAddress.SecondaryAddressType = ldrRow["secondary_address_type"] != DBNull.Value ? ldrRow["secondary_address_type"].ToString() : string.Empty;
                lFileAddress.SecondaryLowAddress = ldrRow["secondary_low_address"] != DBNull.Value ? ldrRow["secondary_low_address"].ToString() : string.Empty;
                lFileAddress.SecondaryHighAddress = ldrRow["secondary_high_address"] != DBNull.Value ? ldrRow["secondary_high_address"].ToString() : string.Empty;
                lFileAddress.SecondaryOddEvenBothFlag = ldrRow["secondary_odd_even_both_flag"] != DBNull.Value ? ldrRow["secondary_odd_even_both_flag"].ToString() : string.Empty;
                lFileAddress.Zip4AddonLow = ldrRow["zip4_addon_low"] != DBNull.Value ? ldrRow["zip4_addon_low"].ToString() : string.Empty;
                lFileAddress.Zip4AddonHigh = ldrRow["zip4_addon_high"] != DBNull.Value ? ldrRow["zip4_addon_high"].ToString() : string.Empty;
                lFileAddress.BaseAltFlag = ldrRow["base_alt_flag"] != DBNull.Value ? ldrRow["base_alt_flag"].ToString() : string.Empty;
                lFileAddress.LACSStatusFlag = ldrRow["lacs_status_flag"] != DBNull.Value ? ldrRow["lacs_status_flag"].ToString() : string.Empty;
                lFileAddress.GovtBuildingFlag = ldrRow["govt_building_flag"] != DBNull.Value ? ldrRow["govt_building_flag"].ToString() : string.Empty;
                lFileAddress.USPSFinanceNumber = ldrRow["usps_financial_number"] != DBNull.Value ? ldrRow["usps_financial_number"].ToString() : string.Empty;
                lFileAddress.StateCode = ldrRow["state_code"] != DBNull.Value ? ldrRow["state_code"].ToString() : string.Empty;
                lFileAddress.CountyFIPSCode = ldrRow["county_fips_code"] != DBNull.Value ? ldrRow["county_fips_code"].ToString() : string.Empty;
                lFileAddress.CongressionalDistrict = ldrRow["congressional_district"] != DBNull.Value ? ldrRow["congressional_district"].ToString() : string.Empty;
                lFileAddress.MunicipalityKey = ldrRow["municipality_key"] != DBNull.Value ? ldrRow["municipality_key"].ToString() : string.Empty;
                lFileAddress.PRUrbanizationKey = ldrRow["pr_urbanization_flag"] != DBNull.Value ? ldrRow["pr_urbanization_flag"].ToString() : string.Empty;
                lFileAddress.CityStateKey = ldrRow["city_state_key"] != DBNull.Value ? ldrRow["city_state_key"].ToString() : string.Empty;
                lFileAddresses.Add(lFileAddress);
            }

            return lFileAddresses;
        }

        /// <summary>
        /// Apply Levenshtein Distance to get the most probable matches.
        /// </summary>
        /// <param name="matchesFromDB">Matches from DB</param>
        /// <param name="addressToBeStandardized">Address to be Standardized</param>
        /// <returns>File Address Matches.</returns>
        private Collection<StandardAddress> GetCorrectedAddresses(Collection<StandardAddress> matchesFromDB, StandardAddress addressToBeStandardized)
        {
            foreach (StandardAddress match in matchesFromDB)
                CalculateLevenshteinDistance(match, addressToBeStandardized);

            // Get exact match using Levenshtein Distance
            Collection<StandardAddress> resultingAddress = GetExactMatch(matchesFromDB, addressToBeStandardized);
            
            // Exact Match found.
            if (resultingAddress != null)
                return resultingAddress;

            // Get Possible Matches
            var leastLevenshteinDistantAddresses = matchesFromDB.Where
                    (address => address.LevenDistStreetNo <= Constants.MaxStreetNoTolerance
                             && address.LevenDistStreetPreDirection <= Constants.MaxStreetPreDirectionTolerance
                             && address.LevenDistStreetName <= Constants.MaxStreetNameTolerance
                             && address.LevenDistStreetPostDirection <= Constants.MaxStreetPostDirectionTolerance
                             && address.LevenDistStreetSuffix <= Constants.MaxStreetSuffixTolerance
                             && address.LevenDistSecondaryAddrType <= Constants.MaxSecondaryAddrTypeTolerance
                             && address.LevenDistSecondaryHighAddress <= Constants.MaxSecondaryHighAddrTolerance
                             && address.LevenDistSecondaryLowAddress <= Constants.MaxSecondaryLowAddrTolerance);

            if (leastLevenshteinDistantAddresses != null && leastLevenshteinDistantAddresses.Count() > 0)
            {
                return GetPossibleMatchFiltered(new Collection<StandardAddress>(leastLevenshteinDistantAddresses.ToList<StandardAddress>()), addressToBeStandardized);
            }
            else
                return new Collection<StandardAddress>();
        }
        
        /// <summary>
        /// Get Exact Match
        /// </summary>
        /// <param name="matchesFromDB">Match From DB</param>
        /// <param name="addressToBeStandardized">Address to be Standardized</param>
        /// <returns>Exact Match</returns>
        private Collection<StandardAddress> GetExactMatch(Collection<StandardAddress> matchesFromDB, StandardAddress addressToBeStandardized)
        {
            // Account for Zip+4
            if (!string.IsNullOrEmpty(addressToBeStandardized.Zip4AddonHigh))
            {
                var matches = matchesFromDB.Where
                (addr => addr.LevenDistStreetNo == 0
                      && addr.LevenDistStreetPreDirection == 0
                      && addr.LevenDistStreetName == 0
                      && addr.LevenDistStreetPostDirection == 0
                      && addr.LevenDistStreetSuffix == 0
                      && addr.LevenDistSecondaryAddrType == 0
                      && addr.LevenDistSecondaryHighAddress == 0
                      && addr.LevenDistSecondaryLowAddress == 0
                      && addr.LevenDistZipPlusfour == 0);

                if (matches != null && matches.Count() > 0)
                    return new Collection<StandardAddress>(matches.ToList<StandardAddress>());
                else
                    return null;
            }
            else
            {
                var matches = matchesFromDB.Where
                (addr => addr.LevenDistStreetNo == 0
                      && addr.LevenDistStreetPreDirection == 0
                      && addr.LevenDistStreetName == 0
                      && addr.LevenDistStreetPostDirection == 0
                      && addr.LevenDistStreetSuffix == 0
                      && addr.LevenDistSecondaryAddrType == 0
                      && addr.LevenDistSecondaryHighAddress == 0
                      && addr.LevenDistSecondaryLowAddress == 0);

                if (matches != null && matches.Count() > 0)
                    return new Collection<StandardAddress>(matches.ToList<StandardAddress>());
                else
                    return null;
            }
        }

        /// <summary>
        /// Get Possible Matches Filtered on Field
        /// </summary>
        /// <param name="matchesFromDB">Matches from DB</param>
        /// <param name="addressToBeStandardized">Address to be Standardized</param>
        /// <returns>Possible Matches</returns>
        private Collection<StandardAddress> GetPossibleMatchFiltered(Collection<StandardAddress> levenshteinFiltered, StandardAddress addressToBeStandardized)
        {
            // Filter on Street Number
            if (!string.IsNullOrEmpty(addressToBeStandardized.PrimaryHighAddress))
            {
                var minimumDistanceOnStreetNumber = levenshteinFiltered.Where(address => address.LevenDistStreetNo == levenshteinFiltered.Min(addr => addr.LevenDistStreetNo));
                
                if (minimumDistanceOnStreetNumber != null && minimumDistanceOnStreetNumber.Count() > 0)
                    return new Collection<StandardAddress>(minimumDistanceOnStreetNumber.ToList<StandardAddress>());
            }

            // No Filter Matched; Return the original.
            return levenshteinFiltered;
        }

        /// <summary>
        /// Calculate Levenshtein Distance
        /// </summary>
        /// <param name="standardAddress">Standard Address</param>
        /// <param name="addressToBeStandardized">Address to be Standardized</param>
        private void CalculateLevenshteinDistance(StandardAddress standardAddress, StandardAddress addressToBeStandardized)
        {
            standardAddress.LevenDistStreetNo = Helper.ComputeLevenshteinDistance(standardAddress.PrimaryHighAddress, addressToBeStandardized.PrimaryHighAddress);
            standardAddress.LevenDistBuildingOrFirmName = Helper.ComputeLevenshteinDistance(standardAddress.BuildingOrFirmName, standardAddress.BuildingOrFirmName);
            standardAddress.LevenDistStreetPreDirection = Helper.ComputeLevenshteinDistance(standardAddress.StreetPreDirection, addressToBeStandardized.StreetPreDirection);
            standardAddress.LevenDistStreetName = Helper.ComputeLevenshteinDistance(standardAddress.StreetName, addressToBeStandardized.StreetName);            
            standardAddress.LevenDistStreetSuffix = Helper.ComputeLevenshteinDistance(standardAddress.StreetSuffix, addressToBeStandardized.StreetSuffix);
            standardAddress.LevenDistStreetPostDirection = Helper.ComputeLevenshteinDistance(standardAddress.StreetPostDirection, addressToBeStandardized.StreetPostDirection);
            standardAddress.LevenDistSecondaryAddrType = Helper.ComputeLevenshteinDistance(standardAddress.SecondaryAddressType, addressToBeStandardized.SecondaryAddressType);
            standardAddress.LevenDistSecondaryHighAddress = Helper.ComputeLevenshteinDistance(standardAddress.SecondaryHighAddress, addressToBeStandardized.SecondaryHighAddress);
            standardAddress.LevenDistSecondaryLowAddress = Helper.ComputeLevenshteinDistance(standardAddress.SecondaryLowAddress, addressToBeStandardized.SecondaryLowAddress);
            standardAddress.LevenDistZipPlusfour = Helper.ComputeLevenshteinDistance(standardAddress.Zip4AddonHigh, addressToBeStandardized.Zip4AddonHigh);
        }

        #endregion
    }
}
