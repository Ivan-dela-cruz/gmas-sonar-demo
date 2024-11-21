using CONTROLEMP.NominaGP.Portal.Framework;
using CONTROLEMP.NominaGP.Portal.Framework.Persistence;
using CONTROLEMP.NominaGP.Portal.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Globalization;
using System.Data.SqlClient;
using Newtonsoft.Json;

namespace CONTROLEMP.NominaGP.Portal.DAL
{
    public class MarcacionBiometricoDAL
    {
        private IDBManager dbManager;

        public MarcacionBiometricoDAL()
        {
            dbManager = new DBManager();
            dbManager.ProviderType = DataProvider.SqlServer;
            dbManager.ConnectionString = Configuration.Cadena;
        }

        #region Marcacion Biometrico DETALLADO
        public List<MarcacionBiometricoStringDTO> ObtenerMarcacionBiometricoPorCodigoJefe(int CodigoPeriodoFiscal, int CodigoJefeFuncionario, int CodigoPeriodoPago, string CodigoUnidad, string CodigoDesignacion, string CodigoCentroCosto1, int EsAgricola,string CodigoSucursal)
        {
            List<MarcacionBiometricoStringDTO> lista = new List<MarcacionBiometricoStringDTO>();
            try
            {
                dbManager.Open();
                dbManager.Command.CommandTimeout = 0;
                dbManager.ExecuteReader(CommandType.Text, string.Format("exec gmas_ObtenerMarcacionBiometricoPorCodigoJefe {0},{1},{2},{3},{4},{5},{6},{7}", CodigoPeriodoFiscal, CodigoPeriodoPago, CodigoJefeFuncionario, CodigoUnidad, CodigoDesignacion, CodigoCentroCosto1, EsAgricola,CodigoSucursal));
                while (dbManager.DataReader.Read())
                {
                    MarcacionBiometricoStringDTO result = new MarcacionBiometricoStringDTO();
                    result.Codigo = dbManager.DataReader.GetInt32(0);
                    result.CodigoFuncionario = dbManager.DataReader.GetInt32(1);
                    result.CodigoPeriodoPago = dbManager.DataReader.GetInt32(2);
                    result.CodigoPeriodoFiscal = dbManager.DataReader.GetInt32(3);
                    result.FechaInicio = dbManager.DataReader.GetString(4);
                    result.FechaFinal = dbManager.DataReader.GetString(5);
                    result.JornadaDiaria = dbManager.DataReader.GetDecimal(6);
                    result.Tiempo = dbManager.DataReader.GetDecimal(7);
                    result.HoraNormal = dbManager.DataReader.GetDecimal(8);
                    result.Hora25 = dbManager.DataReader.GetDecimal(9);
                    result.Hora50 = dbManager.DataReader.GetDecimal(10);
                    result.Hora100 = dbManager.DataReader.GetDecimal(11);
                    result.HoraNoImputable = dbManager.DataReader.GetDecimal(12);
                    result.HoraMulta = dbManager.DataReader.GetDecimal(13);
                    result.Observacion = dbManager.DataReader.GetString(14);
                    result.NombreCompleto = dbManager.DataReader.GetString(15);
                    result.ValorRefrigerio = dbManager.DataReader.GetDecimal(16);
                    result.EsConfirmada = dbManager.DataReader.GetBoolean(17);

                    lista.Add(result);

                }

                return lista;
            }
            catch (Exception ex)
            {
                GmasLogger.Instance.write("Method: " + MethodBase.GetCurrentMethod().DeclaringType.Name + " Error: " + ex.Message + "Stack Trace: " + ex.StackTrace.ToString(), "BalconGP");
                return null;
            }
            finally
            {
                dbManager.Dispose();
            }
        }

