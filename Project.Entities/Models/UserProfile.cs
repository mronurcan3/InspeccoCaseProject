using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Entities.Models
{
    public class UserProfile:BaseEntity
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string EMail { get; set; }



        //Relational Properties

        public virtual AppUser AppUser { get; set; }
    }
}
