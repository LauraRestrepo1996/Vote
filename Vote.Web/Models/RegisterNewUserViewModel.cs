using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Vote.Web.Models
{
    using System.ComponentModel.DataAnnotations;

    public class RegisterNewUserViewModel
    {
        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required]
        [Display(Name = "Occupation")]
        public string Occupation { get; set; }

        [Required]
        [Display(Name = "Stratum")]
        public string Stratum { get; set; }

        [Required]
        [Display(Name = "Gender")]
        public string Gender { get; set; }

        [Required]
        [Display(Name = "Birthdate")]
        public DateTime Birthdate { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public string Username { get; set; }

        [Required]
        [MinLength (6)]
        public string Password { get; set; }

        [Required]
        [Compare("Password")]
        public string Confirm { get; set; }
    }


}
