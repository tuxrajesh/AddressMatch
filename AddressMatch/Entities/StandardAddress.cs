using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AddressMatch.Entities
{
    /// <summary>
    /// Standard Address Format
    /// </summary>
    [Serializable]
    public class StandardAddress
    {
        public int UspsFileAddressId { get; set; }
        public string DetailCode { get; set; }
        public string ZipCode { get; set; }
        public string UpdateKeyNumber { get; set; }
        public string ActionCode { get; set; }
        public string RecordType { get; set; }
        public string CarrierRouteId { get; set; }
        public string StreetPreDirection { get; set; }
        public string StreetName { get; set; }
        public string StreetSuffix { get; set; }
        public string StreetPostDirection { get; set; }
        public string PrimaryLowAddress { get; set; }
        public string PrimaryHighAddress { get; set; }
        public string PrimaryOddEvenBothFlag { get; set; }
        public string BuildingOrFirmName { get; set; }
        public string SecondaryAddressType { get; set; }
        public string SecondaryLowAddress { get; set; }
        public string SecondaryHighAddress { get; set; }
        public string SecondaryOddEvenBothFlag { get; set; }
        public string Zip4AddonLow { get; set; }
        public string Zip4AddonHigh { get; set; }
        public string BaseAltFlag { get; set; }
        public string LACSStatusFlag { get; set; }
        public string GovtBuildingFlag { get; set; }
        public string USPSFinanceNumber { get; set; }
        public string StateCode { get; set; }
        public string CountyFIPSCode { get; set; }
        public string CongressionalDistrict { get; set; }
        public string MunicipalityKey { get; set; }
        public string PRUrbanizationKey { get; set; }
        public string CityStateKey { get; set; }

        #region [Levenshtein Distance Holder]

        public int LevenDistStreetNo { get; set; }
        public int LevenDistStreetPreDirection { get; set; }
        public int LevenDistBuildingOrFirmName { get; set; }
        public int LevenDistStreetSuffix { get; set; }
        public int LevenDistStreetName { get; set; }
        public int LevenDistStreetPostDirection { get; set; }
        public int LevenDistSecondaryAddrType { get; set; }
        public int LevenDistSecondaryHighAddress { get; set; }
        public int LevenDistSecondaryLowAddress { get; set; }

        public int LevenDistCity { get; set; } // Unused
        public int LevenDistZipCode { get; set; } // Unused
        public int LevenDistZipPlusfour { get; set; }

        #endregion
    }
}
