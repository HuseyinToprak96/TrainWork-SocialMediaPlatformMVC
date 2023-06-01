using CoreLayer.Entities;
using CoreLayer.Entities.HelperTables;
using CoreLayer.Entities.Notification;
using CoreLayer.Entities.Shared;
using CoreLayer.Entities.User;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RepositoryLayer.DataContext
{
    internal class AppDbContext : DbContext
    {
        public AppDbContext() : base("dbConnection")
        {

        }
        public DbSet<City> Cities { get; set; }
        public DbSet<District> Districts { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserProfileImages> UserProfileImages { get; set; }
        public DbSet<Follow> Follows { get; set; }
        public DbSet<Shared> Shareds { get; set; }
        public DbSet<SharedLike> SharedLikes { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=Northwind;Trusted_Connection=true");//asp.net core
        //}

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);
        }
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            foreach (var item in ChangeTracker.Entries())
            {
                if (item.Entity is Base entityReferences)
                {
                    switch (item.State)
                    {
                        case EntityState.Added:
                            {
                                entityReferences.CreatedDate = DateTime.Now;
                                break;
                            }
                        case EntityState.Modified:
                            {
                                entityReferences.UpdatedDate = DateTime.Now;
                                break;
                            }
                    }
                }
            }
            return base.SaveChangesAsync(cancellationToken);
        }
        public override int SaveChanges()
        {
            foreach (var item in ChangeTracker.Entries())
            {
                if (item.Entity is Base entityReferences)
                {
                    switch (item.State)
                    {
                        case EntityState.Added:
                            {
                                entityReferences.CreatedDate = DateTime.Now;
                                break;
                            }
                        case EntityState.Modified:
                            {
                                Entry(entityReferences).Property(x => x.CreatedDate).IsModified = false;
                                entityReferences.UpdatedDate = DateTime.Now;
                                break;
                            }
                    }
                }
            }
            return base.SaveChanges(); 

        }
    }
}
