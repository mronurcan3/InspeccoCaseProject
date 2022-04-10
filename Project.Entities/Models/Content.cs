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



        //Relational Properties

        public AppUser AppUser { get; set; }
    }
}
