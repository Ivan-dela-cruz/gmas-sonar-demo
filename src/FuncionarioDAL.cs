using CONTROLEMP.NominaGP.Portal.DTO;
using CONTROLEMP.NominaGP.Portal.Framework;
using CONTROLEMP.NominaGP.Portal.Framework.Persistence;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

using System.Data;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices.WindowsRuntime;

namespace CONTROLEMP.NominaGP.Portal.DAL
{
    public class FuncionarioDAL
    {


        private IDBManager dbManager;

        public FuncionarioDAL()
        {
            dbManager = new DBManager();
            dbManager.ProviderType = DataProvider.SqlServer;
            dbManager.ConnectionString = Configuration.Cadena;
        }

        #region ObtenerFuncionarioBanco
        public List<FuncionarioBancoDTO> ObtenerFuncionarioBanco(int intCodigoFuncionario)
        {
            List<FuncionarioBancoDTO> listfuncionariobanco = new List<FuncionarioBancoDTO>();
            try
            {

                dbManager.Open();
                dbManager.Command.CommandTimeout = 0;
                dbManager.CreateParameters(1);
                dbManager.AddParameters(0, "@CodigoFuncionario", intCodigoFuncionario);
                dbManager.ExecuteReader(CommandType.StoredProcedure, "Portal_Select_FuncionarioBanco");
                while (dbManager.DataReader.Read())
                {
                    FuncionarioBancoDTO funcionariobanco = new FuncionarioBancoDTO();
                    funcionariobanco.CodigoFuncionarioBanco = dbManager.DataReader.SafeGetInt32(0);
                    funcionariobanco.CodigoBanco = dbManager.DataReader.SafeGetInt32(1);
                    funcionariobanco.CodigoFuncionaro = dbManager.DataReader.SafeGetInt32(2);
                    funcionariobanco.NumeroCuentaBancaria = dbManager.DataReader.SafeGetString(3);
                    funcionariobanco.EstadoFuncionarioBanco = dbManager.DataReader.GetBoolean(4);
                    funcionariobanco.TipoCuentaFuncionarioBanco = dbManager.DataReader.SafeGetInt32(5);
                    funcionariobanco.OficinaFuncionarioBanco = dbManager.DataReader.SafeGetString(6);
                    funcionariobanco.CodigoTipoIdentificacion = dbManager.DataReader.SafeGetInt32(7);
                    funcionariobanco.IdentificacionPropietarioCuenta = dbManager.DataReader.SafeGetString(8);
                    funcionariobanco.NombrePropietarioCuenta = dbManager.DataReader.SafeGetString(9);
                    funcionariobanco.EmailPropietarioCuenta = dbManager.DataReader.SafeGetString(10);
                    funcionariobanco.FechaRegistro = dbManager.DataReader.SafeGetDateTime(11);
                    listfuncionariobanco.Add(funcionariobanco);
                }
                return listfuncionariobanco;
            }
            catch (Exception ex)
            {
                //LogManagement.Instance.write(MethodBase.GetCurrentMethod().DeclaringType.Name, MethodBase.GetCurrentMethod().Name, $"Error: {e.Message}", "BUE.Integration.Application.Services");
                GmasLogger.Instance.write("Method: " + MethodBase.GetCurrentMethod().DeclaringType.Name + " Error: " + ex.Message + "Stack Trace: " + ex.StackTrace.ToString(), "BalconGP");
                throw ex;
            }
            finally
            {
                dbManager.Dispose();
            }

        }
        #endregion

