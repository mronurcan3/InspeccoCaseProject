using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Entities.Models
{
    public class Content:BaseEntity
    {

        public string Header { get; set; }
        public string Text { get; set; }

        public int Likes { get; set; }

        public int AppUserID { get; set; }

        public int CompanyID { get; set; }



        //Relational Properties

        public virtual AppUser AppUser { get; set; }

        public virtual Company Company { get; set; }
    }
}
