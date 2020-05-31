using Microsoft.EntityFrameworkCore; 

namespace Db.Context.Models
{
    public partial class AtsTestContext : DbContext
    {
        public static string TestConnectionString =
            @"Server=THINKPAD_EDGE\SQLEXPRESS;initial catalog=AtsTestDb;integrated security=True;MultipleActiveResultSets=True;App=AtsInterviewTask;";

        public AtsTestContext()
        {
        }

        public AtsTestContext(DbContextOptions<AtsTestContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Blogs> Blogs { get; set; }
        public virtual DbSet<Comments> Comments { get; set; }
        public virtual DbSet<PostTags> PostTags { get; set; }
        public virtual DbSet<Posts> Posts { get; set; }
        public virtual DbSet<Tags> Tags { get; set; }
        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<UserBlogs> UserBlogs { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(AtsTestContext.TestConnectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.1-servicing-10028"); 
            AtsTestModelCreator.Setup(modelBuilder); 
            AtsTestContextSeeder.Seed(modelBuilder.Entity<User>());
            AtsTestContextSeeder.Seed(modelBuilder.Entity<Tags>());
        }
    }
}
