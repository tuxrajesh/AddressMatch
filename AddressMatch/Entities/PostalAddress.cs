using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AddressMatch.Entities
{
    /// <summary>
    /// Postal Address Format
    /// </summary>
    [Serializable]
    public class PostalAddress
    {
        public string BuildingOrFirmName { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip5 { get; set; }
        public string Zip4 { get; set; }
        public int UspsAddressId { get; set; }

        /// <summary>
        /// Create string representation of Postal Address
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            string postalAddressString = string.Empty;

            postalAddressString = Helper.AppendStringWithChar(postalAddressString, BuildingOrFirmName, enmAppendCharacter.Space);
            postalAddressString = Helper.AppendStringWithChar(postalAddressString, AddressLine1, enmAppendCharacter.Space);
            postalAddressString = Helper.AppendStringWithChar(postalAddressString, AddressLine2, enmAppendCharacter.Space);
            postalAddressString = Helper.AppendStringWithChar(postalAddressString, City, enmAppendCharacter.Space);
            postalAddressString = Helper.AppendStringWithChar(postalAddressString, State, enmAppendCharacter.Space);
            postalAddressString = Helper.AppendStringWithChar(postalAddressString, Zip5, enmAppendCharacter.Space);
            postalAddressString = Helper.AppendStringWithChar(postalAddressString, Zip4, enmAppendCharacter.Dash);

            return postalAddressString;
        }
    }
}
