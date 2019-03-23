
namespace Vote.Web.Data.Entities
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class Event : IEntity
    {
        public int Id { get; set; }

        [MaxLength(100, ErrorMessage = "The fild {0} only can contain {1} characters length.")]
        [Required]
        public string Name { get; set; }

        [MaxLength(500, ErrorMessage = "The fild {0} only can contain {1} characters length.")]
        [Required]
        public string Description { get; set; }


        [Display(Name = "Start Date")]
        public DateTime StartDate { get; set; }

        [Display(Name = "End Date")]
        public DateTime EndDate { get; set; }

        public User User { get; set; }
    }
}
