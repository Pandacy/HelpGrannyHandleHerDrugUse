﻿using MaakJeNietDrugLogic.ClassesLogic;
using MaakJeNietDrugLogic.Model;
using Microsoft.EntityFrameworkCore;

namespace MaakJeNietDrugDAL.ClassesDB
{
    public class DataBaseContext : DbContext
    {
        public DbSet<Medicine> Medicines { get; set; }
        public DbSet<Account> Accounts { get; set; }

        public DbSet<IntakeMoment> IntakeMoments { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql("server=studmysql01.fhict.local;database=dbi340421;user=dbi340421;password=Dreaming",
            options => options.EnableRetryOnFailure());

            //lokale DB
            //optionsBuilder.UseSqlServer("server=(localdb)\\MSSQLLocalDB;Initial Catalog=MaakJeNietDrug;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False",
            //options => options.EnableRetryOnFailure());
        }
    }
}
