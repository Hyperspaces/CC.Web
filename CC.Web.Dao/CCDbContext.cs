using CC.Web.Model.System;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace CC.Web.Dao
{
    public class CCDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
    }
}
