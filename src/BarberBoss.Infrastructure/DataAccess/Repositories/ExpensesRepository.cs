using BarberBoss.Domain.Entities;
using BarberBoss.Domain.Repositories.Expenses;
using Microsoft.EntityFrameworkCore;

namespace BarberBoss.Infrastructure.DataAccess.Repositories;

internal class ExpensesRepository
 : IExpensesReadOnlyRepository 
// IExpensesWriteOnlyRepository, IExpensesUpdateOnlyRepository
{
    private readonly BarberBossDbContext _dbContext;
    
    public ExpensesRepository(BarberBossDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task Add(Billing expense)
    {
        await _dbContext.Billing.AddAsync(expense);
    }

    public async Task<bool> Delete(long id)
    {
        var result = await _dbContext.Billing.FirstOrDefaultAsync(expense => expense.Id == id);
        if (result is null)
        {
            return false;
        }

        // Corrigido de _dbContext.Expenses para _dbContext.Billing
        _dbContext.Billing.Remove(result); 

        return true;
    }

    // A correção direta do seu erro está aqui: agora retorna List<Billing>
    public async Task<List<Billing>> GetAll()
    {
        return await _dbContext.Billing.AsNoTracking().ToListAsync();
    }

    // Corrigido para retornar Billing?
    async Task<Billing?> IExpensesReadOnlyRepository.GetById(long id)
    {
        return await _dbContext.Billing.AsNoTracking().FirstOrDefaultAsync(expense => expense.Id == id);
    }

    /* Comentado temporariamente até você implementar as outras interfaces
    async Task<Billing?> IExpensesUpdateOnlyRepository.GetById(long id)
    {
        return await _dbContext.Billing.FirstOrDefaultAsync(expense => expense.Id == id);
    }

    public void Update(Billing expense)
    {
        _dbContext.Billing.Update(expense);
    }
    */

public async Task<List<Billing>> FilterByMonth(DateOnly date)
{
    // Criando o primeiro dia do mês como DateOnly
    var startDate = new DateOnly(year: date.Year, month: date.Month, day: 1);
    
    // Descobrindo o último dia do mês e criando a data final como DateOnly
    var daysInMonth = DateTime.DaysInMonth(year: date.Year, month: date.Month);
    var endDate = new DateOnly(year: date.Year, month: date.Month, day: daysInMonth);

    return await _dbContext
        .Billing 
        .AsNoTracking()
        .Where(expense => expense.Date >= startDate && expense.Date <= endDate)
        .OrderBy(expense => expense.Date)
        .ThenBy(expense => expense.BarberName) // Se a entidade usar "ServiceName", altere aqui
        .ToListAsync();
}
}