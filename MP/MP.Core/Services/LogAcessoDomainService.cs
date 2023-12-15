using MP.Core.Entities;
using MP.Core.Interfaces.Repositories;
using MP.Core.Interfaces.Services;

namespace MP.Core.Services
{
    public class LogAcessoDomainService : ILogAcessoDomainService
    {
        private readonly ILogAcessoRepository _logAcessoRepository;

        public LogAcessoDomainService(ILogAcessoRepository logAcessoRepository)
        {
            _logAcessoRepository = logAcessoRepository;
        }

        public async Task Create(LogAcesso entity)
        {
            await _logAcessoRepository.Create(entity);
        }
    }
}
