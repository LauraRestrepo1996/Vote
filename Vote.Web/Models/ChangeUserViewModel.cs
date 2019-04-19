

namespace Vote.Web.Models
{
    using Microsoft.AspNetCore.Mvc.Rendering;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class ChangeUserViewModel
    {

        [Required]
        [Display(Name = "First Name")]
        [MaxLength(100, ErrorMessage = "The fild {0} only can contain {1} characters length.")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        [MaxLength(100, ErrorMessage = "The fild {0} only can contain {1} characters length.")]
        public string LastName { get; set; }

        [Required]
        public string Occupation { get; set; }

        [Required]
        public string Stratum { get; set; }

        [Required]
        public string Gender { get; set; }

        [Required]
        public DateTime Birthdate { get; set; }

        [Required]
        [MaxLength(20, ErrorMessage = "The field {0} only can contain {1} characters length.")]
        public string PhoneNumber { get; set; }

        [Required]
        [Display(Name = "City")]
        [Range(1, int.MaxValue, ErrorMessage = "You must select a city.")]
        public int CityId { get; set; }

      
        public IEnumerable<SelectListItem> Cities { get; set; }

      
        [Display(Name = "Country")]
        [Range(1, int.MaxValue, ErrorMessage = "You must select a country.")]
        public int CountryId { get; set; }

        public IEnumerable<SelectListItem> Countries { get; set; }

    }


}
