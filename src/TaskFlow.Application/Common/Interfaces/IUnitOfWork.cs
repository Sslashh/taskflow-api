namespace TaskFlow.Application.Common.Interfaces;

public interface IUnitOfWork
{
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    IRepository<T> Repository<T>() where T : class;
}
