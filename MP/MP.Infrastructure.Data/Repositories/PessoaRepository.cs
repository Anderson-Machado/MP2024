using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using MP.Core.Entities;
using MP.Core.Interfaces.Repositories;
using MP.CrossCutting.Utils.Model.Repositories;
using MP.Infrastructure.Data.Contexts;
using MP.CrossCutting.Utils.Extensions;

namespace MP.Infrastructure.Data.Repositories
{
    public class PessoaRepository : RepositoryWithSearchBase<Pessoa>, IPessoaRepositories
    {
        public PessoaRepository(MPDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<Pessoa> GetPessoaByMatricula(decimal matricula)
        {

         
            var result = await _dbContext.Set<Pessoa>()
                        .Where(x => x.Id == matricula)
                        .GroupJoin(
                            _dbContext.Set<SituacaoPessoaMultipla>(),
                            pessoa => pessoa.Id,
                            situacao => situacao.CodPessoa,
                            (pessoa, situacoes) => new { Pessoa = pessoa, Situacoes = situacoes }
                        )
                        .SelectMany(
                            x => x.Situacoes.DefaultIfEmpty(),
                            (pessoa, situacao) => new
                            {
                                Pessoa = pessoa.Pessoa,
                                Situacao = situacao
                            }
                        )
                        .GroupJoin(
                            _dbContext.Set<FotoPessoa>(),
                            pessoa => pessoa.Pessoa.Id,
                            foto => foto.Id,
                            (pessoa, fotos) => new { Pessoa = pessoa.Pessoa, Situacao = pessoa.Situacao, Fotos = fotos }
                        )
                        .SelectMany(
                            x => x.Fotos.DefaultIfEmpty(),
                            (pessoa, foto) => new
                            {
                                Pessoa = pessoa.Pessoa,
                                Situacao = pessoa.Situacao,
                                Foto = foto
                            }
                        )
                        .Select(x => new Pessoa
                        {
                            Id = x.Pessoa.Id,
                            Matricula = x.Pessoa.Matricula,
                            NomePessoa = x.Pessoa.NomePessoa,
                            CodSituacaoPessoa = x.Pessoa.CodSituacaoPessoa,
                            SituacaoPessoa = x.Situacao,
                            Imagem = x.Foto.Imagem,
                            FotoPessoa = x.Foto 
                        })
            .FirstOrDefaultAsync();







            return result;
        }
    }
}

