using System.Data.Entity;
using LoginApp.Model;

namespace LoginApp
{
    public class DataBaseContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public DataBaseContext() : base("C:\\VisualStudioProjects\\LoginApp\\LoginApp\\Users.db") { }
    }
}
