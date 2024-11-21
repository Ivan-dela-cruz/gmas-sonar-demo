using CONTROLEMP.NominaGP.Portal.DTO;
using CONTROLEMP.NominaGP.Portal.Framework;
using CONTROLEMP.NominaGP.Portal.Framework.Persistence;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CONTROLEMP.NominaGP.Portal.DAL
{
    public class RecesoAcademicoDAL
    {
        private IDBManager dbManager;

        public RecesoAcademicoDAL()
        {
            dbManager = new DBManager();
            dbManager.ProviderType = DataProvider.SqlServer;
            dbManager.ConnectionString = Configuration.Cadena;
        }

        public RecesoAcademicoDTO ConsultarRecesoAcademico(string IdentificacionFuncionario)
        {
            try
            {
                RecesoAcademicoDTO entRecesoAcademicoDTO = new RecesoAcademicoDTO();
                dbManager.Open();
                dbManager.ExecuteReader(CommandType.Text, string.Format("select top 1 Codigo,CodigoUnidad,a.CodigoPeriodoEscolar,Actividad,FechaDesde,FechaHasta,CantidadDias,Estado,b.NombrePeriodoEscolar from RecesosAcademicos a join PeriodoEscolar b on a.codigoperiodoescolar=b.CodigoPeriodoEscolar join Funcionario f on a.CodigoUnidad=f.CodigoUnidades where a.Estado=1 and b.EstadoPeriodoEscolar=1 and f.IdentificacionFuncionario='{0}' ORDER BY FECHADESDE asc", IdentificacionFuncionario));
               
                while (dbManager.DataReader.Read())
                {
                    entRecesoAcademicoDTO.Codigo = dbManager.DataReader.GetInt32(0);
                    entRecesoAcademicoDTO.CodigoUnidad = dbManager.DataReader.GetInt32(1);
                    entRecesoAcademicoDTO.CodigoPeriodoEscolar = dbManager.DataReader.GetInt32(2);
                    entRecesoAcademicoDTO.Actividad = dbManager.DataReader.GetString(3);
                    entRecesoAcademicoDTO.FechaDesde = dbManager.DataReader.GetDateTime(4);
                    entRecesoAcademicoDTO.FechaHasta = dbManager.DataReader.GetDateTime(5);
                    entRecesoAcademicoDTO.CantidadDias = dbManager.DataReader.GetInt32(6);
                    entRecesoAcademicoDTO.Estado = dbManager.DataReader.GetBoolean(7);
                    entRecesoAcademicoDTO.NombrePeriodoEscolar = dbManager.DataReader.GetString(8);
                }
               
                return entRecesoAcademicoDTO;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
