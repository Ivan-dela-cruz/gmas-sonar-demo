using CONTROLEMP.NominaGP.Portal.DTO;
using CONTROLEMP.NominaGP.Portal.DTO.FormulariosConozca;
using CONTROLEMP.NominaGP.Portal.Framework;
using CONTROLEMP.NominaGP.Portal.Framework.Persistence;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CONTROLEMP.NominaGP.Portal.DAL
{
    public class FormularioEmpleadoDAL
    {
        private IDBManager dbManager;
        public FormularioEmpleadoDAL()
        {
            dbManager = new DBManager();
            dbManager.ProviderType = DataProvider.SqlServer;
            dbManager.ConnectionString = Configuration.Cadena;
        }

        #region Catalogos Generales
        public List<ConfiguracionDTO> ListEstadoCivil()
        {
            List<ConfiguracionDTO> list = new List<ConfiguracionDTO>();
            try
            {
                dbManager.Open();
                dbManager.Command.CommandTimeout = 0;
                string str = "SET LANGUAGE US_ENGLISH select CodigoConfiguracion, DescripcionConfiguracion, EstadoConfiguracion, ValorConfiguracion, ColumnaConfiguracion " +
                    "from Configuraciones " +
                    "where ColumnaConfiguracion = 'EstadoCivil'";
                dbManager.ExecuteReader(CommandType.Text, str);
                while (dbManager.DataReader.Read())
                {
                    ConfiguracionDTO c = new ConfiguracionDTO();
                    c.CodigoConfiguracion = dbManager.DataReader.SafeGetInt32(0);
                    c.DescripcionConfiguracion = dbManager.DataReader.SafeGetString(1);
                    c.EstadoConfiguracion = dbManager.DataReader.GetBoolean(2);
                    c.ValorConfiguracion = dbManager.DataReader.SafeGetString(3);
                    c.ColumnaConfiguracion = dbManager.DataReader.SafeGetString(4);
                    list.Add(c);
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                dbManager.Dispose();
            }
            return list;
        }

        public List<ConfiguracionDTO> ListParentesco()
        {
            List<ConfiguracionDTO> list = new List<ConfiguracionDTO>();
            try
            {
                dbManager.Open();
                dbManager.Command.CommandTimeout = 0;
                string str = "SET LANGUAGE US_ENGLISH select CodigoConfiguracion, DescripcionConfiguracion from Configuraciones " +
                    "where ColumnaConfiguracion = 'RelacionFamiliar'";
                dbManager.ExecuteReader(CommandType.Text, str);
                while (dbManager.DataReader.Read())
                {
                    ConfiguracionDTO c = new ConfiguracionDTO();
                    c.CodigoConfiguracion = dbManager.DataReader.SafeGetInt32(0);
                    c.DescripcionConfiguracion = dbManager.DataReader.SafeGetString(1);
                    //c.EstadoConfiguracion = dbManager.DataReader.GetBoolean(2);
                    //c.ValorConfiguracion = dbManager.DataReader.SafeGetString(3);
                    //c.ColumnaConfiguracion = dbManager.DataReader.SafeGetString(4);
                    list.Add(c);
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                dbManager.Dispose();
            }
            return list;
        }

        public List<NacionalidadDTO> ListNacionalidad()
        {
            List<NacionalidadDTO> list = new List<NacionalidadDTO>();
            try
            {
                dbManager.Open();
                dbManager.Command.CommandTimeout = 0;
                string str = "select * from Nacionalidad WHERE EstadoNacionalidad=1 and NombreNacionalidad like '%ECUATORIANA%'";
                dbManager.ExecuteReader(CommandType.Text, str);
                while (dbManager.DataReader.Read())
                {
                    NacionalidadDTO c = new NacionalidadDTO();
                    c.CodigoNacionalidad = dbManager.DataReader.SafeGetInt32(0);
                    c.NombreNacionalidad = dbManager.DataReader.SafeGetString(1);
                    c.EstadoNacionalidad = dbManager.DataReader.GetBoolean(2);
                    list.Add(c);
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                dbManager.Dispose();
            }
            return list;
        }

        public List<NivelAcademicoDTO> ListNivelEduacion()
        {
            List<NivelAcademicoDTO> list = new List<NivelAcademicoDTO>();
            try
            {
                dbManager.Open();
                dbManager.Command.CommandTimeout = 0;
                string str = "select * from [NivelAcademico] WHERE EstadoNivelAcademico=1 ";
                dbManager.ExecuteReader(CommandType.Text, str);
                while (dbManager.DataReader.Read())
                {
                    NivelAcademicoDTO c = new NivelAcademicoDTO();
                    c.CodigoNivelAcademico = dbManager.DataReader.SafeGetInt32(0);
                    c.NombreNivelAcademico = dbManager.DataReader.SafeGetString(1);
                    c.EstadoNivelAcademico = dbManager.DataReader.GetBoolean(2);
                    list.Add(c);
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                dbManager.Dispose();
            }
            return list;
        }

        public List<TipoLicenciaDTO> ListTipoLicencia()
        {
            List<TipoLicenciaDTO> list = new List<TipoLicenciaDTO>();
            try
            {
                dbManager.Open();
                dbManager.Command.CommandTimeout = 0;
                string str = "select * from TipoLicencia where EstadoTipoLicencia=1";
                dbManager.ExecuteReader(CommandType.Text, str);
                while (dbManager.DataReader.Read())
                {
                    TipoLicenciaDTO c = new TipoLicenciaDTO();
                    c.CodigoTipoLicencia = dbManager.DataReader.SafeGetInt32(0);
                    c.NombreTipoLicencia = dbManager.DataReader.SafeGetString(1);
                    c.EstadoTipoLicencia = dbManager.DataReader.GetBoolean(2);
                    list.Add(c);
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                dbManager.Dispose();
            }
            return list;
        }

        public List<CantonDTO> ListCiudad()
        {
            List<CantonDTO> list = new List<CantonDTO>();
            try
            {
                dbManager.Open();
                dbManager.Command.CommandTimeout = 0;
                string str = "select * from [Canton]";
                dbManager.ExecuteReader(CommandType.Text, str);
                while (dbManager.DataReader.Read())
                {
                    CantonDTO c = new CantonDTO();
                    c.CodigoCanton = dbManager.DataReader.SafeGetInt32(0);
                    c.NombreCanton = dbManager.DataReader.SafeGetString(1);
                    c.Region = dbManager.DataReader.SafeGetString(2);
                    c.CodigoProvincia = dbManager.DataReader.SafeGetInt32(3);
                    list.Add(c);
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                dbManager.Dispose();
            }
            return list;
        }

        public List<CargoDTO> ListCargo()
        {
            List<CargoDTO> list = new List<CargoDTO>();
            try
            {
                dbManager.Open();
                dbManager.Command.CommandTimeout = 0;
                string str = "select * from [Cargo] WHERE EstadoCargo=1";
                dbManager.ExecuteReader(CommandType.Text, str);
                while (dbManager.DataReader.Read())
                {
                    CargoDTO c = new CargoDTO();
                    c.CodigoCargo = dbManager.DataReader.SafeGetInt32(0);
                    c.NombreCargo = dbManager.DataReader.SafeGetString(1);
                    list.Add(c);
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                dbManager.Dispose();
            }
            return list;
        }


        public List<PaisDTO> ListPais()
        {
            List<PaisDTO> list = new List<PaisDTO>();
            try
            {
                dbManager.Open();
                dbManager.Command.CommandTimeout = 0;
                string str = "select * from [Pais] where CodigoPais='ECU'";
                dbManager.ExecuteReader(CommandType.Text, str);
                while (dbManager.DataReader.Read())
                {
                    PaisDTO c = new PaisDTO();
                    c.Codigo = dbManager.DataReader.SafeGetString(0);
                    c.Descripcion = dbManager.DataReader.SafeGetString(1);

                    list.Add(c);
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                dbManager.Dispose();
            }
            return list;
        }

        public List<ProvinciaDTO> ListProvincia()
        {
            List<ProvinciaDTO> list = new List<ProvinciaDTO>();
            try
            {
                dbManager.Open();
                dbManager.Command.CommandTimeout = 0;
                string str = "select * from Provincia where CodigoPais='ECU'";
                dbManager.ExecuteReader(CommandType.Text, str);
                while (dbManager.DataReader.Read())
                {
                    ProvinciaDTO c = new ProvinciaDTO();
                    c.CodigoProvincia = dbManager.DataReader.SafeGetInt32(0);
                    c.NombreProvincia = dbManager.DataReader.SafeGetString(1);
                    c.CodigoPais = dbManager.DataReader.SafeGetString(2);
                    list.Add(c);
                }
            }
            catch (Exception ex)
            {
                GmasLogger.Instance.write("Method: " + MethodBase.GetCurrentMethod().DeclaringType.Name + " Error: " + ex.Message + "Stack Trace: " + ex.StackTrace.ToString(), "BalconGP");
            }
            finally
            {
                dbManager.Dispose();
            }
            return list;
        }

        public List<FormularioActividadEconomica> ListActividadEconomica()
        {
            List<FormularioActividadEconomica> list = new List<FormularioActividadEconomica>();
            try
            {
                dbManager.Open();
                dbManager.Command.CommandTimeout = 0;
                string str = "select * from [FormularioActividadEconomica]";
                dbManager.ExecuteReader(CommandType.Text, str);
                while (dbManager.DataReader.Read())
                {
                    FormularioActividadEconomica c = new FormularioActividadEconomica();
                    c.CODIGO = dbManager.DataReader.SafeGetString(0);
                    c.DESCRIPCION = dbManager.DataReader.SafeGetString(0).Trim() + "-" + dbManager.DataReader.SafeGetString(1);
                    c.NIVEL = dbManager.DataReader.SafeGetInt32(2);
                    list.Add(c);
                }
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
            return list;
        }
        #endregion

        #region Patrimonio
        public PatrimonioDTO ConsultarPatrimonio(int IdFormulario)
        {
            PatrimonioDTO entPatrimonio = new PatrimonioDTO();
            try
            {
                dbManager.Open();
                dbManager.Command.CommandTimeout = 0;
                string str = "EXEC gmas_GetPatrimonioFormulario " + IdFormulario;
                dbManager.ExecuteReader(CommandType.Text, str);
                while (dbManager.DataReader.Read())
                {

                    entPatrimonio.Ingresos = dbManager.DataReader.GetDecimal(0);
                    entPatrimonio.Egresos = dbManager.DataReader.GetDecimal(1);
                    entPatrimonio.Patrimonio = dbManager.DataReader.GetDecimal(2);
                    entPatrimonio.IdFormulario = IdFormulario;
                }
            }
            catch (Exception ex)
            {
                GmasLogger.Instance.write("Method: " + MethodBase.GetCurrentMethod().DeclaringType.Name + " Error: " + ex.Message + "Stack Trace: " + ex.StackTrace.ToString(), "BalconGP");
            }
            finally
            {
                dbManager.Dispose();
            }
            return entPatrimonio;
        }
        public PatrimonioDTO ConsultarPatrimonioGrids(int IdFormulario)
        {
            PatrimonioDTO entPatrimonio = new PatrimonioDTO();
            try
            {
                dbManager.Open();
                dbManager.Command.CommandTimeout = 0;
                string str = "EXEC gmas_GetPatrimonioFormularioGrids " + IdFormulario;
                dbManager.ExecuteReader(CommandType.Text, str);
                while (dbManager.DataReader.Read())
                {

                    entPatrimonio.Ingresos = dbManager.DataReader.GetDecimal(0);
                    entPatrimonio.Egresos = dbManager.DataReader.GetDecimal(1);
                    entPatrimonio.Patrimonio = dbManager.DataReader.GetDecimal(2);
                    entPatrimonio.IdFormulario = IdFormulario;
                }
            }
            catch (Exception ex)
            {
                GmasLogger.Instance.write("Method: " + MethodBase.GetCurrentMethod().DeclaringType.Name + " Error: " + ex.Message + "Stack Trace: " + ex.StackTrace.ToString(), "BalconGP");
            }
            finally
            {
                dbManager.Dispose();
            }
            return entPatrimonio;
        }

        #endregion


        #region Cargas Familiares
        public List<FormularioCargasFamiliares> ListFuncionarioCargasFamiliares(string identificacion)
        {
            List<FormularioCargasFamiliares> list = new List<FormularioCargasFamiliares>();
            try
            {
                dbManager.Open();
                dbManager.Command.CommandTimeout = 0;
                string str = "SET LANGUAGE US_ENGLISH select CodigoFuncionario, Nombres = NombreFamiliar, " +
                    "CodigoRelacionFamiliar = CodigoRelacionFamiliar, FechaNacimiento,ActividadEcon=convert(bit,isnull(IngresosGrabados,0))," +
                    "isnull(IdentificacionFamiliar,''),isnull(PrimerApellidoFamiliar,''),isnull(SegundoApellidoFamiliar,''),convert(bit,isnull(EsDiscapacitado,0)),RutaArchivo=isnull(RutaArchivo,'') " +
                    "from Familiar where CodigoFuncionario in(select CodigoFuncionario from Funcionario where codigorelacion=1 and IdentificacionFuncionario = '" + identificacion + "') ";
                dbManager.ExecuteReader(CommandType.Text, str);
                while (dbManager.DataReader.Read())
                {
                    FormularioCargasFamiliares c = new FormularioCargasFamiliares();
                    c.IdFormularioEmpleado = dbManager.DataReader.SafeGetInt32(0);
                    c.NombresCompletos = dbManager.DataReader.SafeGetString(1);
                    c.CodigoRelacionFamiliar = dbManager.DataReader.SafeGetInt32(2);
                    c.FechaNacimiento = dbManager.DataReader.SafeGetDateTime(3);
                    c.ActividadEcon = dbManager.DataReader.GetBoolean(4);
                    c.IdentificacionFamiliar = dbManager.DataReader.SafeGetString(5);
                    c.PrimerApellidoFamiliar = dbManager.DataReader.SafeGetString(6);
                    c.SegundoApellidoFamiliar = dbManager.DataReader.SafeGetString(7);
                    c.EsDiscapacitado = dbManager.DataReader.GetBoolean(8);
                    c.RutaArchivo = dbManager.DataReader.SafeGetString(9);
                    c.CodigoFuncionario = dbManager.DataReader.SafeGetInt32(0);
                    list.Add(c);
                }
            }
            catch (Exception ex)
            {
                GmasLogger.Instance.write("Method: " + MethodBase.GetCurrentMethod().DeclaringType.Name + " Error: " + ex.Message + "Stack Trace: " + ex.StackTrace.ToString(), "BalconGP");
            }
            finally
            {
                dbManager.Dispose();
            }
            return list;
        }

        public List<FormularioCargasFamiliares> ListFormularioCargasFamiliares(int id)
        {
            List<FormularioCargasFamiliares> list = new List<FormularioCargasFamiliares>();
            try
            {
                dbManager.Open();
                dbManager.Command.CommandTimeout = 0;
                string str = "SET LANGUAGE US_ENGLISH select Id, IdFormularioEmpleado, NombresCompletos, Parentesco, FechaNacimiento, ActividadEcon, IngresosMensuales, GastosMensuales,isnull(IdentificacionFamiliar,''),isnull(PrimerApellidoFamiliar,''),isnull(SegundoApellidoFamiliar,''),isnull(EsDiscapacitado,0)," +
                    "RutaArchivo=(case when RutaArchivo is null or RutaArchivo='' then 'Pendiente' else 'Cargado' end )" +
                    ",isnull(CodigoFuncionario,0) " +
                    "from FormularioCargasFamiliares " +
                    "where IdFormularioEmpleado in(" + id.ToString() + " ) ";
                dbManager.ExecuteReader(CommandType.Text, str);
                while (dbManager.DataReader.Read())
                {
                    FormularioCargasFamiliares c = new FormularioCargasFamiliares();
                    c.Id = dbManager.DataReader.SafeGetInt32(0);
                    c.IdFormularioEmpleado = dbManager.DataReader.SafeGetInt32(1);
                    c.NombresCompletos = dbManager.DataReader.SafeGetString(2);
                    c.CodigoRelacionFamiliar = dbManager.DataReader.SafeGetInt32(3);
                    c.FechaNacimiento = dbManager.DataReader.SafeGetDateTime(4);
                    c.ActividadEcon = dbManager.DataReader.GetBoolean(5);
                    c.IngresosMensuales = dbManager.DataReader.GetDecimal(6);
                    c.GastosMensuales = dbManager.DataReader.GetDecimal(7);
                    c.IdentificacionFamiliar = dbManager.DataReader.SafeGetString(8);
                    c.PrimerApellidoFamiliar = dbManager.DataReader.SafeGetString(9);
                    c.SegundoApellidoFamiliar = dbManager.DataReader.SafeGetString(10);
                    c.EsDiscapacitado = dbManager.DataReader.GetBoolean(11);
                    c.RutaArchivo = dbManager.DataReader.SafeGetString(12);
                    c.CodigoFuncionario = dbManager.DataReader.SafeGetInt32(13);
                    list.Add(c);
                }
            }
            catch (Exception ex)
            {
                GmasLogger.Instance.write("Method: " + MethodBase.GetCurrentMethod().DeclaringType.Name + " Error: " + ex.Message + "Stack Trace: " + ex.StackTrace.ToString(), "BalconGP");
            }
            finally
            {
                dbManager.Dispose();
            }
            return list;
        }

        public void InsertCargasFamiliares(FormularioCargasFamiliares data, string identificacion, string opcion)
        {
            try
            {
                dbManager.Open();
                dbManager.Command.CommandTimeout = 0;
                string str = string.Format(" SET LANGUAGE US_ENGLISH exec gmas_InsertFormularioCargasFamiliares '{0}','{1}',{2},'{3}',{4},{5},{6},'{7}','{8}','{9}',{10},'{11}',{12},'{13}',{14}", identificacion, data.NombresCompletos.ToUpper(), data.CodigoRelacionFamiliar, data.FechaNacimiento.ToString("yyyy-MM-dd"), (data.ActividadEcon == true ? "1" : "0"), data.IngresosMensuales.ToString().Replace(',', '.'), data.GastosMensuales.ToString().Replace(',', '.'), data.IdentificacionFamiliar, data.PrimerApellidoFamiliar, data.SegundoApellidoFamiliar, (data.EsDiscapacitado == true ? "1" : "0"), data.RutaArchivo, data.Id, opcion, data.CodigoFuncionario);
                dbManager.ExecuteNonQuery(CommandType.Text, str);
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

        public void DeleteCargasFamiliares(int Codigo, string IdentificacionFamiliar)
        {
            try
            {
                dbManager.Open();
                dbManager.Command.CommandTimeout = 0;
                string str = string.Format("SET LANGUAGE US_ENGLISH delete from [FormularioCargasFamiliares] where Id={0} DELETE FROM FAMILIAR WHERE IDENTIFICACIONFAMILIAR='{1}'", Codigo, IdentificacionFamiliar);
                dbManager.ExecuteNonQuery(CommandType.Text, str);
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

        public List<FormularioCargasFamiliares> ListFormularioCargasFamiliaresById(int id)
        {
            List<FormularioCargasFamiliares> list = new List<FormularioCargasFamiliares>();
            try
            {
                dbManager.Open();
                dbManager.Command.CommandTimeout = 0;
                string str = "SET LANGUAGE US_ENGLISH select Id, IdFormularioEmpleado, NombresCompletos, Parentesco, FechaNacimiento, ActividadEcon, IngresosMensuales, GastosMensuales " +
                    "from FormularioCargasFamiliares " +
                    "where IdFormularioEmpleado in(" + id.ToString() + ") ";
                dbManager.ExecuteReader(CommandType.Text, str);
                while (dbManager.DataReader.Read())
                {
                    FormularioCargasFamiliares c = new FormularioCargasFamiliares();
                    c.Id = dbManager.DataReader.SafeGetInt32(0);
                    c.IdFormularioEmpleado = dbManager.DataReader.SafeGetInt32(1);
                    c.NombresCompletos = dbManager.DataReader.SafeGetString(2).ToUpper();
                    c.CodigoRelacionFamiliar = dbManager.DataReader.SafeGetInt32(3);
                    c.FechaNacimiento = dbManager.DataReader.SafeGetDateTime(4);
                    c.ActividadEcon = dbManager.DataReader.GetBoolean(5);
                    c.IngresosMensuales = dbManager.DataReader.GetDecimal(6);
                    c.GastosMensuales = dbManager.DataReader.GetDecimal(7);
                    list.Add(c);
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                dbManager.Dispose();
            }
            return list;
        }

        public bool ExistsFormularioCargasFamiliares(string identificacion)
        {
            bool b = false;
            try
            {
                dbManager.Open();
                dbManager.Command.CommandTimeout = 0;
                string str = "select convert(bit,1) from [FormularioCargasFamiliares] " +
                "where IdFormularioEmpleado in( select Id from FormularioEmpleado where Identificacion = '" + identificacion + "' and Estado = 0 ) ";
                dbManager.ExecuteReader(CommandType.Text, str);
                while (dbManager.DataReader.Read())
                {
                    b = dbManager.DataReader.GetBoolean(0);
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
            return b;
        }
        #endregion

        #region Formulario Empleado
        public FormularioEmpleado GetDataEmpleado(string identificacion)
        {
            FormularioEmpleado data = new FormularioEmpleado();
            try
            {
                dbManager.Open();
                dbManager.Command.CommandTimeout = 0;
                string str = string.Format("SET LANGUAGE US_ENGLISH EXEC gmas_GetFormularioEmpleadoIdentificacion '{0}' ", identificacion);
                dbManager.ExecuteReader(CommandType.Text, str);
                while (dbManager.DataReader.Read())
                {
                    data.NombresCompletos = dbManager.DataReader.SafeGetString(0).ToUpper();
                    data.Genero = dbManager.DataReader.SafeGetInt32(1);
                    data.TipoIdentificacion = dbManager.DataReader.SafeGetInt32(2);
                    data.Identificacion = dbManager.DataReader.SafeGetString(3);
                    data.LugarNac = dbManager.DataReader.SafeGetString(4).ToUpper();
                    data.FechaNac = dbManager.DataReader.SafeGetDateTime(5);
                    data.Nacionalidad = dbManager.DataReader.SafeGetInt32(6);
                    data.EstadoCivil = dbManager.DataReader.SafeGetInt32(7);
                    data.TipoLicencia = dbManager.DataReader.SafeGetInt32(8);
                    data.Cargo = dbManager.DataReader.SafeGetInt32(9);
                    string carnet = dbManager.DataReader.SafeGetString(10);
                    if (carnet.Length > 0) data.CarnetDiscapacitado = true; else data.CarnetDiscapacitado = false;
                    data.PaisResidencia = dbManager.DataReader.SafeGetString(11);
                    data.CiudadResidencia = dbManager.DataReader.SafeGetInt32(12);
                    data.Provincia = dbManager.DataReader.SafeGetInt32(13);
                    data.TelefonoDomicilio = dbManager.DataReader.SafeGetString(14);
                    data.Celular = dbManager.DataReader.SafeGetString(15);
                    data.Email = dbManager.DataReader.SafeGetString(16);
                    data.DireccionDomicilio = dbManager.DataReader.SafeGetString(17).ToUpper();
                    data.ProyectoObra = dbManager.DataReader.SafeGetString(18).ToUpper();
                    data.EmailEmpresarial = dbManager.DataReader.SafeGetString(19);
                    data.RutaFoto = dbManager.DataReader.SafeGetString(20);
                    if (dbManager.DataReader.GetString(20) != string.Empty)
                    {
                        data.RutaFoto = dbManager.DataReader.GetString(20);
                        data.Foto = File.ReadAllBytes(data.RutaFoto);
                    }
                }
            }
            catch (Exception ex)
            {
                GmasLogger.Instance.write("Method: " + MethodBase.GetCurrentMethod().DeclaringType.Name + " Error: " + ex.Message + "Stack Trace: " + ex.StackTrace.ToString(), "BalconGP");
            }
            finally
            {
                dbManager.Dispose();
            }
            return data;
        }

        public int GetFormularioEmpleadoLastUpdated(string identificacion)
        {
            int Id = 0;
            try
            {
                dbManager.Open();
                dbManager.Command.CommandTimeout = 0;
                string str = "SET LANGUAGE US_ENGLISH select top 1 f.Id " +
                    "from [FormularioEmpleado] f " +
                    "where f.Identificacion = '" + identificacion + "' and Estado = 0 " +
                    "order by f.FechaHoraTrx desc";
                dbManager.ExecuteReader(CommandType.Text, str);
                while (dbManager.DataReader.Read())
                {
                    Id = dbManager.DataReader.SafeGetInt32(0);
                }
            }
            catch (Exception ex)
            {
                GmasLogger.Instance.write("Method: " + MethodBase.GetCurrentMethod().DeclaringType.Name + " Error: " + ex.Message + "Stack Trace: " + ex.StackTrace.ToString(), "BalconGP");
            }
            finally
            {
                dbManager.Dispose();
            }
            return Id;
        }

        public FormularioEmpleado GetFormularioEmpleadoById(int id)
        {
            FormularioEmpleado data = new FormularioEmpleado();
            try
            {
                dbManager.Open();
                dbManager.Command.CommandTimeout = 0;
                string str = string.Format(" SET LANGUAGE US_ENGLISH exec gmas_GetFormularioEmpleadoById {0}", id);
                dbManager.ExecuteReader(CommandType.Text, str);
                while (dbManager.DataReader.Read())
                {
                    //data.Id = id;
                    data.NombresCompletos = dbManager.DataReader.SafeGetString(0).ToUpper();
                    data.Genero = dbManager.DataReader.SafeGetInt32(1);
                    data.TipoIdentificacion = dbManager.DataReader.SafeGetInt32(2);
                    data.Identificacion = dbManager.DataReader.SafeGetString(3);
                    data.LugarNac = dbManager.DataReader.SafeGetString(4).ToUpper();
                    data.FechaNac = dbManager.DataReader.SafeGetDateTime(5);
                    data.Nacionalidad = dbManager.DataReader.SafeGetInt32(6);
                    data.EstadoCivil = dbManager.DataReader.SafeGetInt32(7);
                    data.NivelEducacion = dbManager.DataReader.SafeGetInt32(8);
                    data.Profesion = dbManager.DataReader.SafeGetString(9).ToUpper();
                    data.TipoLicencia = dbManager.DataReader.SafeGetInt32(10);
                    data.ProyectoObra = dbManager.DataReader.SafeGetString(11).ToUpper();
                    data.CiudadProyecto = dbManager.DataReader.SafeGetInt32(12);
                    data.Cargo = dbManager.DataReader.SafeGetInt32(13);
                    data.CarnetDiscapacitado = dbManager.DataReader.GetBoolean(14);
                    data.PaisResidencia = dbManager.DataReader.SafeGetString(15);
                    data.CiudadResidencia = dbManager.DataReader.SafeGetInt32(16);
                    data.Provincia = dbManager.DataReader.SafeGetInt32(17);
                    data.DireccionDomicilio = dbManager.DataReader.SafeGetString(18).ToUpper();
                    data.NumDireccion = dbManager.DataReader.SafeGetString(19);
                    data.CalleSecundaria = dbManager.DataReader.SafeGetString(20);
                    data.SectorReferencia = dbManager.DataReader.SafeGetString(21);
                    data.Edificio = dbManager.DataReader.SafeGetString(22);
                    data.Piso = dbManager.DataReader.SafeGetString(23);
                    data.TelefonoDomicilio = dbManager.DataReader.SafeGetString(24);
                    data.Celular = dbManager.DataReader.SafeGetString(25);
                    data.Email = dbManager.DataReader.SafeGetString(26);
                    data.ActivEconomicaPrincipal = dbManager.DataReader.SafeGetString(27);
                    data.ActivEconomicaAdicional = dbManager.DataReader.SafeGetString(28);
                    data.IngresosMensuales = dbManager.DataReader.GetDecimal(29);
                    data.GastosMensuales = dbManager.DataReader.GetDecimal(30);

                    data.ConyugNombres = dbManager.DataReader.SafeGetString(31).ToUpper();
                    data.ConyugGenero = dbManager.DataReader.SafeGetInt32(32);
                    data.ConyugTipoIdentificacion = dbManager.DataReader.SafeGetInt32(33);
                    data.ConyugIdentificacion = dbManager.DataReader.SafeGetString(34);
                    data.ConyugLugarNac = dbManager.DataReader.SafeGetString(35).ToUpper();
                    data.ConyugFechaNac = dbManager.DataReader.SafeGetDateTime(36);
                    data.ConyugNacionalidad = dbManager.DataReader.SafeGetInt32(37);
                    data.ConyugTelefonoDom = dbManager.DataReader.SafeGetString(38);
                    data.ConyugCelular = dbManager.DataReader.SafeGetString(39);
                    data.ConyugEmail = dbManager.DataReader.SafeGetString(40);
                    data.ConyugPaisResidencia = dbManager.DataReader.SafeGetString(41);
                    data.ConyugCiudadResidencia = dbManager.DataReader.SafeGetInt32(42);
                    data.ConyugProvincia = dbManager.DataReader.SafeGetInt32(43);
                    data.ConyugNivelEducacion = dbManager.DataReader.SafeGetInt32(44);
                    data.ConyugProfesion = dbManager.DataReader.SafeGetString(45).ToUpper();
                    data.ConyugCargo = dbManager.DataReader.SafeGetString(46).ToUpper();
                    data.ConyugEmpresa = dbManager.DataReader.SafeGetString(47).ToUpper();
                    data.ConyugDireccionEmpresa = dbManager.DataReader.SafeGetString(48).ToUpper();
                    data.ConyugTelefonoEmpresa = dbManager.DataReader.SafeGetString(49);
                    data.ConyugActivEconomicaPrinc = dbManager.DataReader.SafeGetString(50);
                    data.ConyugActivEconomicaAdic = dbManager.DataReader.SafeGetString(51);
                    data.ConyugIngresosMensuales = dbManager.DataReader.GetDecimal(52);
                    data.ConyugGastosMensuales = dbManager.DataReader.GetDecimal(53);

                    data.IngresoActividadPrinc = dbManager.DataReader.GetDecimal(54);
                    data.IngresoOtrasActividades = dbManager.DataReader.GetDecimal(55);
                    data.DescripOtrasActiv = dbManager.DataReader.SafeGetString(56).ToUpper();
                    data.GastosEgresos = dbManager.DataReader.GetDecimal(57);
                    data.TotalActivos = dbManager.DataReader.GetDecimal(58);
                    data.TotalPasivos = dbManager.DataReader.GetDecimal(59);
                    data.TotalPatrimonios = dbManager.DataReader.GetDecimal(60);
                    data.OrigenFondos = dbManager.DataReader.SafeGetString(61);
                    data.DestinoFondos = dbManager.DataReader.SafeGetString(62).ToUpper();

                    data.PolitEsExpuesto = dbManager.DataReader.GetBoolean(63);
                    data.PolitCargo = dbManager.DataReader.SafeGetString(64).ToUpper();
                    data.PolitFechaNombram = dbManager.DataReader.SafeGetDateTime(65);
                    data.PolitFechaCulmin = dbManager.DataReader.SafeGetDateTime(66);
                    data.PolitFamiliarEsExpuesto = dbManager.DataReader.GetBoolean(67);
                    data.PolitParentParentesco = dbManager.DataReader.SafeGetInt32(68);
                    data.PolitParentIdentificacion = dbManager.DataReader.SafeGetString(69);
                    data.PolitParentNombresCompletos = dbManager.DataReader.SafeGetString(70).ToUpper();
                    data.PolitParentCargo = dbManager.DataReader.SafeGetString(71).ToUpper();
                    data.PolitParentFechaNombram = dbManager.DataReader.SafeGetDateTime(72);
                    data.PolitParentFechaCulmin = dbManager.DataReader.SafeGetDateTime(73);

                    data.RelacionComercial = dbManager.DataReader.SafeGetString(74).ToUpper();
                    data.RelacionDetalleObjeto = dbManager.DataReader.SafeGetString(75).ToUpper();
                    data.RelacionTipoContrato = dbManager.DataReader.SafeGetString(76).ToUpper();
                    data.RelacionFechaInicio = dbManager.DataReader.SafeGetDateTime(77);
                    data.RelacionFechaVencim = dbManager.DataReader.SafeGetDateTime(78);
                    data.RelacionValor = dbManager.DataReader.GetDecimal(79);

                    data.SalvedadCausa = dbManager.DataReader.SafeGetString(80).ToUpper();

                    data.CopiaCedula = dbManager.DataReader.GetBoolean(81);
                    data.CopiaPasaporte = dbManager.DataReader.GetBoolean(82);
                    data.Antecedentes = dbManager.DataReader.GetBoolean(83);
                    data.CuentaBancaria = dbManager.DataReader.GetBoolean(84);
                    data.Planilla = dbManager.DataReader.GetBoolean(85);
                    data.CopiaRenta = dbManager.DataReader.GetBoolean(86);
                    data.CopiaCedulaConyugue = dbManager.DataReader.GetBoolean(87);
                    data.PartidasNacHijos = dbManager.DataReader.GetBoolean(88);
                    data.CalificacionArtesan = dbManager.DataReader.GetBoolean(89);
                    data.CertifDiscapacidad = dbManager.DataReader.GetBoolean(90);

                    data.Estado = dbManager.DataReader.SafeGetInt32(91);
                    data.Id = dbManager.DataReader.SafeGetInt32(92);

                    if (dbManager.DataReader.GetString(93) != string.Empty)
                    {
                        data.RutaFoto = dbManager.DataReader.GetString(93);
                        data.Foto = File.ReadAllBytes(data.RutaFoto);
                    }
                    data.EmailEmpresarial = dbManager.DataReader.GetString(94);
                    data.Ubicacion = dbManager.DataReader.GetString(95);
                    data.HojaVida = dbManager.DataReader.GetBoolean(96);
                }
            }
            catch (Exception ex)
            {
                GmasLogger.Instance.write("Method: " + MethodBase.GetCurrentMethod().DeclaringType.Name + " Error: " + ex.Message + "Stack Trace: " + ex.StackTrace.ToString(), "BalconGP");
            }
            finally
            {
                dbManager.Dispose();
            }
            return data;
        }

        public FormularioEmpleado GetFormularioEmpleado(string identificacion)
        {
            FormularioEmpleado data = new FormularioEmpleado();
            try
            {
                dbManager.Open();
                dbManager.Command.CommandTimeout = 0;
                string str = string.Format("SET LANGUAGE US_ENGLISH select top 100 f.NombresCompletos, f.Genero, " +
                    "TipoIdentificacion = f.TipoIdentificacion, f.Identificacion, f.LugarNac, f.FechaNac, f.Nacionalidad, " +
                    "f.EstadoCivil, f.NivelEducacion, f.Profesion, f.TipoLicencia, f.ProyectoObra, f.CiudadProyecto, " +
                    "f.Cargo, f.CarnetDiscapacitado, f.PaisResidencia, f.CiudadResidencia, f.Provincia, " +
                    "f.DireccionDomicilio, f.NumDireccion, f.CalleSecundaria, f.SectorReferencia, f.Edificio, f.Piso, " +
                    "f.TelefonoDomicilio, f.Celular, f.Email, f.ActivEconomicaPrincipal, f.ActivEconomicaAdicional, f.IngresosMensuales, f.GastosMensuales, " +

                    "f.ConyugNombres, f.ConyugGenero, f.ConyugTipoIdentificacion, f.ConyugIdentificacion, f.ConyugLugarNac, f.ConyugFechaNac, f.ConyugNacionalidad, " +
                    "f.ConyugTelefonoDom, f.ConyugCelular, f.ConyugEmail, f.ConyugPaisResidencia, f.ConyugCiudadResidencia, f.ConyugProvincia, " +
                    "f.ConyugNivelEducacion, f.ConyugProfesion, f.ConyugCargo, f.ConyugEmpresa, f.ConyugDireccionEmpresa, f.ConyugTelefonoEmpresa, " +
                    "f.ConyugActivEconomicaPrinc, f.ConyugActivEconomicaAdic, f.ConyugIngresosMensuales, f.ConyugGastosMensuales, " +

                    "f.IngresoActividadPrinc, f.IngresoOtrasActividades, f.DescripOtrasActiv, f.GastosEgresos, f.TotalActivos, f.TotalPasivos, f.TotalPatrimonios, f.OrigenFondos, f.DestinoFondos, " +

                    "f.PolitEsExpuesto, f.PolitCargo, f.PolitFechaNombram, f.PolitFechaCulmin, f.PolitFamiliarEsExpuesto, f.PolitParentParentesco, f.PolitParentIdentificacion, f.PolitParentNombresCompletos, f.PolitParentCargo, f.PolitParentFechaNombram, f.PolitParentFechaCulmin, " +

                    "f.RelacionComercial, f.RelacionDetalleObjeto, f.RelacionTipoContrato, f.RelacionFechaInicio, f.RelacionFechaVencim, f.RelacionValor, " +
                    "f.SalvedadCausa, " +

                    "convert(bit,(case when (CopiaCedula is not null) then '1' else '0' end)), convert(bit,(case when(CopiaPasaporte is not null) then '1' else '0' end)), " +
                    "convert(bit,(case when(Antecedentes is not null) then '1' else '0' end)), convert(bit,(case when(CuentaBancaria is not null) then '1' else '0' end)), " +
                    "convert(bit,(case when(Planilla is not null) then '1' else '0' end)), convert(bit,(case when(CopiaRenta is not null) then '1' else '0' end)), " +
                    "convert(bit,(case when(CopiaCedulaConyugue is not null) then '1' else '0' end)), convert(bit,(case when(PartidasNacHijos is not null) then '1' else '0' end)), " +
                    "convert(bit,(case when(CalificacionArtesan is not null) then '1' else '0' end)), convert(bit,(case when(CertifDiscapacidad is not null) then '1' else '0' end)), " +

                    "f.Estado, f.Id ," +

                    "convert(bit,(case when(HojaVida is not null) then '1' else '0' end))," +
                    "f.Ubicacion, " +
                    "isnull(fun.RutaFoto1,'') " +
                    "from [FormularioEmpleado] f " +
                    "left outer join [FormularioDocumentos] d on f.Id = d.IdFormularioEmpleado " +
                    "left outer join [Funcionario] fun on f.Identificacion = fun.IdentificacionFuncionario and fun.CodigoRelacion=1 " +
                    "where f.Estado = 0 and f.Identificacion = '{0}' ", identificacion);
                dbManager.ExecuteReader(CommandType.Text, str);
                while (dbManager.DataReader.Read())
                {
                    data.NombresCompletos = dbManager.DataReader.SafeGetString(0).ToUpper();
                    data.Genero = dbManager.DataReader.SafeGetInt32(1);
                    data.TipoIdentificacion = dbManager.DataReader.SafeGetInt32(2);
                    data.Identificacion = dbManager.DataReader.SafeGetString(3);
                    data.LugarNac = dbManager.DataReader.SafeGetString(4).ToUpper();
                    data.FechaNac = dbManager.DataReader.SafeGetDateTime(5);
                    data.Nacionalidad = dbManager.DataReader.SafeGetInt32(6);
                    data.EstadoCivil = dbManager.DataReader.SafeGetInt32(7);
                    data.NivelEducacion = dbManager.DataReader.SafeGetInt32(8);
                    data.Profesion = dbManager.DataReader.SafeGetString(9).ToUpper();
                    data.TipoLicencia = dbManager.DataReader.SafeGetInt32(10);
                    data.ProyectoObra = dbManager.DataReader.SafeGetString(11).ToUpper();
                    data.CiudadProyecto = dbManager.DataReader.SafeGetInt32(12);
                    data.Cargo = dbManager.DataReader.SafeGetInt32(13);
                    data.CarnetDiscapacitado = dbManager.DataReader.GetBoolean(14);
                    data.PaisResidencia = dbManager.DataReader.SafeGetString(15);
                    data.CiudadResidencia = dbManager.DataReader.SafeGetInt32(16);
                    data.Provincia = dbManager.DataReader.SafeGetInt32(17);
                    data.DireccionDomicilio = dbManager.DataReader.SafeGetString(18).ToUpper();
                    data.NumDireccion = dbManager.DataReader.SafeGetString(19);
                    data.CalleSecundaria = dbManager.DataReader.SafeGetString(20);
                    data.SectorReferencia = dbManager.DataReader.SafeGetString(21);
                    data.Edificio = dbManager.DataReader.SafeGetString(22);
                    data.Piso = dbManager.DataReader.SafeGetString(23);
                    data.TelefonoDomicilio = dbManager.DataReader.SafeGetString(24);
                    data.Celular = dbManager.DataReader.SafeGetString(25);
                    data.Email = dbManager.DataReader.SafeGetString(26);
                    data.ActivEconomicaPrincipal = dbManager.DataReader.SafeGetString(27);
                    data.ActivEconomicaAdicional = dbManager.DataReader.SafeGetString(28);
                    data.IngresosMensuales = dbManager.DataReader.GetDecimal(29);
                    data.GastosMensuales = dbManager.DataReader.GetDecimal(30);

                    data.ConyugNombres = dbManager.DataReader.SafeGetString(31).ToUpper();
                    data.ConyugGenero = dbManager.DataReader.SafeGetInt32(32);
                    data.ConyugTipoIdentificacion = dbManager.DataReader.SafeGetInt32(33);
                    data.ConyugIdentificacion = dbManager.DataReader.SafeGetString(34);
                    data.ConyugLugarNac = dbManager.DataReader.SafeGetString(35).ToUpper();
                    data.ConyugFechaNac = dbManager.DataReader.SafeGetDateTime(36);
                    data.ConyugNacionalidad = dbManager.DataReader.SafeGetInt32(37);
                    data.ConyugTelefonoDom = dbManager.DataReader.SafeGetString(38);
                    data.ConyugCelular = dbManager.DataReader.SafeGetString(39);
                    data.ConyugEmail = dbManager.DataReader.SafeGetString(40);
                    data.ConyugPaisResidencia = dbManager.DataReader.SafeGetString(41);
                    data.ConyugCiudadResidencia = dbManager.DataReader.SafeGetInt32(42);
                    data.ConyugProvincia = dbManager.DataReader.SafeGetInt32(43);
                    data.ConyugNivelEducacion = dbManager.DataReader.SafeGetInt32(44);
                    data.ConyugProfesion = dbManager.DataReader.SafeGetString(45).ToUpper();
                    data.ConyugCargo = dbManager.DataReader.SafeGetString(46).ToUpper();
                    data.ConyugEmpresa = dbManager.DataReader.SafeGetString(47).ToUpper();
                    data.ConyugDireccionEmpresa = dbManager.DataReader.SafeGetString(48).ToUpper();
                    data.ConyugTelefonoEmpresa = dbManager.DataReader.SafeGetString(49);
                    data.ConyugActivEconomicaPrinc = dbManager.DataReader.SafeGetString(50);
                    data.ConyugActivEconomicaAdic = dbManager.DataReader.SafeGetString(51);
                    data.ConyugIngresosMensuales = dbManager.DataReader.GetDecimal(52);
                    data.ConyugGastosMensuales = dbManager.DataReader.GetDecimal(53);

                    data.IngresoActividadPrinc = dbManager.DataReader.GetDecimal(54);
                    data.IngresoOtrasActividades = dbManager.DataReader.GetDecimal(55);
                    data.DescripOtrasActiv = dbManager.DataReader.SafeGetString(56).ToUpper();
                    data.GastosEgresos = dbManager.DataReader.GetDecimal(57);
                    data.TotalActivos = dbManager.DataReader.GetDecimal(58);
                    data.TotalPasivos = dbManager.DataReader.GetDecimal(59);
                    data.TotalPatrimonios = dbManager.DataReader.GetDecimal(60);
                    data.OrigenFondos = dbManager.DataReader.SafeGetString(61).ToUpper();
                    data.DestinoFondos = dbManager.DataReader.SafeGetString(62).ToUpper();

                    data.PolitEsExpuesto = dbManager.DataReader.GetBoolean(63);
                    data.PolitCargo = dbManager.DataReader.SafeGetString(64).ToUpper();
                    data.PolitFechaNombram = dbManager.DataReader.SafeGetDateTime(65);
                    data.PolitFechaCulmin = dbManager.DataReader.SafeGetDateTime(66);
                    data.PolitFamiliarEsExpuesto = dbManager.DataReader.GetBoolean(67);
                    data.PolitParentParentesco = dbManager.DataReader.SafeGetInt32(68);
                    data.PolitParentIdentificacion = dbManager.DataReader.SafeGetString(69);
                    data.PolitParentNombresCompletos = dbManager.DataReader.SafeGetString(70).ToUpper();
                    data.PolitParentCargo = dbManager.DataReader.SafeGetString(71).ToUpper();
                    data.PolitParentFechaNombram = dbManager.DataReader.SafeGetDateTime(72);
                    data.PolitParentFechaCulmin = dbManager.DataReader.SafeGetDateTime(73);

                    data.RelacionComercial = dbManager.DataReader.SafeGetString(74).ToUpper();
                    data.RelacionDetalleObjeto = dbManager.DataReader.SafeGetString(75).ToUpper();
                    data.RelacionTipoContrato = dbManager.DataReader.SafeGetString(76).ToUpper();
                    data.RelacionFechaInicio = dbManager.DataReader.SafeGetDateTime(77);
                    data.RelacionFechaVencim = dbManager.DataReader.SafeGetDateTime(78);
                    data.RelacionValor = dbManager.DataReader.GetDecimal(79);

                    data.SalvedadCausa = dbManager.DataReader.SafeGetString(80).ToUpper();

                    data.CopiaCedula = dbManager.DataReader.GetBoolean(81);
                    data.CopiaPasaporte = dbManager.DataReader.GetBoolean(82);
                    data.Antecedentes = dbManager.DataReader.GetBoolean(83);
                    data.CuentaBancaria = dbManager.DataReader.GetBoolean(84);
                    data.Planilla = dbManager.DataReader.GetBoolean(85);
                    data.CopiaRenta = dbManager.DataReader.GetBoolean(86);
                    data.CopiaCedulaConyugue = dbManager.DataReader.GetBoolean(87);
                    data.PartidasNacHijos = dbManager.DataReader.GetBoolean(88);
                    data.CalificacionArtesan = dbManager.DataReader.GetBoolean(89);
                    data.CertifDiscapacidad = dbManager.DataReader.GetBoolean(90);

                    data.Estado = dbManager.DataReader.SafeGetInt32(91);
                    data.Id = dbManager.DataReader.SafeGetInt32(92);

                    data.HojaVida = dbManager.DataReader.GetBoolean(93);
                    data.Ubicacion = dbManager.DataReader.SafeGetString(94);
                    if (dbManager.DataReader.GetString(95) != string.Empty)
                    {
                        data.RutaFoto = dbManager.DataReader.GetString(95);
                        data.Foto = File.ReadAllBytes(data.RutaFoto);
                    }
                }
            }
            catch (Exception ex)
            {
                GmasLogger.Instance.write("Method: " + MethodBase.GetCurrentMethod().DeclaringType.Name + " Error: " + ex.Message + "Stack Trace: " + ex.StackTrace.ToString(), "BalconGP");
            }
            finally
            {
                dbManager.Dispose();
            }
            return data;
        }

        public int ExistsFormularioEmpleado(string identificacion)
        {
            int b = 0;
            try
            {
                dbManager.Open();
                dbManager.Command.CommandTimeout = 0;
                string str = "SET LANGUAGE US_ENGLISH select Id from [FormularioEmpleado] " +
                "where Identificacion = '" + identificacion + "' and Estado = 0 ";
                dbManager.ExecuteReader(CommandType.Text, str);
                while (dbManager.DataReader.Read())
                {
                    b = dbManager.DataReader.GetInt32(0);
                }
            }
            catch (Exception ex)
            {
                GmasLogger.Instance.write("Method: " + MethodBase.GetCurrentMethod().DeclaringType.Name + " Error: " + ex.Message + "Stack Trace: " + ex.StackTrace.ToString(), "BalconGP");
                b = 0;
            }
            finally
            {
                dbManager.Dispose();
            }
            return b;
        }

        public void InsertFormularioEmpleado(FormularioEmpleado data)
        {
            string content = JsonConvert.SerializeObject(data);
            try
            {
                dbManager.Open();
                dbManager.Command.CommandTimeout = 0;
                string str = string.Format("SET LANGUAGE US_ENGLISH EXEC gmas_FormularioEmpleadoActualizar '{0}'", content);
                dbManager.ExecuteNonQuery(CommandType.Text, str);
            }
            catch (Exception ex)
            {
                GmasLogger.Instance.write("Method: " + MethodBase.GetCurrentMethod().DeclaringType.Name + " Error: " + ex.Message + " JSON: " + content, "BalconGP");
            }
            finally
            {
                dbManager.Dispose();
            }
        }

        public void UpdateFormularioEmpleado(FormularioEmpleado data)
        {
            string content = JsonConvert.SerializeObject(data);
            try
            {
                dbManager.Open();
                dbManager.Command.CommandTimeout = 0;
                string str = String.Format("SET LANGUAGE US_ENGLISH EXEC gmas_FormularioEmpleadoActualizar '{0}'", content);

                dbManager.ExecuteNonQuery(CommandType.Text, str);


            }
            catch (Exception ex)
            {
                GmasLogger.Instance.write("Method: " + MethodBase.GetCurrentMethod().DeclaringType.Name + " Error: " + ex.Message + " JSON " + content, "BalconGP");
            }
            finally
            {
                dbManager.Dispose();
            }
        }

        public void CerrarFormularioEmpleado(string identificacion)
        {
            try
            {
                dbManager.Open();
                dbManager.Command.CommandTimeout = 0;
                string str = "SET LANGUAGE US_ENGLISH update FormularioEmpleado set Estado = 1, FechaCierre = getdate() " +
                    "where Identificacion = '" + identificacion + "' " +
                    "and(Estado IS NULL OR Estado = 0)";
                var v = dbManager.ExecuteNonQuery(CommandType.Text, str);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                dbManager.Dispose();
            }
        }

        public List<SummaryFormEmpleado> GetSummaryFormEmpleado(string identificacion, int id)
        {
            List<SummaryFormEmpleado> list = new List<SummaryFormEmpleado>();
            try
            {
                dbManager.Open();
                dbManager.Command.CommandTimeout = 0;
                string str = "sp_Get_FormularioEmpleadoEdidar";
                dbManager.CreateParameters(1);
                dbManager.AddParameters(0, "@Identificacion", identificacion);

                dbManager.ExecuteReader(CommandType.StoredProcedure, str);
                while (dbManager.DataReader.Read())
                {
                    SummaryFormEmpleado c = new SummaryFormEmpleado();
                    c.Id = dbManager.DataReader.SafeGetInt32(0);
                    c.IdForm = dbManager.DataReader.SafeGetString(1);
                    c.TipoIdentificacion = dbManager.DataReader.SafeGetString(2);
                    c.Identificacion = dbManager.DataReader.SafeGetString(3);
                    c.NombresCompletos = dbManager.DataReader.SafeGetString(4).ToUpper();
                    c.CiudadResidencia = dbManager.DataReader.SafeGetString(5).ToUpper();
                    c.FechaHoraTrx = dbManager.DataReader.SafeGetDateTime(6);
                    c.Estado = dbManager.DataReader.SafeGetString(7);
                    list.Add(c);
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                dbManager.Dispose();
            }
            return list;
        }


        #endregion

        #region Inmuebles
        public List<FormularioInmuebles> ListFormularioInmuebles(int id)
        {
            List<FormularioInmuebles> list = new List<FormularioInmuebles>();
            try
            {
                dbManager.Open();
                dbManager.Command.CommandTimeout = 0;
                string str = "SET LANGUAGE US_ENGLISH select Id, IdFormularioEmpleado, Tipo, AvaluoComercial " +
                    "from FormularioInmuebles " +
                    "where IdFormularioEmpleado in(" + id.ToString() + ") ";
                dbManager.ExecuteReader(CommandType.Text, str);
                while (dbManager.DataReader.Read())
                {
                    FormularioInmuebles c = new FormularioInmuebles();
                    c.Id = dbManager.DataReader.SafeGetInt32(0);
                    c.IdFormularioEmpleado = dbManager.DataReader.SafeGetInt32(1);
                    c.Tipo = dbManager.DataReader.SafeGetString(2);
                    c.AvaluoComercial = dbManager.DataReader.GetDecimal(3);
                    list.Add(c);
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                dbManager.Dispose();
            }
            return list;
        }

        public void InsertInmuebles(FormularioInmuebles data, string identificacion)
        {
            try
            {
                dbManager.Open();
                dbManager.Command.CommandTimeout = 0;
                //string str = "if (not exists(select Id from [FormularioInmuebles] where IdFormularioEmpleado in (select Id from FormularioEmpleado where Identificacion = '" + identificacion + "' and convert(date,FechaHoraTrx) = convert(date,getdate()) )) )  insert into [FormularioInmuebles] (IdFormularioEmpleado,Tipo,AvaluoComercial) " +
                string str = "SET LANGUAGE US_ENGLISH insert into [FormularioInmuebles] (IdFormularioEmpleado,Tipo,AvaluoComercial) " +
                    "values( (select Id from FormularioEmpleado where Identificacion = '" + identificacion + "' and Estado = 0 ) , '" + data.Tipo + "', " + data.AvaluoComercial.ToString().Replace(',', '.') + ")";
                dbManager.ExecuteNonQuery(CommandType.Text, str);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                dbManager.Dispose();
            }
        }

        public void UpdateInmuebles(FormularioInmuebles data)
        {
            try
            {
                dbManager.Open();
                dbManager.Command.CommandTimeout = 0;
                string str = "SET LANGUAGE US_ENGLISH update [FormularioInmuebles] set " +
                    "Tipo = '" + data.Tipo.ToString() + "', " +
                    "AvaluoComercial = " + data.AvaluoComercial.ToString().ToString().Replace(',', '.') + " " +
                    "where Id = " + data.Id.ToString() + " ";
                dbManager.ExecuteNonQuery(CommandType.Text, str);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                dbManager.Dispose();
            }
        }
        public List<FormularioInmuebles> ListFormularioInmueblesById(int id)
        {
            List<FormularioInmuebles> list = new List<FormularioInmuebles>();
            try
            {
                dbManager.Open();
                dbManager.Command.CommandTimeout = 0;
                string str = "SET LANGUAGE US_ENGLISH select Id, IdFormularioEmpleado, Tipo, AvaluoComercial " +
                    "from FormularioInmuebles " +
                    "where IdFormularioEmpleado in(" + id.ToString() + ") ";
                dbManager.ExecuteReader(CommandType.Text, str);
                while (dbManager.DataReader.Read())
                {
                    FormularioInmuebles c = new FormularioInmuebles();
                    c.Id = dbManager.DataReader.SafeGetInt32(0);
                    c.IdFormularioEmpleado = dbManager.DataReader.SafeGetInt32(1);
                    c.Tipo = dbManager.DataReader.SafeGetString(2);
                    c.AvaluoComercial = dbManager.DataReader.GetDecimal(3);
                    list.Add(c);
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                dbManager.Dispose();
            }
            return list;
        }

        public void DeleteInmuebles(int Codigo)
        {
            try
            {
                dbManager.Open();
                dbManager.Command.CommandTimeout = 0;
                string str = "SET LANGUAGE US_ENGLISH delete from FormularioInmuebles where Id=" + Codigo;
                dbManager.ExecuteNonQuery(CommandType.Text, str);
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                dbManager.Dispose();
            }
        }
        #endregion

        #region Vehiculos
        public List<FormularioVehiculos> ListFormularioVehiculos(int id)
        {
            List<FormularioVehiculos> list = new List<FormularioVehiculos>();
            try
            {
                dbManager.Open();
                dbManager.Command.CommandTimeout = 0;
                string str = "SET LANGUAGE US_ENGLISH select Id, IdFormularioEmpleado, Marca, Modelo, Anio, Valor " +
                    "from FormularioVehiculos " +
                    "where IdFormularioEmpleado in(" + id.ToString() + " ) ";
                dbManager.ExecuteReader(CommandType.Text, str);
                while (dbManager.DataReader.Read())
                {
                    FormularioVehiculos c = new FormularioVehiculos();
                    c.Id = dbManager.DataReader.SafeGetInt32(0);
                    c.IdFormularioEmpleado = dbManager.DataReader.SafeGetInt32(1);
                    c.Marca = dbManager.DataReader.SafeGetString(2).ToUpper();
                    c.Modelo = dbManager.DataReader.SafeGetString(3).ToUpper();
                    c.Anio = dbManager.DataReader.SafeGetInt32(4);
                    c.Valor = dbManager.DataReader.GetDecimal(5);
                    list.Add(c);
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                dbManager.Dispose();
            }
            return list;
        }

        public void InsertVehiculos(FormularioVehiculos data, string identificacion)
        {
            try
            {
                dbManager.Open();
                dbManager.Command.CommandTimeout = 0;
                //string str = "if (not exists(select Id from [FormularioVehiculos] where IdFormularioEmpleado in (select Id from FormularioEmpleado where Identificacion = '" + identificacion + "' and convert(date,FechaHoraTrx) = convert(date,getdate()) )) )  insert into [FormularioVehiculos] (IdFormularioEmpleado, Marca, Modelo, Anio, Valor) " +
                string str = "SET LANGUAGE US_ENGLISH insert into [FormularioVehiculos] (IdFormularioEmpleado, Marca, Modelo, Anio, Valor) " +
                "values( (select Id from FormularioEmpleado where Identificacion = '" + identificacion + "' and Estado = 0 ) , '" + data.Marca.ToUpper() + "', " +
                    "'" + data.Modelo.ToUpper() + "', " + data.Anio.ToString() + ", " + data.Valor.ToString().ToString().Replace(',', '.') + ")";
                dbManager.ExecuteNonQuery(CommandType.Text, str);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                dbManager.Dispose();
            }
        }

        public void UpdateVehiculos(FormularioVehiculos data)
        {
            try
            {
                dbManager.Open();
                dbManager.Command.CommandTimeout = 0;
                string str = "SET LANGUAGE US_ENGLISH update [FormularioVehiculos] set " +
                    "Marca = '" + data.Marca.ToUpper() + "', " +
                    "Modelo = '" + data.Modelo.ToUpper() + "', " +
                    "Anio = " + data.Anio.ToString() + ", " +
                    "Valor = " + data.Valor.ToString().Replace(',', '.') + " " +
                    "where Id = " + data.Id.ToString() + "; ";
                dbManager.ExecuteNonQuery(CommandType.Text, str);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                dbManager.Dispose();
            }
        }

        public List<FormularioVehiculos> ListFormularioVehiculosById(int id)
        {
            List<FormularioVehiculos> list = new List<FormularioVehiculos>();
            try
            {
                dbManager.Open();
                dbManager.Command.CommandTimeout = 0;
                string str = "SET LANGUAGE US_ENGLISH select Id, IdFormularioEmpleado, Marca, Modelo, Anio, Valor " +
                    "from FormularioVehiculos " +
                    "where IdFormularioEmpleado in(" + id.ToString() + ") ";
                dbManager.ExecuteReader(CommandType.Text, str);
                while (dbManager.DataReader.Read())
                {
                    FormularioVehiculos c = new FormularioVehiculos();
                    c.Id = dbManager.DataReader.SafeGetInt32(0);
                    c.IdFormularioEmpleado = dbManager.DataReader.SafeGetInt32(1);
                    c.Marca = dbManager.DataReader.SafeGetString(2).ToUpper();
                    c.Modelo = dbManager.DataReader.SafeGetString(3).ToUpper();
                    c.Anio = dbManager.DataReader.SafeGetInt32(4);
                    c.Valor = dbManager.DataReader.GetDecimal(5);
                    list.Add(c);
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                dbManager.Dispose();
            }
            return list;
        }

        public void DeleteVehiculos(int Codigo)
        {
            try
            {
                dbManager.Open();
                dbManager.Command.CommandTimeout = 0;
                string str = "SET LANGUAGE US_ENGLISH delete from FormularioVehiculos where Id=" + Codigo;
                dbManager.ExecuteNonQuery(CommandType.Text, str);
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                dbManager.Dispose();
            }
        }
        #endregion

        #region Muebles y Enseres
        public FormularioMueblesEnseres ObtenerFormularioMueblesEnseres(int id)
        {
            FormularioMueblesEnseres c = new FormularioMueblesEnseres();
            try
            {
                dbManager.Open();
                dbManager.Command.CommandTimeout = 0;
                string str = "SET LANGUAGE US_ENGLISH select Id, IdFormularioEmpleado, isnull(Tipo,''), isnull(Valor,0.00) " +
                    "from [FormularioMueblesEnseres] " +
                    "where IdFormularioEmpleado in(" + id.ToString() + " ) ";
                dbManager.ExecuteReader(CommandType.Text, str);
                while (dbManager.DataReader.Read())
                {
                    c.Id = dbManager.DataReader.SafeGetInt32(0);
                    c.IdFormularioEmpleado = dbManager.DataReader.SafeGetInt32(1);
                    c.Tipo = dbManager.DataReader.SafeGetString(2);
                    c.Valor = dbManager.DataReader.GetDecimal(3);

                }
            }
            catch (Exception ex)
            {
                GmasLogger.Instance.write("Method: " + MethodBase.GetCurrentMethod().DeclaringType.Name + " Error: " + ex.Message + "Stack Trace: " + ex.StackTrace.ToString(), "BalconGP");
            }
            finally
            {
                dbManager.Dispose();
            }
            return c;
        }

        public void InsertMueblesEnseres(FormularioMueblesEnseres data, string identificacion)
        {
            try
            {
                dbManager.Open();
                dbManager.Command.CommandTimeout = 0;

                string str = "SET LANGUAGE US_ENGLISH insert into [FormularioMueblesEnseres] (IdFormularioEmpleado, Tipo, Valor) " +
                    "values( (select Id from FormularioEmpleado where Identificacion = '" + identificacion + "' and Estado = 0 ) , '" + data.Tipo + "'," +
                    " " + data.Valor.ToString().Replace(',', '.') + ")";
                dbManager.ExecuteNonQuery(CommandType.Text, str);
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

        public void UpdateMueblesEnseres(FormularioMueblesEnseres data)
        {
            try
            {
                dbManager.Open();
                dbManager.Command.CommandTimeout = 0;
                string str = "SET LANGUAGE US_ENGLISH " +
                    "DECLARE @EXISTE INT=0 SELECT @EXISTE=Id FROM FormularioMueblesEnseres WHERE IdFormularioEmpleado=" + data.IdFormularioEmpleado +
                    "if (@EXISTE>0) begin" +
                    " update [FormularioMueblesEnseres] set " +
                    " Tipo = '" + data.Tipo.ToString() + "', " +
                    " Valor = " + data.Valor.ToString().ToString().Replace(',', '.') + " " +
                    " where IdFormularioEmpleado = " + data.IdFormularioEmpleado.ToString() + " END " +
                    " ELSE BEGIN " +
                    " insert into [FormularioMueblesEnseres] (IdFormularioEmpleado, Tipo, Valor) " +
                      "values(" + data.IdFormularioEmpleado.ToString() + ",'" + data.Tipo + "'," + data.Valor.ToString().Replace(',', '.') + ")" +
                    " END";
                dbManager.ExecuteNonQuery(CommandType.Text, str);
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

        public void DeleteMueblesEnseres(int Codigo)
        {
            try
            {
                dbManager.Open();
                dbManager.Command.CommandTimeout = 0;
                string str = "SET LANGUAGE US_ENGLISH delete from FormularioMueblesEnseres where Id=" + Codigo;
                dbManager.ExecuteNonQuery(CommandType.Text, str);
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                dbManager.Dispose();
            }
        }

        public List<FormularioMueblesEnseres> ListFormularioMueblesEnseresById(int id)
        {
            List<FormularioMueblesEnseres> list = new List<FormularioMueblesEnseres>();
            try
            {
                dbManager.Open();
                dbManager.Command.CommandTimeout = 0;
                string str = "SET LANGUAGE US_ENGLISH select Id, IdFormularioEmpleado, Tipo, Valor " +
                    "from [FormularioMueblesEnseres] " +
                    "where IdFormularioEmpleado in(" + id.ToString() + ") ";
                dbManager.ExecuteReader(CommandType.Text, str);
                while (dbManager.DataReader.Read())
                {
                    FormularioMueblesEnseres c = new FormularioMueblesEnseres();
                    c.Id = dbManager.DataReader.SafeGetInt32(0);
                    c.IdFormularioEmpleado = dbManager.DataReader.SafeGetInt32(1);
                    c.Tipo = dbManager.DataReader.SafeGetString(2);
                    c.Valor = dbManager.DataReader.GetDecimal(3);
                    list.Add(c);
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                dbManager.Dispose();
            }
            return list;
        }
        #endregion

        #region Inversiones
        public FormularioInversiones ObtenerFormularioInversiones(int id)
        {
            FormularioInversiones c = new FormularioInversiones();
            try
            {
                dbManager.Open();
                dbManager.Command.CommandTimeout = 0;
                string str = "SET LANGUAGE US_ENGLISH select Id, IdFormularioEmpleado, isnull(TipoInversion,''), isnull(NumOperacion,0), isnull(Institucion,''), isnull(Valor,0.00) " +
                    "from [FormularioInversiones] " +
                    "where IdFormularioEmpleado in(" + id.ToString() + " ) ";
                dbManager.ExecuteReader(CommandType.Text, str);
                while (dbManager.DataReader.Read())
                {
                    c.Id = dbManager.DataReader.SafeGetInt32(0);
                    c.IdFormularioEmpleado = dbManager.DataReader.SafeGetInt32(1);
                    c.TipoInversion = dbManager.DataReader.SafeGetString(2);
                    c.NumOperacion = dbManager.DataReader.SafeGetInt32(3);
                    c.Institucion = dbManager.DataReader.SafeGetString(4).ToUpper();
                    c.Valor = dbManager.DataReader.GetDecimal(5);
                }
            }
            catch (Exception ex)
            {
                GmasLogger.Instance.write("Method: " + MethodBase.GetCurrentMethod().DeclaringType.Name + " Error: " + ex.Message + "Stack Trace: " + ex.StackTrace.ToString(), "BalconGP");
            }
            finally
            {
                dbManager.Dispose();
            }
            return c;
        }
        public void InsertInversiones(FormularioInversiones data, string identificacion)
        {
            try
            {
                dbManager.Open();
                dbManager.Command.CommandTimeout = 0;
                //string str = "if (not exists(select Id from [FormularioInversiones] where IdFormularioEmpleado in (select Id from FormularioEmpleado where Identificacion = '" + identificacion + "' and convert(date,FechaHoraTrx) = convert(date,getdate()) )) )  insert into [FormularioInversiones] (IdFormularioEmpleado, TipoInversion, NumOperacion, Institucion, Valor) " +
                string str = "SET LANGUAGE US_ENGLISH insert into [FormularioInversiones] (IdFormularioEmpleado, TipoInversion, NumOperacion, Institucion, Valor) " +
                    "values( (select Id from FormularioEmpleado where Identificacion = '" + identificacion + "' and Estado = 0 ) , '" + data.TipoInversion + "'," +
                    " " + data.NumOperacion + ", '" + data.Institucion.ToUpper() + "', " + data.Valor.ToString().Replace(',', '.') + ")";
                dbManager.ExecuteNonQuery(CommandType.Text, str);
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

        public void UpdateInversiones(FormularioInversiones data)
        {
            try
            {
                dbManager.Open();
                dbManager.Command.CommandTimeout = 0;
                string str = "SET LANGUAGE US_ENGLISH " +
                   "DECLARE @EXISTE INT=0 SELECT @EXISTE=Id FROM FormularioInversiones WHERE IdFormularioEmpleado=" + data.IdFormularioEmpleado +
                   " if (@EXISTE>0) begin" +
                   " update [FormularioInversiones] set " +
                    "TipoInversion = '" + data.TipoInversion + "', " +
                    "NumOperacion = " + data.NumOperacion.ToString() + ", " +
                    "Institucion = '" + data.Institucion.ToUpper() + "', " +
                    "Valor = " + data.Valor.ToString().ToString().Replace(',', '.') + " " +
                    "where IdFormularioEmpleado = " + data.IdFormularioEmpleado.ToString() + " END " +
                   " ELSE BEGIN " +
                   " insert into [FormularioInversiones] (IdFormularioEmpleado, Valor) " +
                     "values(" + data.IdFormularioEmpleado.ToString() + "," + data.Valor.ToString().Replace(',', '.') + ")" +
                   " END";

                dbManager.ExecuteNonQuery(CommandType.Text, str);
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
        public List<FormularioInversiones> ListFormularioInversiones(int id)
        {
            List<FormularioInversiones> list = new List<FormularioInversiones>();
            try
            {
                dbManager.Open();
                dbManager.Command.CommandTimeout = 0;
                string str = "SET LANGUAGE US_ENGLISH select Id, IdFormularioEmpleado, TipoInversion, NumOperacion, Institucion, Valor " +
                    "from [FormularioInversiones] " +
                    "where IdFormularioEmpleado in(" + id.ToString() + " ) ";
                dbManager.ExecuteReader(CommandType.Text, str);
                while (dbManager.DataReader.Read())
                {
                    FormularioInversiones c = new FormularioInversiones();
                    c.Id = dbManager.DataReader.SafeGetInt32(0);
                    c.IdFormularioEmpleado = dbManager.DataReader.SafeGetInt32(1);
                    c.TipoInversion = dbManager.DataReader.SafeGetString(2);
                    c.NumOperacion = dbManager.DataReader.SafeGetInt32(3);
                    c.Institucion = dbManager.DataReader.SafeGetString(4).ToUpper();
                    c.Valor = dbManager.DataReader.GetDecimal(5);
                    list.Add(c);
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                dbManager.Dispose();
            }
            return list;
        }

        public void DeleteInversiones(int Codigo)
        {
            try
            {
                dbManager.Open();
                dbManager.Command.CommandTimeout = 0;
                string str = "SET LANGUAGE US_ENGLISH delete from FormularioInversiones where Id=" + Codigo;
                dbManager.ExecuteNonQuery(CommandType.Text, str);
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                dbManager.Dispose();
            }
        }
        public List<FormularioInversiones> ListFormularioInversionesById(int id)
        {
            List<FormularioInversiones> list = new List<FormularioInversiones>();
            try
            {
                dbManager.Open();
                dbManager.Command.CommandTimeout = 0;
                string str = "SET LANGUAGE US_ENGLISH select Id, IdFormularioEmpleado, TipoInversion, NumOperacion, Institucion, Valor " +
                    "from [FormularioInversiones] " +
                    "where IdFormularioEmpleado in(" + id.ToString() + ") ";
                dbManager.ExecuteReader(CommandType.Text, str);
                while (dbManager.DataReader.Read())
                {
                    FormularioInversiones c = new FormularioInversiones();
                    c.Id = dbManager.DataReader.SafeGetInt32(0);
                    c.IdFormularioEmpleado = dbManager.DataReader.SafeGetInt32(1);
                    c.TipoInversion = dbManager.DataReader.SafeGetString(2).ToUpper();
                    c.NumOperacion = dbManager.DataReader.SafeGetInt32(3);
                    c.Institucion = dbManager.DataReader.SafeGetString(4).ToUpper();
                    c.Valor = dbManager.DataReader.GetDecimal(5);
                    list.Add(c);
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                dbManager.Dispose();
            }
            return list;
        }

        #endregion

        #region Dinero en efectivo , Bancos y otros
        public FormularioDineroBanco ObtenerFormularioDineroBanco(int id)
        {
            FormularioDineroBanco c = new FormularioDineroBanco();
            try
            {
                dbManager.Open();
                dbManager.Command.CommandTimeout = 0;
                string str = "SET LANGUAGE US_ENGLISH select Id, IdFormularioEmpleado, isnull(Banco,0.00),isnull(Efectivo,0.00) " +
                    "from [FormularioDineroBanco]  " +
                    "where IdFormularioEmpleado in(" + id.ToString() + " ) ";
                dbManager.ExecuteReader(CommandType.Text, str);
                while (dbManager.DataReader.Read())
                {

                    c.Id = dbManager.DataReader.SafeGetInt32(0);
                    c.IdFormularioEmpleado = dbManager.DataReader.SafeGetInt32(1);
                    c.Banco = dbManager.DataReader.GetDecimal(2);
                    c.Efectivo = dbManager.DataReader.GetDecimal(3);

                }
            }
            catch (Exception ex)
            {
                GmasLogger.Instance.write("Method: " + MethodBase.GetCurrentMethod().DeclaringType.Name + " Error: " + ex.Message + "Stack Trace: " + ex.StackTrace.ToString(), "BalconGP");
            }
            finally
            {
                dbManager.Dispose();
            }
            return c;
        }

        public List<FormularioDineroBanco> ListFormularioDineroBancoById(int id)
        {
            List<FormularioDineroBanco> list = new List<FormularioDineroBanco>();
            try
            {
                dbManager.Open();
                dbManager.Command.CommandTimeout = 0;
                string str = "SET LANGUAGE US_ENGLISH select *" +
                    "from [FormularioDineroBanco]  " +
                    "where IdFormularioEmpleado in(" + id.ToString() + ") ";
                dbManager.ExecuteReader(CommandType.Text, str);
                while (dbManager.DataReader.Read())
                {
                    FormularioDineroBanco c = new FormularioDineroBanco();
                    c.Id = dbManager.DataReader.SafeGetInt32(0);
                    c.IdFormularioEmpleado = dbManager.DataReader.SafeGetInt32(1);
                    c.Banco = dbManager.DataReader.GetDecimal(2);
                    c.Efectivo = dbManager.DataReader.GetDecimal(3);
                    list.Add(c);
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                dbManager.Dispose();
            }
            return list;
        }
        public void InsertDineroBanco(FormularioDineroBanco data, string identificacion)
        {
            try
            {
                dbManager.Open();
                dbManager.Command.CommandTimeout = 0;
                string str = "SET LANGUAGE US_ENGLISH insert into [FormularioDineroBanco] (IdFormularioEmpleado, Banco,Efectivo) " +
                    "values( (select Id from FormularioEmpleado where Identificacion = '" + identificacion + "' and Estado = 0 ) , " +
                     data.Banco.ToString().Replace(',', '.') + "," +
                     data.Efectivo.ToString().Replace(',', '.') + ")";
                dbManager.ExecuteNonQuery(CommandType.Text, str);
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

        public void UpdateDineroBanco(FormularioDineroBanco data)
        {
            try
            {
                dbManager.Open();
                dbManager.Command.CommandTimeout = 0;

                string str = "SET LANGUAGE US_ENGLISH " +
                  "DECLARE @EXISTE INT=0 SELECT @EXISTE=Id FROM FormularioDineroBanco WHERE IdFormularioEmpleado=" + data.IdFormularioEmpleado +
                  " if (@EXISTE>0) begin" +
                  " update [FormularioDineroBanco] set " +
                   " Banco = " + data.Banco.ToString().ToString().Replace(',', '.') + ", " +
                   " Efectivo = " + data.Efectivo.ToString().ToString().Replace(',', '.') +
                   " where IdFormularioEmpleado = " + data.IdFormularioEmpleado.ToString() + " END " +
                  " ELSE BEGIN " +
                  " insert into [FormularioDineroBanco] (IdFormularioEmpleado, Banco,Efectivo) " +
                    "values(" + data.IdFormularioEmpleado.ToString() + "," + data.Banco.ToString().Replace(',', '.') + "," + data.Efectivo.ToString().Replace(',', '.') + ")" +
                  " END";
                dbManager.ExecuteNonQuery(CommandType.Text, str);
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

        public void DeleteDineroBanco(int Codigo)
        {
            try
            {
                dbManager.Open();
                dbManager.Command.CommandTimeout = 0;
                string str = "SET LANGUAGE US_ENGLISH delete from FormularioDineroBanco where Id=" + Codigo;
                dbManager.ExecuteNonQuery(CommandType.Text, str);
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                dbManager.Dispose();
            }
        }

        #endregion

        #region Cuentas por Cobrar
        public List<FormularioCxC> ListFormularioCxC(int id)
        {
            List<FormularioCxC> list = new List<FormularioCxC>();
            try
            {
                dbManager.Open();
                dbManager.Command.CommandTimeout = 0;
                string str = "SET LANGUAGE US_ENGLISH select Id, IdFormularioEmpleado, Deudor, Monto, FechaVencimiento " +
                    "from [FormularioCuentasXCobrar] " +
                   "where IdFormularioEmpleado in(" + id.ToString() + " ) ";
                dbManager.ExecuteReader(CommandType.Text, str);
                while (dbManager.DataReader.Read())
                {
                    FormularioCxC c = new FormularioCxC();
                    c.Id = dbManager.DataReader.SafeGetInt32(0);
                    c.IdFormularioEmpleado = dbManager.DataReader.SafeGetInt32(1);
                    c.Deudor = dbManager.DataReader.SafeGetString(2).ToUpper();
                    c.Monto = dbManager.DataReader.GetDecimal(3);
                    c.FechaVencimiento = dbManager.DataReader.SafeGetDateTime(4);
                    list.Add(c);
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                dbManager.Dispose();
            }
            return list;
        }

        public void InsertCxC(FormularioCxC data, string identificacion)
        {
            try
            {
                dbManager.Open();
                dbManager.Command.CommandTimeout = 0;
                //string str = "if (not exists(select Id from [FormularioCuentasXCobrar] where IdFormularioEmpleado in (select Id from FormularioEmpleado where Identificacion = '" + identificacion + "' and convert(date,FechaHoraTrx) = convert(date,getdate()) )) )  insert into [FormularioCuentasXCobrar] (IdFormularioEmpleado, Deudor, Monto, FechaVencimiento) " +
                string str = "SET LANGUAGE US_ENGLISH insert into [FormularioCuentasXCobrar] (IdFormularioEmpleado, Deudor, Monto, FechaVencimiento) " +
                    "values( (select Id from FormularioEmpleado where Identificacion = '" + identificacion + "' and Estado = 0 ) , '" + data.Deudor.ToUpper() + "'," +
                    " " + data.Monto.ToString().Replace(',', '.') + ", '" + data.FechaVencimiento.ToString("yyyy-MM-dd") + "')";
                //data.ConyugFechaNac.ToString("yyyy-MM-dd")
                dbManager.ExecuteNonQuery(CommandType.Text, str);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                dbManager.Dispose();
            }
        }

        public void UpdateCxC(FormularioCxC data)
        {
            try
            {
                dbManager.Open();
                dbManager.Command.CommandTimeout = 0;
                string str = "SET LANGUAGE US_ENGLISH update [FormularioCuentasXCobrar] set " +
                    "Deudor = '" + data.Deudor.ToUpper() + "', " +
                    "Monto = '" + data.Monto.ToString().Replace(',', '.') + "', " +
                    "FechaVencimiento = '" + data.FechaVencimiento.ToString("yyyy-MM-dd") + "' " +
                    "where Id = " + data.Id.ToString() + "; ";
                dbManager.ExecuteNonQuery(CommandType.Text, str);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                dbManager.Dispose();
            }
        }

        public List<FormularioCxC> ListFormularioCxCById(int id)
        {
            List<FormularioCxC> list = new List<FormularioCxC>();
            try
            {
                dbManager.Open();
                dbManager.Command.CommandTimeout = 0;
                string str = "SET LANGUAGE US_ENGLISH select Id, IdFormularioEmpleado, Deudor, Monto, FechaVencimiento " +
                    "from [FormularioCuentasXCobrar] " +
                    "where IdFormularioEmpleado in(" + id.ToString() + ") ";
                dbManager.ExecuteReader(CommandType.Text, str);
                while (dbManager.DataReader.Read())
                {
                    FormularioCxC c = new FormularioCxC();
                    c.Id = dbManager.DataReader.SafeGetInt32(0);
                    c.IdFormularioEmpleado = dbManager.DataReader.SafeGetInt32(1);
                    c.Deudor = dbManager.DataReader.SafeGetString(2).ToUpper();
                    c.Monto = dbManager.DataReader.GetDecimal(3);
                    c.FechaVencimiento = dbManager.DataReader.SafeGetDateTime(4);
                    list.Add(c);
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                dbManager.Dispose();
            }
            return list;
        }

        #endregion

        #region Cuentas por Pagar
        public FormularioCxP ObtenerFormularioCxP(int id)
        {
            FormularioCxP c = new FormularioCxP();
            try
            {
                dbManager.Open();
                dbManager.Command.CommandTimeout = 0;
                string str = "SET LANGUAGE US_ENGLISH select Id, IdFormularioEmpleado, isnull(TarjetaCredito,0.00), isnull(Prestamo,0.00), isnull(Otros,0.00) from [FormularioCuentasXPagar] " +
                    "where IdFormularioEmpleado in(" + id.ToString() + " ) ";
                dbManager.ExecuteReader(CommandType.Text, str);
                while (dbManager.DataReader.Read())
                {
                    c.Id = dbManager.DataReader.SafeGetInt32(0);
                    c.IdFormularioEmpleado = dbManager.DataReader.SafeGetInt32(1);
                    c.TarjetaCredito = dbManager.DataReader.GetDecimal(2);
                    c.Prestamo = dbManager.DataReader.GetDecimal(3);
                    c.Otros = dbManager.DataReader.GetDecimal(4);
                }
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
            return c;
        }

        public List<FormularioCxP> ListFormularioCxP(int id)
        {
            List<FormularioCxP> list = new List<FormularioCxP>();
            try
            {
                dbManager.Open();
                dbManager.Command.CommandTimeout = 0;
                string str = "SET LANGUAGE US_ENGLISH select Id, IdFormularioEmpleado, isnull(TarjetaCredito,0.00), isnull(Prestamo,0.00), isnull(Otros,0.00) from [FormularioCuentasXPagar] " +
                    "where IdFormularioEmpleado in(" + id.ToString() + " ) ";
                dbManager.ExecuteReader(CommandType.Text, str);
                while (dbManager.DataReader.Read())
                {
                    FormularioCxP c = new FormularioCxP();
                    c.Id = dbManager.DataReader.SafeGetInt32(0);
                    c.IdFormularioEmpleado = dbManager.DataReader.SafeGetInt32(1);
                    c.TarjetaCredito = dbManager.DataReader.GetDecimal(2);
                    c.Prestamo = dbManager.DataReader.GetDecimal(3);
                    c.Otros = dbManager.DataReader.GetDecimal(4);
                    list.Add(c);
                }
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
            return list;
        }

        public void InsertCxP(FormularioCxP data, string identificacion)
        {
            string content = JsonConvert.SerializeObject(data);
            try
            {
                dbManager.Open();
                dbManager.Command.CommandTimeout = 0;

                string str = "SET LANGUAGE US_ENGLISH insert into [FormularioCuentasXPagar] (IdFormularioEmpleado, TarjetaCredito, Prestamo, Otros) " +
                    "values( (select Id from FormularioEmpleado where Identificacion = '" + identificacion + "' and Estado = 0 ) , " +
                     data.TarjetaCredito.ToString().Replace(',', '.') + "," +
                     data.Prestamo.ToString().Replace(',', '.') + "," +
                    data.Otros.ToString().Replace(',', '.') + ")";
                dbManager.ExecuteNonQuery(CommandType.Text, str);
            }
            catch (Exception ex)
            {
                GmasLogger.Instance.write("Method: " + MethodBase.GetCurrentMethod().DeclaringType.Name + " Error: " + ex.Message + " JSON: " + content, "BalconGP");
            }
            finally
            {
                dbManager.Dispose();
            }
        }

        public void UpdateCxP(FormularioCxP data)
        {
            string content = JsonConvert.SerializeObject(data);
            try
            {
                dbManager.Open();
                dbManager.Command.CommandTimeout = 0;

                string str = "SET LANGUAGE US_ENGLISH " +
                 "DECLARE @EXISTE INT=0 SELECT @EXISTE=Id FROM FormularioCuentasXPagar WHERE IdFormularioEmpleado=" + data.IdFormularioEmpleado +
                 " if (@EXISTE>0) begin" +
                 " update [FormularioCuentasXPagar] set " +
                  " TarjetaCredito= " + data.TarjetaCredito.ToString().Replace(',', '.') + ", " +
                    " Prestamo= " + data.Prestamo.ToString().Replace(',', '.') + ", " +
                    " Otros= " + data.Otros.ToString().Replace(',', '.') +
                    " where IdFormularioEmpleado = " + data.IdFormularioEmpleado.ToString() + " END " +
                 " ELSE BEGIN " +
                 " insert into [FormularioCuentasXPagar] (IdFormularioEmpleado, TarjetaCredito,Prestamo,Otros) " +
                   " values(" + data.IdFormularioEmpleado.ToString() + ","
                   + data.TarjetaCredito.ToString().Replace(',', '.') + ","
                   + data.Prestamo.ToString().Replace(',', '.') + ","
                   + data.Otros.ToString().Replace(',', '.') + ")" +
                 " END";

                dbManager.ExecuteNonQuery(CommandType.Text, str);
            }
            catch (Exception ex)
            {
                GmasLogger.Instance.write("Method: " + MethodBase.GetCurrentMethod().DeclaringType.Name + " Error: " + ex.Message + " JSON: " + content, "BalconGP");
            }
            finally
            {
                dbManager.Dispose();
            }
        }

        public List<FormularioCxP> ListFormularioCxPById(int id)
        {
            List<FormularioCxP> list = new List<FormularioCxP>();
            try
            {
                dbManager.Open();
                dbManager.Command.CommandTimeout = 0;
                string str = "SET LANGUAGE US_ENGLISH select Id, IdFormularioEmpleado, ISNULL(TarjetaCredito,0.00), isnull(Prestamo,0.00), ISNULL(Otros,0.00) " +
                    "from [FormularioCuentasXPagar] " +
                    "where IdFormularioEmpleado in(" + id.ToString() + ") ";
                dbManager.ExecuteReader(CommandType.Text, str);
                while (dbManager.DataReader.Read())
                {
                    FormularioCxP c = new FormularioCxP();
                    c.Id = dbManager.DataReader.SafeGetInt32(0);
                    c.IdFormularioEmpleado = dbManager.DataReader.SafeGetInt32(1);

                    c.TarjetaCredito = dbManager.DataReader.GetDecimal(2);
                    c.Prestamo = dbManager.DataReader.GetDecimal(3);
                    c.Otros = dbManager.DataReader.GetDecimal(4);

                    list.Add(c);
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                dbManager.Dispose();
            }
            return list;
        }

        public void DeleteCxP(int Codigo)
        {
            try
            {
                dbManager.Open();
                dbManager.Command.CommandTimeout = 0;
                string str = "SET LANGUAGE US_ENGLISH delete from FormularioCuentasXPagar where Id=" + Codigo;
                dbManager.ExecuteNonQuery(CommandType.Text, str);
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                dbManager.Dispose();
            }
        }

        #endregion

        #region Referencias Personales
        public List<FormularioReferenciasPersonales> ListFormularioReferenciasPersonales(int id)
        {
            List<FormularioReferenciasPersonales> list = new List<FormularioReferenciasPersonales>();
            try
            {
                dbManager.Open();
                dbManager.Command.CommandTimeout = 0;
                string str = "SET LANGUAGE US_ENGLISH select Id, IdFormularioEmpleado, NombresCompletos, Parentesco, CelularTelefono " +
                    "from [FormularioReferenciasPersonales] " +
                    "where IdFormularioEmpleado in(" + id.ToString() + " ) ";
                dbManager.ExecuteReader(CommandType.Text, str);
                while (dbManager.DataReader.Read())
                {
                    FormularioReferenciasPersonales c = new FormularioReferenciasPersonales();
                    c.Id = dbManager.DataReader.SafeGetInt32(0);
                    c.IdFormularioEmpleado = dbManager.DataReader.SafeGetInt32(1);
                    c.NombresCompletos = dbManager.DataReader.SafeGetString(2).ToUpper();
                    c.CodigoRelacionFamiliar = dbManager.DataReader.SafeGetInt32(3);
                    c.CelularTelefono = dbManager.DataReader.SafeGetString(4);
                    list.Add(c);
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                dbManager.Dispose();
            }
            return list;
        }

        public void InsertReferenciasPersonales(FormularioReferenciasPersonales data, string identificacion)
        {
            try
            {
                dbManager.Open();
                dbManager.Command.CommandTimeout = 0;
                string str = "SET LANGUAGE US_ENGLISH insert into [FormularioReferenciasPersonales] (IdFormularioEmpleado, NombresCompletos, Parentesco, CelularTelefono) " +
                    "values( (select Id from FormularioEmpleado where Identificacion = '" + identificacion + "' and Estado = 0 ) , '" + data.NombresCompletos.ToUpper() + "'," +
                    " " + data.CodigoRelacionFamiliar.ToString() + ", '" + data.CelularTelefono + "')";
                dbManager.ExecuteNonQuery(CommandType.Text, str);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                dbManager.Dispose();
            }
        }

        public void UpdateReferenciasPersonales(FormularioReferenciasPersonales data)
        {
            try
            {
                dbManager.Open();
                dbManager.Command.CommandTimeout = 0;
                string str = "SET LANGUAGE US_ENGLISH update [FormularioReferenciasPersonales] set " +
                    "NombresCompletos = '" + data.NombresCompletos.ToUpper() + "', " +
                    "Parentesco = " + data.CodigoRelacionFamiliar.ToString() + ", " +
                    "CelularTelefono = '" + data.CelularTelefono + "' " +
                    "where Id = " + data.Id.ToString() + "; ";
                dbManager.ExecuteNonQuery(CommandType.Text, str);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                dbManager.Dispose();
            }
        }


        public List<FormularioReferenciasPersonales> ListFormularioReferenciasPersonalesById(int id)
        {
            List<FormularioReferenciasPersonales> list = new List<FormularioReferenciasPersonales>();
            try
            {
                dbManager.Open();
                dbManager.Command.CommandTimeout = 0;
                string str = "SET LANGUAGE US_ENGLISH select Id, IdFormularioEmpleado, NombresCompletos, Parentesco, CelularTelefono " +
                    "from [FormularioReferenciasPersonales] " +
                    "where IdFormularioEmpleado in(" + id.ToString() + ") ";
                dbManager.ExecuteReader(CommandType.Text, str);
                while (dbManager.DataReader.Read())
                {
                    FormularioReferenciasPersonales c = new FormularioReferenciasPersonales();
                    c.Id = dbManager.DataReader.SafeGetInt32(0);
                    c.IdFormularioEmpleado = dbManager.DataReader.SafeGetInt32(1);
                    c.NombresCompletos = dbManager.DataReader.SafeGetString(2).ToUpper();
                    c.CodigoRelacionFamiliar = dbManager.DataReader.SafeGetInt32(3);
                    c.CelularTelefono = dbManager.DataReader.SafeGetString(4);
                    list.Add(c);
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                dbManager.Dispose();
            }
            return list;
        }

        public void DeleteReferenciasPersonales(int Codigo)
        {
            try
            {
                dbManager.Open();
                dbManager.Command.CommandTimeout = 0;
                string str = "SET LANGUAGE US_ENGLISH delete from FormularioReferenciasPersonales where Id=" + Codigo;
                dbManager.ExecuteNonQuery(CommandType.Text, str);
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                dbManager.Dispose();
            }
        }
        #endregion

        #region Documentos
        public void UpdateFotografia(string Ruta, string identificacion)
        {
            try
            {
                dbManager.Open();
                dbManager.Command.CommandTimeout = 0;
                dbManager.CreateParameters(2);
                dbManager.AddParameters(0, "@Identificacion", identificacion);
                dbManager.AddParameters(1, "@RutaArchivo", Ruta);
                dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "sp_Update_FormularioFotografia");
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
        public void UpdateDocumentosHojaVida(string Ruta, string identificacion)
        {
            try
            {
                dbManager.Open();
                dbManager.Command.CommandTimeout = 0;
                dbManager.CreateParameters(2);
                dbManager.AddParameters(0, "@Identificacion", identificacion);
                dbManager.AddParameters(1, "@RutaArchivo", Ruta);
                dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "sp_Update_FormularioDocumentosHojaVida");
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

        public void UpdateDocumentosCedula(byte[] data, string identificacion)
        {
            try
            {
                dbManager.Open();
                dbManager.Command.CommandTimeout = 0;
                dbManager.CreateParameters(2);
                dbManager.AddParameters(0, "@Identificacion", identificacion);
                dbManager.AddParameters(1, "@data", data);
                dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "sp_Update_FormularioDocumentosCedula");
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                dbManager.Dispose();
            }
        }

        public void UpdateDocumentosPasaporte(byte[] data, string identificacion)
        {
            try
            {
                dbManager.Open();
                dbManager.Command.CommandTimeout = 0;
                dbManager.CreateParameters(2);
                dbManager.AddParameters(0, "@Identificacion", identificacion);
                dbManager.AddParameters(1, "@data", data);
                dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "sp_Update_FormularioDocumentosPasaporte");
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                dbManager.Dispose();
            }
        }

        public void UpdateDocumentosAntecedentes(byte[] data, string identificacion)
        {
            try
            {
                dbManager.Open();
                dbManager.Command.CommandTimeout = 0;
                dbManager.CreateParameters(2);
                dbManager.AddParameters(0, "@Identificacion", identificacion);
                dbManager.AddParameters(1, "@data", data);
                dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "sp_Update_FormularioDocumentosAntecedentes");
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                dbManager.Dispose();
            }
        }

        public void UpdateDocumentosCuentaBancaria(byte[] data, string identificacion)
        {
            try
            {
                dbManager.Open();
                dbManager.Command.CommandTimeout = 0;
                dbManager.CreateParameters(2);
                dbManager.AddParameters(0, "@Identificacion", identificacion);
                dbManager.AddParameters(1, "@data", data);
                dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "sp_Update_FormularioDocumentosCuentaBancaria");
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                dbManager.Dispose();
            }
        }

        public void UpdateDocumentosPlanilla(byte[] data, string identificacion)
        {
            try
            {
                dbManager.Open();
                dbManager.Command.CommandTimeout = 0;
                dbManager.CreateParameters(2);
                dbManager.AddParameters(0, "@Identificacion", identificacion);
                dbManager.AddParameters(1, "@data", data);
                dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "sp_Update_FormularioDocumentosPlanilla");
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                dbManager.Dispose();
            }
        }

        public void UpdateDocumentosRenta(byte[] data, string identificacion)
        {
            try
            {
                dbManager.Open();
                dbManager.Command.CommandTimeout = 0;
                dbManager.CreateParameters(2);
                dbManager.AddParameters(0, "@Identificacion", identificacion);
                dbManager.AddParameters(1, "@data", data);
                dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "sp_Update_FormularioDocumentosRenta");
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                dbManager.Dispose();
            }
        }

        public void UpdateDocumentosCedulaConyugue(byte[] data, string identificacion)
        {
            try
            {
                dbManager.Open();
                dbManager.Command.CommandTimeout = 0;
                dbManager.CreateParameters(2);
                dbManager.AddParameters(0, "@Identificacion", identificacion);
                dbManager.AddParameters(1, "@data", data);
                dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "sp_Update_FormularioDocumentosCedulaConyugue");
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                dbManager.Dispose();
            }
        }

        public void UpdateDocumentosPartidasNacHijos(byte[] data, string identificacion)
        {
            try
            {
                dbManager.Open();
                dbManager.Command.CommandTimeout = 0;
                dbManager.CreateParameters(2);
                dbManager.AddParameters(0, "@Identificacion", identificacion);
                dbManager.AddParameters(1, "@data", data);
                dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "sp_Update_FormularioDocumentosPartidasNacHijos");
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                dbManager.Dispose();
            }
        }

        public void UpdateDocumentosCalificacionArtesan(byte[] data, string identificacion)
        {
            try
            {
                dbManager.Open();
                dbManager.Command.CommandTimeout = 0;
                dbManager.CreateParameters(2);
                dbManager.AddParameters(0, "@Identificacion", identificacion);
                dbManager.AddParameters(1, "@data", data);
                dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "sp_Update_FormularioDocumentosCalificacionArtesan");
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                dbManager.Dispose();
            }
        }

        public void UpdateDocumentosCertifDiscapacidad(byte[] data, string identificacion)
        {
            try
            {
                dbManager.Open();
                dbManager.Command.CommandTimeout = 0;
                dbManager.CreateParameters(2);
                dbManager.AddParameters(0, "@Identificacion", identificacion);
                dbManager.AddParameters(1, "@data", data);
                dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "sp_Update_FormularioDocumentosCertifDiscapacidad");
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                dbManager.Dispose();
            }
        }

        public byte[] GetDocumentosCedula(string identificacion)
        {
            byte[] data = null;
            try
            {
                dbManager.Open();
                dbManager.Command.CommandTimeout = 0;
                string str = "SET LANGUAGE US_ENGLISH select CopiaCedula " +
                    "from [FormularioDocumentos] " +
                    "where IdFormularioEmpleado in(select Id from FormularioEmpleado where Identificacion = '" + identificacion + "' and Estado = 0 ) ";
                dbManager.ExecuteReader(CommandType.Text, str);
                while (dbManager.DataReader.Read())
                {
                    data = (byte[])dbManager.DataReader.GetValue(0);
                }
            }
            catch (Exception)
            {
                data = null;
            }
            finally
            {
                dbManager.Dispose();
            }
            return data;
        }

        public byte[] GetDocumentosPasaporte(string identificacion)
        {
            byte[] data = null;
            try
            {
                dbManager.Open();
                dbManager.Command.CommandTimeout = 0;
                string str = "SET LANGUAGE US_ENGLISH select CopiaPasaporte " +
                    "from [FormularioDocumentos] " +
                    "where IdFormularioEmpleado in(select Id from FormularioEmpleado where Identificacion = '" + identificacion + "' and Estado = 0 ) ";
                dbManager.ExecuteReader(CommandType.Text, str);
                while (dbManager.DataReader.Read())
                {
                    data = (byte[])dbManager.DataReader.GetValue(0);
                }
            }
            catch (Exception)
            {
                data = null;
            }
            finally
            {
                dbManager.Dispose();
            }
            return data;
        }

        public byte[] GetDocumentosAntecedentes(string identificacion)
        {
            byte[] data = null;
            try
            {
                dbManager.Open();
                dbManager.Command.CommandTimeout = 0;
                string str = "SET LANGUAGE US_ENGLISH select isnull(Antecedentes,0) " +
                    "from [FormularioDocumentos] " +
                    "where IdFormularioEmpleado in(select Id from FormularioEmpleado where Identificacion = '" + identificacion + "' and Estado = 0 ) ";
                dbManager.ExecuteReader(CommandType.Text, str);
                while (dbManager.DataReader.Read())
                {
                    data = (byte[])dbManager.DataReader.GetValue(0);
                }
            }
            catch (Exception)
            {
                data = null;
            }
            finally
            {
                dbManager.Dispose();
            }
            return data;
        }

        public byte[] GetDocumentosCuentaBancaria(string identificacion)
        {
            byte[] data = null;
            try
            {
                dbManager.Open();
                dbManager.Command.CommandTimeout = 0;
                string str = "SET LANGUAGE US_ENGLISH select CuentaBancaria " +
                    "from [FormularioDocumentos] " +
                    "where IdFormularioEmpleado in(select Id from FormularioEmpleado where Identificacion = '" + identificacion + "' and Estado = 0 ) ";
                dbManager.ExecuteReader(CommandType.Text, str);
                while (dbManager.DataReader.Read())
                {
                    data = (byte[])dbManager.DataReader.GetValue(0);
                }
            }
            catch (Exception)
            {
                data = null;
            }
            finally
            {
                dbManager.Dispose();
            }
            return data;
        }

        public byte[] GetDocumentosPlanilla(string identificacion)
        {
            byte[] data = null;
            try
            {
                dbManager.Open();
                dbManager.Command.CommandTimeout = 0;
                string str = "SET LANGUAGE US_ENGLISH select Planilla " +
                    "from [FormularioDocumentos] " +
                    "where IdFormularioEmpleado in(select Id from FormularioEmpleado where Identificacion = '" + identificacion + "' and Estado = 0 ) ";
                dbManager.ExecuteReader(CommandType.Text, str);
                while (dbManager.DataReader.Read())
                {
                    data = (byte[])dbManager.DataReader.GetValue(0);
                }
            }
            catch (Exception)
            {
                data = null;
            }
            finally
            {
                dbManager.Dispose();
            }
            return data;
        }

        public byte[] GetDocumentosCopiaRenta(string identificacion)
        {
            byte[] data = null;
            try
            {
                dbManager.Open();
                dbManager.Command.CommandTimeout = 0;
                string str = "SET LANGUAGE US_ENGLISH select CopiaRenta " +
                    "from [FormularioDocumentos] " +
                    "where IdFormularioEmpleado in(select Id from FormularioEmpleado where Identificacion = '" + identificacion + "' and Estado = 0 ) ";
                dbManager.ExecuteReader(CommandType.Text, str);
                while (dbManager.DataReader.Read())
                {
                    data = (byte[])dbManager.DataReader.GetValue(0);
                }
            }
            catch (Exception)
            {
                data = null;
            }
            finally
            {
                dbManager.Dispose();
            }
            return data;
        }

        public byte[] GetDocumentosCopiaCedulaConyugue(string identificacion)
        {
            byte[] data = null;
            try
            {
                dbManager.Open();
                dbManager.Command.CommandTimeout = 0;
                string str = "SET LANGUAGE US_ENGLISH select CopiaCedulaConyugue " +
                    "from [FormularioDocumentos] " +
                    "where IdFormularioEmpleado in(select Id from FormularioEmpleado where Identificacion = '" + identificacion + "' and Estado = 0 ) ";
                dbManager.ExecuteReader(CommandType.Text, str);
                while (dbManager.DataReader.Read())
                {
                    data = (byte[])dbManager.DataReader.GetValue(0);
                }
            }
            catch (Exception)
            {
                data = null;
            }
            finally
            {
                dbManager.Dispose();
            }
            return data;
        }

        public byte[] GetDocumentosPartidasNacHijos(string identificacion)
        {
            byte[] data = null;
            try
            {
                dbManager.Open();
                dbManager.Command.CommandTimeout = 0;
                string str = "SET LANGUAGE US_ENGLISH select PartidasNacHijos " +
                    "from [FormularioDocumentos] " +
                    "where IdFormularioEmpleado in(select Id from FormularioEmpleado where Identificacion = '" + identificacion + "' and Estado = 0 ) ";
                dbManager.ExecuteReader(CommandType.Text, str);
                while (dbManager.DataReader.Read())
                {
                    data = (byte[])dbManager.DataReader.GetValue(0);
                }
            }
            catch (Exception)
            {
                data = null;
            }
            finally
            {
                dbManager.Dispose();
            }
            return data;
        }

        public byte[] GetDocumentosCalificacionArtesan(string identificacion)
        {
            byte[] data = null;
            try
            {
                dbManager.Open();
                dbManager.Command.CommandTimeout = 0;
                string str = "SET LANGUAGE US_ENGLISH select CalificacionArtesan " +
                    "from [FormularioDocumentos] " +
                    "where IdFormularioEmpleado in(select Id from FormularioEmpleado where Identificacion = '" + identificacion + "' and Estado = 0 ) ";
                dbManager.ExecuteReader(CommandType.Text, str);
                while (dbManager.DataReader.Read())
                {
                    data = (byte[])dbManager.DataReader.GetValue(0);
                }
            }
            catch (Exception)
            {
                data = null;
            }
            finally
            {
                dbManager.Dispose();
            }
            return data;
        }

        public byte[] GetDocumentosCertifDiscapacidad(string identificacion)
        {
            byte[] data = null;
            try
            {
                dbManager.Open();
                dbManager.Command.CommandTimeout = 0;
                string str = "SET LANGUAGE US_ENGLISH select CertifDiscapacidad " +
                    "from [FormularioDocumentos] " +
                    "where IdFormularioEmpleado in(select Id from FormularioEmpleado where Identificacion = '" + identificacion + "' and Estado = 0 ) ";
                dbManager.ExecuteReader(CommandType.Text, str);
                while (dbManager.DataReader.Read())
                {
                    data = (byte[])dbManager.DataReader.GetValue(0);
                }
            }
            catch (Exception)
            {
                data = null;
            }
            finally
            {
                dbManager.Dispose();
            }
            return data;
        }

        public byte[] GetDocumentosCedulaById(int id)
        {
            byte[] data = null;
            try
            {
                dbManager.Open();
                dbManager.Command.CommandTimeout = 0;
                string str = "SET LANGUAGE US_ENGLISH select CopiaCedula " +
                    "from [FormularioDocumentos] " +
                    "where IdFormularioEmpleado in(" + id.ToString() + ") ";
                dbManager.ExecuteReader(CommandType.Text, str);
                while (dbManager.DataReader.Read())
                {
                    data = (byte[])dbManager.DataReader.GetValue(0);
                }
            }
            catch (Exception)
            {
                data = null;
            }
            finally
            {
                dbManager.Dispose();
            }
            return data;
        }

        public byte[] GetDocumentosPasaporteById(int id)
        {
            byte[] data = null;
            try
            {
                dbManager.Open();
                dbManager.Command.CommandTimeout = 0;
                string str = "SET LANGUAGE US_ENGLISH  select CopiaPasaporte " +
                    "from [FormularioDocumentos] " +
                    "where IdFormularioEmpleado in(" + id.ToString() + ") ";
                dbManager.ExecuteReader(CommandType.Text, str);
                while (dbManager.DataReader.Read())
                {
                    data = (byte[])dbManager.DataReader.GetValue(0);
                }
            }
            catch (Exception)
            {
                data = null;
            }
            finally
            {
                dbManager.Dispose();
            }
            return data;
        }

        public byte[] GetDocumentosAntecedentesById(int id)
        {
            byte[] data = null;
            try
            {
                dbManager.Open();
                dbManager.Command.CommandTimeout = 0;
                string str = "SET LANGUAGE US_ENGLISH select Antecedentes " +
                    "from [FormularioDocumentos] " +
                    "where IdFormularioEmpleado in(" + id.ToString() + ") ";
                dbManager.ExecuteReader(CommandType.Text, str);
                while (dbManager.DataReader.Read())
                {
                    data = (byte[])dbManager.DataReader.GetValue(0);
                }
            }
            catch (Exception)
            {
                data = null;
            }
            finally
            {
                dbManager.Dispose();
            }
            return data;
        }

        public byte[] GetDocumentosCuentaBancariaById(int id)
        {
            byte[] data = null;
            try
            {
                dbManager.Open();
                dbManager.Command.CommandTimeout = 0;
                string str = "SET LANGUAGE US_ENGLISH select CuentaBancaria " +
                    "from [FormularioDocumentos] " +
                    "where IdFormularioEmpleado in(" + id.ToString() + ") ";
                dbManager.ExecuteReader(CommandType.Text, str);
                while (dbManager.DataReader.Read())
                {
                    data = (byte[])dbManager.DataReader.GetValue(0);
                }
            }
            catch (Exception)
            {
                data = null;
            }
            finally
            {
                dbManager.Dispose();
            }
            return data;
        }

        public byte[] GetDocumentosPlanillaById(int id)
        {
            byte[] data = null;
            try
            {
                dbManager.Open();
                dbManager.Command.CommandTimeout = 0;
                string str = "SET LANGUAGE US_ENGLISH select Planilla " +
                    "from [FormularioDocumentos] " +
                    "where IdFormularioEmpleado in(" + id.ToString() + ") ";
                dbManager.ExecuteReader(CommandType.Text, str);
                while (dbManager.DataReader.Read())
                {
                    data = (byte[])dbManager.DataReader.GetValue(0);
                }
            }
            catch (Exception)
            {
                data = null;
            }
            finally
            {
                dbManager.Dispose();
            }
            return data;
        }

        public byte[] GetDocumentosCopiaRentaById(int id)
        {
            byte[] data = null;
            try
            {
                dbManager.Open();
                dbManager.Command.CommandTimeout = 0;
                string str = "SET LANGUAGE US_ENGLISH select CopiaRenta " +
                    "from [FormularioDocumentos] " +
                    "where IdFormularioEmpleado in(" + id.ToString() + ") ";
                dbManager.ExecuteReader(CommandType.Text, str);
                while (dbManager.DataReader.Read())
                {
                    data = (byte[])dbManager.DataReader.GetValue(0);
                }
            }
            catch (Exception)
            {
                data = null;
            }
            finally
            {
                dbManager.Dispose();
            }
            return data;
        }
        public byte[] GetDocumentosCopiaCedulaConyugueById(int id)
        {
            byte[] data = null;
            try
            {
                dbManager.Open();
                dbManager.Command.CommandTimeout = 0;
                string str = "SET LANGUAGE US_ENGLISH select CopiaCedulaConyugue " +
                    "from [FormularioDocumentos] " +
                    "where IdFormularioEmpleado in(" + id.ToString() + ") ";
                dbManager.ExecuteReader(CommandType.Text, str);
                while (dbManager.DataReader.Read())
                {
                    data = (byte[])dbManager.DataReader.GetValue(0);
                }
            }
            catch (Exception)
            {
                data = null;
            }
            finally
            {
                dbManager.Dispose();
            }
            return data;
        }

        public byte[] GetDocumentosPartidasNacHijosById(int id)
        {
            byte[] data = null;
            try
            {
                dbManager.Open();
                dbManager.Command.CommandTimeout = 0;
                string str = "SET LANGUAGE US_ENGLISH select PartidasNacHijos " +
                    "from [FormularioDocumentos] " +
                    "where IdFormularioEmpleado in(" + id.ToString() + ") ";
                dbManager.ExecuteReader(CommandType.Text, str);
                while (dbManager.DataReader.Read())
                {
                    data = (byte[])dbManager.DataReader.GetValue(0);
                }
            }
            catch (Exception)
            {
                data = null;
            }
            finally
            {
                dbManager.Dispose();
            }
            return data;
        }
        public byte[] GetDocumentosCalificacionArtesanById(int id)
        {
            byte[] data = null;
            try
            {
                dbManager.Open();
                dbManager.Command.CommandTimeout = 0;
                string str = "SET LANGUAGE US_ENGLISH select CalificacionArtesan " +
                    "from [FormularioDocumentos] " +
                    "where IdFormularioEmpleado in(" + id.ToString() + ") ";
                dbManager.ExecuteReader(CommandType.Text, str);
                while (dbManager.DataReader.Read())
                {
                    data = (byte[])dbManager.DataReader.GetValue(0);
                }
            }
            catch (Exception)
            {
                data = null;
            }
            finally
            {
                dbManager.Dispose();
            }
            return data;
        }

        public byte[] GetDocumentosCertifDiscapacidadById(int id)
        {
            byte[] data = null;
            try
            {
                dbManager.Open();
                dbManager.Command.CommandTimeout = 0;
                string str = "SET LANGUAGE US_ENGLISH select CertifDiscapacidad " +
                    "from [FormularioDocumentos] " +
                    "where IdFormularioEmpleado in(" + id.ToString() + ") ";
                dbManager.ExecuteReader(CommandType.Text, str);
                while (dbManager.DataReader.Read())
                {
                    data = (byte[])dbManager.DataReader.GetValue(0);
                }
            }
            catch (Exception)
            {
                data = null;
            }
            finally
            {
                dbManager.Dispose();
            }
            return data;
        }

        public string GetRutaDocumentosHojaVida(int id)
        {
            try
            {
                string Ruta = "";
                dbManager.Open();
                dbManager.Command.CommandTimeout = 0;
                dbManager.ExecuteReader(CommandType.Text, string.Format("set language us_english; SELECT HojaVida FROM FormularioDocumentos where IdFormularioEmpleado={0}", id));
                while (dbManager.DataReader.Read())
                {
                    Ruta = dbManager.DataReader.GetString(0);
                }

                return Ruta;
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

        #region Estudios Realizados
        public List<FormularioEstudioRealizado> ListFuncionarioEstudiosRealizados(string identificacion)
        {
            List<FormularioEstudioRealizado> list = new List<FormularioEstudioRealizado>();
            try
            {
                dbManager.Open();
                dbManager.Command.CommandTimeout = 0;
                string str = "SET LANGUAGE US_ENGLISH select CodigoFuncionario,CodigoNivelAcademico,isnull(CodigoCentroAcademico,0),isnull(TituloObtenido,'')" +
                    "from EstudioRealizadoFuncionario where CodigoFuncionario in(select CodigoFuncionario from Funcionario where codigorelacion=1 and IdentificacionFuncionario = '" + identificacion + "') ";
                dbManager.ExecuteReader(CommandType.Text, str);
                while (dbManager.DataReader.Read())
                {
                    FormularioEstudioRealizado c = new FormularioEstudioRealizado();
                    c.CodigoFuncionario = dbManager.DataReader.SafeGetInt32(0);
                    c.CodigoNivelAcademico = dbManager.DataReader.SafeGetInt32(1);
                    c.CodigoCentroAcademico = dbManager.DataReader.SafeGetInt32(2);
                    c.TituloObtenido = dbManager.DataReader.SafeGetString(3);
                    list.Add(c);
                }
            }
            catch (Exception ex)
            {
                GmasLogger.Instance.write("Method: " + MethodBase.GetCurrentMethod().DeclaringType.Name + " Error: " + ex.Message + "Stack Trace: " + ex.StackTrace.ToString(), "BalconGP");
            }
            finally
            {
                dbManager.Dispose();
            }
            return list;
        }

        public List<FormularioEstudioRealizado> ListFormularioEstudioRealizadoById(int id)
        {
            List<FormularioEstudioRealizado> list = new List<FormularioEstudioRealizado>();
            try
            {
                dbManager.Open();
                dbManager.Command.CommandTimeout = 0;
                string str = "SET LANGUAGE US_ENGLISH select Id, IdFormularioEmpleado,CodigoFuncionario,CodigoNivelAcademico,CodigoCentroAcademico,TituloObtenido ," +
                    "RutaArchivo=(case when RutaArchivo is null or RutaArchivo='' then 'Pendiente' else 'Cargado' end )" +
                    ",isnull(CodigoFuncionario,0) " +
                    "from FormularioEstudioRealizado " +
                    "where IdFormularioEmpleado in(" + id.ToString() + " ) ";
                dbManager.ExecuteReader(CommandType.Text, str);
                while (dbManager.DataReader.Read())
                {
                    FormularioEstudioRealizado c = new FormularioEstudioRealizado();
                    c.Id = dbManager.DataReader.SafeGetInt32(0);
                    c.IdFormularioEmpleado = dbManager.DataReader.SafeGetInt32(1);
                    c.CodigoFuncionario = dbManager.DataReader.SafeGetInt32(2);
                    c.CodigoNivelAcademico = dbManager.DataReader.SafeGetInt32(3);
                    c.CodigoCentroAcademico = dbManager.DataReader.SafeGetInt32(4);
                    c.TituloObtenido = dbManager.DataReader.SafeGetString(5);
                    c.RutaArchivo = dbManager.DataReader.SafeGetString(6);
                    list.Add(c);
                }
            }
            catch (Exception ex)
            {
                GmasLogger.Instance.write("Method: " + MethodBase.GetCurrentMethod().DeclaringType.Name + " Error: " + ex.Message + "Stack Trace: " + ex.StackTrace.ToString(), "BalconGP");
            }
            finally
            {
                dbManager.Dispose();
            }
            return list;
        }

        public void InsertFormularioEstudioRealizado(FormularioEstudioRealizado data, string identificacion, string opcion)
        {
            try
            {
                dbManager.Open();
                dbManager.Command.CommandTimeout = 0;
                string str = string.Format(" SET LANGUAGE US_ENGLISH exec gmas_ActualizarFormularioEstudioRealizado {0},{1},{2},{3},{4},'{5}','{6}','{7}','{8}'", data.Id, data.IdFormularioEmpleado, data.CodigoFuncionario, data.CodigoNivelAcademico is null ? 0 : data.CodigoNivelAcademico, data.CodigoCentroAcademico is null ? 0 : data.CodigoCentroAcademico, data.TituloObtenido, data.RutaArchivo, opcion, identificacion);
                dbManager.ExecuteNonQuery(CommandType.Text, str);
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

        public void DeleteFormularioEstudioRealizado(FormularioEstudioRealizado data)
        {
            try
            {
                dbManager.Open();
                dbManager.Command.CommandTimeout = 0;
                string str = string.Format("SET LANGUAGE US_ENGLISH delete from [FormularioEstudioRealizado] where Id={0} delete from EstudioRealizadoFuncionario where CodigoFuncionario={1} and CodigoNivelAcademico={2} and CodigoCentroAcademico={3} and TituloObtenido='{4}'", data.Id, data.CodigoFuncionario, data.CodigoNivelAcademico, data.CodigoCentroAcademico, data.TituloObtenido);
                dbManager.ExecuteNonQuery(CommandType.Text, str);
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


        public bool ExistsFormularioEstudioRealizado(string identificacion)
        {
            bool b = false;
            try
            {
                dbManager.Open();
                dbManager.Command.CommandTimeout = 0;
                string str = "select convert(bit,1) from [FormularioEstudioRealizado] " +
                "where IdFormularioEmpleado in( select Id from FormularioEmpleado where Identificacion = '" + identificacion + "' and Estado = 0 ) ";
                dbManager.ExecuteReader(CommandType.Text, str);
                while (dbManager.DataReader.Read())
                {
                    b = dbManager.DataReader.GetBoolean(0);
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
            return b;
        }
        #endregion

        #region Relacion Comercial
        public List<FormularioRelacionComercial> ListFormularioRelacionComercial(int id)
        {
            List<FormularioRelacionComercial> list = new List<FormularioRelacionComercial>();
            try
            {
                dbManager.Open();
                dbManager.Command.CommandTimeout = 0;
                string str = "SET LANGUAGE US_ENGLISH select Id, IdFormularioEmpleado, Parentesco, NombreEmpresaPN " +
                    "from [FormularioRelacionComercial] " +
                   "where IdFormularioEmpleado in(" + id.ToString() + " ) ";
                dbManager.ExecuteReader(CommandType.Text, str);
                while (dbManager.DataReader.Read())
                {
                    FormularioRelacionComercial c = new FormularioRelacionComercial();
                    c.Id = dbManager.DataReader.SafeGetInt32(0);
                    c.IdFormularioEmpleado = dbManager.DataReader.SafeGetInt32(1);
                    c.Parentesco = dbManager.DataReader.SafeGetString(2).ToUpper();
                    c.NombreEmpresaPN = dbManager.DataReader.SafeGetString(3).ToUpper();
                    list.Add(c);
                }
            }
            catch (Exception ex)
            {
                GmasLogger.Instance.write("Method: " + MethodBase.GetCurrentMethod().DeclaringType.Name + " Error: " + ex.Message + "Stack Trace: " + ex.StackTrace.ToString(), "BalconGP");
            }
            finally
            {
                dbManager.Dispose();
            }
            return list;
        }

        public void InsertFormularioRelacionComercial(FormularioRelacionComercial data, string identificacion)
        {
            try
            {
                dbManager.Open();
                dbManager.Command.CommandTimeout = 0;
                string str = "SET LANGUAGE US_ENGLISH insert into [FormularioRelacionComercial] (IdFormularioEmpleado, Parentesco, NombreEmpresaPN) " +
                    "values( (select Id from FormularioEmpleado where Identificacion = '" + identificacion + "' and Estado = 0 ) , '" + data.Parentesco.ToUpper() + "'," +
                    "'" + data.NombreEmpresaPN.ToUpper() + "')";
                dbManager.ExecuteNonQuery(CommandType.Text, str);
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

        public void UpdateFormularioRelacionComercial(FormularioRelacionComercial data)
        {
            try
            {
                dbManager.Open();
                dbManager.Command.CommandTimeout = 0;
                string str = "SET LANGUAGE US_ENGLISH update [FormularioRelacionComercial] set " +
                    " Parentesco = '" + data.Parentesco.ToUpper() + "', " +
                    " NombreEmpresaPN = '" + data.NombreEmpresaPN.ToUpper() + "'" +
                    " where Id = " + data.Id.ToString() + "; ";
                dbManager.ExecuteNonQuery(CommandType.Text, str);
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

        public void DeleteFormularioRelacionComercial(int Codigo)
        {
            try
            {
                dbManager.Open();
                dbManager.Command.CommandTimeout = 0;
                string str = "SET LANGUAGE US_ENGLISH delete from FormularioRelacionComercial where Id=" + Codigo;
                dbManager.ExecuteNonQuery(CommandType.Text, str);
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
    }
}
