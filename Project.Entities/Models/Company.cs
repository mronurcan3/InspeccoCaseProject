using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Entities.Models
{
    public class Company:BaseEntity
    {


        public string Name { get; set; }

        //Relational Properties

        public virtual List<AppUser> AppUsers { get; set; }

        public virtual List<Content> Contents { get; set; }
    }
}