        #region ActualizarFuncionarioBanco
        public void ActualizarFuncionarioBanco(FuncionarioBancoDTO funcionariobancoDTO)
        {
            try
            {
                dbManager.Open();
                dbManager.Command.CommandTimeout = 0;
                dbManager.CreateParameters(8);
                dbManager.AddParameters(0, "@CodigoFuncionarioBanco", funcionariobancoDTO.CodigoFuncionarioBanco);
                dbManager.AddParameters(1, "@CodigoBanco", funcionariobancoDTO.CodigoBanco);
                dbManager.AddParameters(2, "@NumeroCuentaBancaria", funcionariobancoDTO.NumeroCuentaBancaria);
                dbManager.AddParameters(3, "@TipoCuentaFuncionarioBanco", funcionariobancoDTO.TipoCuentaFuncionarioBanco);
                dbManager.AddParameters(4, "@CodigoTipoIdentificacion", funcionariobancoDTO.CodigoTipoIdentificacion);
                dbManager.AddParameters(5, "@IdentificacionPropietarioCuenta", funcionariobancoDTO.IdentificacionPropietarioCuenta);
                dbManager.AddParameters(6, "@NombrePropietarioCuenta", funcionariobancoDTO.NombrePropietarioCuenta);
                dbManager.AddParameters(7, "@EstadoFuncionarioBanco", funcionariobancoDTO.EstadoFuncionarioBanco);

                dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "Portal_Update_FuncionarioBanco");
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
        #endregion

        #region InsertarFuncionarioBanco
        public void InsertarFuncionarioBanco(FuncionarioBancoDTO funcionariobancoDTO)
        {
            try
            {
                dbManager.Open();
                dbManager.Command.CommandTimeout = 0;
                dbManager.CreateParameters(7);
                dbManager.AddParameters(0, "@CodigoBanco", funcionariobancoDTO.CodigoBanco);
                dbManager.AddParameters(1, "@CodigoFuncionario", funcionariobancoDTO.CodigoFuncionaro);
                dbManager.AddParameters(2, "@NumeroCuentaBancaria", funcionariobancoDTO.NumeroCuentaBancaria);
                dbManager.AddParameters(3, "@TipoCuentaFuncionarioBanco", funcionariobancoDTO.TipoCuentaFuncionarioBanco);
                dbManager.AddParameters(4, "@CodigoTipoIdentificacion", funcionariobancoDTO.CodigoTipoIdentificacion);
                dbManager.AddParameters(5, "@IdentificacionPropietarioCuenta", funcionariobancoDTO.IdentificacionPropietarioCuenta);
                dbManager.AddParameters(6, "@NombrePropietarioCuenta", funcionariobancoDTO.NombrePropietarioCuenta);

                dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "Portal_Insert_FuncionarioBanco");
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
        #endregion


        #region ObtenerFuncionario(string strIdentificacion)
        public List<FuncionarioDTO> GetByBusiness(int CodigoEmpresa)
        {
            List<FuncionarioDTO> result = new List<FuncionarioDTO>();
            try
            {
                dbManager.Open();
                dbManager.Command.CommandTimeout = 0;
                dbManager.ExecuteReader(CommandType.Text, string.Format("Select CodigoFuncionario, IdentificacionFuncionario, NombreCompleto,CorreoElectronicoFuncionario=isnull(CorreoElectronicoFuncionario,'mtoapaxi@grupomas.com') from Funcionario Where CodigoEmpresa = {0} and CodigoRelacion=1 order by NombreCompleto asc", CodigoEmpresa));
                while (dbManager.DataReader.Read())
                {
                    FuncionarioDTO item = new FuncionarioDTO();
                    item.CodigoFuncionario = dbManager.DataReader.GetInt32(0);
                    item.Identificacion = ((dbManager.DataReader.SafeGetString(1) != null) ? dbManager.DataReader.SafeGetString(1) : "");
                    item.NombreCompleto = ((dbManager.DataReader.SafeGetString(2) != null) ? dbManager.DataReader.SafeGetString(2) : "");
                    item.CorreoElectronicoFuncionario = dbManager.DataReader.GetString(3);
                    result.Add(item);
                }

                return result;
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
        public FuncionarioDTO ObtenerFuncionario(string strIdentificacion)
        {
            FuncionarioDTO funcionario = new FuncionarioDTO();

            try
            {
                dbManager.Open();
                dbManager.Command.CommandTimeout = 0;
                dbManager.CreateParameters(1);
                dbManager.AddParameters(0, "id", strIdentificacion);
                dbManager.ExecuteReader(CommandType.StoredProcedure, "Portal_Select_Funcionarios");
                while (dbManager.DataReader.Read())
                {
                    funcionario.CodigoFuncionario = dbManager.DataReader.SafeGetInt32(0);
                    funcionario.TipoIdentificacion = dbManager.DataReader.SafeGetInt32(1);
                    funcionario.Identificacion = dbManager.DataReader.SafeGetString(2);
                    funcionario.CodigoIESS = dbManager.DataReader.SafeGetString(3);
                    funcionario.PrimerNombre = dbManager.DataReader.SafeGetString(4);
                    funcionario.SegundoNombre = dbManager.DataReader.SafeGetString(5);
                    funcionario.PrimerApellido = dbManager.DataReader.SafeGetString(6);
                    funcionario.SegundoApellido = dbManager.DataReader.SafeGetString(7);

                    funcionario.FechaNacimiento = dbManager.DataReader.SafeGetDateTime(8);
                    funcionario.EstadoCivil = dbManager.DataReader.SafeGetInt32(9);
                    funcionario.Sexo = dbManager.DataReader.SafeGetInt32(10);
                    funcionario.Genero = dbManager.DataReader.SafeGetInt32(11);
                    funcionario.GrupoEtnico = dbManager.DataReader.SafeGetInt32(12);
                    funcionario.Region = dbManager.DataReader.SafeGetInt32(13);
                    funcionario.TipoSangre = dbManager.DataReader.SafeGetInt32(14);

                    funcionario.Nacionalidad = dbManager.DataReader.SafeGetInt32(15);
                    funcionario.PaisNacimiento = dbManager.DataReader.SafeGetString(16);
                    funcionario.Provincia = dbManager.DataReader.SafeGetInt32(17);
                    funcionario.Canton = dbManager.DataReader.SafeGetInt32(18);
                    funcionario.Parroquia = dbManager.DataReader.SafeGetInt32(19);
                    funcionario.CiudadNacimiento = dbManager.DataReader.SafeGetString(20);
                    funcionario.CiudadTrabajo = dbManager.DataReader.SafeGetString(21);
                    funcionario.Imagen = dbManager.DataReader.SafeGetByte(22);
                    //
                    funcionario.DireccionExactaFuncionario = dbManager.DataReader.SafeGetString(23);
                    funcionario.CorreoElectronicoFuncionario = dbManager.DataReader.SafeGetString(24);
                    funcionario.CorreoElectronicoFuncionarioPersonal = dbManager.DataReader.SafeGetString(25);
                    funcionario.TelefonoCasa = dbManager.DataReader.SafeGetString(26);
                    funcionario.TelefonoCelular = dbManager.DataReader.SafeGetString(27);
                    funcionario.TelefonoOficina = dbManager.DataReader.SafeGetString(28);
                    funcionario.TelefonoOficinaExtension = dbManager.DataReader.SafeGetString(29);
                    funcionario.ContactoEmergenciaNombre = dbManager.DataReader.SafeGetString(30);
                    funcionario.ContactoEmergenciaTelefono = dbManager.DataReader.SafeGetString(31);
                    funcionario.NumeroOficina = dbManager.DataReader.SafeGetString(32);
                    funcionario.Contrasena = dbManager.DataReader.SafeGetString(33);
                    funcionario.NombreCompleto = dbManager.DataReader.SafeGetString(34);
                    //
                    funcionario.CodigoEmpresa = dbManager.DataReader.SafeGetInt32(35);
                    funcionario.Empresa = dbManager.DataReader.SafeGetString(36);
                    funcionario.CodigoJefeFuncionario = dbManager.DataReader.SafeGetInt32(37);
                    funcionario.JefeFuncionario = dbManager.DataReader.SafeGetString(38);
                    funcionario.CorreoJefeFuncionario = dbManager.DataReader.SafeGetString(39);
                    funcionario.DiasBaseVacacion = dbManager.DataReader.GetDecimal(40);
                    funcionario.DiasAdicionalesVacacion = dbManager.DataReader.GetDecimal(41);
                    funcionario.DiasExtrasVacacion = dbManager.DataReader.GetDecimal(42);
                    funcionario.DiasUsadosVacacion = dbManager.DataReader.GetDecimal(43);
                    funcionario.DiasLibresVacacion = dbManager.DataReader.GetDecimal(44);
                    funcionario.DiasUsadosExtras = dbManager.DataReader.GetDecimal(45);
                    funcionario.DiasLibresExtras = dbManager.DataReader.GetDecimal(46);
                    funcionario.DiasUsadosAntiguedad = dbManager.DataReader.GetDecimal(47);
                    funcionario.DiasLibresAntiguedad = dbManager.DataReader.GetDecimal(48);
                    //funcionario.listFuncionarioBanco = ObtenerFuncionarioBanco(funcionario.CodigoFuncionario);
                    //funcionario.listFamiliar = ObtenerFamiliar(funcionario.CodigoFuncionario);

                    if (dbManager.DataReader.GetString(49) != string.Empty)
                    {

                        funcionario.Imagen = File.ReadAllBytes(dbManager.DataReader.GetString(49));
                    }

                }
                return funcionario;
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

        #region Obtener Colaboradores
        public List<ColaboradorDTO> ObtenerColaboradores(int CodigoJefe)
        {
            try
            {
                dbManager.Open();
                dbManager.Command.CommandTimeout = 0;
                dbManager.CreateParameters(1);
                dbManager.AddParameters(0, "@CodigoFuncionario", CodigoJefe);
                dbManager.ExecuteReader(CommandType.StoredProcedure, "gmas_sp_PortalConsultarBoletasColaboradores");
                List<ColaboradorDTO> lstColaboradorDTO = new List<ColaboradorDTO>();
                while (dbManager.DataReader.Read())
                {
                    ColaboradorDTO entColaboradorDTO = new ColaboradorDTO();
                    entColaboradorDTO.IdBoletaVacaciones = dbManager.DataReader.SafeGetInt32(0);
                    entColaboradorDTO.CodigoFuncionario = dbManager.DataReader.SafeGetInt32(1);
                    entColaboradorDTO.NombreCompleto = dbManager.DataReader.SafeGetString(2);
                    entColaboradorDTO.CorreoElectronicoFuncionario = dbManager.DataReader.SafeGetString(3);
                    entColaboradorDTO.FechaDesde = dbManager.DataReader.SafeGetString(4);
                    entColaboradorDTO.FechaHasta = dbManager.DataReader.SafeGetString(5);
                    entColaboradorDTO.CantidadDiasVacacion = dbManager.DataReader.SafeGetInt32(6);
                    entColaboradorDTO.NombreConfirmador = dbManager.DataReader.SafeGetString(7);
                    entColaboradorDTO.CorreoConfirmador = dbManager.DataReader.SafeGetString(8);
                    entColaboradorDTO.DiasLibresVacacion = dbManager.DataReader.GetDecimal(9);
                    entColaboradorDTO.DiasLibresAntiguedad = dbManager.DataReader.GetDecimal(10);
                    entColaboradorDTO.DiasLibresExtras = dbManager.DataReader.GetDecimal(11);
                    entColaboradorDTO.TipoVacacion = dbManager.DataReader.SafeGetString(12);
                    lstColaboradorDTO.Add(entColaboradorDTO);
                }
                return lstColaboradorDTO;
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

        public List<FuncionarioDTO> ObtenerEquipo(string IdentificacionJefe)
        {
            try
            {
                dbManager.Open();
                dbManager.CreateParameters(1);
                dbManager.AddParameters(0, "@Identificacion", IdentificacionJefe);
                dbManager.ExecuteReader(CommandType.StoredProcedure, "gmas_ListarEquipoPortal");
                List<FuncionarioDTO> lstFuncionarioDTO = new List<FuncionarioDTO>();
                while (dbManager.DataReader.Read())
                {
                    FuncionarioDTO entColaboradorDTO = new FuncionarioDTO();
                    entColaboradorDTO.Identificacion = dbManager.DataReader.SafeGetString(0);
                    entColaboradorDTO.CodigoFuncionario = dbManager.DataReader.SafeGetInt32(1);
                    entColaboradorDTO.NombreCompleto = dbManager.DataReader.SafeGetString(2);
                    entColaboradorDTO.CorreoElectronicoFuncionario = dbManager.DataReader.SafeGetString(3);
                    entColaboradorDTO.JefeFuncionario = dbManager.DataReader.SafeGetString(4);
                    entColaboradorDTO.CodigoJefeFuncionario = dbManager.DataReader.SafeGetInt32(5);
                    entColaboradorDTO.CorreoJefeFuncionario = dbManager.DataReader.SafeGetString(6);

                    lstFuncionarioDTO.Add(entColaboradorDTO);
                }
                return lstFuncionarioDTO;
            }
            catch (Exception ex)
            {
                GmasLogger.Instance.write("Method: " + MethodBase.GetCurrentMethod().DeclaringType.Name + " Error: " + ex.Message + "Stack Trace: " + ex.StackTrace.ToString(), "BalconGP");
                throw ex;
            }
            finally
            {
                dbManager.Dispose();

            }
        }
        #endregion

        #region ObtenerFuncionario1(int CodigoFuncionario)
        public FuncionarioDTO ObtenerFuncionario1(int CodigoFuncionario)
        {
            FuncionarioDTO funcionario = new FuncionarioDTO();
            try
            {
                dbManager.Open();
                dbManager.Command.CommandTimeout = 0;
                dbManager.CreateParameters(1);
                dbManager.AddParameters(0, "@CodigoFuncionario", CodigoFuncionario);
                dbManager.ExecuteReader(CommandType.StoredProcedure, "Portal_Select_Funcionarios1");
                while (dbManager.DataReader.Read())
                {
                    funcionario.CodigoFuncionario = dbManager.DataReader.SafeGetInt32(0);
                    funcionario.TipoIdentificacion = dbManager.DataReader.SafeGetInt32(1);
                    funcionario.Identificacion = dbManager.DataReader.SafeGetString(2);
                    funcionario.CodigoIESS = dbManager.DataReader.SafeGetString(3);
                    funcionario.PrimerNombre = dbManager.DataReader.SafeGetString(4);
                    funcionario.SegundoNombre = dbManager.DataReader.SafeGetString(5);
                    funcionario.PrimerApellido = dbManager.DataReader.SafeGetString(6);
                    funcionario.SegundoApellido = dbManager.DataReader.SafeGetString(7);

                    funcionario.FechaNacimiento = dbManager.DataReader.SafeGetDateTime(8);
                    funcionario.EstadoCivil = dbManager.DataReader.SafeGetInt32(9);
                    funcionario.Sexo = dbManager.DataReader.SafeGetInt32(10);
                    funcionario.Genero = dbManager.DataReader.SafeGetInt32(11);
                    funcionario.GrupoEtnico = dbManager.DataReader.SafeGetInt32(12);
                    funcionario.Region = dbManager.DataReader.SafeGetInt32(13);
                    funcionario.TipoSangre = dbManager.DataReader.SafeGetInt32(14);

                    funcionario.Nacionalidad = dbManager.DataReader.SafeGetInt32(15);
                    funcionario.PaisNacimiento = dbManager.DataReader.SafeGetString(16);
                    funcionario.Provincia = dbManager.DataReader.SafeGetInt32(17);
                    funcionario.Canton = dbManager.DataReader.SafeGetInt32(18);
                    funcionario.Parroquia = dbManager.DataReader.SafeGetInt32(19);
                    funcionario.CiudadNacimiento = dbManager.DataReader.SafeGetString(20);
                    funcionario.CiudadTrabajo = dbManager.DataReader.SafeGetString(21);
                    funcionario.Imagen = dbManager.DataReader.SafeGetByte(22);
                    //
                    funcionario.DireccionExactaFuncionario = dbManager.DataReader.SafeGetString(23);
                    funcionario.CorreoElectronicoFuncionario = dbManager.DataReader.SafeGetString(24);
                    funcionario.CorreoElectronicoFuncionarioPersonal = dbManager.DataReader.SafeGetString(25);
                    funcionario.TelefonoCasa = dbManager.DataReader.SafeGetString(26);
                    funcionario.TelefonoCelular = dbManager.DataReader.SafeGetString(27);
                    funcionario.TelefonoOficina = dbManager.DataReader.SafeGetString(28);
                    funcionario.TelefonoOficinaExtension = dbManager.DataReader.SafeGetString(29);
                    funcionario.ContactoEmergenciaNombre = dbManager.DataReader.SafeGetString(30);
                    funcionario.ContactoEmergenciaTelefono = dbManager.DataReader.SafeGetString(31);
                    funcionario.NumeroOficina = dbManager.DataReader.SafeGetString(32);
                    funcionario.Contrasena = dbManager.DataReader.SafeGetString(33);
                    funcionario.NombreCompleto = dbManager.DataReader.SafeGetString(34);
                    //
                    funcionario.CodigoEmpresa = dbManager.DataReader.SafeGetInt32(35);
                    funcionario.Empresa = dbManager.DataReader.SafeGetString(36);
                    funcionario.CodigoJefeFuncionario = dbManager.DataReader.SafeGetInt32(37);
                    funcionario.JefeFuncionario = dbManager.DataReader.SafeGetString(38);
                    funcionario.CorreoJefeFuncionario = dbManager.DataReader.SafeGetString(39);
                    funcionario.DiasLibresVacacion = dbManager.DataReader.GetDecimal(40);

                    //funcionario.listFuncionarioBanco = ObtenerFuncionarioBanco(funcionario.CodigoFuncionario);
                }

                return funcionario;
            }
            catch (Exception ex)
            {
                GmasLogger.Instance.write("Method: " + MethodBase.GetCurrentMethod().DeclaringType.Name + " Error: " + ex.Message + "Stack Trace: " + ex.StackTrace.ToString(), "BalconGP");
                throw ex;
            }
            finally
            {
                dbManager.Dispose();
            }
        }
        #endregion

        #region GuardarFuncionario
        public void GuardarFuncionario(FuncionarioDTO funcionario)
        {
            try
            {
                dbManager.Open();
                dbManager.Command.CommandTimeout = 0;
                dbManager.CreateParameters(33);
                dbManager.AddParameters(0, "@TipoIdentificacion", funcionario.TipoIdentificacion);
                dbManager.AddParameters(1, "@Identificacion", funcionario.Identificacion);
                dbManager.AddParameters(2, "@CodigoIESS", funcionario.CodigoIESS);
                dbManager.AddParameters(3, "@PrimerNombre", funcionario.PrimerNombre);
                dbManager.AddParameters(4, "@SegundoNombre", funcionario.SegundoNombre);
                dbManager.AddParameters(5, "@PrimerApellido", funcionario.PrimerApellido);
                dbManager.AddParameters(6, "@SegundoApellido", funcionario.SegundoApellido);
                dbManager.AddParameters(7, "@FechaNacimiento", funcionario.FechaNacimiento);
                dbManager.AddParameters(8, "@EstadoCivil", funcionario.EstadoCivil);
                dbManager.AddParameters(9, "@Sexo", funcionario.Sexo);
                dbManager.AddParameters(10, "@Genero", funcionario.Genero);
                dbManager.AddParameters(11, "@GrupoEtnico", funcionario.GrupoEtnico);
                dbManager.AddParameters(12, "@Region", funcionario.Region);
                dbManager.AddParameters(13, "@TipoSangre", funcionario.TipoSangre);
                dbManager.AddParameters(14, "@Nacionalidad", funcionario.Nacionalidad);
                dbManager.AddParameters(15, "@PaisNacimiento", funcionario.PaisNacimiento);
                dbManager.AddParameters(16, "@Provincia", funcionario.Provincia);
                dbManager.AddParameters(17, "@Canton", funcionario.Canton);
                dbManager.AddParameters(18, "@Parroquia", funcionario.Parroquia);
                dbManager.AddParameters(19, "@CiudadNacimiento", funcionario.CiudadNacimiento);
                dbManager.AddParameters(20, "@CiudadTrabajo", funcionario.CiudadTrabajo);
                dbManager.AddParameters(21, "@FotoFuncionario", funcionario.Imagen);

                dbManager.AddParameters(22, "@DireccionFuncionario", funcionario.DireccionExactaFuncionario);
                dbManager.AddParameters(23, "@CorreoFuncionario", funcionario.CorreoElectronicoFuncionario);
                dbManager.AddParameters(24, "@CorreoFuncionarioPersonal", funcionario.CorreoElectronicoFuncionarioPersonal);
                dbManager.AddParameters(25, "@TelefonoCasa", funcionario.TelefonoCasa);
                dbManager.AddParameters(26, "@TelefonoCelular", funcionario.TelefonoCelular);
                //dbManager.AddParameters(27, "@TelefonoOficina", funcionario.TelefonoOficina);
                //dbManager.AddParameters(28, "@TelefonoOficinaExtension", funcionario.TelefonoOficinaExtension);
                //dbManager.AddParameters(31, "@NumeroOficina", funcionario.NumeroOficina);
                dbManager.AddParameters(27, "@TelefonoOficina", string.Empty);
                dbManager.AddParameters(28, "@TelefonoOficinaExtension", string.Empty);
                dbManager.AddParameters(29, "@ContactoEmergenciaNombre", funcionario.ContactoEmergenciaNombre);
                dbManager.AddParameters(30, "@ContactoEmergenciaTelefono", funcionario.ContactoEmergenciaTelefono);
                dbManager.AddParameters(31, "@NumeroOficina", string.Empty);
                //dbManager.AddParameters(32, "@Contrasena", funcionario.Contrasena);
                dbManager.AddParameters(32, "@Contrasena", string.Empty);

                dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "Portal_Update_Funcionarios");
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
        #endregion

        #region Login
        public LoginDTO Login(string username, string password)
        {
            string res = "";
            LoginDTO loginDTO = new LoginDTO();
            try
            {
                dbManager.Open();
                dbManager.Command.CommandTimeout = 0;
                dbManager.CreateParameters(2);
                dbManager.AddParameters(0, "@username", username);
                dbManager.AddParameters(1, "@password", password);
                dbManager.ExecuteReader(CommandType.StoredProcedure, "Portal_Login");
                while (dbManager.DataReader.Read())
                {
                    loginDTO.RespuestaLogin = dbManager.DataReader.SafeGetString(0);
                    loginDTO.CodigoFuncionario = dbManager.DataReader.SafeGetInt32(1);
                    loginDTO.IdentificacionFuncionario = dbManager.DataReader.SafeGetString(2);
                    loginDTO.UserName = dbManager.DataReader.SafeGetString(3) != null ? dbManager.DataReader.SafeGetString(3) : string.Empty;
                }
                return loginDTO;
            }
            catch (Exception ex)
            {
                GmasLogger.Instance.write("Method: " + MethodBase.GetCurrentMethod().DeclaringType.Name + " Error: " + ex.Message + "Stack Trace: " + ex.StackTrace.ToString(), "BalconGP");
                throw ex;
            }
            finally
            {
                dbManager.Dispose();
            }
        }
        #endregion

        #region UpdatePasswordFuncionario
        public void UpdatePasswordFuncionario(FuncionarioDTO funcionarioDTO)
        {
            try
            {
                dbManager.Open();
                dbManager.Command.CommandTimeout = 0;
                dbManager.CreateParameters(2);
                dbManager.AddParameters(0, "@Identificacion", funcionarioDTO.Identificacion);
                dbManager.AddParameters(1, "@Contrasena", funcionarioDTO.ContrasenaTemp);
                dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "Portal_Update_Password_Funcionario");
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
        #endregion

        #region GuardarPrestamoFuncionario
        public PrestamoDTO GuardarPrestamoFuncionario(PrestamoDTO prestamoDTO)
        {
            try
            {
                dbManager.Open();
                dbManager.Command.CommandTimeout = 0;
                dbManager.CreateParameters(11);
                dbManager.AddParameters(0, "@CodigoEmpresa", prestamoDTO.CodigoEmpresa);
                dbManager.AddParameters(1, "@CodigoFuncionarioAutoriza", prestamoDTO.CodigoFuncionarioAutoriza);
                dbManager.AddParameters(2, "@FechaInicial", prestamoDTO.FechaInicial);
                dbManager.AddParameters(3, "@MontoPrestamo", prestamoDTO.MontoPrestamo);
                dbManager.AddParameters(4, "@NumeroCuotas", prestamoDTO.NumeroCuotas);
                dbManager.AddParameters(5, "@CodigoTipoPrestamo", prestamoDTO.CodigoTipoPrestamo);
                dbManager.AddParameters(6, "@ValorCuota", prestamoDTO.ValorCuota);
                dbManager.AddParameters(7, "@Observaciones", prestamoDTO.Observaciones);
                dbManager.AddParameters(8, "@CodigoFrecuencia", prestamoDTO.CodigoFrecuencia);
                dbManager.AddParameters(9, "@CodigoFuncionario", prestamoDTO.CodigoFuncionario);
                dbManager.AddParameters(10, "@UsuarioAuditoria", prestamoDTO.UsuarioAuditoria);
                dbManager.ExecuteReader(CommandType.StoredProcedure, "gmas_Portal_Insert_PrestamoFuncionario");
                while (dbManager.DataReader.Read())
                {
                    prestamoDTO.CodigoPrestamo = dbManager.DataReader.SafeGetInt32(0);
                    prestamoDTO.MsgValidaSueldo = dbManager.DataReader.SafeGetString(1);
                }
                return prestamoDTO;
            }
            catch (Exception ex)
            {
                GmasLogger.Instance.write("Method: " + MethodBase.GetCurrentMethod().DeclaringType.Name + " Error: " + ex.Message + "Stack Trace: " + ex.StackTrace.ToString(), "BalconGP");
                throw ex;
            }
            finally
            {
                dbManager.Dispose();
            }
        }
        #endregion

        #region ActualizarEstadoPrestamo
        public PrestamoDTO ActualizarEstadoPrestamo(PrestamoDTO prestamoDTO)
        {
            string content = JsonConvert.SerializeObject(prestamoDTO);
            try
            {
                dbManager.Open();
                dbManager.Command.CommandTimeout = 0;
                dbManager.CreateParameters(2);
                dbManager.AddParameters(0, "@CodigoPrestamo", prestamoDTO.CodigoPrestamo);
                dbManager.AddParameters(1, "@Estado", prestamoDTO.EstadoPrestamo);
                dbManager.ExecuteReader(CommandType.StoredProcedure, "Portal_Update_EstadoPrestamo");
                while (dbManager.DataReader.Read())
                {
                    prestamoDTO.Msg = dbManager.DataReader.SafeGetString(0);
                }
                return prestamoDTO;
            }
            catch (Exception ex)
            {
                GmasLogger.Instance.write("Method: " + MethodBase.GetCurrentMethod().DeclaringType.Name + " Error: " + ex.Message + "DATOS: " + content, "BalconGP");
                throw ex;
            }
            finally
            {
                dbManager.Dispose();
            }
        }

        public int ActualizarEstadoConfirmarPrestamo(int CodigoPrestamo, string Estado)
        {
            try
            {
                dbManager.Open();
                dbManager.ExecuteNonQuery(CommandType.Text, string.Format("update Prestamo set EstadoPrestamo=case when '{0}'='S' then 1 else 0 end,EsConfirmadoPrestamo=case when '{1}'='S' then 1 else 0 end where CodigoPrestamo={2} ", Estado, Estado, CodigoPrestamo));

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
        public int EliminarPrestamo(int CodigoPrestamo)
        {
            try
            {
                dbManager.Open();
                dbManager.ExecuteNonQuery(CommandType.Text, string.Format("delete Amortizacion where CodigoPrestamo={0} delete Prestamo where CodigoPrestamo={1} ", CodigoPrestamo, CodigoPrestamo));

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
        #endregion

        #region ValidaSueldoPrestamoFuncionario
        public PrestamoDTO ValidaSueldoPrestamoFuncionario(PrestamoDTO prestamoDTO)
        {
            string Estado = string.Empty;
            try
            {
                dbManager.Open();
                dbManager.Command.CommandTimeout = 0;
                dbManager.CreateParameters(3);
                dbManager.AddParameters(0, "@ValorCuota", prestamoDTO.ValorCuota);
                dbManager.AddParameters(1, "@CodigoFrecuencia", prestamoDTO.CodigoFrecuencia);
                dbManager.AddParameters(2, "@CodigoFuncionario", prestamoDTO.CodigoFuncionario);
                dbManager.ExecuteReader(CommandType.StoredProcedure, "Portal_ValidaSueldo_PrestamoFuncionario");
                while (dbManager.DataReader.Read())
                {
                    prestamoDTO.MsgValidaSueldo = dbManager.DataReader.SafeGetString(0);
                }
                return prestamoDTO;
            }
            catch (Exception ex)
            {
                GmasLogger.Instance.write("Method: " + MethodBase.GetCurrentMethod().DeclaringType.Name + " Error: " + ex.Message + "Stack Trace: " + ex.StackTrace.ToString(), "BalconGP"); ;
                throw ex;
            }
            finally
            {
                dbManager.Dispose();
            }
        }
        #endregion

        #region ObtenerInfoBienvenida
        public BienvenidosDTO ObtenerInfoBienvenida(int CodigoFuncionario)
        {
            BienvenidosDTO bienvenidosDTO = new BienvenidosDTO();
            try
            {
                dbManager.Open();
                dbManager.Command.CommandTimeout = 0;
                dbManager.CreateParameters(1);
                dbManager.AddParameters(0, "@CodigoFuncionario", CodigoFuncionario);
                dbManager.ExecuteReader(CommandType.StoredProcedure, "Portal_Info_Bienvenida_Funcionario");
                while (dbManager.DataReader.Read())
                {
                    bienvenidosDTO.Prestamos = dbManager.DataReader.SafeGetString(0);
                    bienvenidosDTO.Cumpleanios = dbManager.DataReader.SafeGetString(1);
                    bienvenidosDTO.Antiguedad = dbManager.DataReader.SafeGetString(2);
                }
                return bienvenidosDTO;
            }
            catch (Exception ex)
            {
                GmasLogger.Instance.write("Method: " + MethodBase.GetCurrentMethod().DeclaringType.Name + " Error: " + ex.Message + "Stack Trace: " + ex.StackTrace.ToString(), "BalconGP");
                throw ex;
            }
            finally
            {
                dbManager.Dispose();
            }
        }
        #endregion

        #region ObtenerPrestamoFuncionario
        public PrestamoDTO ObtenerPrestamoFuncionario(int CodigoPrestamo)
        {
            PrestamoDTO prestamoDTO = new PrestamoDTO();
            try
            {
                dbManager.Open();
                dbManager.Command.CommandTimeout = 0;
                dbManager.CreateParameters(1);
                dbManager.AddParameters(0, "@CodigoPrestamo", CodigoPrestamo);
                dbManager.ExecuteReader(CommandType.StoredProcedure, "Portal_Select_PrestamoFuncionario");
                while (dbManager.DataReader.Read())
                {
                    prestamoDTO.CodigoPrestamo = dbManager.DataReader.SafeGetInt32(0);
                    prestamoDTO.CodigoEmpresa = dbManager.DataReader.SafeGetInt32(1);
                    prestamoDTO.NombreEmpresa = dbManager.DataReader.SafeGetString(2);
                    prestamoDTO.CodigoFuncionarioAutoriza = dbManager.DataReader.SafeGetInt32(3);
                    prestamoDTO.JefeFuncionarioAutoriza = dbManager.DataReader.SafeGetString(4);
                    prestamoDTO.FechaInicial = dbManager.DataReader.SafeGetDateTime(5);
                    prestamoDTO.MontoPrestamo = dbManager.DataReader.GetDecimal(6);
                    prestamoDTO.NumeroCuotas = dbManager.DataReader.SafeGetInt32(7);
                    prestamoDTO.CodigoTipoPrestamo = dbManager.DataReader.SafeGetInt32(8);
                    prestamoDTO.NombreTipoPrestamo = dbManager.DataReader.SafeGetString(9);
                    prestamoDTO.ValorCuota = dbManager.DataReader.GetDecimal(10);
                    prestamoDTO.Observaciones = dbManager.DataReader.SafeGetString(11);
                    prestamoDTO.CodigoFrecuencia = dbManager.DataReader.SafeGetInt32(12);
                    prestamoDTO.NombreFrecuencia = dbManager.DataReader.SafeGetString(13);
                    prestamoDTO.CodigoFuncionario = dbManager.DataReader.SafeGetInt32(14);
                    prestamoDTO.NombreFuncionario = dbManager.DataReader.SafeGetString(15);
                    prestamoDTO.CorreoFuncionario = dbManager.DataReader.SafeGetString(16);
                    prestamoDTO.EstadoPrestamo = dbManager.DataReader.SafeGetString(17);
                    prestamoDTO.CorreoJefe = dbManager.DataReader.SafeGetString(18);
                }
                return prestamoDTO;
            }
            catch (Exception ex)
            {
                GmasLogger.Instance.write("Method: " + MethodBase.GetCurrentMethod().DeclaringType.Name + " Error: " + ex.Message + "Stack Trace: " + ex.StackTrace.ToString(), "BalconGP");
                throw ex;
            }
            finally
            {
                dbManager.Dispose();
            }
        }
        #endregion

        #region ValidaAnticipoFuncionario
        public PrestamoDTO ValidaAnticipoFuncionario(PrestamoDTO prestamoDTO)
        {
            string Estado = string.Empty;
            try
            {
                dbManager.Open();
                dbManager.Command.CommandTimeout = 0;
                dbManager.CreateParameters(2);
                dbManager.AddParameters(0, "@ValorCuota", prestamoDTO.ValorCuota);
                dbManager.AddParameters(1, "@CodigoFuncionario", prestamoDTO.CodigoFuncionario);
                dbManager.ExecuteReader(CommandType.StoredProcedure, "Portal_Valida_Anticipo_Funcionario");
                while (dbManager.DataReader.Read())
                {
                    prestamoDTO.Msg = dbManager.DataReader.SafeGetString(0);
                }
                return prestamoDTO;
            }
            catch (Exception ex)
            {
                GmasLogger.Instance.write("Method: " + MethodBase.GetCurrentMethod().DeclaringType.Name + " Error: " + ex.Message + "Stack Trace: " + ex.StackTrace.ToString(), "BalconGP");
                throw ex;
            }
            finally
            {
                dbManager.Dispose();
            }
        }
        #endregion

        #region GuardarAnticipoFuncionario
        public PrestamoDTO GuardarAnticipoFuncionario(PrestamoDTO prestamoDTO)
        {
            try
            {
                dbManager.Open();
                dbManager.Command.CommandTimeout = 0;
                dbManager.CreateParameters(8);
                dbManager.AddParameters(0, "@CodigoEmpresa", prestamoDTO.CodigoEmpresa);
                dbManager.AddParameters(1, "@CodigoFuncionarioAutoriza", prestamoDTO.CodigoFuncionarioAutoriza);
                dbManager.AddParameters(2, "@FechaInicial", prestamoDTO.FechaInicial);
                dbManager.AddParameters(3, "@MontoPrestamo", prestamoDTO.MontoPrestamo);
                dbManager.AddParameters(4, "@NumeroCuotas", prestamoDTO.NumeroCuotas);
                dbManager.AddParameters(5, "@ValorCuota", prestamoDTO.ValorCuota);
                dbManager.AddParameters(6, "@Observaciones", prestamoDTO.Observaciones);
                dbManager.AddParameters(7, "@CodigoFuncionario", prestamoDTO.CodigoFuncionario);
                dbManager.ExecuteReader(CommandType.StoredProcedure, "Portal_Insert_Anticipo_Funcionario");
                while (dbManager.DataReader.Read())
                {
                    prestamoDTO.CodigoPrestamo = dbManager.DataReader.SafeGetInt32(0);
                    prestamoDTO.Msg = dbManager.DataReader.SafeGetString(1);
                }
                return prestamoDTO;
            }
            catch (Exception ex)
            {
                GmasLogger.Instance.write("Method: " + MethodBase.GetCurrentMethod().DeclaringType.Name + " Error: " + ex.Message + "Stack Trace: " + ex.StackTrace.ToString(), "BalconGP");
                throw ex;
            }
            finally
            {
                dbManager.Dispose();
            }
        }
        #endregion

        #region EsPrimerIngreso
        public bool EsPrimerIngreso(string CodigoFuncionario)
        {
            bool result = true;
            try
            {
                dbManager.Open();
                dbManager.Command.CommandTimeout = 0;
                dbManager.ExecuteReader(CommandType.Text, "select EsPrimerIngreso=isnull(EsPrimerIngreso,1) from funcionario where CodigoFuncionario = '" + CodigoFuncionario + "'");
                while (dbManager.DataReader.Read())
                {
                    result = dbManager.DataReader.GetBoolean(0);
                }
                return result;
            }
            catch (Exception ex)
            {
                GmasLogger.Instance.write("Method: " + MethodBase.GetCurrentMethod().DeclaringType.Name + " Error: " + ex.Message + "Stack Trace: " + ex.StackTrace.ToString(), "BalconGP");
                throw ex;
            }
            finally
            {
                dbManager.Dispose();
            }
        }


        public void ActualizarPasswordPrimerIngreso(string CodigoFuncionario, string Clave)
        {
            try
            {
                dbManager.Open();
                dbManager.Command.CommandTimeout = 0;
                dbManager.ExecuteNonQuery(CommandType.Text, String.Format("Update Funcionario set Constrasena = '{0}', EsPrimerIngreso = 0 where CodigoFuncionario = {1} ", Clave, CodigoFuncionario));
            }
            catch (Exception ex)
            {
                GmasLogger.Instance.write("Method: " + MethodBase.GetCurrentMethod().DeclaringType.Name + " Error: " + ex.Message + "Stack Trace: " + ex.StackTrace.ToString(), "BalconGP");
                throw ex;
            }
            finally
            {
                dbManager.Dispose();
            }
        }
        #endregion

        #region Catalogos
        public List<PeriodoPagoDTO> ListarPeriodoPago(int CodigoPeriodoFiscal)
        {
            try
            {
                dbManager.Open();
                dbManager.Command.CommandTimeout = 0;
                dbManager.CreateParameters(1);
                dbManager.AddParameters(0, "@CodigoPeriodoFiscal", CodigoPeriodoFiscal);
                dbManager.ExecuteReader(CommandType.StoredProcedure, "gmas_rptConsultarPeriodoPagoCerrado");
                List<PeriodoPagoDTO> lstPeriodoPagoDTO = new List<PeriodoPagoDTO>();
                while (dbManager.DataReader.Read())
                {
                    PeriodoPagoDTO entPeriodoPagoDTO = new PeriodoPagoDTO();
                    entPeriodoPagoDTO.CodigoPeriodoPago = dbManager.DataReader.SafeGetInt32(0);
                    entPeriodoPagoDTO.NombrePeriodoPago = dbManager.DataReader.SafeGetString(1);
                    entPeriodoPagoDTO.CodigoMes = dbManager.DataReader.SafeGetInt32(2);
                    lstPeriodoPagoDTO.Add(entPeriodoPagoDTO);
                }
                return lstPeriodoPagoDTO;
            }
            catch (Exception ex)
            {

                GmasLogger.Instance.write("Method: " + MethodBase.GetCurrentMethod().DeclaringType.Name + " Error: " + ex.Message + "Stack Trace: " + ex.StackTrace.ToString(), "BalconGP");
                throw ex;
            }
            finally
            {
                dbManager.Dispose();
            }
        }

        public List<PeriodoPagoDTO> ObtenerPeriodoPago(int CodigoFuncionario, int DiaCorte)
        {
            try
            {
                dbManager.Open();
                dbManager.ExecuteReader(CommandType.Text, String.Format("SET LANGUAGE US_ENGLISH EXEC gmas_ObtenerPeriodoPagoBoletaVacacion {0},{1}", CodigoFuncionario, DiaCorte));
                List<PeriodoPagoDTO> lstPeriodoPagoDTO = new List<PeriodoPagoDTO>();
                while (dbManager.DataReader.Read())
                {
                    PeriodoPagoDTO entPeriodoPagoDTO = new PeriodoPagoDTO();
                    entPeriodoPagoDTO.CodigoPeriodoPago = dbManager.DataReader.SafeGetInt32(0);
                    entPeriodoPagoDTO.NombrePeriodoPago = dbManager.DataReader.SafeGetString(1);
                    entPeriodoPagoDTO.CodigoMes = dbManager.DataReader.SafeGetInt32(2);
                    lstPeriodoPagoDTO.Add(entPeriodoPagoDTO);
                }
                return lstPeriodoPagoDTO;
            }
            catch (Exception ex)
            {
                GmasLogger.Instance.write("Method: " + MethodBase.GetCurrentMethod().DeclaringType.Name + " Error: " + ex.Message + "Stack Trace: " + ex.StackTrace.ToString(), "BalconGP");
                throw ex;
            }
            finally
            {
                dbManager.Dispose();
            }
        }

        public List<PeriodoFiscalDTO> ObtenerPeriodoFiscal()
        {
            try
            {
                dbManager.Open();
                dbManager.ExecuteReader(CommandType.Text, "select top 1 CodigoPeriodoFiscal,NombrePeriodoFiscal from PeriodoFiscal order by 2 desc");
                List<PeriodoFiscalDTO> lstPeriodoFiscalDTO = new List<PeriodoFiscalDTO>();
                while (dbManager.DataReader.Read())
                {
                    PeriodoFiscalDTO entPeriodoFiscalDTO = new PeriodoFiscalDTO();
                    entPeriodoFiscalDTO.CodigoPeriodoFiscal = dbManager.DataReader.SafeGetInt32(0);
                    entPeriodoFiscalDTO.NombrePeriodoFiscal = dbManager.DataReader.SafeGetString(1);

                    lstPeriodoFiscalDTO.Add(entPeriodoFiscalDTO);
                }
                return lstPeriodoFiscalDTO;
            }
            catch (Exception ex)
            {
                GmasLogger.Instance.write("Method: " + MethodBase.GetCurrentMethod().DeclaringType.Name + " Error: " + ex.Message + "Stack Trace: " + ex.StackTrace.ToString(), "BalconGP");
                throw ex;
            }
            finally
            {
                dbManager.Dispose();
            }
        }
        public List<PeriodoPagoDTO> ListarPeriodoPagoAperturaPlanilla(int CodigoPeriodoFiscal)
        {
            List<PeriodoPagoDTO> lista = new List<PeriodoPagoDTO>();
            try
            {
                dbManager.Open();
                dbManager.Command.CommandTimeout = 0;
                dbManager.ExecuteReader(CommandType.Text, string.Format("SET LANGUAGE US_ENGLISH;  select pp.CodigoPeriodoPago,pp.NombrePeriodoPago,pp.CodigoMes from PeriodoPago pp where pp.CodigoFrecuencia=1 and pp.CodigoPeriodoPago not in( select CodigoPeriodo from AperturaPlanilla where CodigoEjercicio={0} and EstadoApertura='C')", CodigoPeriodoFiscal));
                while (dbManager.DataReader.Read())
                {
                    PeriodoPagoDTO item = new PeriodoPagoDTO() { CodigoPeriodoPago = dbManager.DataReader.GetInt32(0), NombrePeriodoPago = dbManager.DataReader.SafeGetString(1) };
                    lista.Add(item);
                }
                return lista;
            }
            catch (Exception ex)
            {
                GmasLogger.Instance.write("Method: " + MethodBase.GetCurrentMethod().DeclaringType.Name + " Error: " + ex.Message + "Stack Trace: " + ex.StackTrace.ToString(), "BalconGP");
                throw ex;
            }
            finally
            {
                dbManager.Dispose();
            }
        }
        #endregion

        #region Vacaciones
        /// <summary>
        /// Permitirá ver si no existen boletas de vacaciones 
        /// </summary>
        /// <returns></returns>
        public bool ValidarFechasVacacion(int CodigoFuncionario, DateTime FechaInicio, DateTime FechaFin)
        {
            try
            {
                dbManager.Open();
                dbManager.Command.CommandTimeout = 0;
                dbManager.ExecuteReader(CommandType.Text, string.Format("SET LANGUAGE US_ENGLISH;  select IdBoletaVacaciones from BoletaVacaciones where codigofuncionario={0} and '{1}' between FechaInicioVacacion and FechaFinVacacion  ", CodigoFuncionario, Convert.ToDateTime(FechaInicio).ToString("yyyy-MM-dd")));
                int existe = 0;
                while (dbManager.DataReader.Read())
                {
                    existe = dbManager.DataReader.GetInt32(0);
                }
                if (existe == 0)
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                GmasLogger.Instance.write("Method: " + MethodBase.GetCurrentMethod().DeclaringType.Name + " Error: " + ex.Message + "Stack Trace: " + ex.StackTrace.ToString(), "BalconGP");
                return false;
            }
            finally
            {
                dbManager.Dispose();
            }
        }

        public int ValidarFuncionario(string identificacion)
        {
            int result = 0;
            try
            {
                dbManager.Open();
                dbManager.Command.CommandTimeout = 0;
                dbManager.ExecuteReader(CommandType.Text, "SET LANGUAGE US_ENGLISH;  Select CodigoFuncionario from Funcionario Where CodigoRelacion=1 and IdentificacionFuncionario = '" + identificacion.Trim() + "'");
                while (dbManager.DataReader.Read())
                {
                    result = dbManager.DataReader.GetInt32(0);
                }

                return result;
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

        public int ActualizarBoletaVacacion(BoletaVacacionDTO entBoletaVacacion)
        {
            try
            {
                //dbManager.Open();
                //dbManager.ExecuteReader(CommandType.Text, string.Format("select IdBoletaVacaciones from BoletaVacaciones where codigofuncionario={0} and FechaInicioVacacion ='{1}' and FechaFinVacacion='{2}'", entBoletaVacacion.CodigoFuncionario, Convert.ToDateTime(entBoletaVacacion.FechaInicioVacacion).ToString("yyyy-MM-dd"), Convert.ToDateTime(entBoletaVacacion.FechaFinVacacion).ToString("yyyy-MM-dd")));
                int IdBoletaVacaciones = 0;
                //while (dbManager.DataReader.Read())
                //{
                //    IdBoletaVacaciones = dbManager.DataReader.GetInt32(0);
                //}
                if (IdBoletaVacaciones == 0)
                {
                    dbManager.Open();
                    if (entBoletaVacacion.CodigoPeriodoVacacionesFuncionario > 0)
                    {

                        dbManager.ExecuteReader(CommandType.Text, string.Format("set language us_english  INSERT INTO BoletaVacaciones(CodigoFuncionario,FechaBoletaVacaciones,EsPagadaBoletaVacaciones,ObservacionesBoletaVacaciones,CodigoFuncionarioResponsable,EstadoBoletaVacaciones,EsVacacionMasiva,EsConfirmadaBoletaVacaciones,EsAutorizadaBoletaVacacion,FechaInicioVacacion,FechaFinVacacion,CantidadDiasVacacion,EsExtra,EsVacacion,EsAntiguedad,EsEnviadoMail,EsEnviadoMailConfirmacion,CodigoPeriodoVacacionesFuncionario,Periodo, DiasPagados, DiasGozados,UsuarioAuditoria,FechaAuditoria,CodigoPeriodoPago,CodigoPeriodoFiscal) VALUES ({0},'{1}',{2},'{3}',{4},{5},{6},'{7}','{8}','{9}','{10}',{11},{12},{13},{14},{15},{16},{17},'{18}',{19},{20},'{21}',getdate(),{22},{23}) declare @CodigoBoleta int=0 SELECT TOP 1 @CodigoBoleta = SCOPE_IDENTITY() FROM BoletaVacaciones select @CodigoBoleta as CodigoBoleta", entBoletaVacacion.CodigoFuncionario, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), entBoletaVacacion.EsPagadaBoletaVacaciones == true ? 1 : 0, entBoletaVacacion.ObservacionesBoletaVacaciones, entBoletaVacacion.CodigoFuncionarioResponsable, 0, 0, 'N', 'N', Convert.ToDateTime(entBoletaVacacion.FechaInicioVacacion).ToString("yyyy-MM-dd"), Convert.ToDateTime(entBoletaVacacion.FechaFinVacacion).ToString("yyyy-MM-dd"), entBoletaVacacion.CantidadDiasVacacion, entBoletaVacacion.EsExtra == true ? 1 : 0, entBoletaVacacion.EsVacacion == true ? 1 : 0, entBoletaVacacion.EsAntiguedad == true ? 1 : 0, 0, 0, entBoletaVacacion.CodigoPeriodoVacacionesFuncionario, entBoletaVacacion.Periodo, entBoletaVacacion.DiasPagados, entBoletaVacacion.DiasGozados, entBoletaVacacion.UsuarioAuditoria, entBoletaVacacion.CodigoPeriodoPago, entBoletaVacacion.CodigoPeriodoFiscal));
                    }
                    else
                    {
                        dbManager.ExecuteReader(CommandType.Text, string.Format("set language us_english  INSERT INTO BoletaVacaciones(CodigoFuncionario,FechaBoletaVacaciones,EsPagadaBoletaVacaciones,ObservacionesBoletaVacaciones,CodigoFuncionarioResponsable,EstadoBoletaVacaciones,EsVacacionMasiva,EsConfirmadaBoletaVacaciones,EsAutorizadaBoletaVacacion,FechaInicioVacacion,FechaFinVacacion,CantidadDiasVacacion,EsExtra,EsVacacion,EsAntiguedad,EsEnviadoMail,EsEnviadoMailConfirmacion,Periodo, DiasPagados, DiasGozados,UsuarioAuditoria,FechaAuditoria,CodigoPeriodoPago,CodigoPeriodoFiscal) VALUES ({0},'{1}',{2},'{3}',{4},{5},{6},'{7}','{8}','{9}','{10}',{11},{12},{13},{14},{15},{16},'{17}',{18},{19},'{20}',getdate(),{21},{22}) declare @CodigoBoleta int=0 SELECT TOP 1 @CodigoBoleta = SCOPE_IDENTITY() FROM BoletaVacaciones select @CodigoBoleta as CodigoBoleta", entBoletaVacacion.CodigoFuncionario, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), entBoletaVacacion.EsPagadaBoletaVacaciones == true ? 1 : 0, entBoletaVacacion.ObservacionesBoletaVacaciones, entBoletaVacacion.CodigoFuncionarioResponsable, 0, 0, 'N', 'N', Convert.ToDateTime(entBoletaVacacion.FechaInicioVacacion).ToString("yyyy-MM-dd"), Convert.ToDateTime(entBoletaVacacion.FechaFinVacacion).ToString("yyyy-MM-dd"), entBoletaVacacion.CantidadDiasVacacion, entBoletaVacacion.EsExtra == true ? 1 : 0, entBoletaVacacion.EsVacacion == true ? 1 : 0, entBoletaVacacion.EsAntiguedad == true ? 1 : 0, 0, 0, entBoletaVacacion.Periodo, entBoletaVacacion.DiasPagados, entBoletaVacacion.DiasGozados, entBoletaVacacion.UsuarioAuditoria, entBoletaVacacion.CodigoPeriodoPago, entBoletaVacacion.CodigoPeriodoFiscal));
                    }
                    while (dbManager.DataReader.Read())
                    {
                        IdBoletaVacaciones = dbManager.DataReader.GetInt32(0);
                    }
                }
                else
                {
                    dbManager.Open();
                    dbManager.ExecuteReader(CommandType.Text, string.Format("set language us_english  update BoletaVacaciones set ObservacionesBoletaVacaciones='{0}',FechaInicioVacacion='{1}',FechaFinVacacion='{2}',CantidadDiasVacacion={3} where CodigoFuncionario={4} and IdBoletaVacaciones={5}", entBoletaVacacion.ObservacionesBoletaVacaciones, Convert.ToDateTime(entBoletaVacacion.FechaInicioVacacion).ToString("yyyy-MM-dd"), Convert.ToDateTime(entBoletaVacacion.FechaFinVacacion).ToString("yyyy-MM-dd"), entBoletaVacacion.CantidadDiasVacacion, entBoletaVacacion.CodigoFuncionario, IdBoletaVacaciones));
                }

                return IdBoletaVacaciones;
            }
            catch (Exception ex)
            {
                GmasLogger.Instance.write("Method: " + MethodBase.GetCurrentMethod().DeclaringType.Name + " Error: " + ex.Message + "Stack Trace: " + ex.StackTrace.ToString(), "BalconGP");
                return -1;
            }
            finally
            {
                dbManager.Dispose();
            }
        }

        public int ActualizarEstadoBoletaVacacion(int CodigoBoleta, string AutorizadoBoleta, int Flujo)
        {
            try
            {
                dbManager.Open();
                dbManager.ExecuteReader(CommandType.Text, string.Format("SET LANGUAGE US_ENGLISH;  select IdBoletaVacaciones from BoletaVacaciones where IdBoletaVacaciones={0} ", CodigoBoleta));
                int IdBoletaVacaciones = 0;
                while (dbManager.DataReader.Read())
                {
                    IdBoletaVacaciones = dbManager.DataReader.GetInt32(0);
                }
                if (IdBoletaVacaciones != 0)
                {
                    dbManager.Open();
                    dbManager.ExecuteReader(CommandType.Text, string.Format("SET LANGUAGE US_ENGLISH;  update BoletaVacaciones set EsAutorizadaBoletaVacacion='{0}',EsConfirmadaBoletaVacaciones='{1}',EstadoBoletaVacaciones={2},EsEnviadoMail ={3},EsEnviadoMailConfirmacion ={4} where IdBoletaVacaciones={5}", AutorizadoBoleta, AutorizadoBoleta, AutorizadoBoleta == "S" ? 1 : 0, 1, 1, IdBoletaVacaciones));
                    if (Flujo == 1 && AutorizadoBoleta == "S")
                    {
                        dbManager.Open();
                        dbManager.ExecuteReader(CommandType.Text, string.Format("SET LANGUAGE US_ENGLISH;  exec gmas_SPBoletaVacacionActualizarPeriodoVacacion {0}", IdBoletaVacaciones));
                    }
                }

                return IdBoletaVacaciones;
            }
            catch (Exception ex)
            {
                GmasLogger.Instance.write("Method: " + MethodBase.GetCurrentMethod().DeclaringType.Name + " Error: " + ex.Message + "Stack Trace: " + ex.StackTrace.ToString(), "BalconGP");
                return -1;
            }
            finally
            {
                dbManager.Dispose();
            }
        }

        public int EliminarBoletaVacacion(int Codigo)
        {
            try
            {
                dbManager.Open();
                dbManager.ExecuteNonQuery(CommandType.Text, string.Format("SET LANGUAGE US_ENGLISH;  delete BoletaVacaciones where IdBoletaVacaciones={0}", Codigo));

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

        public int ActualizarEstadoBoletaVacacionConfirmacion(int CodigoBoleta, string AutorizadoBoleta, int Opcion, string Observacion, string Usuario)
        {
            try
            {
                dbManager.Open();
                dbManager.ExecuteReader(CommandType.Text, string.Format("select IdBoletaVacaciones, DiasPagados, DiasGozados from BoletaVacaciones where IdBoletaVacaciones={0} ", CodigoBoleta));
                int IdBoletaVacaciones = 0;
                int diasPagados = 0;
                int diasGozados = 0;
                while (dbManager.DataReader.Read())
                {
                    IdBoletaVacaciones = dbManager.DataReader.GetInt32(0);
                    diasPagados = dbManager.DataReader.GetInt32(1);
                    diasGozados = dbManager.DataReader.GetInt32(2);
                }
                if (IdBoletaVacaciones != 0)
                {
                    dbManager.Open();
                    if (Opcion == 2)
                    {
                        dbManager.ExecuteReader(CommandType.Text, string.Format("SET LANGUAGE US_ENGLISH; update BoletaVacaciones set EstadoBoletaVacaciones={0} ,EsEnviadoMailConfirmacion ={1},ObservacionConfirmacion ='{2}',FechaConfirmacion=getdate(),UsuarioConfirmacion='{3}' where IdBoletaVacaciones={4}", AutorizadoBoleta == "S" ? 1 : 0, 1, Observacion, Usuario, IdBoletaVacaciones));

                    }
                    else
                    {
                        dbManager.ExecuteReader(CommandType.Text, string.Format("SET LANGUAGE US_ENGLISH; update BoletaVacaciones set EsAutorizadaBoletaVacacion='{0}',EsEnviadoMail={1},FechaAutorizacion=getdate() where IdBoletaVacaciones={2}", AutorizadoBoleta, 1, IdBoletaVacaciones));
                    }
                }

                return IdBoletaVacaciones;
            }
            catch (Exception ex)
            {
                GmasLogger.Instance.write("Method: " + MethodBase.GetCurrentMethod().DeclaringType.Name + " Error: " + ex.Message + "Stack Trace: " + ex.StackTrace.ToString(), "BalconGP");
                return -1;
            }
            finally
            {
                dbManager.Dispose();
            }
        }

        public BoletaVacacionDTO ConsultarBoletaVacacion(int CodigoBoleta)
        {
            try
            {
                BoletaVacacionDTO entBoleta = new BoletaVacacionDTO();
                dbManager.Open();
                dbManager.ExecuteReader(CommandType.Text, string.Format("SET LANGUAGE US_ENGLISH;  SELECT IdBoletaVacaciones,b.CodigoFuncionario,FechaBoletaVacaciones,EsPagadaBoletaVacaciones,ObservacionesBoletaVacaciones,CodigoFuncionarioResponsable,EstadoBoletaVacaciones,EsVacacionMasiva,EsConfirmadaBoletaVacaciones,EsAutorizadaBoletaVacacion,FechaInicioVacacion,FechaFinVacacion,CantidadDiasVacacion,CodigoPeriodoVacacionesFuncionario=isnull(CodigoPeriodoVacacionesFuncionario,0),NombreCompleto=(case when f.EsAgricola=0 then f.NombreCompleto else '(AGRICOLA) '+ f.NombreCompleto END),f.IdentificacionFuncionario,isnull(f.CorreoElectronicoFuncionario,f.CorreoElectronicoFuncionarioPersonal),NombreJefe=j.NombreCompleto,CorreoJefe=j.CorreoElectronicoFuncionario,EsExtra=isnull(b.EsExtra,0),CodigoJefe=j.CodigoFuncionario,EsEnviadoMail =ISNULL(EsEnviadoMail ,0),EsVacacion=isnull(EsVacacion,0),EsAntiguedad=isnull(EsAntiguedad,0),EsEnviadoMailConfirmacion  =ISNULL(EsEnviadoMailConfirmacion  ,0),b.Periodo,isnull(b.DiasPagados,0),isnull(b.DiasGozados,0),isnull(CodigoPeriodoExtra,0),isnull(b.FechaAutorizacion,getdate()) FROM BoletaVacaciones b join Funcionario f on b.CodigoFuncionario=f.CodigoFuncionario join Funcionario j on j.CodigoFuncionario=f.CodigoJefeFuncionario join Empresa e on e.codigoempresa=f.codigoempresa where IdBoletaVacaciones={0}", CodigoBoleta));

                while (dbManager.DataReader.Read())
                {
                    entBoleta.IdBoletaVacaciones = dbManager.DataReader.GetInt32(0);
                    entBoleta.CodigoFuncionario = dbManager.DataReader.GetInt32(1);
                    entBoleta.FechaBoletaVacaciones = dbManager.DataReader.GetDateTime(2);
                    entBoleta.ObservacionesBoletaVacaciones = dbManager.DataReader.SafeGetString(4);
                    entBoleta.FechaInicioVacacion = dbManager.DataReader.GetDateTime(10);
                    entBoleta.FechaFinVacacion = dbManager.DataReader.GetDateTime(11);
                    entBoleta.CantidadDiasVacacion = dbManager.DataReader.GetInt32(12);
                    entBoleta.CodigoPeriodoVacacionesFuncionario = dbManager.DataReader.GetInt32(13);
                    entBoleta.NombreEmpleado = dbManager.DataReader.SafeGetString(14);
                    entBoleta.IdentificacionEmpleado = dbManager.DataReader.SafeGetString(15);
                    entBoleta.CorreoElectronicoFuncionario = dbManager.DataReader.SafeGetString(16);
                    entBoleta.NombreJefe = dbManager.DataReader.SafeGetString(17);
                    entBoleta.CorreoJefe = dbManager.DataReader.SafeGetString(18);
                    entBoleta.EsExtra = dbManager.DataReader.GetBoolean(19);
                    entBoleta.CodigoFuncionarioResponsable = dbManager.DataReader.SafeGetInt32(20);
                    entBoleta.EsEnviadoMail = dbManager.DataReader.GetBoolean(21);
                    entBoleta.EsVacacion = dbManager.DataReader.GetBoolean(22);
                    entBoleta.EsAntiguedad = dbManager.DataReader.GetBoolean(23);
                    entBoleta.EsEnviadoMailConfirmacion = dbManager.DataReader.GetBoolean(24);
                    entBoleta.Periodo = dbManager.DataReader.SafeGetString(25);
                    entBoleta.DiasPagados = dbManager.DataReader.SafeGetInt32(26);
                    entBoleta.DiasGozados = dbManager.DataReader.SafeGetInt32(27);
                    entBoleta.CodigoPeriodoExtra = dbManager.DataReader.SafeGetInt32(28);
                    entBoleta.FechaAutorizacion = dbManager.DataReader.GetDateTime(29);
                }
                return entBoleta;
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

        public List<AutorizadorDTO> ConsultarAutorizador(string Tipo)
        {
            try
            {
                dbManager.Open();
                dbManager.ExecuteReader(CommandType.Text, string.Format("SET LANGUAGE US_ENGLISH;  select CodigoAutorizador,IdentificacionAutorizador,NombreCompletoAutorizador,CorreoAutorizador,CodigoTipoAutorizacion from ConfiguracionAutorizador a join Configuraciones b on a.CodigoTipoAutorizacion=b.CodigoConfiguracion where b.ColumnaConfiguracion='Autorizador' and ValorConfiguracion='{0}'", Tipo));
                List<AutorizadorDTO> lstAutorizadorDTO = new List<AutorizadorDTO>();
                while (dbManager.DataReader.Read())
                {
                    AutorizadorDTO entAutorizador = new AutorizadorDTO();
                    entAutorizador.CodigoAutorizador = dbManager.DataReader.GetInt32(0);
                    entAutorizador.IdentificacionAutorizador = dbManager.DataReader.SafeGetString(1);
                    entAutorizador.NombreCompletoAtorizador = dbManager.DataReader.SafeGetString(2);
                    entAutorizador.CorreoAutorizador = dbManager.DataReader.SafeGetString(3);
                    entAutorizador.CodigoTipoAutorizacion = dbManager.DataReader.GetInt32(4);
                    lstAutorizadorDTO.Add(entAutorizador);
                }

                return lstAutorizadorDTO;
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

        public List<PeriodoVacacionFuncionarioDTO> ListarPeriodosVacacionFuncionario(int CodigoFuncionario)
        {
            try
            {
                dbManager.Open();
                dbManager.ExecuteReader(CommandType.Text, string.Format("SET LANGUAGE US_ENGLISH;  exec gmas_PeriodoVacacionFuncionarioPortal {0}", CodigoFuncionario));
                List<PeriodoVacacionFuncionarioDTO> lstResultado = new List<PeriodoVacacionFuncionarioDTO>();
                while (dbManager.DataReader.Read())
                {
                    PeriodoVacacionFuncionarioDTO entPeriodoVacacionFuncionarioDTO = new PeriodoVacacionFuncionarioDTO();
                    entPeriodoVacacionFuncionarioDTO.FechaDesde = dbManager.DataReader.SafeGetString(3);
                    entPeriodoVacacionFuncionarioDTO.FechaHasta = dbManager.DataReader.SafeGetString(4);
                    entPeriodoVacacionFuncionarioDTO.Estado = "Abierto";
                    entPeriodoVacacionFuncionarioDTO.DiasBase = dbManager.DataReader.GetDecimal(5);
                    entPeriodoVacacionFuncionarioDTO.DiasAdicional = dbManager.DataReader.GetDecimal(6);
                    entPeriodoVacacionFuncionarioDTO.DiasExtras = dbManager.DataReader.GetDecimal(7);
                    entPeriodoVacacionFuncionarioDTO.DiasUsados = dbManager.DataReader.GetDecimal(8);
                    entPeriodoVacacionFuncionarioDTO.DiasLibres = dbManager.DataReader.GetDecimal(9);
                    entPeriodoVacacionFuncionarioDTO.DiasUsadosExtras = dbManager.DataReader.GetDecimal(10);//DIAS ANTICIPADOS VACACION
                    entPeriodoVacacionFuncionarioDTO.DiasLibresExtras = dbManager.DataReader.GetDecimal(11);
                    entPeriodoVacacionFuncionarioDTO.FechaCalculoFuncionario = dbManager.DataReader.GetDateTime(12).ToString("yyyy-MM-dd");
                    entPeriodoVacacionFuncionarioDTO.PeriodoExtra = dbManager.DataReader.SafeGetString(13);
                    entPeriodoVacacionFuncionarioDTO.PeriodoVacacion = dbManager.DataReader.SafeGetString(14);
                    entPeriodoVacacionFuncionarioDTO.CodigoPeriodoVacacionesFuncionario = dbManager.DataReader.GetInt32(15);
                    entPeriodoVacacionFuncionarioDTO.Seleccionado = dbManager.DataReader.GetBoolean(16);
                    entPeriodoVacacionFuncionarioDTO.DiasLibresLey = dbManager.DataReader.GetDecimal(17);
                    entPeriodoVacacionFuncionarioDTO.DiasLibresAntiguedad = dbManager.DataReader.GetDecimal(18);
                    lstResultado.Add(entPeriodoVacacionFuncionarioDTO);
                }

                return lstResultado;
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
        public List<PeriodoVacacionFuncionarioDTO> ListarPeriodosVacacionFuncionarioGeneral(int CodigoFuncionario)
        {
            try
            {
                dbManager.Open();
                dbManager.ExecuteReader(CommandType.Text, string.Format("SET LANGUAGE US_ENGLISH;  exec gmas_PeriodoVacacionFuncionarioPortalGeneral {0}", CodigoFuncionario));
                List<PeriodoVacacionFuncionarioDTO> lstResultado = new List<PeriodoVacacionFuncionarioDTO>();
                while (dbManager.DataReader.Read())
                {
                    PeriodoVacacionFuncionarioDTO entPeriodoVacacionFuncionarioDTO = new PeriodoVacacionFuncionarioDTO();
                    entPeriodoVacacionFuncionarioDTO.FechaDesde = dbManager.DataReader.SafeGetString(3);
                    entPeriodoVacacionFuncionarioDTO.FechaHasta = dbManager.DataReader.SafeGetString(4);
                    entPeriodoVacacionFuncionarioDTO.Estado = "Abierto";
                    entPeriodoVacacionFuncionarioDTO.DiasBase = dbManager.DataReader.GetDecimal(5);
                    entPeriodoVacacionFuncionarioDTO.DiasAdicional = dbManager.DataReader.GetDecimal(6);
                    entPeriodoVacacionFuncionarioDTO.DiasExtras = dbManager.DataReader.GetDecimal(7);
                    entPeriodoVacacionFuncionarioDTO.DiasUsados = dbManager.DataReader.GetDecimal(8);
                    entPeriodoVacacionFuncionarioDTO.DiasLibres = dbManager.DataReader.GetDecimal(9);
                    entPeriodoVacacionFuncionarioDTO.DiasUsadosExtras = dbManager.DataReader.GetDecimal(10);
                    entPeriodoVacacionFuncionarioDTO.DiasLibresExtras = dbManager.DataReader.GetDecimal(11);
                    entPeriodoVacacionFuncionarioDTO.FechaCalculoFuncionario = dbManager.DataReader.GetDateTime(12).ToString("yyyy-MM-dd");
                    entPeriodoVacacionFuncionarioDTO.PeriodoExtra = dbManager.DataReader.SafeGetString(13);
                    entPeriodoVacacionFuncionarioDTO.PeriodoVacacion = dbManager.DataReader.SafeGetString(14);
                    entPeriodoVacacionFuncionarioDTO.CodigoPeriodoVacacionesFuncionario = dbManager.DataReader.GetInt32(15);
                    entPeriodoVacacionFuncionarioDTO.Seleccionado = dbManager.DataReader.GetBoolean(16);
                    entPeriodoVacacionFuncionarioDTO.DiasLibresLey = dbManager.DataReader.GetDecimal(17);
                    entPeriodoVacacionFuncionarioDTO.DiasLibresAntiguedad = dbManager.DataReader.GetDecimal(18);
                    lstResultado.Add(entPeriodoVacacionFuncionarioDTO);
                }

                return lstResultado;
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

        public int ConsultarDiasLibresPeriodoVacacion(int CodigoFuncionario, int CodigoPeriodoVacacion)
        {
            try
            {
                dbManager.Open();
                dbManager.ExecuteReader(CommandType.Text, string.Format("SET LANGUAGE US_ENGLISH;  select convert(int,DiasLibresPeriodoVacacionesFuncionario) from PeriodoVacacionesFuncionario where CodigoFuncionario={0} and CodigoPeriodoVacacionesFuncionario={1}", CodigoFuncionario, CodigoPeriodoVacacion));
                List<PeriodoVacacionFuncionarioDTO> lstResultado = new List<PeriodoVacacionFuncionarioDTO>();
                int Dias = 0;
                while (dbManager.DataReader.Read())
                {
                    Dias = dbManager.DataReader.GetInt32(0);
                }

                return Dias;
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
        #endregion

        #region ANTICIPO DE VACACIONES
        public bool ValidarFechasAnticipoVacacion(int CodigoFuncionario, DateTime FechaInicio, DateTime FechaFin)
        {
            try
            {
                dbManager.Open();
                dbManager.Command.CommandTimeout = 0;
                dbManager.ExecuteReader(CommandType.Text, string.Format("set language us_english  SELECT Codigo FROM FuncionarioAnticipoVacacion where codigofuncionario={0} and '{1}' between FechaDesde and FechaHasta ", CodigoFuncionario, Convert.ToDateTime(FechaInicio).ToString("yyyy-MM-dd")));
                int existe = 0;
                while (dbManager.DataReader.Read())
                {
                    existe = dbManager.DataReader.GetInt32(0);
                }
                if (existe == 0)
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                GmasLogger.Instance.write("Method: " + MethodBase.GetCurrentMethod().DeclaringType.Name + " Error: " + ex.Message + "Stack Trace: " + ex.StackTrace.ToString(), "BalconGP");

                return false;
            }
            finally
            {
                dbManager.Dispose();
            }
        }

        public FinSemanaDTO ValidarFinSemana(int CodigoFuncionario, DateTime FechaInicio, DateTime FechaFin, int CodigoPeriodo)
        {
            try
            {
                dbManager.Open();
                dbManager.ExecuteReader(CommandType.Text, string.Format("set language us_english exec gmas_ConsultarDiasFinSemanaAnticipoVacacion '{0}','{1}',{2},{3}", Convert.ToDateTime(FechaInicio).ToString("yyyy-MM-dd"), Convert.ToDateTime(FechaFin).ToString("yyyy-MM-dd"), CodigoFuncionario, CodigoPeriodo));

                FinSemanaDTO finSemanaDTO = new FinSemanaDTO();
                while (dbManager.DataReader.Read())
                {
                    finSemanaDTO.DiasLaborables = dbManager.DataReader.GetInt32(0);
                    finSemanaDTO.DiasTotal = dbManager.DataReader.GetInt32(1);
                    finSemanaDTO.FinSemana = dbManager.DataReader.GetInt32(2);
                }
                return finSemanaDTO;
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
        public FinSemanaDTO ValidarFinSemanaVacacion(int CodigoFuncionario, DateTime FechaInicio, DateTime FechaFin, int CodigoPeriodo)
        {
            try
            {
                dbManager.Open();
                dbManager.ExecuteReader(CommandType.Text, string.Format("set language us_english exec gmas_ConsultarDiasFinSemanaBoletaVacacion '{0}','{1}',{2},{3}", Convert.ToDateTime(FechaInicio).ToString("yyyy-MM-dd"), Convert.ToDateTime(FechaFin).ToString("yyyy-MM-dd"), CodigoFuncionario, CodigoPeriodo));

                FinSemanaDTO finSemanaDTO = new FinSemanaDTO();
                while (dbManager.DataReader.Read())
                {
                    finSemanaDTO.DiasLaborables = dbManager.DataReader.GetInt32(0);
                    finSemanaDTO.DiasTotal = dbManager.DataReader.GetInt32(1);
                    finSemanaDTO.FinSemana = dbManager.DataReader.GetInt32(2);
                }
                return finSemanaDTO;
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

        public int ActualizarAnticipoVacacion(AnticipoVacacionDTO entAnticipoVacacion)
        {
            try
            {
                dbManager.Open();
                dbManager.ExecuteReader(CommandType.Text, string.Format("set language us_english  SELECT Codigo FROM FuncionarioAnticipoVacacion where codigofuncionario={0} and '{1}' between FechaDesde and FechaHasta ", entAnticipoVacacion.CodigoFuncionario, Convert.ToDateTime(entAnticipoVacacion.FechaDesde).ToString("yyyy-MM-dd")));
                int CodigoAnticipoVacacion = 0;
                while (dbManager.DataReader.Read())
                {
                    CodigoAnticipoVacacion = dbManager.DataReader.GetInt32(0);
                }
                if (CodigoAnticipoVacacion == 0)
                {
                    dbManager.Open();

                    dbManager.ExecuteReader(CommandType.Text, string.Format("SET LANGUAGE US_ENGLISH; EXEC gmas_InsertFuncionarioAnticipoVacacion {0},{1},'{2}','{3}',{4},'{5}',{6},'{7}'", entAnticipoVacacion.CodigoFuncionario, entAnticipoVacacion.CodigoPeriodoVacacionesFuncionario, Convert.ToDateTime(entAnticipoVacacion.FechaDesde).ToString("yyyy-MM-dd"), Convert.ToDateTime(entAnticipoVacacion.FechaHasta).ToString("yyyy-MM-dd"), entAnticipoVacacion.CantidadDias, entAnticipoVacacion.Observacion == null ? string.Empty : entAnticipoVacacion.Observacion, entAnticipoVacacion.CodigoFuncionarioAutoriza, entAnticipoVacacion.UsuarioAuditoria));
                    while (dbManager.DataReader.Read())
                    {
                        CodigoAnticipoVacacion = dbManager.DataReader.GetInt32(0);
                    }
                }

                return CodigoAnticipoVacacion;
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
        public int ActualizarEstadoAutorizadoAnticipoVacacion(int Codigo, int Estado)
        {
            try
            {
                dbManager.Open();
                dbManager.ExecuteNonQuery(CommandType.Text, string.Format("SET LANGUAGE US_ENGLISH;  update FuncionarioAnticipoVacacion set Estado={0},EsAutorizado={1},EsConfirmado=0 where Codigo={2}", Estado, Estado, Codigo));
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
        public int ActualizarEstadoConfirmadoAnticipoVacacion(int Codigo, int Estado, string Observacion)
        {
            try
            {
                dbManager.Open();
                dbManager.ExecuteNonQuery(CommandType.Text, string.Format("SET LANGUAGE US_ENGLISH;  update FuncionarioAnticipoVacacion set Estado={0},EsConfirmado={1},ObservacionConfirmacion='{2}' where Codigo={3}", Estado, Estado, Observacion == null ? string.Empty : Observacion, Codigo));

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

        public int EliminarAnticipoVacacion(int Codigo)
        {
            try
            {
                dbManager.Open();
                dbManager.ExecuteNonQuery(CommandType.Text, string.Format("SET LANGUAGE US_ENGLISH;  delete FuncionarioAnticipoVacacion where Codigo={0}", Codigo));

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


        public AnticipoVacacionDTO ObtenerAnticipoVacacion(int Codigo)
        {
            AnticipoVacacionDTO anticipoDTO = new AnticipoVacacionDTO();
            try
            {
                dbManager.Open();
                dbManager.ExecuteReader(CommandType.Text, string.Format("SET LANGUAGE US_ENGLISH;  exec gmas_ObtenerAnticipoVacacion_Balcon {0}", Codigo));
                while (dbManager.DataReader.Read())
                {
                    anticipoDTO.Codigo = dbManager.DataReader.SafeGetInt32(0);
                    anticipoDTO.CodigoFuncionario = dbManager.DataReader.SafeGetInt32(1);
                    anticipoDTO.CodigoPeriodoVacacionesFuncionario = dbManager.DataReader.SafeGetInt32(2);
                    anticipoDTO.FechaDesde = dbManager.DataReader.SafeGetDateTime(3);
                    anticipoDTO.FechaHasta = dbManager.DataReader.SafeGetDateTime(4);
                    anticipoDTO.CantidadDias = dbManager.DataReader.SafeGetInt32(5);
                    anticipoDTO.Observacion = dbManager.DataReader.SafeGetString(6);
                    anticipoDTO.Estado = dbManager.DataReader.GetBoolean(7);
                    anticipoDTO.CodigoFuncionarioAutoriza = dbManager.DataReader.SafeGetInt32(8);
                    anticipoDTO.EsAutorizado = dbManager.DataReader.GetBoolean(9);
                    anticipoDTO.EsConfirmado = dbManager.DataReader.GetBoolean(10);
                    anticipoDTO.UsuarioAuditoria = dbManager.DataReader.SafeGetString(11);
                    anticipoDTO.FechaAuditoria = dbManager.DataReader.SafeGetDateTime(12);
                    anticipoDTO.NombreEmpleado = dbManager.DataReader.SafeGetString(13);
                    anticipoDTO.CorreoEmpleado = dbManager.DataReader.SafeGetString(14);
                    anticipoDTO.NombreJefe = dbManager.DataReader.SafeGetString(15);
                    anticipoDTO.CorreoJefe = dbManager.DataReader.SafeGetString(16);
                    anticipoDTO.Periodo = dbManager.DataReader.SafeGetString(17);
                }
                return anticipoDTO;
            }
            catch (Exception ex)
            {
                GmasLogger.Instance.write("Method: " + MethodBase.GetCurrentMethod().DeclaringType.Name + " Error: " + ex.Message + "Stack Trace: " + ex.StackTrace.ToString(), "BalconGP");
                throw ex;
            }
            finally
            {
                dbManager.Dispose();
            }
        }
        #endregion

        #region PERMISOS
        public bool ValidarFechasPermiso(int CodigoFuncionario, DateTime FechaInicio, DateTime FechaFin, string TipoPermiso)
        {
            try
            {
                if (TipoPermiso.Contains("Horas"))
                {
                    return true;
                }
                else
                {
                    dbManager.Open();
                    dbManager.Command.CommandTimeout = 0;
                    dbManager.ExecuteReader(CommandType.Text, string.Format("set language us_english  select CodigoPermiso from Permiso where codigofuncionario={0} and '{1}' between FechaInicioPermiso and FechaFinPermiso ", CodigoFuncionario, Convert.ToDateTime(FechaInicio).ToString("yyyy-MM-dd")));
                    int existe = 0;
                    while (dbManager.DataReader.Read())
                    {
                        existe = dbManager.DataReader.GetInt32(0);
                    }
                    if (existe == 0)
                        return true;
                    else
                        return false;
                }
            }
            catch (Exception ex)
            {
                GmasLogger.Instance.write("Method: " + MethodBase.GetCurrentMethod().DeclaringType.Name + " Error: " + ex.Message + "Stack Trace: " + ex.StackTrace.ToString(), "BalconGP");

                return false;
            }
            finally
            {
                dbManager.Dispose();
            }
        }

        public HorasPermisosDTO ValidarPermisoXHoras(int CodigoFuncionario, string HoraDesde, string HoraHasta, string TipoPermiso)
        {
            try
            {
                dbManager.Open();
                dbManager.Command.CommandTimeout = 0;
                dbManager.ExecuteReader(CommandType.Text, string.Format("set language us_english  EXEC gmas_ValidarPermisosXHoras '{0}','{1}',{2},'{3}'", HoraDesde, HoraHasta, CodigoFuncionario, TipoPermiso));
                HorasPermisosDTO horasPermisosDTO = new HorasPermisosDTO();
                while (dbManager.DataReader.Read())
                {
                    horasPermisosDTO.HoraSolicitada = dbManager.DataReader.GetDecimal(0);
                    horasPermisosDTO.HorasRegistradas = dbManager.DataReader.GetDecimal(1);
                    horasPermisosDTO.PendienteSolicitar = dbManager.DataReader.GetDecimal(2);
                }
                return horasPermisosDTO;
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
        public int ActualizarPermiso(PermisoDTO entPermiso)
        {
            try
            {
                string content = JsonConvert.SerializeObject(entPermiso);
                GmasLogger.Instance.write("Method: " + MethodBase.GetCurrentMethod().DeclaringType.Name + " JSON: " + content, "BalconGP");


                int CodigoPermiso = 0;
                if (CodigoPermiso == 0)
                {
                    dbManager.Open();
                    if (entPermiso.CodigoFuncionarioAutorizo > 0)
                    {
                        dbManager.ExecuteReader(CommandType.Text, string.Format("set language us_english  INSERT INTO Permiso (CodigoFuncionario,FechaInicioPermiso,FechaFinPermiso,CodigoTipoPermiso,JustificacionPermiso,CodigoFuncionarioAutorizo,EstadoPermiso,FechaBoletaPermiso,GeneraAccionPersonalPermiso,DiasHabilesPermiso,EsConfirmadoPermiso,EsAutorizadoPermiso,EsConsumidoPermiso,UsuarioAuditoria,FechaAuditoria,HoraDesde,HoraHasta) VALUES ({0},'{1}','{2}',{3},'{4}',{5},{6},'{7}',{8},'{9}','{10}',{11},{12},'{13}',getdate(),'{14}','{15}') declare @CodigoPermiso int=0 SELECT TOP 1 @CodigoPermiso = SCOPE_IDENTITY() FROM Permiso select @CodigoPermiso as CodigoPermiso", entPermiso.CodigoFuncionario, Convert.ToDateTime(entPermiso.FechaInicioPermiso).ToString("yyyy-MM-dd"), Convert.ToDateTime(entPermiso.FechaFinPermiso).ToString("yyyy-MM-dd"), entPermiso.CodigoTipoPermiso, entPermiso.JustificacionPermiso, entPermiso.CodigoFuncionarioAutorizo, 0, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), 0, entPermiso.DiasHabilesPermiso, 0, 0, 0, entPermiso.UsuarioAuditoria, entPermiso.HoraDesde, entPermiso.HoraHasta));
                    }
                    else
                    {
                        dbManager.ExecuteReader(CommandType.Text, string.Format("set language us_english  INSERT INTO Permiso (CodigoFuncionario,FechaInicioPermiso,FechaFinPermiso,CodigoTipoPermiso,JustificacionPermiso,EstadoPermiso,FechaBoletaPermiso,GeneraAccionPersonalPermiso,DiasHabilesPermiso,EsConfirmadoPermiso,EsAutorizadoPermiso,EsConsumidoPermiso,UsuarioAuditoria,FechaAuditoria,HoraDesde,HoraHasta) VALUES ({0},'{1}','{2}',{3},'{4}',{5},'{6}',{7},'{8}','{9}',{10},{11},'{12}',getdate(),'{13}','{13}') declare @CodigoPermiso int=0 SELECT TOP 1 @CodigoPermiso = SCOPE_IDENTITY() FROM Permiso select @CodigoPermiso as CodigoPermiso", entPermiso.CodigoFuncionario, Convert.ToDateTime(entPermiso.FechaInicioPermiso).ToString("yyyy-MM-dd"), Convert.ToDateTime(entPermiso.FechaFinPermiso).ToString("yyyy-MM-dd"), entPermiso.CodigoTipoPermiso, entPermiso.JustificacionPermiso, 0, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), 0, entPermiso.DiasHabilesPermiso, 0, 0, 0, entPermiso.UsuarioAuditoria, entPermiso.HoraDesde, entPermiso.HoraHasta));
                    }
                    while (dbManager.DataReader.Read())
                    {
                        CodigoPermiso = dbManager.DataReader.GetInt32(0);
                    }


                }
                else
                {
                    dbManager.Open();
                    dbManager.ExecuteReader(CommandType.Text, string.Format("set language us_english  update Permiso set JustificacionPermiso='{0}',FechaInicioPermiso='{1}',FechaFinPermiso='{2}',DiasHabilesPermiso={3} where CodigoFuncionario={4} and CodigoPermiso={5}", entPermiso.JustificacionPermiso, Convert.ToDateTime(entPermiso.FechaInicioPermiso).ToString("yyyy-MM-dd"), Convert.ToDateTime(entPermiso.FechaFinPermiso).ToString("yyyy-MM-dd"), entPermiso.DiasHabilesPermiso, entPermiso.CodigoFuncionario, CodigoPermiso));
                }

                return CodigoPermiso;
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
        public PermisoDTO ActualizarEstadoPermiso(PermisoDTO permisoDTO)
        {
            try
            {
                dbManager.Open();
                dbManager.ExecuteNonQuery(CommandType.Text, string.Format("SET LANGUAGE US_ENGLISH;  update Permiso set EstadoPermiso={0},EsConfirmadoPermiso=0,EsAutorizadoPermiso=0 where CodigoPermiso={1}", permisoDTO.EstadoPermiso == true ? 1 : 0, permisoDTO.CodigoPermiso));
                return permisoDTO;
            }
            catch (Exception ex)
            {
                GmasLogger.Instance.write("Method: " + MethodBase.GetCurrentMethod().DeclaringType.Name + " Error: " + ex.Message + "Stack Trace: " + ex.StackTrace.ToString(), "BalconGP");

                permisoDTO.CodigoPermiso = 0;
                return permisoDTO;
            }
            finally
            {
                dbManager.Dispose();
            }
        }
        public int ActualizarEstadoConfirmarPermiso(int CodigoPermiso, string Estado)
        {
            try
            {
                dbManager.Open();
                dbManager.ExecuteNonQuery(CommandType.Text, string.Format("SET LANGUAGE US_ENGLISH;  update Permiso set EstadoPermiso=case when '{0}'='S' then 1 else 0 end,EsConfirmadoPermiso=case when '{1}'='S' then 1 else 0 end,EsAutorizadoPermiso=case when '{2}'='S' then 1 else 0 end where CodigoPermiso={3} ", Estado, Estado, Estado, CodigoPermiso));

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

        public int EliminarPermiso(int CodigoPermiso)
        {
            try
            {
                dbManager.Open();
                dbManager.ExecuteNonQuery(CommandType.Text, string.Format("SET LANGUAGE US_ENGLISH;  delete Permiso where CodigoPermiso={0} ", CodigoPermiso));

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

        public PermisoDTO ObtenerPermisoFuncionario(int CodigoPermiso)
        {
            PermisoDTO permisoDTO = new PermisoDTO();
            try
            {
                dbManager.Open();
                dbManager.ExecuteReader(CommandType.Text, string.Format("SET LANGUAGE US_ENGLISH;  select CodigoPermiso,p.CodigoFuncionario,CodigoFuncionarioAutorizo=isnull(jefe.CodigoFuncionario,0),p.CodigoTipoPermiso,DiasHabilesPermiso=isnull(p.DiasHabilesPermiso,0),p.FechaBoletaPermiso,p.FechaInicioPermiso,p.FechaFinPermiso,JustificacionPermiso=isnull(p.JustificacionPermiso,''),Empleado=(case when f.EsAgricola=0 then f.NombreCompleto else '(AGRICOLA) '+ f.NombreCompleto END),CorreoEmpleado=ISNULL(F.CorreoElectronicoFuncionario,f.CorreoElectronicoFuncionarioPersonal),Jefe=jefe.NombreCompleto,CorreoJefe=isnull(JEFE.CorreoElectronicoFuncionario,jefe.CorreoElectronicoFuncionarioPersonal),tp.NombreTipoPermiso,isnull(p.HoraDesde,''),isnull(p.HoraHasta,'') from Permiso p join Funcionario f on f.codigofuncionario=p.codigofuncionario left join Funcionario jefe on f.CodigoJefeFuncionario=jefe.codigofuncionario join TipoPermiso tp on tp.CodigoTipoPermiso=p.CodigoTipoPermiso where p.codigopermiso={0}", CodigoPermiso));
                while (dbManager.DataReader.Read())
                {
                    permisoDTO.CodigoPermiso = dbManager.DataReader.SafeGetInt32(0);
                    permisoDTO.CodigoFuncionario = dbManager.DataReader.SafeGetInt32(1);
                    permisoDTO.CodigoFuncionarioAutorizo = dbManager.DataReader.SafeGetInt32(2);
                    permisoDTO.CodigoTipoPermiso = dbManager.DataReader.SafeGetInt32(3);
                    permisoDTO.DiasHabilesPermiso = dbManager.DataReader.SafeGetInt32(4);
                    permisoDTO.FechaBoletaPermiso = dbManager.DataReader.SafeGetDateTime(5);
                    permisoDTO.FechaInicioPermiso = dbManager.DataReader.SafeGetDateTime(6);
                    permisoDTO.FechaFinPermiso = dbManager.DataReader.SafeGetDateTime(7);
                    permisoDTO.JustificacionPermiso = dbManager.DataReader.SafeGetString(8);
                    permisoDTO.NombreEmpleado = dbManager.DataReader.SafeGetString(9);
                    permisoDTO.CorreoEmpleado = dbManager.DataReader.SafeGetString(10);
                    permisoDTO.NombreJefe = dbManager.DataReader.SafeGetString(11);
                    permisoDTO.CorreoJefe = dbManager.DataReader.SafeGetString(12);
                    permisoDTO.NombreTipoPermiso = dbManager.DataReader.SafeGetString(13);
                    permisoDTO.HoraDesde = dbManager.DataReader.SafeGetString(14);
                    permisoDTO.HoraHasta = dbManager.DataReader.SafeGetString(15);
                }
                return permisoDTO;
            }
            catch (Exception ex)
            {
                GmasLogger.Instance.write("Method: " + MethodBase.GetCurrentMethod().DeclaringType.Name + " Error: " + ex.Message + "Stack Trace: " + ex.StackTrace.ToString(), "BalconGP");
                throw ex;
            }
            finally
            {
                dbManager.Dispose();
            }
        }
        #endregion

        #region DATOS DE LA ORGANIZACION
        public DatosOrganizacionDTO ObtenerDatosOrganizacion(string Identificacion)
        {
            try
            {
                dbManager.Open();
                dbManager.Command.CommandTimeout = 0;
                dbManager.ExecuteReader(CommandType.Text, string.Format("SET LANGUAGE US_ENGLISH; exec gmas_BalconServicios_DatosOrganizacion '{0}'", Identificacion));
                DatosOrganizacionDTO entColaboradorDTO = new DatosOrganizacionDTO();
                while (dbManager.DataReader.Read())
                {

                    entColaboradorDTO.Sucursal = dbManager.DataReader.SafeGetString(0);
                    entColaboradorDTO.NombreCargo = dbManager.DataReader.SafeGetString(1);
                    entColaboradorDTO.NombreTipoContratacion = dbManager.DataReader.SafeGetString(2);
                    entColaboradorDTO.Sueldo = dbManager.DataReader.SafeGetString(3);
                    entColaboradorDTO.FechaCalculoFuncionario = dbManager.DataReader.SafeGetString(4);
                    entColaboradorDTO.ReportaA = dbManager.DataReader.SafeGetString(5);
                    entColaboradorDTO.CentroCosto1 = dbManager.DataReader.SafeGetString(6);
                    entColaboradorDTO.CentroCosto2 = dbManager.DataReader.SafeGetString(7);

                }
                return entColaboradorDTO;
            }
            catch (Exception ex)
            {
                GmasLogger.Instance.write("Method: " + MethodBase.GetCurrentMethod().DeclaringType.Name + " Error: " + ex.Message + "Stack Trace: " + ex.StackTrace.ToString(), "BalconGP");
                throw ex;
            }
            finally
            {
                dbManager.Dispose();

            }
        }
        #endregion

        #region ESTUDIOS REALIZADOS
        public List<EstudiosRealizadosDTO> ObtenerEstudiosRealizados(string Identificacion)
        {
            try
            {
                dbManager.Open();
                dbManager.Command.CommandTimeout = 0;
                dbManager.ExecuteReader(CommandType.Text, string.Format("SET LANGUAGE US_ENGLISH;  exec gmas_BalconServicios_EstudiosRealizados '{0}'", Identificacion));
                List<EstudiosRealizadosDTO> lstEstudiosRealizadosDTO = new List<EstudiosRealizadosDTO>();
                while (dbManager.DataReader.Read())
                {
                    EstudiosRealizadosDTO entEstudiosRealizadosDTO = new EstudiosRealizadosDTO();
                    entEstudiosRealizadosDTO.CodigoEstudioRealizadoFuncionario = dbManager.DataReader.SafeGetInt32(0);
                    entEstudiosRealizadosDTO.TituloObtenido = dbManager.DataReader.SafeGetString(1);
                    entEstudiosRealizadosDTO.NumeroRegistroSenescyt = dbManager.DataReader.SafeGetString(2);
                    entEstudiosRealizadosDTO.NombreCentroAcademico = dbManager.DataReader.SafeGetString(3);
                    entEstudiosRealizadosDTO.NombreNivelAcademico = dbManager.DataReader.SafeGetString(4);
                    entEstudiosRealizadosDTO.CodigoCentroAcademico = dbManager.DataReader.SafeGetInt32(5);
                    entEstudiosRealizadosDTO.CodigoNivelAcademico = dbManager.DataReader.SafeGetInt32(6);
                    lstEstudiosRealizadosDTO.Add(entEstudiosRealizadosDTO);
                }
                return lstEstudiosRealizadosDTO;
            }
            catch (Exception ex)
            {
                GmasLogger.Instance.write("Method: " + MethodBase.GetCurrentMethod().DeclaringType.Name + " Error: " + ex.Message + "Stack Trace: " + ex.StackTrace.ToString(), "BalconGP");
                throw ex;
            }
            finally
            {
                dbManager.Dispose();

            }
        }

        public int ActualizarEstudiosRealizados(EstudiosRealizadosDTO estudios)
        {
            try
            {
                dbManager.Open();
                dbManager.Command.CommandTimeout = 0;
                dbManager.ExecuteNonQuery(CommandType.Text, string.Format("SET LANGUAGE US_ENGLISH;  exec gmas_BalconServicios_ActualizarEstudiosRealizados {0},{1},{2},{3},'{4}','{5}'", estudios.CodigoEstudioRealizadoFuncionario, estudios.CodigoFuncionario, estudios.CodigoNivelAcademico, estudios.CodigoCentroAcademico, estudios.TituloObtenido, estudios.NumeroRegistroSenescyt));
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

        public int InsertarEstudiosRealizados(EstudiosRealizadosDTO estudios)
        {
            try
            {
                dbManager.Open();
                dbManager.Command.CommandTimeout = 0;
                dbManager.ExecuteNonQuery(CommandType.Text, string.Format("exec gmas_BalconServicios_InsertarEstudiosRealizados {0},{1},{2},'{3}','{4}'", estudios.CodigoFuncionario, estudios.CodigoNivelAcademico, estudios.CodigoCentroAcademico, estudios.TituloObtenido, estudios.NumeroRegistroSenescyt));
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
        #endregion

        #region DATOS MOVIMIENTOS
        public DatosMovimientoDTO ObtenerDatosMovimiento(string Identificacion)
        {
            try
            {
                dbManager.Open();
                dbManager.Command.CommandTimeout = 0;
                dbManager.ExecuteReader(CommandType.Text, string.Format("SET LANGUAGE US_ENGLISH;  exec gmas_BalconServicios_DatosMovimiento '{0}'", Identificacion));
                DatosMovimientoDTO entDatosMovimientoDTO = new DatosMovimientoDTO();
                while (dbManager.DataReader.Read())
                {

                    entDatosMovimientoDTO.FR = dbManager.DataReader.SafeGetString(0);
                    entDatosMovimientoDTO.DC = dbManager.DataReader.SafeGetString(1);
                    entDatosMovimientoDTO.DT = dbManager.DataReader.SafeGetString(2);


                }
                return entDatosMovimientoDTO;
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

        public IngresoAnualDTO ObtenerIngresosAnuales(String IdentifiacionFuncionario)
        {
            try
            {
                dbManager.Open();
                dbManager.ExecuteReader(CommandType.Text, "EXEC gmas_ObtenerIngresosAnualesPortal '" + IdentifiacionFuncionario + "'");
                IngresoAnualDTO entIngresoAnualDTO = new IngresoAnualDTO();
                while (dbManager.DataReader.Read())
                {
                    entIngresoAnualDTO.IngresoAnual = dbManager.DataReader.GetDecimal(0);
                    entIngresoAnualDTO.IngresoOtroEmpleador = dbManager.DataReader.GetDecimal(1);
                    entIngresoAnualDTO.EsGalapagos = dbManager.DataReader.GetInt32(2);
                    entIngresoAnualDTO.Tope = dbManager.DataReader.GetDecimal(3);
                    entIngresoAnualDTO.CantidadFamiliar = dbManager.DataReader.GetInt32(4);
                }
                return entIngresoAnualDTO;
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

        public int FuncionarioApto(string Identificacion)
        {
            try
            {
                dbManager.Open();
                dbManager.ExecuteReader(CommandType.Text, string.Format("SET LANGUAGE US_ENGLISH; SELECT Codigo from FuncionarioListaNegra WHERE Identificacion ='{0}'", Identificacion));
                int Resultado = 0;
                while (dbManager.DataReader.Read())
                {
                    Resultado = dbManager.DataReader.SafeGetInt32(0);
                }
                return Resultado;
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

        public string ObtenerNombreFuncionario(string strIdentificacion)
        {
            string Nombre = "";

            try
            {
                dbManager.Open();
               
                dbManager.ExecuteReader(CommandType.Text, string.Format("select NombreCompleto from Funcionario where EstadoFuncionario=1 and IdentificacionFuncionario='{0}'",strIdentificacion));
                while (dbManager.DataReader.Read())
                {                   
                    Nombre = dbManager.DataReader.SafeGetString(0);                
                }
                return Nombre;
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
    }
}
