using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SRS.ViewModels
{
    public class UserViewModel
    {
        public string Id { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        [Compare("Password", ErrorMessage = "The password and confirm password do not match.")]
        public string ConfirmPassword { get; set; }

        public string Role { get; set; }
        [Required]
        public string SelectedRoleId { get; set; }

        [Display(Name = "E-mail Address:")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Display(Name = "Full Name:")]
        [StringLength(256)]
        public string FullName { get; set; }

        [Display(Name = "Phone Number:")]
        [StringLength(20)]
        public string Phone { get; set; }

        [Display(Name = "Location:")]
        [StringLength(50)]
        public string Location { get; set; }

        [Display(Name = "Business Area:")]
        [StringLength(50)]
        public string BusinessArea { get; set; }

        [Display(Name = "Test Filed")]
        [StringLength(256)]
        public string TestFiled { get; set; }
    }
}
