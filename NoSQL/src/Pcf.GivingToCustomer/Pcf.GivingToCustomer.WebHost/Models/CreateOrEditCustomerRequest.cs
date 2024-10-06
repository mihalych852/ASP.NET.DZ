using System;
using System.Collections.Generic;

namespace Pcf.GivingToCustomer.WebHost.Models
{
    /// <example>
    /// {
    ///  "firstName": "Иван",
    ///  "lastName": "Васильев",
    ///  "email": "ivan_vasiliev@somemail.ru",
    ///  "preferences": [
    ///    "Auto"
    ///  ]
    ///}
    /// </example>>
    public class CreateOrEditCustomerRequest
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        //public List<Guid> PreferenceIds { get; set; }
        public List<string> Preferences { get; set; }
    }
}