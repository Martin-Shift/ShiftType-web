using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ShiftType.DbModels;
using Microsoft.VisualBasic;

namespace ShiftType.DbModels;

public class TypingDbContext : IdentityDbContext<User, IdentityRole<int>, int>
{
    public TypingDbContext(DbContextOptions<TypingDbContext> options) : base(options)
    {
    }
    public virtual DbSet<Quote> Quotes { get; set; }
    public virtual DbSet<ImageFile> Images { get; set; }
    public virtual DbSet<Result> Results { get; set; }
    public virtual DbSet<Badge> Badges { get; set; }
}

