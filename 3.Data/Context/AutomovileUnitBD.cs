﻿using _3.Data.Model;
using Microsoft.EntityFrameworkCore;

namespace _3.Data.Context;

public class AutomovileUnitBD : DbContext
{
    public AutomovileUnitBD()
    {

    }

    public AutomovileUnitBD(DbContextOptions<AutomovileUnitBD> options) : base(options)
    {
    }

    public DbSet<User> TUsers { get; set; }
    public DbSet<Automobile> TAutomobiles { get; set; }
    
    public DbSet<RequestRent> TRentRequests { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            var serverVersion = new MySqlServerVersion(new Version(8, 0, 29));
            optionsBuilder.UseMySql("Server=sql10.freemysqlhosting.net,3306;Uid=sql10663655;Pwd=RJ9M4uKMyH;Database=sql10663655;", serverVersion);
        }
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<User>().ToTable("User");
        builder.Entity<User>().HasKey(p => p.Id);
        builder.Entity<User>().Property(p => p.Id).ValueGeneratedOnAdd();
        builder.Entity<User>().Property(c => c.Name).IsRequired().HasMaxLength(50);
        builder.Entity<User>().Property(c => c.Lastname).IsRequired().HasMaxLength(50);
        builder.Entity<User>().Property(c => c.DateCreated).HasDefaultValue(DateTime.Now);
        builder.Entity<User>().Property(c => c.IsActive).HasDefaultValue(true);
        builder.Entity<User>().Property(c =>c.LastAutomobileCreation).HasDefaultValue(DateTime.Now);
        builder.Entity<User>().Property(c =>c.AutomobilesCreatedInInterval).HasDefaultValue(0);

        builder.Entity<User>().Property(c => c.UserType).IsRequired();
        builder.Entity<User>().OwnsOne(p => p.Adress, location =>
        {
            location.Property(d => d.Department).IsRequired().HasMaxLength(50);
            location.Property(d => d.Province).IsRequired().HasMaxLength(50);
            location.Property(d => d.District).IsRequired().HasMaxLength(50);
            location.Property(d => d.Street).IsRequired().HasMaxLength(50);
        });
        
        builder.Entity<Automobile>(entity =>
        {
            entity.ToTable("Automovil");
            entity.HasKey(p => p.Id);
            entity.Property(p => p.Id).ValueGeneratedOnAdd();
            entity.Property(c => c.Brand).IsRequired().HasMaxLength(50);
        });
        
        builder.Entity<RequestRent>(entity =>
        {
            entity.ToTable("RequestRent");
            entity.HasKey(p => p.Id);
            entity.Property(p => p.Id).ValueGeneratedOnAdd();
            entity.Property(c => c.DateCreated).HasDefaultValue(DateTime.Now);
            entity.Property(c => c.DateUpdate).HasDefaultValue(DateTime.Now);
        });
        
        builder.Entity<Notifications>(entity =>
        {
            entity.ToTable("Notifications");
            entity.HasKey(p => p.Id);
            entity.Property(p => p.Id).ValueGeneratedOnAdd();
        });
        builder.Entity<Request>(entity =>
        {
            entity.ToTable("Solicitudes");
            entity.HasKey(p => p.Id);
            entity.Property(p => p.Id).ValueGeneratedOnAdd();
        });
    }
}