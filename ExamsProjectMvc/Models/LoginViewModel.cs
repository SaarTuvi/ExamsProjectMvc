using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ExamsProjectMvc.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Must Enter Login Name")]
        public string LoginName { get; set; }
        [Required(ErrorMessage = "Must Enter password")]
        public string Password { get; set; }
        public bool StayConnected { get; set; } = false;
        public bool IsAttempFailed = false;

    }
}
