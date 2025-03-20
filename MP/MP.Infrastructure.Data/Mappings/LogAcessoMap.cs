using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MP.Core.Entities;

namespace MP.Infrastructure.Data.Mappings
{
    public class LogAcessoMap : IEntityTypeConfiguration<LogAcesso>
    {
        public void Configure(EntityTypeBuilder<LogAcesso> builder)
        {
            builder.ToTable("LOG_ACESSO");

            builder.HasKey(c => c.Id);
            builder.Property(c => c.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("CD_LOG_ACESSO");

            builder.Property(c => c.Credencial)
                .HasColumnName("NU_CREDENCIAL");
            
            builder.Property(c => c.Equipamento)
                .HasColumnName("CD_EQUIPAMENTO");
            
            builder.Property(c => c.DataRequisicao)
               .HasColumnName("DT_REQUISICAO");

            builder.Property(c => c.SendidoConsulta)
               .HasColumnName("TP_SENTIDO_CONSULTA");

            builder.Property(c => c.Evento)
               .HasColumnName("TP_EVENTO");

            builder.Property(c => c.CodAreaOrigem)
               .HasColumnName("CD_AREA_ORIGEM");

            builder.Property(c => c.CodAreaDestino)
               .HasColumnName("CD_AREA_DESTINO");

            builder.Property(c => c.Funcao)
               .HasColumnName("NU_FUNCAO");

            builder.Property(c => c.CodGrupo)
               .HasColumnName("CD_GRUPO");

            builder.Property(c => c.Matricula)
               .HasColumnName("NU_MATRICULA");

            builder.Property(c => c.CodVisitante)
               .HasColumnName("CD_VISITANTE");

            builder.Property(c => c.NuDataRequisicao)
               .HasColumnName("NU_DATA_REQUISICAO");

            builder.Property(c => c.NuHoraRequisicao)
               .HasColumnName("NU_HORA_REQUISICAO");

            builder.Property(c => c.DataPersistencia)
               .HasColumnName("DT_PERSISTENCIA");

            builder.Property(c => c.DsEquipamento)
               .HasColumnName("DS_EQUIPAMENTO");

            builder.Property(c => c.DsAreaDestino)
               .HasColumnName("DS_AREA_DESTINO");

            builder.Property(c => c.DsAreaOrigem)
               .HasColumnName("DS_AREA_ORIGEM");

            builder.Property(c => c.DsGrupo)
               .HasColumnName("DS_GRUPO");

            builder.Property(c => c.Nome)
               .HasColumnName("NM_PESSOA");

            builder.Ignore(c => c.Notifications);
            builder.Ignore(c => c.IsValid);

        }
    }
}
