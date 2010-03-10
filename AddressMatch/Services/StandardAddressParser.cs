using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using AddressMatch.Entities;

namespace AddressMatch.Services
{
    /// <summary>
    /// Standard Address Parsing Logic
    /// </summary>
    [Serializable]
    public class StandardAddressParser
    {
        /// <summary>
        /// File Address
        /// </summary>
        private StandardAddress StdAddress;

        #region [Public Methods]

        /// <summary>
        /// Convert Postal Address to Standard Address
        /// </summary>
        /// <param name="postalAddress">Postal Address</param>
        /// <returns>Standard Address</returns>
        public StandardAddress ConvertPostalAddressToStandardAddress(PostalAddress postalAddress)
        {
            StdAddress = new StandardAddress();

            GetAddress1Fields(postalAddress);
            GetAddress2Fields(postalAddress);
            GetCityStateFields(postalAddress);
            GetZipFields(postalAddress);

            return StdAddress;
        }

        #endregion

        #region [Private Methods]

        /// <summary>
        /// Get Address Line 1 from Postal Address
        /// Address Line 1 is split into:
        /// PrimaryHighAddress            
        /// StreetPreDirection
        /// StreetName            
        /// StreetSuffix
        /// StreetPostDirection
        /// </summary>
        /// <param name="postalAddress">Postal Address</param>
        private void GetAddress1Fields(PostalAddress postalAddress)
        {
            if (StdAddress == null)
                StdAddress = new StandardAddress();

            Regex lregex = new Regex(@"(?<streetno>(\d{1,5}))\s+(?<streetpre>(("+ Constants.StreetDirections +@")\s+)?)(?<streetname>(\S+(?:\s+\S+)*?)\s+)(?<streetsuffix>(("+ Constants.StreetSuffixCommonUsage + @"))(\s+)?)(?<streetpost>(("+ Constants.StreetDirections + @")+)?)");
            Match lregexMatch = lregex.Match(postalAddress.AddressLine1);

            if (lregexMatch.Success)
            {
                StdAddress.PrimaryHighAddress = lregexMatch.Groups["streetno"].Value.Trim();
                StdAddress.PrimaryLowAddress = lregexMatch.Groups["streetno"].Value.Trim();

                //TODO: Apply Standardization like Suffix for Street Pre and Post Direction.
                StdAddress.StreetPreDirection = lregexMatch.Groups["streetpre"].Value.Trim();
                StdAddress.StreetName = lregexMatch.Groups["streetname"].Value.Trim();

                // Get Standard Suffix.
                if (!string.IsNullOrEmpty(lregexMatch.Groups["streetsuffix"].Value))
                    StdAddress.StreetSuffix = GetStandardStreetSuffix(lregexMatch.Groups["streetsuffix"].Value);
                else
                    StdAddress.StreetSuffix = string.Empty;

                StdAddress.StreetPostDirection = lregexMatch.Groups["streetpost"].Value.Trim();
            }
        }

        /// <summary>
        /// Get Address 2 Fields
        /// Address Line 2 is split up into:
        /// SecondaryHighAddress, SecondaryLowAddress (both are same mostly)
        /// SecondaryAddressType
        /// </summary>
        /// <param name="postalAddress">Postal Address</param>
        private void GetAddress2Fields(PostalAddress postalAddress)
        {            
            if (StdAddress == null)
                StdAddress = new StandardAddress();

            Regex lregex = new Regex(@"(?:(?<addrtype>("+ Constants.AddressTypes + @"))?(?:(?:\s+|-)(?<doorno>\d+)))$");
            Match lregexMatch = lregex.Match(postalAddress.AddressLine2);

            if (lregexMatch.Success)
            {
                StdAddress.SecondaryAddressType = lregexMatch.Groups["addrtype"].Value;
                StdAddress.SecondaryHighAddress = lregexMatch.Groups["doorno"].Value;
                StdAddress.SecondaryLowAddress = lregexMatch.Groups["doorno"].Value;
            }
        }

        private void GetCityStateFields(PostalAddress postalAddress)
        {

        }

        /// <summary>
        /// Get Zip Code and Zip+4
        /// Zip 5 is Zip Code
        /// Zip 4 is Zip4AddonHigh and Zip4AddonLow
        /// </summary>
        /// <param name="postalAddress">Postal Address</param>
        private void GetZipFields(PostalAddress postalAddress)
        {
            if (StdAddress == null)
                StdAddress = new StandardAddress();

            StdAddress.ZipCode = postalAddress.Zip5;
            StdAddress.Zip4AddonHigh = postalAddress.Zip4;
            StdAddress.Zip4AddonLow = postalAddress.Zip4;
        }

        /// <summary>
        /// Get Standard Street Suffix
        /// </summary>
        /// <param name="streetSuffix">Entered Street Suffix</param>
        /// <returns>Standard Street Suffix</returns>
        private string GetStandardStreetSuffix(string streetSuffix)
        {
            return new MatchDataService(Constants.ConnectionString).GetStandardStreetSuffix(streetSuffix);
        }

        #endregion
    }
}
