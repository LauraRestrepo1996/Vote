using System;
using System.Collections.Generic;
using System.Text;

namespace Vote.Common.Models
{
    using System.ComponentModel.DataAnnotations;

    public class RecoverPasswordRequest
    {
        [Required]
        public string Email { get; set; }
    }

}
