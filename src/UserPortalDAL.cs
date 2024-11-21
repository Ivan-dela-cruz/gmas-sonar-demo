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
    public class UserPortalDAL
    {
        public IDBManager dbManager;
        public UserPortalDAL()
        {
            dbManager = new DBManager();
            dbManager.ProviderType = DataProvider.SqlServer;
            dbManager.ConnectionString = Configuration.Cadena;
        }

        public void ActualizarUserPortal(UserPortalDTO userPortalDTO)
        {
            try
            {
                dbManager.Open();
                dbManager.Command.CommandTimeout = 0;
                dbManager.CreateParameters(1);
                dbManager.AddParameters(0, "@UserName", userPortalDTO.UserName);
                
              

                dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "gmas_ActualizarUserPortal");
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                dbManager.Dispose();
            }
        }

    }
}
