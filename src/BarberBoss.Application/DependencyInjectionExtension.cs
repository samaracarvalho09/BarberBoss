using BarberBoss.Application.UseCases.Expenses.GetAll;
using Microsoft.Extensions.DependencyInjection;
using BarberBoss.Application.AutoMapper;


namespace BarberBoss.Application;

public static class DependencyInjectionExtension
{ public static void AddApplication(this IServiceCollection services)
    {
        AddAutoMapper(services);
        AddUseCases(services);
    }

    // private static void AddAutoMapper(IServiceCollection services)
    // {
    //     services.AddAutoMapper(typeof(AutoMapping).Assembly);
        
    // }

    private static void AddAutoMapper(IServiceCollection services)
{
    // Configura explicitamente qual Profile adicionar
    services.AddAutoMapper(config => config.AddProfile<AutoMapping>());
}

    private static void AddUseCases(IServiceCollection services)
    {
        services.AddScoped<IGetAllExpenseUseCase, GetAllExpenseUseCase>(); 
        
    }
}