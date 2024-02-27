using MP.Core.Entities;


namespace MP.Core.Interfaces.Services
{
    public interface ILogAcessoDomainService
    {
        Task Create(LogAcesso entity);
        Task CreateMany(IEnumerable<LogAcesso> entity);

    }
}
