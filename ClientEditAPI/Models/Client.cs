using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ClientEditAPI.Models
{
    public class Client
    {
        public string FirstName { get; set; }
        public string Surname { get; set; }
        public string IdentityType { get; set; }
        public string IdentityNumber { get; set; }
        public string DOB { get; set; }
    }
}