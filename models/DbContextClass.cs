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
        public DbSet<People> Peoples { get; set; }
        public DbSet<Meeting> Meetings { get; set; }

    }
}