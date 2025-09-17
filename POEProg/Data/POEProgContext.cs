using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using POEProg.Models;

namespace POEProg.Data
{
    public class POEProgContext : DbContext
    {
        public POEProgContext (DbContextOptions<POEProgContext> options)
            : base(options)
        {
        }

        public DbSet<POEProg.Models.Lecturer> Lecturer { get; set; } = default!;
        public DbSet<POEProg.Models.Document> Document { get; set; } = default!;
        public DbSet<POEProg.Models.Claim> Claim { get; set; } = default!;
        public DbSet<POEProg.Models.Approval> Approval { get; set; } = default!;
    }
}
