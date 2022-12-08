using BusTickets.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace BusTicket.DataAcess
{
    public static class DbInitializer
    {
        public static async Task InitializeAsync(BusApplicationDBContext context, IServiceProvider serviceProvider, UserManager<IdentityUser> userManager)
        {
            var RoleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            string[] roleNames = { "Admin","User" };
            IdentityResult roleResult;
            foreach (var RoleName in roleNames)
            {
                var roleExists = await RoleManager.RoleExistsAsync(RoleName);
                if (!roleExists)
                {
                    roleResult = await RoleManager.CreateAsync(new IdentityRole(RoleName));
                }
            }
            string Email = "Xyz@outlook.com";
            string Password = "XYZ@123";
            if (userManager.FindByEmailAsync(Email).Result==null)
            {
                IdentityUser user = new IdentityUser();
                user.UserName = Email;
                user.Email = Email;
                user.PasswordHash = Password;
                IdentityResult result = userManager.CreateAsync(user).Result;
                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, "Admin").Wait();
                }
            }
        }   
            
            

              
            
     }
                
            
           

            
    }

