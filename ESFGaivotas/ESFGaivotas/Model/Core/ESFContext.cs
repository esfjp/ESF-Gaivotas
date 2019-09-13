using ESFGaivotas.Enumerations;
using ESFGaivotas.Model.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ESFGaivotas.Model.Core
{
    public class ESFContext : DbContext
    {
        private const string databaseName = "ESFGaivotas.db";

        public ESFContext() : base()
        {
            Database.EnsureCreated();
        }

        public virtual DbSet<UserModel> User { get; set; }
        public virtual DbSet<ReportModel> Reports { get; set; }
        public virtual DbSet<DebrisModel> Debris { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var databasePath = "";
            switch (Device.RuntimePlatform)
            {
                case Device.Android:
                    databasePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), databaseName);
                    break;  
                case Device.iOS:
                    SQLitePCL.Batteries_V2.Init();
                    databasePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "..", "Library", databaseName);
                    break;
                default:
                    throw new NotImplementedException("Plataforma deste dispositivo não é suportada!");
            }

            var test = File.Exists(databasePath);
            optionsBuilder.UseSqlite($"Filename={databasePath}");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserModel>()
                .HasKey(p => p.Id);

            modelBuilder.Entity<ReportModel>()
                .HasKey(p => p.Id);

            modelBuilder.Entity<DebrisModel>()
                .HasKey(p => p.Id);

            var user = new UserModel { Id = 1, Login = "joaopessoa", PasswordHash = "gaivotas", FirstName = "Sem Fronteiras", ProfilePicture = "Logo.png" };
            modelBuilder.Entity<UserModel>()
                .HasData(user);

        }
    }
}
