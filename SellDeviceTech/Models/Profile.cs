using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SellDeviceTech.Models
{
    public class Profile
    {
        public string Id { get; set; }
        public string given_name { get; set; }
        public string name { get; set; }
        public string picture { get; set; }
        public string email { get; set; }
        public string email_verified { get; set; }
        public string Gender { get; set; }
        public string ObjectType { get; set; }
    }
}