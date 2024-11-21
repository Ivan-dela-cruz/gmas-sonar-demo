using CONTROLEMP.NominaGP.Portal.DTO;
using CONTROLEMP.NominaGP.Portal.Framework;
using CONTROLEMP.NominaGP.Portal.Framework.Persistence;
using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;

namespace CONTROLEMP.NominaGP.Portal.DAL
{
    public class FamiliarDAL
    {
        public IDBManager dbManager;

        public FamiliarDAL()
        {
            dbManager = new DBManager();
            dbManager.ProviderType = DataProvider.SqlServer;
            dbManager.ConnectionString = Configuration.Cadena;
        }

        #region ObtenerFamiliar
        public List<FamiliarDTO> ObtenerFamiliar(int intCodigoFuncionario)
        {
            List<FamiliarDTO> listFamiliar = new List<FamiliarDTO>();
            try
            {
                dbManager.Open();
                dbManager.Command.CommandTimeout = 0;
                dbManager.CreateParameters(1);
                dbManager.AddParameters(0, "@CodigoFuncionario", intCodigoFuncionario);
                dbManager.ExecuteReader(CommandType.StoredProcedure, "Portal_Select_Familiar");
                while (dbManager.DataReader.Read())
                {
                    FamiliarDTO entFamiliar = new FamiliarDTO();
                    entFamiliar.CodigoFamiliar = dbManager.DataReader.SafeGetInt32(0);
                    entFamiliar.NombreFamiliar = dbManager.DataReader.SafeGetString(1);
                    entFamiliar.PrimerApellidoFamiliar = dbManager.DataReader.SafeGetString(2);
                    entFamiliar.SegundoApellidoFamiliar = dbManager.DataReader.SafeGetString(3);
                    entFamiliar.EdadFamiliar = dbManager.DataReader.GetDecimal(4);
                    entFamiliar.EscolaridadFamiliar = dbManager.DataReader.SafeGetString(5);
                    entFamiliar.ProfesionFamiliar = dbManager.DataReader.SafeGetString(6);
                    entFamiliar.FechaNacimiento = dbManager.DataReader.SafeGetDateTime(7);
                    entFamiliar.EsDiscapacitado = dbManager.DataReader.GetBoolean(8);
                    entFamiliar.EnfermedadCatastrofica = dbManager.DataReader.GetBoolean(9);
                    entFamiliar.Sexo = dbManager.DataReader.SafeGetInt32(10);
                    entFamiliar.GradoDiscapacidad = dbManager.DataReader.GetDecimal(11);
                    entFamiliar.CodigoRelacionFamiliar = dbManager.DataReader.GetInt32(12);
                    entFamiliar.CodigoEstadoCivil = dbManager.DataReader.GetInt32(13);
                    //entFamiliar.FechaEvento = dbManager.DataReader.GetDateTime(14);
                    entFamiliar.EstadoFamiliar = dbManager.DataReader.GetBoolean(15);
                    entFamiliar.Observacion = dbManager.DataReader.SafeGetString(16);
                    entFamiliar.CampoAdicional = dbManager.DataReader.SafeGetString(17);
                    entFamiliar.CodigoFuncionario = dbManager.DataReader.SafeGetInt32(18);
                    entFamiliar.IdentificacionFamiliar = dbManager.DataReader.SafeGetString(19);
                    entFamiliar.RutaArchivo = dbManager.DataReader.SafeGetString(20);
                    entFamiliar.IngresosGrabados = dbManager.DataReader.GetBoolean(21);
                    listFamiliar.Add(entFamiliar);
                }
                return listFamiliar;
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
        #endregion

        #region Actualizar Familiar
        public void ActualizarFamiliar(FamiliarDTO FamiliarDTO)
        {
            try
            {
                DateTime FechaDesde= DateTime.Now;
                dbManager.Open();
                dbManager.Command.CommandTimeout = 0;
                dbManager.CreateParameters(14);
                dbManager.AddParameters(0, "@CodigoFamiliar", FamiliarDTO.CodigoFamiliar);
                dbManager.AddParameters(1, "@NombreFamiliar", FamiliarDTO.NombreFamiliar);
                dbManager.AddParameters(2, "@PrimerApellidoFamiliar", FamiliarDTO.PrimerApellidoFamiliar);
                dbManager.AddParameters(3, "@SegundoApellidoFamiliar", FamiliarDTO.SegundoApellidoFamiliar);
                dbManager.AddParameters(4, "@EdadFamiliar", FamiliarDTO.EdadFamiliar);
                dbManager.AddParameters(5, "@FechaNacimiento", FamiliarDTO.FechaNacimiento);
                dbManager.AddParameters(6, "@Sexo", FamiliarDTO.Sexo);
                dbManager.AddParameters(7, "@CodigoRelacionFamiliar", FamiliarDTO.CodigoRelacionFamiliar);
                dbManager.AddParameters(8, "@CodigoEstadoCivil", FamiliarDTO.CodigoEstadoCivil);
                dbManager.AddParameters(9, "@CodigoFuncionario", FamiliarDTO.CodigoFuncionario);
                dbManager.AddParameters(10, "@IdentificacionFamiliar", FamiliarDTO.IdentificacionFamiliar);
                dbManager.AddParameters(11, "@RutaArchivo", FamiliarDTO.RutaArchivo);
                dbManager.AddParameters(12, "@EsDiscapacitado", FamiliarDTO.EsDiscapacitado == true ? 1 : 0);
                dbManager.AddParameters(13, "@IngresosGrabados", FamiliarDTO.IngresosGrabados == true ? 1 : 0);

                dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "Portal_Update_Familiar");
                
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
        #endregion

        #region Insertar Familiar
        public void InsertarFamiliar(FamiliarDTO FamiliarDTO)
        {
            try
            {
                DateTime FechaDesde = DateTime.Now;
                dbManager.Open();
                dbManager.Command.CommandTimeout = 0;
                dbManager.CreateParameters(14);
                dbManager.AddParameters(0, "@CodigoFamiliar", FamiliarDTO.CodigoFamiliar);
                dbManager.AddParameters(1, "@NombreFamiliar", FamiliarDTO.NombreFamiliar);
                dbManager.AddParameters(2, "@PrimerApellidoFamiliar", FamiliarDTO.PrimerApellidoFamiliar);
                dbManager.AddParameters(3, "@SegundoApellidoFamiliar", FamiliarDTO.SegundoApellidoFamiliar);
                dbManager.AddParameters(4, "@EdadFamiliar", FamiliarDTO.EdadFamiliar);
                dbManager.AddParameters(5, "@FechaNacimiento", FamiliarDTO.FechaNacimiento);
                dbManager.AddParameters(6, "@Sexo", FamiliarDTO.Sexo);
                dbManager.AddParameters(7, "@CodigoRelacionFamiliar", FamiliarDTO.CodigoRelacionFamiliar);
                dbManager.AddParameters(8, "@CodigoEstadoCivil", FamiliarDTO.CodigoEstadoCivil);
                dbManager.AddParameters(9, "@CodigoFuncionario", FamiliarDTO.CodigoFuncionario);
                dbManager.AddParameters(10, "@IdentificacionFamiliar", FamiliarDTO.IdentificacionFamiliar);
                dbManager.AddParameters(11, "@RutaArchivo", FamiliarDTO.RutaArchivo);
                dbManager.AddParameters(12, "@EsDiscapacitado", FamiliarDTO.EsDiscapacitado == true ? 1 : 0);
                dbManager.AddParameters(13, "@IngresosGrabados", FamiliarDTO.IngresosGrabados == true ? 1 : 0);

                dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "Portal_Insert_Familiar");

                
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
        #endregion
    }
}
