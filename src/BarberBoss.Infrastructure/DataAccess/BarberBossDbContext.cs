using BarberBoss.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BarberBoss.Infrastructure.DataAccess;

internal class BarberBossDbContext : DbContext
{
  public BarberBossDbContext(DbContextOptions options) : base (options) {}


// o <Billing> vem de "BarberBoss.Domain.Entities"
  public DbSet<Billing> Billing { get; set; }
  
}