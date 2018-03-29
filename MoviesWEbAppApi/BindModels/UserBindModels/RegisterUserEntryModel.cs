using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MoviesWEbAppApi.BindModels
{
    public class RegisterUserEntryModel
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
        [EmailAddress]
        public string EMail { get; set; }
        public string AvatarUrl { get; set; }
    }
}
