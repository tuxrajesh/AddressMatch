using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using AddressMatch.Entities;

namespace AddressMatch.Services
{
    /// <summary>
    /// Postal Address Parser
    /// </summary>
    [Serializable]
    public class PostalAddressParser
    {
        #region [Private Methods]

        /// <summary>
        /// Convert Standard Address to Postal Address
        /// </summary>
        /// <param name="standardAddresses">Standard Addresses</param>
        /// <returns>Postal Address Collection</returns>
        public Collection<PostalAddress> ConvertStandardAddressToPostalAddress(Collection<StandardAddress> standardAddresses)
        {
            Collection<PostalAddress> postalAddresses = new Collection<PostalAddress>();

            foreach (StandardAddress lfileAddress in standardAddresses)
                postalAddresses.Add(ConvertStandardAddressToPostal(lfileAddress));

            return postalAddresses;
        }

        /// <summary>
        /// Convert Standard Address to Postal Address
        /// </summary>
        /// <param name="standardAddress">Standard Address</param>
        /// <returns>Postal Address</returns>
        public PostalAddress ConvertStandardAddressToPostal(StandardAddress standardAddress)
        {
            PostalAddress postalAddress = new PostalAddress();

            postalAddress.BuildingOrFirmName = standardAddress.BuildingOrFirmName;

            // Address Line 1
            // <Address Number> <Street Pre Direction> <Street Name> <Street Suffix> <Street Post Direction>
            string buildingAddressLine1 = string.Empty;
            buildingAddressLine1 = Helper.AppendStringWithChar(buildingAddressLine1, standardAddress.PrimaryHighAddress, enmAppendCharacter.Space);
            buildingAddressLine1 = Helper.AppendStringWithChar(buildingAddressLine1, standardAddress.StreetPreDirection, enmAppendCharacter.Space);
            buildingAddressLine1 = Helper.AppendStringWithChar(buildingAddressLine1, standardAddress.StreetName, enmAppendCharacter.Space);
            buildingAddressLine1 = Helper.AppendStringWithChar(buildingAddressLine1, standardAddress.StreetSuffix, enmAppendCharacter.Space);
            buildingAddressLine1 = Helper.AppendStringWithChar(buildingAddressLine1, standardAddress.StreetPostDirection, enmAppendCharacter.Space);
            postalAddress.AddressLine1 = buildingAddressLine1;

            // Address Line 2
            // <Secondary Address Type> <Secondary Address>
            string buildingAddressLine2 = string.Empty;
            buildingAddressLine2 = Helper.AppendStringWithChar(buildingAddressLine2, standardAddress.SecondaryAddressType, enmAppendCharacter.Space);
            buildingAddressLine2 = Helper.AppendStringWithChar(buildingAddressLine2, standardAddress.SecondaryHighAddress, enmAppendCharacter.Space);
            postalAddress.AddressLine2 = buildingAddressLine2;

            postalAddress.City = standardAddress.CityStateKey; // TODO: Need to retrieve from city-state mapping
            postalAddress.State = standardAddress.StateCode;
            postalAddress.Zip5 = standardAddress.ZipCode;
            postalAddress.Zip4 = standardAddress.Zip4AddonHigh;            

            postalAddress.UspsAddressId = standardAddress.UspsFileAddressId;

            return postalAddress;
        }

        #endregion
    }
}
