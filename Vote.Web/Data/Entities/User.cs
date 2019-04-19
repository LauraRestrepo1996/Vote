
namespace Vote.Web.Data.Entities
{
    using Microsoft.AspNetCore.Identity;
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class User : IdentityUser
    {
        [Display(Name = "First Name")]
        [MaxLength(100, ErrorMessage = "The fild {0} only can contain {1} characters length.")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]

        [MaxLength(100, ErrorMessage = "The fild {0} only can contain {1} characters length.")]
        public string LastName { get; set; }

        [Display(Name = "Full Name")]
        public string FullName { get { return $"{this.FirstName} {this.LastName}"; } }


        public string Ocupattion { get; set; }

        public string Stratum { get; set; }

        public string Gender { get; set; }

        public DateTime Birthdate { get; set; }

        [Display(Name = "Phone Number")]
        public override string PhoneNumber { get => base.PhoneNumber; set => base.PhoneNumber = value; }

        [Display(Name = "Email Confirmed")]
        public override bool EmailConfirmed { get => base.EmailConfirmed; set => base.EmailConfirmed = value; }

        [NotMapped] // Nos indica si el usuario es Admin o no
        [Display(Name = "Is Admin?")]
        public bool IsAdmin { get; set; }


        public int CityId { get; set; }

        public City City { get; set; }
 


    }
}
