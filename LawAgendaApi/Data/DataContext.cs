using LawAgendaApi.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace LawAgendaApi.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
            
        }

        public DbSet<A1CaseType> A1CaseTypes { get; set; }
        public DbSet<A1FileType> A1FileTypes { get; set; }
        public DbSet<A1UserType> A1UserTypes { get; set; }
        public DbSet<Case> Cases { get; set; }
        public DbSet<CaseTimeline> CaseTimelines { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<File> Files { get; set; }
        public DbSet<User> Users { get; set; }
        
        
        
        
    }
}