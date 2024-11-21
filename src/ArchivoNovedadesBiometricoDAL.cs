using CONTROLEMP.NominaGP.Portal.Framework;
using CONTROLEMP.NominaGP.Portal.Framework.Persistence;
using CONTROLEMP.NominaGP.Portal.DTO;
using System;
using System.Collections.Generic;
using System.Data;

namespace CONTROLEMP.NominaGP.Portal.DAL
{
    public class ArchivoNovedadesBiometricoDAL
    {
        private IDBManager dbManager;

        public ArchivoNovedadesBiometricoDAL()
        {
            dbManager = new DBManager();
            dbManager.ProviderType = DataProvider.SqlServer;
            dbManager.ConnectionString = Configuration.Cadena;
        }

        //Archivos del BIOMETRICO
        public List<ArchivoNovedadesBiometricoDTO> ObtenerArchivosBiometrico()
        {
            List<ArchivoNovedadesBiometricoDTO> lista = new List<ArchivoNovedadesBiometricoDTO>();
            try
            {
                dbManager.Open();
                dbManager.Command.CommandTimeout = 0;
                dbManager.ExecuteReader(CommandType.Text, "select * from ArchivoNovedadesBiometrico where Cargado = 0");
                while (dbManager.DataReader.Read())
                {
                    ArchivoNovedadesBiometricoDTO result = new ArchivoNovedadesBiometricoDTO();
                    result.CodigoBiometrico = dbManager.DataReader.GetInt32(0);
                    result.RutaArchivo = dbManager.DataReader.SafeGetString(1);
                    result.Cargado = dbManager.DataReader.GetBoolean(2);
                    result.CodigoMesAfectacion = dbManager.DataReader.GetInt32(3);
                    result.CodigoPeriodoFiscal = dbManager.DataReader.GetInt32(4);
                    result.EsVertical = dbManager.DataReader.GetBoolean(5);
                    result.FechaCarga = dbManager.DataReader.GetDateTime(6);
                    result.UsuarioCarga = dbManager.DataReader.SafeGetString(7);
                    result.Reemplazar = dbManager.DataReader.GetBoolean(8);
                    lista.Add(result);
                }

                return lista;
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
