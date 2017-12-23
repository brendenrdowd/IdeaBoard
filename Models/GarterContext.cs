using Microsoft.EntityFrameworkCore;
 
namespace garterExam.Models
{
    public class GarterContext : DbContext
    {
        // base() calls the parent class' constructor passing the "options" parameter along
        public GarterContext(DbContextOptions<GarterContext> options) : base(options) { }

        // public DbSet<ModelName> TableName {get;set;}
        public DbSet<User> Users {get;set;}
        public DbSet<Idea> Ideas {get;set;}
        public DbSet<Medium> Mediums {get;set;}
    }
}