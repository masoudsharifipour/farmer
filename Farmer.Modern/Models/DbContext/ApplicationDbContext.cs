using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Farmer.Modern.Dto;
using Farmer.Modern.Models;

namespace Farmer.Modern.Models.DbContext;

public class ApplicationDbContext : IdentityDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<ApplicationUser> Users { get; set; }
    
    public DbSet<Garden> Garden { get; set; }
    
    public DbSet<Farmer.Modern.Models.Category> Category { get; set; }

    
    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<Garden>();
        base.OnModelCreating(builder);
        
    }

    
    public DbSet<Farmer.Modern.Models.Product> Product { get; set; }

    
    public DbSet<Farmer.Modern.Models.WaterMotor> WaterMotor { get; set; }

    
    public DbSet<Farmer.Modern.Models.Experiment> Experiment { get; set; }

    
    public DbSet<Farmer.Modern.Models.Harvest> Harvest { get; set; }

    
    public DbSet<Farmer.Modern.Models.Work> Work { get; set; }
    
}