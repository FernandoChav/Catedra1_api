using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
namespace Catedra1.src.data
{
    public class AppDbContext(DbContextOptions dbContextOptions) :  DbContext(dbContextOptions)
    {
        
    }
}