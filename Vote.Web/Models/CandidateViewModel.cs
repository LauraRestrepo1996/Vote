using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using Vote.Web.Data.Entities;

namespace Vote.Web.Models
{
    public class CandidateViewModel : Candidate
    {
        [Display(Name = "Image")]
        public IFormFile ImageFile { get; set; }

    }
}
