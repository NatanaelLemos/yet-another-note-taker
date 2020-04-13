﻿using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using NoteTaker.Data.Mappers;
using NoteTaker.Domain.Entities;

namespace NoteTaker.Data
{
    public class NoteTakerContext : DbContext
    {
        public static string DatabasePath = "notetaker.db";

        public NoteTakerContext()
        {
            Database.Migrate();
        }

        public DbSet<Notebook> Notebooks { get; set; }
        public DbSet<Note> Notes { get; set; }
        public DbSet<Settings> Settings { get; set; }
        public DbSet<User> Users { get; set; }

        public override int SaveChanges()
        {
            UpdateEntities();
            return base.SaveChanges();
        }

        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            UpdateEntities();
            return base.SaveChanges(acceptAllChangesOnSuccess);
        }

        public override Task<int> SaveChangesAsync(
            bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            UpdateEntities();
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            UpdateEntities();
            return base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite($"Data Source={DatabasePath}");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            MapModels(modelBuilder);

            //Do not change the dependent entities of the foreign keys
            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }

            //Allows SqLite to handle DateTimeOffset
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                var properties = entityType.ClrType.GetProperties().Where(p => p.PropertyType == typeof(DateTimeOffset));
                foreach (var property in properties)
                {
                    modelBuilder
                        .Entity(entityType.Name)
                        .Property(property.Name)
                        .HasConversion(new DateTimeOffsetToBinaryConverter());
                }
            }
        }

        private void MapModels(ModelBuilder modelBuilder)
        {
            new NotebookMapper(modelBuilder.Entity<Notebook>());
            new NoteMapper(modelBuilder.Entity<Note>());
            new SettingsMapper(modelBuilder.Entity<Settings>());
            new UserMapper(modelBuilder.Entity<User>());
        }

        private void UpdateEntities()
        {
            foreach (var item in ChangeTracker.Entries())
            {
                if (item.Entity is EntityBase entity)
                {
                    if (item.State == EntityState.Added)
                    {
                        //Ensure that the key is set
                        if (entity.Id == Guid.Empty)
                        {
                            entity.Id = Guid.NewGuid();
                        }

                        //Forces the added and modified dates to now
                        entity.CreatedOn = DateTimeOffset.Now;
                        entity.UpdatedOn = DateTimeOffset.Now;
                    }

                    if (item.State == EntityState.Modified)
                    {
                        //Always update the modified date
                        entity.UpdatedOn = DateTime.Now;
                    }
                }
            }
        }
    }
}
