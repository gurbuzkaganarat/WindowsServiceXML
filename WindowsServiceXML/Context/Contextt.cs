using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsServiceXML.Entity;

namespace WindowsServiceXML.Context
{
    public class Contextt : Microsoft.EntityFrameworkCore.DbContext
    {

        public Microsoft.EntityFrameworkCore.DbSet<CountryInfo> CountryInfo { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=Niutari\\SQLEXPRESS; Database=Countries; User ID=SA;  Password=123; TrustServerCertificate=True");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);



        }
    }
}
