using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Møteplanlegger.models;

namespace Møteplanlegger
{

    //this class gives the program the option to interact with a databse using entetiyFramework.
    public class DbContextClass : DbContext
    {
        public DbContextClass(DbContextOptions<DbContextClass> options)
                : base(options) { }
        public DbSet<People> peoples => Set<People>();

        public DbSet<Meeting> meetings => Set<Meeting>();
    }
}