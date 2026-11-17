

using BarberBoss.Infrastructure.DataAccess;
using BarberBoss.Infrastructure.DataAccess.Repositories;


using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using BarberBoss.Domain.Repositories.Billings;
using BarberBoss.Domain.Repositories;


namespace BarberBoss.Infrastructure;
public static class DependencyInjectionExtension
{
  public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
  {
    AddDbContext(services, configuration);
    AddRepositories(services);
  }

      private static void AddRepositories(IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IBillingReadOnlyRepository, BillingRepository>();
        // services.AddScoped<IBillingWriteOnlyRepository, BillingRepository>();
        // services.AddScoped<IBillingUpdateOnlyRepository, BillingRepository>();

    }

  private static void AddDbContext(IServiceCollection services, IConfiguration configuration)
  {
    var connectionString = configuration.GetConnectionString("Connection");

    var version = ServerVersion.AutoDetect(connectionString);
    var serverVersion = new MySqlServerVersion(version);

    services.AddDbContext<BarberBossDbContext>(config => config.UseMySql(connectionString, serverVersion));
  }
}