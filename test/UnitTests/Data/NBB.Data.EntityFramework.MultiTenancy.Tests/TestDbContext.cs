﻿// Copyright (c) TotalSoft.
// This source code is licensed under the MIT license.

using Microsoft.EntityFrameworkCore;

namespace NBB.Data.EntityFramework.MultiTenancy.Tests
{
    public class TestDbContext : MultiTenantDbContext
    {
        public DbSet<TestEntity> TestEntities { get; set; }
        public DbSet<SimpleEntity> SimpleEntities { get; set; }

        public TestDbContext(DbContextOptions<TestDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new TestEntityConfiguration());
            modelBuilder.ApplyConfiguration(new SimpleEntityConfiguration());

            base.OnModelCreating(modelBuilder);
        }
    }
}
