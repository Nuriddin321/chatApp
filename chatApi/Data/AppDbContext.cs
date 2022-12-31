using System.Text.RegularExpressions;
using chatApi.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace chatApi.Data;

public class AppDbContext : IdentityDbContext<AppUser, IdentityRole, string>
{
    public DbSet<ChatGroup>? ChatGroups {get; set;}
    public DbSet<AppUser>? Users {get; set;}
    public AppDbContext(DbContextOptions options) : base(options) { }
}