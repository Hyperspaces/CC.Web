using CC.Web.Model.System;
using Microsoft.EntityFrameworkCore;

namespace CC.Web.Dao.Core
{
    public class CCDbContext : DbContext
    {
        public CCDbContext(DbContextOptions<CCDbContext> options) : base(options) 
        { 

        }

        public DbSet<User> Users { get; set; }
    }
}
