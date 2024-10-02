using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Catedra1.src.models;

namespace Catedra1.src.data
{
    public class DataSeeder
    {
        public static void Initializable(AppDbContext context)
        {
            if(!context.Users.Any())
            {
                context.Users.AddRange(
                    new User
                    {
                        Rut = "211805307",
                        Name = "Fernando",
                        Email = "fpatric25@gmail.com",
                        Gender = "masculino",
                        Birthdate = new DateOnly(2002,11,18)
                    }   
                );

                context.SaveChanges();
            }
        }
    }
}