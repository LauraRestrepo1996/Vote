
namespace Vote.Web.Data.Entities
{
    using Microsoft.AspNetCore.Identity;
    using System;
    using System.ComponentModel.DataAnnotations;

    public class User : IdentityUser
    {
        [MaxLength(100, ErrorMessage = "The fild {0} only can contain {1} characters length.")]
        public string FirstName { get; set; }

        [MaxLength(100, ErrorMessage = "The fild {0} only can contain {1} characters length.")]
        public string LastName { get; set; }

        public string Ocupattion { get; set; }

        public string Stratum { get; set; }

        public string Gender { get; set; }

        public DateTime Birthdate { get; set; }

        public int CityId { get; set; }

        public City City { get; set; }


    }
}
