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
            //       var result = await _dbContext.Set<SituacaoPessoaMultipla>()
            //            .Where(x => x.CodSituacaoPessoa == matricula)
            //                .Join(
            //                    _dbSet,
            //                    pessoa => pessoa.Id,
            //                    situacao => situacao.CodSituacaoPessoa,
            //                    (situacao, pessoa) => new Pessoa
            //                    {
            //                        Id = pessoa.Id,
            //                        Matricula = pessoa.Matricula,
            //                        NomePessoa = pessoa.NomePessoa,
            //                        CodSituacaoPessoa = pessoa.CodSituacaoPessoa,
            //                        SituacaoPessoa = situacao
            //                    }
            //                )
            //.FirstOrDefaultAsync();

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
                  .Select(x => new Pessoa
                  {
                      Id = x.Pessoa.Id,
                      Matricula = x.Pessoa.Matricula,
                      NomePessoa = x.Pessoa.NomePessoa,
                      CodSituacaoPessoa = x.Pessoa.CodSituacaoPessoa,
                      SituacaoPessoa = x.Situacao
                  })
          .FirstOrDefaultAsync();






            return result;
        }
    }
}

