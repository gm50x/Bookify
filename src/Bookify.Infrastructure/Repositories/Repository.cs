using Bookify.Domain.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace Bookify.Infrastructure.Repositories;
internal abstract class Repository<T>
    where T : Entity
{
    protected readonly ApplicationDbContext _dbContext;

    protected Repository(ApplicationDbContext context)
    {
        _dbContext = context;
    }

    public async Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _dbContext
            .Set<T>()
            .FirstOrDefaultAsync(user => user.Id == id, cancellationToken);
    }

    public void Add(T entity)
    {
        _dbContext.Add(entity);
    }
}
