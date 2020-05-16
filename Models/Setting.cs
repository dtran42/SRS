using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SRS.Models
{
    public class Settings
    {
        public string WFSchema { get; set; }

        public List<ApplicationUser> Employees { get; set; }
        public List<IdentityRole> Roles { get; set; }
        public List<StructDivision> StructDivision { get; set; }

        public string SchemeName
        {
            get { return "SimpleWF"; }
        }
    }
}
