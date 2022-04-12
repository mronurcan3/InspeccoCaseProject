using Project.DAL.Context;
using Project.Entities.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.DAL.StrategyPattern
{
    public class MyInit:DropCreateDatabaseIfModelChanges<MyContext>
    {

        protected override void Seed(MyContext context)
        {
            UserProfile userProfile1 = new UserProfile()
            {
                FirstName = "alisya",
                LastName = "yildirim",
                EMail = "alisyayildirim@gmail.com",
                



            };

            Content content = new Content()
            {
                Header = "Animal",
                Text = "Test123"

            };


            AppUser appUser1 = new AppUser()
            {

                UserName = "alisya1",
                Password = "123",
                Role = Entities.Enums.UserRole.Admin,
                UserProfile = userProfile1,
                Contents = new List<Content> { content}
                
            };

            Company company1 = new Company()
            {
                Name = "THE YILDIRIMLAR",
                AppUsers = new List<AppUser>()
                {
                    appUser1,

                },

                Contents = new List<Content> { content }
            };

            context.AppUsers.Add(appUser1);
            context.UserProfiles.Add(userProfile1);
            context.Contents.Add(content);
            context.Companies.Add(company1);

            context.SaveChanges();





        }
    }
}
