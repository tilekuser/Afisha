using Afisha.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;


namespace Afisha
{
    public class AfishaContext : IdentityDbContext<User, IdentityRole<int>, int>
    {
        public DbSet<User> users { get; set; }
        public DbSet<Concert> concerts { get; set; }
        public DbSet<Seans> seanses { get; set; }
        public DbSet<Reservation> reservations { get; set; }
        public DbSet<Location> locations { get; set; }

        public AfishaContext(DbContextOptions options) : base(options)
        {

        }
    }
}
