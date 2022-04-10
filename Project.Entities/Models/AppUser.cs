using Project.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Entities.Models
{
    public class AppUser:BaseEntity
    {
        public AppUser()
        {
            ActivationCode = Guid.NewGuid();
        }
        public string UserName { get; set; }

        public string Password { get; set; }

        public UserRole Role { get; set; }

        public Guid ActivationCode { get; set; }




        //Relational Properties

        public UserProfile UserProfile { get; set; }

        public List<Content> Contents { get; set; }
    }
}
