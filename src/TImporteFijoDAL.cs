using CONTROLEMP.NominaGP.Portal.DTO;
using CONTROLEMP.NominaGP.Portal.Framework;
using CONTROLEMP.NominaGP.Portal.Framework.Persistence;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CONTROLEMP.NominaGP.Portal.DAL
{
    public class TImporteFijoDAL
    {
        private IDBManager dbManager;
        public TImporteFijoDAL()
        {
            dbManager = new DBManager();
            dbManager.ProviderType = DataProvider.SqlServer;
            dbManager.ConnectionString = Configuration.Cadena;
        }

        public int GuardarProyeccionGasto(TImporteFijoDTO entProyeccion)
        {
            try
            {
                DateTime FechaDesde = DateTime.Now;
                dbManager.Open();
                dbManager.ExecuteReader(CommandType.Text, string.Format("EXEC gmas_UpdateProyeccionGastosPortal '{0}',{1},{2},{3},{4},{5},{6}", entProyeccion.IdentificacionFuncionario, entProyeccion.Vivienda.ToString().Replace(',', '.'), entProyeccion.Educacion.ToString().Replace(',', '.'), entProyeccion.Salud.ToString().Replace(',', '.'), entProyeccion.Vestimenta.ToString().Replace(',', '.'), entProyeccion.Alimentacion.ToString().Replace(',', '.'), entProyeccion.Turismo.ToString().Replace(',', '.')));

               

                return 1;
            }
            catch (Exception ex)
            {
                GmasLogger.Instance.write("Method: " + MethodBase.GetCurrentMethod().DeclaringType.Name + " Error: " + ex.Message + "Stack Trace: " + ex.StackTrace.ToString(), "Nomina");
                
                return 0;
            }
            finally
            {
                dbManager.Dispose();
            }
        }

        public void SubirFormularioProyeccionGastos(byte[] data, string identificacion)
        {
            try
            {
                DateTime FechaDesde = DateTime.Now;
                dbManager.Open();
                dbManager.Command.CommandTimeout = 0;
                dbManager.CreateParameters(2);
                dbManager.AddParameters(0, "@Identificacion", identificacion);
                dbManager.AddParameters(1, "@data", data);
                dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "gmas_UpdateTImporteFijoFormulario");

               

            }
            catch (Exception ex)
            {
                GmasLogger.Instance.write("Method: " + MethodBase.GetCurrentMethod().DeclaringType.Name + " Error: " + ex.Message + "Stack Trace: " + ex.StackTrace.ToString(), "Nomina");
                throw ex;
            }
            finally
            {
                dbManager.Dispose();
            }
        }

        public byte[] GetFormularioProyeccionGastos(string identificacion)
        {
            byte[] data = null;
            try
            {
                dbManager.Open();
                dbManager.Command.CommandTimeout = 0;
                string str = "SET LANGUAGE US_ENGLISH exec gmas_DownloadTImporteFijoFormulario '" + identificacion + "'";
                dbManager.ExecuteReader(CommandType.Text, str);
                while (dbManager.DataReader.Read())
                {
                    data = (byte[])dbManager.DataReader.GetValue(0);
                }
            }
            catch (Exception ex)
            {
                GmasLogger.Instance.write("Method: " + MethodBase.GetCurrentMethod().DeclaringType.Name + " Error: " + ex.Message + "Stack Trace: " + ex.StackTrace.ToString(), "Nomina");
                
                data = null;
            }
            finally
            {
                dbManager.Dispose();
            }
            return data;
        }

        public TImporteFijoDTO ObtenerValoresProyeccionGastos(string IdentificacionFuncionario)
        {

            try
            {
                dbManager.Open();
                dbManager.Command.CommandTimeout = 0;
                dbManager.CreateParameters(1);
                dbManager.AddParameters(0, "@IdentificacionFuncionario", IdentificacionFuncionario);
                dbManager.ExecuteReader(CommandType.StoredProcedure, "gmas_ObtenerFormularioProyeccionGastosPortal");
                TImporteFijoDTO entProyeccion = new TImporteFijoDTO();
                while (dbManager.DataReader.Read())
                {
                    entProyeccion.Vivienda = dbManager.DataReader.GetDecimal(10).ToString().Replace(',', '.');
                    entProyeccion.Educacion = dbManager.DataReader.GetDecimal(11).ToString().Replace(',', '.');
                    entProyeccion.Salud = dbManager.DataReader.GetDecimal(12).ToString().Replace(',', '.');
                    entProyeccion.Vestimenta = dbManager.DataReader.GetDecimal(13).ToString().Replace(',', '.');
                    entProyeccion.Alimentacion = dbManager.DataReader.GetDecimal(14).ToString().Replace(',', '.');
                    entProyeccion.Turismo = dbManager.DataReader.GetDecimal(15).ToString().Replace(',', '.');
                    entProyeccion.TotalGastos = dbManager.DataReader.GetDecimal(16).ToString().Replace(',', '.');
                    entProyeccion.Bloquear = dbManager.DataReader.GetBoolean(21);
                }
                return entProyeccion;
            }
            catch (Exception ex)
            {
                GmasLogger.Instance.write("Method: " + MethodBase.GetCurrentMethod().DeclaringType.Name + " Error: " + ex.Message + "Stack Trace: " + ex.StackTrace.ToString(), "Nomina");
                
                return null;
            }
            finally
            {
                dbManager.Dispose();
            }
        }

        public ConfiguracionProyeccionGastoDTO ObtenerFechasCortePG(string Anno, int Mes)
        {

            try
            {
                dbManager.Open();
                dbManager.Command.CommandTimeout = 0;

                dbManager.ExecuteReader(CommandType.Text, string.Format("SET LANGUAGE US_ENGLISH select PG.CodigoMes,PF.NombrePeriodoFiscal,PG.FechaDesde,PG.FechaHasta from ConfiguracionProyeccionGasto PG JOIN PERIODOFISCAL PF ON PG.CodigoPeriodoFiscal=PF.CodigoPeriodoFiscal WHERE PF.NombrePeriodoFiscal='{0}' AND PG.CodigoMes={1}", Anno, Mes));
                ConfiguracionProyeccionGastoDTO entPG = new ConfiguracionProyeccionGastoDTO();
                while (dbManager.DataReader.Read())
                {
                    entPG.CodigoMes = dbManager.DataReader.GetInt32(0);
                    entPG.NombrePeriodoFiscal = dbManager.DataReader.GetString(1).ToString();
                    entPG.FechaDesde = dbManager.DataReader.GetDateTime(2);
                    entPG.FechaHasta = dbManager.DataReader.GetDateTime(3);

                }
                return entPG;
            }
            catch (Exception ex)
            {
                GmasLogger.Instance.write("Method: " + MethodBase.GetCurrentMethod().DeclaringType.Name + " Error: " + ex.Message + "Stack Trace: " + ex.StackTrace.ToString(), "Nomina");
                throw ex;
            }
            finally
            {
                dbManager.Dispose();
            }
        }
    }
}
