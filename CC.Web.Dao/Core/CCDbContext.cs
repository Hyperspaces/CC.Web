﻿using CC.Web.Model;
using CC.Web.Model.System;
using Microsoft.EntityFrameworkCore;
using System;

namespace CC.Web.Dao.Core
{
    public class CCDbContext : DbContext
    {
        public CCDbContext(DbContextOptions<CCDbContext> options) : base(options) 
        { 

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .Property(e => e.Id)
                .HasConversion<Guid>();

            modelBuilder.Entity<User>()
                .Property(e => e.InsertTime)
                .HasDefaultValueSql("getdate()");

            modelBuilder.Entity<User>()
                .Property(e => e.UpdateTime)
                .HasDefaultValueSql("getdate()");

            modelBuilder.Entity<Article>()
                .HasOne(e => e.User)
                .WithMany(e => e.Articles);

            modelBuilder.Entity<Article>()
                .Property(e => e.InsertTime)
                .HasDefaultValueSql("getdate()");

            modelBuilder.Entity<Article>()
                .Property(e => e.UpdateTime)
                .HasDefaultValueSql("getdate()");

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<User> Users { get; set; }

        public DbSet<Article> Articles { get; set; }
    }
}
