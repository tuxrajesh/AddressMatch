using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AddressMatch.Entities;
using AddressMatch.Services;

namespace AddressMatch
{
    public class Validator
    {
        /// <summary>        
        /// Validates the Address
        /// Standardizes and returns matching standard address.
        /// </summary>
        /// <param name="address1">Address 1</param>
        /// <param name="address2">Address 2</param>
        /// <param name="city">City</param>
        /// <param name="state">State</param>
        /// <param name="zipCode">Zip Code</param>
        /// <param name="zipplusfour">Zip +4</param>
        /// <returns>Standardized Address</returns>
        public Collection<PostalAddress> ValidateAddress(string address1, string address2, string city, string state, string zipCode, string zipplusfour)
        {
            Collection<PostalAddress> standardized = new Collection<PostalAddress>();

            // Create the Address object.
            PostalAddress addressInput = new PostalAddress() { AddressLine1 = address1, AddressLine2 = address2, City = city, State = state, Zip5 = zipCode, Zip4 = zipplusfour };
            StandardAddressParser stdParser = new StandardAddressParser();
            StandardAddress nonstdAddress = stdParser.ConvertPostalAddressToStandardAddress(addressInput);

            // Get Standard Matches.
            MatchDataService matchService = new MatchDataService(Constants.ConnectionString);
            Collection<StandardAddress> matches = matchService.GetMatchingAddressFromDB(nonstdAddress);

            // Convert matches to Postal Address
            PostalAddressParser postalParser = new PostalAddressParser();
            standardized = postalParser.ConvertStandardAddressToPostalAddress(matches);

            return standardized;
        }
    }
}
