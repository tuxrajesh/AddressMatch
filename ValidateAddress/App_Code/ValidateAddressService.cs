using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web;
using System.Web.Services;
using AddressMatch;
using AddressMatch.Entities;

[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]

public class ValidateAddressService : System.Web.Services.WebService
{
    public ValidateAddressService () {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    [WebMethod]
    public string ValidateAddress(string address1, string address2, string city, string state, string zipCode, string zipplusfour)
    {
        string normalizedAddress = string.Empty;
        
        Validator addressMatch = new Validator();
        Collection<PostalAddress> standardAddresses = addressMatch.ValidateAddress(address1, address2, city, state, zipCode, zipplusfour);

        foreach (PostalAddress postalAddress in standardAddresses)
        {
            normalizedAddress = Helper.AppendStringWithChar(normalizedAddress, postalAddress.ToString(), enmAppendCharacter.SemiColon);
        }

        return normalizedAddress;
    }
    
}