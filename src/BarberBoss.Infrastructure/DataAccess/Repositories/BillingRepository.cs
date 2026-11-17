using BarberBoss.Domain.Entities;
using BarberBoss.Domain.Repositories.Billings;
using Microsoft.EntityFrameworkCore;

namespace BarberBoss.Infrastructure.DataAccess.Repositories;

internal class BillingRepository
 : IBillingReadOnlyRepository 
// IBillingWriteOnlyRepository, IBillingUpdateOnlyRepository
{
    private readonly BarberBossDbContext _dbContext;
    
    public BillingRepository(BarberBossDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task Add(Billing billing)
    {
        await _dbContext.Billing.AddAsync(billing);
    }

    public async Task<bool> Delete(long id)
    {
        var result = await _dbContext.Billing.FirstOrDefaultAsync(billing => billing.Id == id);
        if (result is null)
        {
            return false;
        }

        _dbContext.Billing.Remove(result); 

        return true;
    }

    // A correção direta do seu erro está aqui: agora retorna List<Billing>
    public async Task<List<Billing>> GetAll()
    {
        return await _dbContext.Billing.AsNoTracking().ToListAsync();
    }

    // Corrigido para retornar Billing?
    async Task<Billing?> IBillingReadOnlyRepository.GetById(long id)
    {
        return await _dbContext.Billing.AsNoTracking().FirstOrDefaultAsync(billing => billing.Id == id);
    }

    /* Comentado temporariamente até você implementar as outras interfaces
    async Task<Billing?> IBillingUpdateOnlyRepository.GetById(long id)
    {
        return await _dbContext.Billing.FirstOrDefaultAsync(billing => billing.Id == id);
    }

    public void Update(Billing billing)
    {
        _dbContext.Billing.Update(billing);
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
        .Where(billing => billing.Date >= startDate && billing.Date <= endDate)
        .OrderBy(billing => billing.Date)
        .ThenBy(billing => billing.BarberName) // Se a entidade usar "ServiceName", altere aqui
        .ToListAsync();
}
}