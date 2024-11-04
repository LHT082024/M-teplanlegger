using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Møteplanlegger.models;

namespace Møteplanlegger
{
    public class DbContextClass : DbContext
    {
        public DbContextClass(DbContextOptions<DbContextClass> options)
                : base(options) { }
        public DbSet<People> peoples => Set<People>();

        public DbSet<Meeting> meetings => Set<Meeting>();
    }
}