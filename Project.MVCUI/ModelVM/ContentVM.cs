using Project.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project.MVCUI.ModelVM
{
    public class ContentVM
    {
        public Content MyProperty { get; set; }

        public List<Content> Contents { get; set; }

        public AppUser AppUser { get; set; }
        public List<AppUser> AppUsers { get; set; }

        public Company Company { get; set; }
    }
}