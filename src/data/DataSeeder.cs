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
                    Birthdate = new DateOnly(1990, 5, 15)  
                    },
                    new User
                    {
                        Rut = "211805308",
                        Name = "Andrea",
                        Email = "andrea@example.com",
                        Gender = "femenino",
                        Birthdate = new DateOnly(1992, 4, 25)
                    },
                    new User
                    {
                        Rut = "211805309",
                        Name = "Carlos",
                        Email = "carlos@example.com",
                        Gender = "masculino",
                        Birthdate = new DateOnly(1988, 6, 10)
                    },
                    new User
                    {
                        Rut = "211805310",
                        Name = "Marta",
                        Email = "marta@example.com",
                        Gender = "femenino",
                        Birthdate = new DateOnly(1994, 2, 8)
                    },
                    new User
                    {
                        Rut = "211805311",
                        Name = "Jorge",
                        Email = "jorge@example.com",
                        Gender = "masculino",
                        Birthdate = new DateOnly(1985, 7, 30)
                    },
                    new User
                    {
                        Rut = "211805312",
                        Name = "Lucía",
                        Email = "lucia@example.com",
                        Gender = "femenino",
                        Birthdate = new DateOnly(1993, 11, 12)
                    },
                    new User
                    {
                        Rut = "211805313",
                        Name = "Pedro",
                        Email = "pedro@example.com",
                        Gender = "masculino",
                        Birthdate = new DateOnly(1990, 9, 20)
                    },
                    new User
                    {
                        Rut = "211805314",
                        Name = "Paula",
                        Email = "paula@example.com",
                        Gender = "femenino",
                        Birthdate = new DateOnly(1987, 8, 19)
                    },
                    new User
                    {
                        Rut = "211805315",
                        Name = "Sebastián",
                        Email = "sebastian@example.com",
                        Gender = "masculino",
                        Birthdate = new DateOnly(1991, 12, 3)
                    },
                    new User
                    {
                        Rut = "211805316",
                        Name = "Mariana",
                        Email = "mariana@example.com",
                        Gender = "femenino",
                        Birthdate = new DateOnly(1995, 10, 15)
                    },
                    new User
                    {
                        Rut = "211805317",
                        Name = "Raúl",
                        Email = "raul@example.com",
                        Gender = "masculino",
                        Birthdate = new DateOnly(1996, 1, 22)
                    }
                );

                context.SaveChanges();
            }
        }
    }
}