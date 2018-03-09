using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MoviesWEbAppApi.BindModels
{
    public class UserLoginBindModel
    {
        [EmailAddress]
        public string EMail { get; set; }
        [Required]
        public string password { get; set; }
    }
}
