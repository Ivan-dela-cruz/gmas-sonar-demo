using CONTROLEMP.NominaGP.Portal.Framework.Persistence;
using CONTROLEMP.NominaGP.Portal.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CONTROLEMP.NominaGP.Portal.DTO;
using System.Data;

namespace CONTROLEMP.NominaGP.Portal.DAL
{
    public class EnvioCorreoDAL
    {

        private IDBManager dbManager;

        public EnvioCorreoDAL()
        {
            dbManager = new DBManager();
            dbManager.ProviderType = DataProvider.SqlServer;
            dbManager.ConnectionString = Configuration.Cadena;
        }

        public int InsertarEnviosPendientes(EnvioMailCabeceraDTO envioMailCabeceraDTO,int TieneAdjunto)
        {
            
            try
            {
                dbManager.Open();
                dbManager.Command.CommandTimeout = 0;

                dbManager.CreateParameters(8);
                dbManager.AddParameters(0, "@EmailTO", envioMailCabeceraDTO.EmailTO);
                dbManager.AddParameters(1, "@EmailCC", envioMailCabeceraDTO.EmailCC);
                dbManager.AddParameters(2, "@EmailCCO", envioMailCabeceraDTO.EmailCCO);
                dbManager.AddParameters(3, "@Subject", envioMailCabeceraDTO.Subject);
                dbManager.AddParameters(4, "@Body", envioMailCabeceraDTO.Body);
                dbManager.AddParameters(5, "@solicitante", envioMailCabeceraDTO.Solicitante);
                dbManager.AddParameters(6, "@TieneAdjunto", envioMailCabeceraDTO.TieneAdjunto);
                dbManager.AddParameters(7, "@UsuarioAuditoria", envioMailCabeceraDTO.UsuarioAuditoria);
                dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "gmas_InsertEnvioMail");

                return 1;
            }
            catch (Exception ex)
            {
                return -1;
            }
            finally
            {
                dbManager.Dispose();
            }
            
        }
    }
}
