
namespace Vote.Web.Data.Entities
{
    using System;
    using System.Collections.Generic;
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
        [DisplayFormat(DataFormatString ="{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        public DateTime StartDate { get; set; }

        [Display(Name = "End Date")]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        public DateTime EndDate { get; set; }

        public User User { get; set; }

        public ICollection<Vote> Votes { get; set; }

        [Display(Name = "# Votes")]
        public int NumberVote { get { return this.Votes == null ? 0 : this.Votes.Count; } }
    }
}
