using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;


namespace handson.Models
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options) : base(options)
        {
        }

        public DbSet<Contas> contas { get; set; }



    }
}
