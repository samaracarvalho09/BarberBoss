using BarberBoss.Domain.Entities;

namespace BarberBoss.Domain.Repositories.Expenses;
public interface IExpensesReadOnlyRepository
{
    Task<List<Billing>> GetAll();
    Task<Billing?> GetById(long id);
    Task<List<Billing>> FilterByMonth(DateOnly date);
}