        public void ActualizarMarcacionBiometrico(MarcacionBiometricoDTO marcacionbiometrico)
        {
            try
            {
                dbManager.Open();
                dbManager.Command.CommandTimeout = 0;
                dbManager.CreateParameters(16);
                dbManager.AddParameters(0, "@Codigo", marcacionbiometrico.Codigo);
                dbManager.AddParameters(1, "@CodigoFuncionario", marcacionbiometrico.CodigoFuncionario);
                dbManager.AddParameters(2, "@FechaInicio", marcacionbiometrico.FechaInicio);
                dbManager.AddParameters(3, "@FechaFinal", marcacionbiometrico.FechaFinal);
                dbManager.AddParameters(4, "@JornadaDiaria", marcacionbiometrico.JornadaDiaria);
                dbManager.AddParameters(5, "@Tiempo", marcacionbiometrico.Tiempo);
                dbManager.AddParameters(6, "@HoraNormal", marcacionbiometrico.HoraNormal);
                dbManager.AddParameters(7, "@Hora25", marcacionbiometrico.Hora25);
                dbManager.AddParameters(8, "@Hora50", marcacionbiometrico.Hora50);
                dbManager.AddParameters(9, "@Hora100", marcacionbiometrico.Hora100);
                dbManager.AddParameters(10, "@HoraNoImputable", marcacionbiometrico.HoraNoImputable);
                dbManager.AddParameters(11, "@HoraMulta", marcacionbiometrico.HoraMulta);
                dbManager.AddParameters(12, "@Observacion", marcacionbiometrico.Observacion);
                dbManager.AddParameters(13, "@ValorRefrigerio", marcacionbiometrico.ValorRefrigerio);
                dbManager.AddParameters(14, "@EsConfirmada", marcacionbiometrico.EsConfirmada);
                dbManager.AddParameters(15, "@Usuario", marcacionbiometrico.Usuario);


                dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "Portal_Update_MarcacionBiometrico");
            }
            catch (Exception ex)
            {
                GmasLogger.Instance.write("Method: " + MethodBase.GetCurrentMethod().DeclaringType.Name + " Error: " + ex.Message + "Stack Trace: " + ex.StackTrace.ToString(), "BalconGP");
            }
            finally
            {
                dbManager.Dispose();
            }
        }

