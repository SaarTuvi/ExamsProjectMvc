using Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DAL.Models.AppUsers
{
    public abstract class AbstractAppUser : IAppUser
    {
        [Key]
        public int ID { get; set; }
        public string LoginName { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
    }
}
