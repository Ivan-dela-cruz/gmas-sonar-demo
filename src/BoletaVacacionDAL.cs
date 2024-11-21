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
    
    public class BoletaVacacionDAL
    {
        private IDBManager dbManager;
        public BoletaVacacionDAL()
        {
            dbManager = new DBManager();
            dbManager.ProviderType = DataProvider.SqlServer;
            dbManager.ConnectionString = Configuration.Cadena;
        }

        public void EliminarBoletaVacacion(int IdBoletaVacaciones)
        {
            try
            {
                dbManager.Open();
                dbManager.ExecuteNonQuery(CommandType.Text, string.Format("set language us_english DELETE FROM BoletaVacaciones where IdBoletaVacaciones={0}", IdBoletaVacaciones));
            }
            catch
            {
                throw;
            }
            finally
            {
                dbManager.Dispose();
            }
        }
    }
}
