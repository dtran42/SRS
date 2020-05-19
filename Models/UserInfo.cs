using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SRS.Models
{
    public class UserInfo
    {
        [Display(Name = "Email")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Display(Name = "Full Name")]
        [StringLength(256)]
        public string FullName { get; set; }
        
        [Display(Name = "Phone Number")]
        [StringLength(20)]
        public string Phone { get; set; }
        
        [Display(Name = "Location")]
        [StringLength(50)]
        public string Location { get; set; }
        
        [Display(Name = "Business Area")]
        [StringLength(50)]
        public string BusinessArea { get; set; }
        
        [Display(Name = "Test Filed")]
        [StringLength(256)]
        public string TestFiled { get; set; }
    }
}
