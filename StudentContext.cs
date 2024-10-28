using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Azure_Blob_Trigger
{
    public class StudentContext : DbContext
    {
        public DbSet<Student> Students { get; set; }

        public StudentContext(DbContextOptions<StudentContext> options) : base(options) { }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured) { 


            optionsBuilder.UseSqlServer(Environment.GetEnvironmentVariable("SqlConnectionString"));

            }
        }


    }
}
