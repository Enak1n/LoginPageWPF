using System.Data.Entity;

namespace LoginApp
{
    public class DataBaseContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public DataBaseContext() : base("DefaultConnection") { }
    }
}
