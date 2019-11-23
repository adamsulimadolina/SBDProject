using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Project.Models;

namespace Project.Data
{
    public class ProjectContext : DbContext
    {
        public ProjectContext(DbContextOptions options)
            : base(options)
        {
        }
        /* protected override void OnModelCreating(ModelBuilder modelBuilder)
         {
             foreach(var foreignKey in modelBuilder.Model.GetEntityTypes().SelectMany(e=>e.GetForeignKeys()))
             {
                 foreignKey.DeleteBehavior = DeleteBehavior.Restrict;
             }
         }*/

        public DbSet<UserModel> User { get; set; }
        public DbSet<TenantModel> Tenants { get; set; }
        public DbSet<RoomModel> Rooms { get; set; }
        public DbSet<PairsModel> Pairs { get; set; }

        public DbSet<OwnerModel> Owners { get; set; }
        public DbSet<OpinionModel> Opinions { get; set; }
        public DbSet<MessagesModel> Messages { get; set; }
        public DbSet<LogsModel> Logs { get; set; }
        public DbSet<FlatModel> Flats { get; set; }
        public DbSet<CityModel> Citys { get; set; }
        public DbSet<AdvertisementModel> Advertisements { get; set; }

    }
}
