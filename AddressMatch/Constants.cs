using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AddressMatch
{
    /// <summary>
    /// Holds constants and Enumerations.
    /// </summary>
    [Serializable]
    public class Constants
    {
        /// <summary>
        /// Individual Field Length
        /// </summary>
        public static class FieldLength
        {
            public const int DetailCode = 1;
            public const int ZipCode = 5;
            public const int UpdateKeyNumber = 10;
            public const int ActionCode = 1;
            public const int RecordType = 1;
            public const int CarrierRouteId = 4;
            public const int StreetPreDirection = 2;
            public const int StreetName = 28;
            public const int StreetSuffix = 4;
            public const int StreetPostDirection = 2;
            public const int PrimaryLowAddress = 10;
            public const int PrimaryHighAddress = 10;
            public const int PrimaryOddEvenBothFlag = 1;
            public const int BuildingOrFirmName = 40;
            public const int SecondaryAddressType = 4;
            public const int SecondaryLowAddress = 8;
            public const int SecondaryHighAddress = 8;
            public const int SecondaryOddEvenBothFlag = 1;
            public const int Zip4AddonLow = 4;
            public const int Zip4AddonHigh = 4;
            public const int BaseAltFlag = 1;
            public const int LACSStatusFlag = 1;
            public const int GovtBuildingFlag = 1;
            public const int USPSFinanceNumber = 6;
            public const int StateCode = 2;
            public const int CountyFIPSCode = 3;
            public const int CongressionalDistrict = 2;
            public const int MunicipalityKey = 6;
            public const int PRUrbanizationKey = 6;
            public const int CityStateKey = 6;
        }

        /// <summary>
        /// Field Start Position
        /// </summary>
        public static class FieldStartPosition
        {
            public const int DetailCode = 0;
            public const int ZipCode = 1;
            public const int UpdateKeyNumber = 6;
            public const int ActionCode = 16;
            public const int RecordType = 17;
            public const int CarrierRouteId = 18;
            public const int StreetPreDirection = 22;
            public const int StreetName = 24;
            public const int StreetSuffix = 52;
            public const int StreetPostDirection = 56;
            public const int PrimaryLowAddress = 58;
            public const int PrimaryHighAddress = 68;
            public const int PrimaryOddEvenBothFlag = 78;
            public const int BuildingOrFirmName = 79;
            public const int SecondaryAddressType = 119;
            public const int SecondaryLowAddress = 123;
            public const int SecondaryHighAddress = 131;
            public const int SecondaryOddEvenBothFlag = 139;
            public const int Zip4AddonLow = 140;
            public const int Zip4AddonHigh = 144;
            public const int BaseAltFlag = 148;
            public const int LACSStatusFlag = 149;
            public const int GovtBuildingFlag = 150;
            public const int USPSFinanceNumber = 151;
            public const int StateCode = 157;
            public const int CountyFIPSCode = 159;
            public const int CongressionalDistrict = 162;
            public const int MunicipalityKey = 164;
            public const int PRUrbanizationKey = 170;
            public const int CityStateKey = 176;
        }

        public const int MaxStreetNoTolerance = 3;
        public const int MaxBuildingOrFirmNameTolerance = 3;
        public const int MaxStreetPreDirectionTolerance = 3;
        public const int MaxStreetNameTolerance = 3;
        public const int MaxStreetSuffixTolerance = 3;
        public const int MaxStreetPostDirectionTolerance = 3;
        public const int MaxSecondaryAddrTypeTolerance = 3;
        public const int MaxSecondaryHighAddrTolerance = 3;
        public const int MaxSecondaryLowAddrTolerance = 3;
        
        public const string AddressTypes = "APT|BSMT|BLDG|DEPT|FL|FRNT|HNGR|LBBY|LOT|LOWR|OFC|PH|PIER|REAR|RM|SIDE|SLIP|SPC|STOP|STE|TRLR|UNIT|UPPR";
        public const string StreetDirections = "N|NW|NE|S|SW|SE|E|W";
        public const string StreetSuffixes = "ALY|ANX|ARC|AVE|BCH|BG|BGS|BLF|BLFS|BLVD|BND|BR|BRG|BRK|BRKS|BTM|BYP|BYU|CIR|CIRS|CLB|CLF|CLFS|CMN|COR|CORS|CP|CPE|CRES|CRK|CRSE|CRST|CSWY|CT|CTR|CTRS|CTS|CURV|CV|CVS|CYN|DL|DM|DR|DRS|DV|EST|ESTS|EXPY|EXT|EXTS|FALL|FLD|FLDS|FLS|FLT|FLTS|FRD|FRDS|FRG|FRGS|FRK|FRKS|FRST|FRY|FT|FWY|GDN|GDNS|GLN|GLNS|GRN|GRNS|GRV|GRVS|GTWY|HBR|HBRS|HL|HLS|HOLW|HTS|HVN|HWY|INLT|IS|ISLE|ISS|JCT|JCTS|KNL|KNLS|KY|KYS|LAND|LCK|LCKS|LDG|LF|LGT|LGTS|LK|LKS|LN|LNDG|LOOP|MALL|MDW|MDWS|MEWS|ML|MLS|MNR|MNRS|MSN|MT|MTN|MTNS|MTWY|NCK|OPAS|ORCH|OVAL|PARK|PASS|PATH|PIKE|PKWY|PL|PLN|PLNS|PLZ|PNE|PNES|PR|PRT|PRTS|PSGE|PT|PTS|RADL|RAMP|RD|RDG|RDGS|RDS|RIV|RNCH|ROW|RPD|RPDS|RST|RTE|RUE|RUN|SHL|SHLS|SHR|SHRS|SKWY|SMT|SPG|SPGS|SPUR|SQ|SQS|ST|STA|STRA|STRM|STS|TER|TPKE|TRAK|TRCE|TRFY|TRL|TRWY|TUNL|UN|UNS|UPAS|VIA|VIS|VL|VLG|VLGS|VLY|VLYS|VW|VWS|WALK|WALL|WAY|WAYS|WL|WLS|XING|XRD";
        public const string StreetSuffixCommonUsage = "ALLEE|ALLEY|ALLY|ALY|ANEX|ANNEX|ANNX|ANX|ARC|ARCADE|AV|AVE|AVEN|AVENU|AVENUE|AVN|AVNUE|BAYOO|BAYOU|BCH|BEACH|BEND|BND|BLF|BLUF|BLUFF|BLUFFS|BOT|BOTTM|BOTTOM|BTM|BLVD|BOUL|BOULEVARD|BOULV|BR|BRANCH|BRNCH|BRDGE|BRG|BRIDGE|BRK|BROOK|BROOKS|BURG|BURGS|BYP|BYPA|BYPAS|BYPASS|BYPS|CAMP|CMP|CP|CANYN|CANYON|CNYN|CYN|CAPE|CPE|CAUSEWAY|CAUSWAY|CSWY|CEN|CENT|CENTER|CENTR|CENTRE|CNTER|CNTR|CTR|CENTERS|CIR|CIRC|CIRCL|CIRCLE|CRCL|CRCLE|CIRCLES|CLF|CLIFF|CLFS|CLIFFS|CLB|CLUB|COMMON|COR|CORNER|CORNERS|CORS|COURSE|CRSE|COURT|CRT|CT|COURTS|CT|COVE|CV|COVES|CK|CR|CREEK|CRK|CRECENT|CRES|CRESCENT|CRESENT|CRSCNT|CRSENT|CRSNT|CREST|CROSSING|CRSSING|CRSSNG|XING|CROSSROAD|CURVE|DALE|DL|DAM|DM|DIV|DIVIDE|DV|DVD|DR|DRIV|DRIVE|DRV|DRIVES|EST|ESTATE|ESTATES|ESTS|EXP|EXPR|EXPRESS|EXPRESSWAY|EXPW|EXPY|EXT|EXTENSION|EXTN|EXTNSN|EXTENSIONS|EXTS|FALL|FALLS|FLS|FERRY|FRRY|FRY|FIELD|FLD|FIELDS|FLDS|FLAT|FLT|FLATS|FLTS|FORD|FRD|FORDS|FOREST|FORESTS|FRST|FORG|FORGE|FRG|FORGES|FORK|FRK|FORKS|FRKS|FORT|FRT|FT|FREEWAY|FREEWY|FRWAY|FRWY|FWY|GARDEN|GARDN|GDN|GRDEN|GRDN|GARDENS|GDNS|GRDNS|GATEWAY|GATEWY|GATWAY|GTWAY|GTWY|GLEN|GLN|GLENS|GREEN|GRN|GREENS|GROV|GROVE|GRV|GROVES|HARB|HARBOR|HARBR|HBR|HRBOR|HARBORS|HAVEN|HAVN|HVN|HEIGHT|HEIGHTS|HGTS|HT|HTS|HIGHWAY|HIGHWY|STRM|ST|STR|STREET|STRT|STREETS|SMT|SUMIT|SUMITT|SUMMIT|TER|TERR|TERRACE|THROUGHWAY|TRACE|TRACES|TRCE|TRACK|TRACKS|TRAK|TRK|TRKS|TRAFFICWAY|TRFY|TR|TRAIL|TRAILS|TRL|TRLS|TUNEL|TUNL|TUNLS|TUNNEL|TUNNELS|TUNNL|TPK|TPKE|TRNPK|TRPK|TURNPIKE|TURNPK|UNDERPASS|UN|UNION|UNIONS|VALLEY|VALLY|VLLY|VLY|VALLEYS|VLYS|VDCT|VIA|VIADCT|VIADUCT|VIEW|VW|VIEWS|VWS|VILL|VILLAG|VILLAGE|VILLG|VILLIAGE|VLG|VILLAGES|VLGS|VILLE|VL|VIS|VIST|VISTA|VST|VSTA|WALK|WALKS|WALL|WAY|WY|WAYS|WELL|WELLS|WLS|HIWAY|HIWY|HWAY|HWY|HILL|HL|HILLS|HLS|HLLW|HOLLOW|HOLLOWS|HOLW|HOLWS|INLET|INLT|IS|ISLAND|ISLND|ISLANDS|ISLNDS|ISS|ISLE|ISLES|JCT|JCTION|JCTN|JUNCTION|JUNCTN|JUNCTON|JCTNS|JCTS|JUNCTIONS|KEY|KY|KEYS|KYS|KNL|KNOL|KNOLL|KNLS|KNOLLS|LAKE|LK|LAKES|LKS|LAND|LANDING|LNDG|LNDNG|LA|LANE|LANES|LN|LGT|LIGHT|LIGHTS|LF|LOAF|LCK|LOCK|LCKS|LOCKS|LDG|LDGE|LODG|LODGE|LOOP|LOOPS|MALL|MANOR|MNR|MANORS|MNRS|MDW|MEADOW|MDWS|MEADOWS|MEDOWS|MEWS|MILL|ML|MILLS|MLS|MISSION|MISSN|MSN|MSSN|MOTORWAY|MNT|MOUNT|MT|MNTAIN|MNTN|MOUNTAIN|MOUNTIN|MTIN|MTN|MNTNS|MOUNTAINS|NCK|NECK|ORCH|ORCHARD|ORCHRD|OVAL|OVL|OVERPASS|PARK|PK|PRK|PARKS|PARKWAY|PARKWY|PKWAY|PKWY|PKY|PARKWAYS|PKWYS|PASS|PASSAGE|PATH|PATHS|PIKE|PIKES|PINE|PINES|PNES|PL|PLACE|PLAIN|PLN|PLAINES|PLAINS|PLNS|PLAZA|PLZ|PLZA|POINT|PT|POINTS|PTS|PORT|PRT|PORTS|PRTS|PR|PRAIRIE|PRARIE|PRR|RAD|RADIAL|RADIEL|RADL|RAMP|RANCH|RANCHES|RNCH|RNCHS|RAPID|RPD|RAPIDS|RPDS|REST|RST|RDG|RDGE|RIDGE|RDGS|RIDGES|RIV|RIVER|RIVR|RVR|RD|ROAD|RDS|ROADS|ROUTE|ROW|RUE|RUN|SHL|SHOAL|SHLS|SHOALS|SHOAR|SHORE|SHR|SHOARS|SHORES|SHRS|SKYWAY|SPG|SPNG|SPRING|SPRNG|SPGS|SPNGS|SPRINGS|SPRNGS|SPUR|SPURS|SQ|SQR|SQRE|SQU|SQUARE|SQRS|SQUARES|STA|STATION|STATN|STN|STRA|STRAV|STRAVE|STRAVEN|STRAVENUE|STRAVN|STRVN|STRVNUE|STREAM|STREME";
        public const string ConnectionString = "Data Source=192.10.200.98;User ID=VRSDEV;password=89bx#D1A;Initial Catalog=VRS_SANDBOX;TimeOut=10;Persist Security Info=True;Asynchronous Processing=True";
    }

    public enum enmAppendCharacter
    {
        Comma = 1,
        Space = 2,
        CommaSpace = 3,
        Dash = 4,
        SemiColon = 5
    }
}