        public int ConfirmaMarcacionBiometrico(MarcacionBiometricoStringDTO MarcacionBiometrico)
        {
            try
            {

                dbManager.Open();
                dbManager.Command.CommandTimeout = 0;
                dbManager.CreateParameters(19);
                dbManager.AddParameters(0, "@CodigoFuncionario", MarcacionBiometrico.CodigoFuncionario);
                dbManager.AddParameters(1, "@CodigoPeriodoPago", MarcacionBiometrico.CodigoPeriodoPago);
                dbManager.AddParameters(2, "@CodigoPeriodoFiscal", MarcacionBiometrico.CodigoPeriodoFiscal);
                dbManager.AddParameters(3, "@FechaInicio", Convert.ToDateTime(MarcacionBiometrico.FechaInicio));
                dbManager.AddParameters(4, "@FechaFinal", Convert.ToDateTime(MarcacionBiometrico.FechaFinal));
                dbManager.AddParameters(5, "@JornadaDiaria", MarcacionBiometrico.JornadaDiaria);
                dbManager.AddParameters(6, "@Tiempo", MarcacionBiometrico.Tiempo);
                dbManager.AddParameters(7, "@HoraNormal", MarcacionBiometrico.HoraNormal);
                dbManager.AddParameters(8, "@Hora25", MarcacionBiometrico.Hora25);
                dbManager.AddParameters(9, "@Hora50", MarcacionBiometrico.Hora50);
                dbManager.AddParameters(10, "@Hora100", MarcacionBiometrico.Hora100);
                dbManager.AddParameters(11, "@HoraNoImputable", MarcacionBiometrico.HoraNoImputable);
                dbManager.AddParameters(12, "@HoraMulta", MarcacionBiometrico.HoraMulta);
                dbManager.AddParameters(13, "@Observacion", MarcacionBiometrico.Observacion);
                dbManager.AddParameters(14, "@EsConfirmada", MarcacionBiometrico.EsConfirmada);
                dbManager.AddParameters(15, "@EsConfirmadaFinal", MarcacionBiometrico.EsConfirmadaFinal);
                dbManager.AddParameters(16, "@Usuario", MarcacionBiometrico.Usuario);
                dbManager.AddParameters(17, "@ValorRecargo", MarcacionBiometrico.ValorRecargo);
                dbManager.AddParameters(18, "@ValorRefrigerio", MarcacionBiometrico.ValorRefrigerio);


                dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "Portal_Insert_MarcacionBiometricoConfirmada");


                return 1;

            }
            catch (Exception ex)
            {
                GmasLogger.Instance.write("Method: " + MethodBase.GetCurrentMethod().DeclaringType.Name + " Error: " + ex.Message + "Stack Trace: " + ex.StackTrace.ToString(), "BalconGP");
                return 0;
            }
        }

        public int InsertaMarcacionBiometrico(MarcacionBiometricoStringDTO MarcacionBiometrico)
        {
            try
            {

                dbManager.Open();
                dbManager.Command.CommandTimeout = 0;
                dbManager.CreateParameters(18);
                dbManager.AddParameters(0, "@CodigoFuncionario", MarcacionBiometrico.CodigoFuncionario);
                dbManager.AddParameters(1, "@CodigoPeriodoPago", MarcacionBiometrico.CodigoPeriodoPago);
                dbManager.AddParameters(2, "@CodigoPeriodoFiscal", MarcacionBiometrico.CodigoPeriodoFiscal);
                dbManager.AddParameters(3, "@FechaInicio", Convert.ToDateTime(MarcacionBiometrico.FechaInicio));
                dbManager.AddParameters(4, "@FechaFinal", Convert.ToDateTime(MarcacionBiometrico.FechaFinal));
                dbManager.AddParameters(5, "@JornadaDiaria", MarcacionBiometrico.JornadaDiaria);
                dbManager.AddParameters(6, "@Tiempo", MarcacionBiometrico.Tiempo);
                dbManager.AddParameters(7, "@HoraNormal", MarcacionBiometrico.HoraNormal);
                dbManager.AddParameters(8, "@Hora25", MarcacionBiometrico.Hora25);
                dbManager.AddParameters(9, "@Hora50", MarcacionBiometrico.Hora50);
                dbManager.AddParameters(10, "@Hora100", MarcacionBiometrico.Hora100);
                dbManager.AddParameters(11, "@HoraNoImputable", MarcacionBiometrico.HoraNoImputable);
                dbManager.AddParameters(12, "@HoraMulta", MarcacionBiometrico.HoraMulta);
                dbManager.AddParameters(13, "@Observacion", MarcacionBiometrico.Observacion);
                dbManager.AddParameters(14, "@EsConfirmada", MarcacionBiometrico.EsConfirmada);
                dbManager.AddParameters(15, "@EsConfirmadaFinal", MarcacionBiometrico.EsConfirmadaFinal);
                dbManager.AddParameters(16, "@Usuario", MarcacionBiometrico.Usuario);
                dbManager.AddParameters(17, "@ValorRefrigerio", MarcacionBiometrico.ValorRefrigerio);


                dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "Portal_Insert_MarcacionBiometrico");


                return 1;

            }
            catch (Exception ex)
            {
                GmasLogger.Instance.write("Method: " + MethodBase.GetCurrentMethod().DeclaringType.Name + " Error: " + ex.Message + "Stack Trace: " + ex.StackTrace.ToString(), "BalconGP");
                return 0;
            }
        }

        public List<ConfiguracionAutorizadorDTO> ObtenerAutorizadorBiometrico(int CodigoFuncionario )
        {
            List<ConfiguracionAutorizadorDTO> lista = new List<ConfiguracionAutorizadorDTO>();
            try
            {
                dbManager.Open();
                dbManager.Command.CommandTimeout = 0;
                dbManager.ExecuteReader(CommandType.Text, string.Format("exec gmas_ObtenerConfirmadorBiometricoF1 {0}", CodigoFuncionario));
                while (dbManager.DataReader.Read())
                {
                    ConfiguracionAutorizadorDTO resultConfiguracionAutorizador = new ConfiguracionAutorizadorDTO();
                    resultConfiguracionAutorizador.CodigoAutorizador = dbManager.DataReader.GetInt32(0);
                    resultConfiguracionAutorizador.IdentificacionAutorizador = dbManager.DataReader.GetString(1);
                    resultConfiguracionAutorizador.NombreCompletoAutorizador = dbManager.DataReader.GetString(2);
                    resultConfiguracionAutorizador.TelefonoCelular = dbManager.DataReader.GetString(3);
                    resultConfiguracionAutorizador.CorreoAutorizador = dbManager.DataReader.GetString(4);

                    lista.Add(resultConfiguracionAutorizador);

                }

                return lista;
            }
            catch (Exception ex)
            {
                GmasLogger.Instance.write("Method: " + MethodBase.GetCurrentMethod().DeclaringType.Name + " Error: " + ex.Message + "Stack Trace: " + ex.StackTrace.ToString(), "BalconGP");
                return null;
            }
            finally
            {
                dbManager.Dispose();
            }
        }
        #endregion

        #region MARCACIONES RESUMIDAS
        /// <summary>
        /// SUPERINTENDENTE
        /// </summary>

        public List<MarcacionBiometricoResumidoDTO> ObtenerMarcacionBiometricoResumido(int CodigoPeriodoFiscal, int CodigoJefeFuncionario, int CodigoPeriodoPago, string CodigoUnidad, string CodigoDesignacion, string CodigoCentroCosto1, int EsAgricola, string opcion)
        {
            List<MarcacionBiometricoResumidoDTO> lista = new List<MarcacionBiometricoResumidoDTO>();
            try
            {
                dbManager.Open();
                dbManager.Command.CommandTimeout = 0;
                dbManager.ExecuteReader(CommandType.Text, string.Format("exec gmas_ObtenerMarcacionBiometricoResumido {0},{1},{2},{3},{4},{5},{6},'{7}'", CodigoPeriodoFiscal, CodigoPeriodoPago, CodigoJefeFuncionario, CodigoUnidad, CodigoDesignacion, CodigoCentroCosto1, EsAgricola, opcion));
                while (dbManager.DataReader.Read())
                {
                    MarcacionBiometricoResumidoDTO result = new MarcacionBiometricoResumidoDTO();
                    result.Codigo = dbManager.DataReader.GetInt32(0);
                    result.CodigoFuncionario = dbManager.DataReader.GetInt32(1);
                    result.CodigoPeriodoPago = dbManager.DataReader.GetInt32(2);
                    result.CodigoPeriodoFiscal = dbManager.DataReader.GetInt32(3);
                    result.FechaInicio = dbManager.DataReader.GetString(4);
                    result.FechaFinal = dbManager.DataReader.GetString(5);
                    result.JornadaDiaria = dbManager.DataReader.GetDecimal(6);
                    result.Tiempo = dbManager.DataReader.GetDecimal(7);
                    result.HoraNormal = dbManager.DataReader.GetDecimal(8);
                    result.Hora25 = dbManager.DataReader.GetDecimal(9);
                    result.Hora50 = dbManager.DataReader.GetDecimal(10);
                    result.Hora100 = dbManager.DataReader.GetDecimal(11);
                    result.HoraMulta = dbManager.DataReader.GetDecimal(12);
                    result.Observacion = dbManager.DataReader.GetString(13);
                    result.EsConfirmada = dbManager.DataReader.GetBoolean(14);
                    result.FechaConfirmada = dbManager.DataReader.GetString(15);
                    result.UsuarioConfirmada = dbManager.DataReader.GetString(16);
                    result.TiempoAjustado = dbManager.DataReader.GetDecimal(17);
                    result.Costo = dbManager.DataReader.GetDecimal(18);
                    result.NombreCompleto = dbManager.DataReader.GetString(19);

                    lista.Add(result);

                }

                return lista;
            }
            catch (Exception ex)
            {
                GmasLogger.Instance.write("Method: " + MethodBase.GetCurrentMethod().DeclaringType.Name + " Error: " + ex.Message + "Stack Trace: " + ex.StackTrace.ToString(), "BalconGP");
                return null;
            }
            finally
            {
                dbManager.Dispose();
            }
        }

        /// <summary>
        /// GERENTE DE PROYECTO - GERENTE GENERAL
        /// </summary>

        public List<MarcacionBiometricoCostoDTO> ObtenerMarcacionBiometricoCosto(int CodigoPeriodoFiscal, int CodigoJefeFuncionario, int CodigoPeriodoPago, string CodigoUnidad, string CodigoDesignacion, string CodigoCentroCosto1, int EsAgricola, string opcion)
        {
            List<MarcacionBiometricoCostoDTO> lista = new List<MarcacionBiometricoCostoDTO>();
            try
            {
                dbManager.Open();
                dbManager.Command.CommandTimeout = 0;
                dbManager.ExecuteReader(CommandType.Text, string.Format("exec gmas_ObtenerMarcacionBiometricoCosto {0},{1},{2},{3},{4},{5},{6},'{7}'", CodigoPeriodoFiscal, CodigoPeriodoPago, CodigoJefeFuncionario, CodigoUnidad, CodigoDesignacion, CodigoCentroCosto1, EsAgricola, opcion));
                while (dbManager.DataReader.Read())
                {
                    MarcacionBiometricoCostoDTO result = new MarcacionBiometricoCostoDTO();
                    result.Codigo = dbManager.DataReader.GetInt32(0);
                    result.CodigoFuncionario = dbManager.DataReader.GetInt32(1);
                    result.CodigoPeriodoPago = dbManager.DataReader.GetInt32(2);
                    result.CodigoPeriodoFiscal = dbManager.DataReader.GetInt32(3);
                    result.FechaInicio = dbManager.DataReader.GetString(4);
                    result.FechaFinal = dbManager.DataReader.GetString(5);

                    result.Hora25 = dbManager.DataReader.GetDecimal(6);
                    result.Hora50 = dbManager.DataReader.GetDecimal(7);
                    result.Hora100 = dbManager.DataReader.GetDecimal(8);


                    result.EsConfirmada2 = dbManager.DataReader.GetBoolean(9);
                    result.FechaConfirmada2 = dbManager.DataReader.GetString(10);
                    result.UsuarioConfirmada2 = dbManager.DataReader.GetString(11);
                    result.Observacion2 = dbManager.DataReader.GetString(12);

                    result.EsConfirmada3 = dbManager.DataReader.GetBoolean(13);
                    result.FechaConfirmada3 = dbManager.DataReader.GetString(14);
                    result.UsuarioConfirmada3 = dbManager.DataReader.GetString(15);
                    result.Observacion3 = dbManager.DataReader.GetString(16);

                    result.CampoAdicional = dbManager.DataReader.GetString(17);

                    result.NombreCompleto = dbManager.DataReader.GetString(18);
                    result.NombreCargo = dbManager.DataReader.GetString(19);
                    result.CentroCosto = dbManager.DataReader.GetString(20);


                    lista.Add(result);

                }

                return lista;
            }
            catch (Exception ex)
            {
                GmasLogger.Instance.write("Method: " + MethodBase.GetCurrentMethod().DeclaringType.Name + " Error: " + ex.Message + "Stack Trace: " + ex.StackTrace.ToString(), "BalconGP");
                return null;
            }
            finally
            {
                dbManager.Dispose();
            }
        }

        public void ActualizarMarcacionBiometricoDetalladoC0(string CodigoMarcacionBiometrico, string CodigoFuncionarios,string Usuario)
        {
            try
            {
                dbManager.Open();
                dbManager.Command.CommandTimeout = 0;
                dbManager.CreateParameters(3);
                dbManager.AddParameters(0, "@CodigoMarcacionBiometrico", CodigoMarcacionBiometrico);
                dbManager.AddParameters(1, "@CodigoFuncionarios", CodigoFuncionarios);
                dbManager.AddParameters(2, "@Usuario", Usuario);

                dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "gmas_update_MarcacionBiometricoResumidoC0");
            }
            catch (Exception ex)
            {
                GmasLogger.Instance.write("Method: " + MethodBase.GetCurrentMethod().DeclaringType.Name + " Error: " + ex.Message + "Stack Trace: " + ex.StackTrace.ToString(), "BalconGP");
            }
            finally
            {
                dbManager.Dispose();
            }

        }

        public void ActualizarMarcacionBiometricoResumidoC1(MarcacionBiometricoResumidoDTO marcacionbiometrico)
        {
            try
            {
                dbManager.Open();
                dbManager.Command.CommandTimeout = 0;
                dbManager.CreateParameters(10);
                dbManager.AddParameters(0, "@Codigo", marcacionbiometrico.Codigo);
                dbManager.AddParameters(1, "@CodigoFuncionario", marcacionbiometrico.CodigoFuncionario);
                dbManager.AddParameters(2, "@HoraNormal", marcacionbiometrico.HoraNormal);
                dbManager.AddParameters(3, "@Hora25", marcacionbiometrico.Hora25);
                dbManager.AddParameters(4, "@Hora50", marcacionbiometrico.Hora50);
                dbManager.AddParameters(5, "@Hora100", marcacionbiometrico.Hora100);
                dbManager.AddParameters(6, "@HoraMulta", marcacionbiometrico.HoraMulta);
                dbManager.AddParameters(7, "@Observacion", marcacionbiometrico.Observacion);
                dbManager.AddParameters(8, "@EsConfirmada", marcacionbiometrico.EsConfirmada);
                dbManager.AddParameters(9, "@UsuarioConfirmada", marcacionbiometrico.UsuarioConfirmada);

                dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "gmas_update_MarcacionBiometricoConfirmadaC1");
            }
            catch (Exception ex)
            {
                GmasLogger.Instance.write("Method: " + MethodBase.GetCurrentMethod().DeclaringType.Name + " Error: " + ex.Message + "Stack Trace: " + ex.StackTrace.ToString(), "BalconGP");
            }
            finally
            {
                dbManager.Dispose();
            }

        }

        public void ActualizarMarcacionBiometricoResumidoC2(MarcacionBiometricoCostoDTO marcacionbiometrico)
        {
            try
            {
                dbManager.Open();
                dbManager.Command.CommandTimeout = 0;
                dbManager.CreateParameters(9);
                dbManager.AddParameters(0, "@Codigo", marcacionbiometrico.Codigo);
                dbManager.AddParameters(1, "@CodigoFuncionario", marcacionbiometrico.CodigoFuncionario);

                dbManager.AddParameters(2, "@Hora25", marcacionbiometrico.Hora25);
                dbManager.AddParameters(3, "@Hora50", marcacionbiometrico.Hora50);
                dbManager.AddParameters(4, "@Hora100", marcacionbiometrico.Hora100);

                dbManager.AddParameters(5, "@Observacion2", marcacionbiometrico.Observacion2);
                dbManager.AddParameters(6, "@EsConfirmada2", marcacionbiometrico.EsConfirmada2);
                dbManager.AddParameters(7, "@UsuarioConfirmada2", marcacionbiometrico.UsuarioConfirmada2);

                dbManager.AddParameters(8, "@Data", marcacionbiometrico.CampoAdicional);


                dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "gmas_update_MarcacionBiometricoConfirmadaC2");
            }
            catch (Exception ex)
            {
                GmasLogger.Instance.write("Method: " + MethodBase.GetCurrentMethod().DeclaringType.Name + " Error: " + ex.Message + "Stack Trace: " + ex.StackTrace.ToString(), "BalconGP");
            }
            finally
            {
                dbManager.Dispose();
            }

        }

        public void ActualizarMarcacionBiometricoResumidoC3(MarcacionBiometricoCostoDTO marcacionbiometrico)
        {
            try
            {
                dbManager.Open();
                dbManager.Command.CommandTimeout = 0;
                dbManager.CreateParameters(9);
                dbManager.AddParameters(0, "@Codigo", marcacionbiometrico.Codigo);
                dbManager.AddParameters(1, "@CodigoFuncionario", marcacionbiometrico.CodigoFuncionario);

                dbManager.AddParameters(2, "@Hora25", marcacionbiometrico.Hora25);
                dbManager.AddParameters(3, "@Hora50", marcacionbiometrico.Hora50);
                dbManager.AddParameters(4, "@Hora100", marcacionbiometrico.Hora100);

                dbManager.AddParameters(5, "@Observacion3", marcacionbiometrico.Observacion3);
                dbManager.AddParameters(6, "@EsConfirmada3", marcacionbiometrico.EsConfirmada3);
                dbManager.AddParameters(7, "@UsuarioConfirmada3", marcacionbiometrico.UsuarioConfirmada3);

                dbManager.AddParameters(8, "@Data", marcacionbiometrico.CampoAdicional);


                dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "gmas_update_MarcacionBiometricoConfirmadaC3");
            }
            catch (Exception ex)
            {
                GmasLogger.Instance.write("Method: " + MethodBase.GetCurrentMethod().DeclaringType.Name + " Error: " + ex.Message + "Stack Trace: " + ex.StackTrace.ToString(), "BalconGP");
            }
            finally
            {
                dbManager.Dispose();
            }

        }
        public List<MarcacionBiometricoMailDTO> ObtenerMarcacionBiometricoMail(int CodigoJefeFuncionario, string Opcion)
        {
            List<MarcacionBiometricoMailDTO> lstresultado = new List<MarcacionBiometricoMailDTO>();
            try
            {
                dbManager.Open();
                dbManager.Command.CommandTimeout = 0;
                dbManager.ExecuteReader(CommandType.Text, string.Format("exec gmas_ObtenerMarcacionBiometricoMail {0},'{1}'", CodigoJefeFuncionario, Opcion));
                while (dbManager.DataReader.Read())
                {
                    MarcacionBiometricoMailDTO resultado = new MarcacionBiometricoMailDTO();
                    resultado.Emisor = dbManager.DataReader.GetString(0);
                    resultado.MailEmisor = dbManager.DataReader.GetString(1);
                    resultado.Receptor = dbManager.DataReader.GetString(2);
                    resultado.MailReceptor = dbManager.DataReader.GetString(3);
                    lstresultado.Add(resultado);
                }

                return lstresultado;
            }
            catch (Exception ex)
            {
                GmasLogger.Instance.write("Method: " + MethodBase.GetCurrentMethod().DeclaringType.Name + " Error: " + ex.Message + "Stack Trace: " + ex.StackTrace.ToString(), "BalconGP");
                return null;
            }
            finally
            {
                dbManager.Dispose();
            }
        }

        public int RegistrarMarcacionBiometricoCosto(List<tblMarcacionBiometricoCostoDTO> lstResultado, string UsuarioAudit)
        {
            try
            {
                string xml = string.Empty;
                string comillas = "\"";
                xml = "<biometrico>";
                foreach (var item in lstResultado)
                {
                    xml += "<linea CodigoFuncionario=" + comillas + item.CodigoFuncionario + comillas + " CodigoPeriodoPago=" + comillas + item.CodigoPeriodoPago + comillas + " CodigoPeriodoFiscal=" + comillas + item.CodigoPeriodoFiscal + comillas + " FechaInicial=" + comillas + item.FechaInicio + comillas + " FechaFinal=" + comillas + item.FechaFinal + comillas + " Hora25=" + comillas + item.Hora25.ToString().Replace(",", ".") + comillas + " Hora50=" + comillas + item.Hora50.ToString().Replace(",", ".") + comillas + " Hora100=" + comillas + item.Hora100.ToString().Replace(",", ".") + comillas + " ></linea>";
                }
                xml += "</biometrico>";

                dbManager.Open();
                dbManager.ExecuteNonQuery(CommandType.Text, string.Format("exec gmas_InsertarMarcacionBiometricoCosto '{0}','{1}'", xml, UsuarioAudit));
                return 1;
            }
            catch (Exception ex)
            {
                GmasLogger.Instance.write("Method: " + MethodBase.GetCurrentMethod().DeclaringType.Name + " Error: " + ex.Message + "Stack Trace: " + ex.StackTrace.ToString(), "BalconGP");
                return 0;
            }
            finally
            {
                dbManager.Dispose();
            }

        }

        public int RegistrarMarcacionBiometricoCostoAprobacion(List<tblMarcacionBiometricoCostoDTO> lstResultado, string UsuarioAudit)
        {
            try
            {
                string xml = string.Empty;
                string comillas = "\"";
                xml = "<biometrico>";
                foreach (var item in lstResultado)
                {
                    xml += "<linea CodigoFuncionario=" + comillas + item.CodigoFuncionario + comillas + " CodigoPeriodoPago=" + comillas + item.CodigoPeriodoPago + comillas + " CodigoPeriodoFiscal=" + comillas + item.CodigoPeriodoFiscal + comillas  + " ></linea>";
                }
                xml += "</biometrico>";

                dbManager.Open();
                dbManager.ExecuteNonQuery(CommandType.Text, string.Format("exec gmas_InsertarMarcacionBiometricoCostoAprobacion '{0}','{1}'", xml, UsuarioAudit));
                return 1;
            }
            catch (Exception ex)
            {
                GmasLogger.Instance.write("Method: " + MethodBase.GetCurrentMethod().DeclaringType.Name + " Error: " + ex.Message + "Stack Trace: " + ex.StackTrace.ToString(), "BalconGP");
                return 0;
            }
            finally
            {
                dbManager.Dispose();
            }

        }

        public bool bulkData(DataTable tabla, string tableName, bool isSuccess)
        {

            using (SqlBulkCopy bulkCopy = new SqlBulkCopy(dbManager.ConnectionString, SqlBulkCopyOptions.KeepIdentity |
                    SqlBulkCopyOptions.UseInternalTransaction))
            {
                foreach (DataColumn c in tabla.Columns)

                    bulkCopy.ColumnMappings.Add(c.ColumnName, c.ColumnName);
                bulkCopy.BatchSize = 1000;
                bulkCopy.DestinationTableName = tableName;
                try
                {
                    bulkCopy.WriteToServer(tabla);
                    isSuccess = true;
                }
                catch (Exception ex)
                {
                    GmasLogger.Instance.write("Method: " + MethodBase.GetCurrentMethod().DeclaringType.Name + " Error: " + ex.Message + "Stack Trace: " + ex.StackTrace.ToString(), "BalconGP");
                    isSuccess = false;
                }
            }
            return isSuccess;
        }


        #endregion
    }
}


