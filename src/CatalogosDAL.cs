using CONTROLEMP.NominaGP.Portal.Framework;
using CONTROLEMP.NominaGP.Portal.Framework.Persistence;
using CONTROLEMP.NominaGP.Portal.DTO;
using System;
using System.Collections.Generic;
using System.Data;

namespace CONTROLEMP.NominaGP.Portal.DAL
{
    public class CatalogosDAL
    {
        private IDBManager dbManager;

        public CatalogosDAL()
        {
            dbManager = new DBManager();
            dbManager.ProviderType = DataProvider.SqlServer;
            dbManager.ConnectionString = Configuration.Cadena;
        }

        #region ObtenerTipoIdentificacion
        public List<CatalogosDTO> ObtenerTipoIdentificacion()
        {
            List<CatalogosDTO> listCatalogos = new List<CatalogosDTO>();
            try
            {
                dbManager.Open();
                dbManager.Command.CommandTimeout = 0;
                dbManager.ExecuteReader(CommandType.Text, "select * from TipoIdentificacion");
                while (dbManager.DataReader.Read())
                {
                    CatalogosDTO tipoIdentificacion = new CatalogosDTO();
                    tipoIdentificacion.Codigo = dbManager.DataReader.SafeGetInt32(0);
                    tipoIdentificacion.Descripcion = dbManager.DataReader.SafeGetString(1);
                    listCatalogos.Add(tipoIdentificacion);
                }
                return listCatalogos;
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
        #endregion

        #region ObtenerEstadoCivil
        public List<CatalogosDTO> ObtenerEstadoCivil()
        {
            List<CatalogosDTO> listCatalogos = new List<CatalogosDTO>();
            try
            {
                dbManager.Open();
                dbManager.Command.CommandTimeout = 0;
                dbManager.ExecuteReader(CommandType.Text, "select * from Configuraciones where ColumnaConfiguracion = 'EstadoCivil'");
                while (dbManager.DataReader.Read())
                {
                    CatalogosDTO estadocivil = new CatalogosDTO();
                    estadocivil.Codigo = dbManager.DataReader.SafeGetInt32(0);
                    estadocivil.Descripcion = dbManager.DataReader.SafeGetString(1);
                    listCatalogos.Add(estadocivil);
                }
                return listCatalogos;
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
        #endregion

        #region ObtenerSexo
        public List<CatalogosDTO> ObtenerSexo()
        {
            List<CatalogosDTO> listCatalogos = new List<CatalogosDTO>();
            try
            {
                dbManager.Open();
                dbManager.Command.CommandTimeout = 0;
                dbManager.ExecuteReader(CommandType.Text, "select * from Configuraciones where ColumnaConfiguracion = 'Sexo'");
                while (dbManager.DataReader.Read())
                {
                    CatalogosDTO sexo = new CatalogosDTO();
                    sexo.Codigo = dbManager.DataReader.SafeGetInt32(0);
                    sexo.Descripcion = dbManager.DataReader.SafeGetString(1);
                    listCatalogos.Add(sexo);
                }
                return listCatalogos;
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
        #endregion

        #region ObtenerGenero
        public List<CatalogosDTO> ObtenerGenero()
        {
            List<CatalogosDTO> listCatalogos = new List<CatalogosDTO>();
            try
            {
                dbManager.Open();
                dbManager.Command.CommandTimeout = 0;
                dbManager.ExecuteReader(CommandType.Text, "select * from Genero");
                while (dbManager.DataReader.Read())
                {
                    CatalogosDTO genero = new CatalogosDTO();
                    genero.Codigo = dbManager.DataReader.SafeGetInt32(0);
                    genero.Descripcion = dbManager.DataReader.SafeGetString(1);
                    listCatalogos.Add(genero);
                }
                return listCatalogos;
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
        #endregion

        #region ObtenerGrupoEtnico
        public List<CatalogosDTO> ObtenerGrupoEtnico()
        {
            List<CatalogosDTO> listCatalogos = new List<CatalogosDTO>();
            try
            {
                dbManager.Open();
                dbManager.Command.CommandTimeout = 0;
                dbManager.ExecuteReader(CommandType.Text, "select * from GrupoEtnico");
                while (dbManager.DataReader.Read())
                {
                    CatalogosDTO grupoetnico = new CatalogosDTO();
                    grupoetnico.Codigo = dbManager.DataReader.SafeGetInt32(0);
                    grupoetnico.Descripcion = dbManager.DataReader.SafeGetString(1);
                    listCatalogos.Add(grupoetnico);
                }
                return listCatalogos;
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
        #endregion

        #region ObtenerRegion
        public List<CatalogosDTO> ObtenerRegion()
        {
            List<CatalogosDTO> listCatalogos = new List<CatalogosDTO>();
            try
            {
                dbManager.Open();
                dbManager.Command.CommandTimeout = 0;
                dbManager.ExecuteReader(CommandType.Text, "select * from Regiones");
                while (dbManager.DataReader.Read())
                {
                    CatalogosDTO region = new CatalogosDTO();
                    region.Codigo = dbManager.DataReader.SafeGetInt32(0);
                    region.Descripcion = dbManager.DataReader.SafeGetString(1);
                    listCatalogos.Add(region);
                }
                return listCatalogos;
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
        #endregion

        #region ObtenerTipoDeSangre
        public List<CatalogosDTO> ObtenerTipoDeSangre()
        {
            List<CatalogosDTO> listCatalogos = new List<CatalogosDTO>();
            try
            {
                dbManager.Open();
                dbManager.Command.CommandTimeout = 0;
                dbManager.ExecuteReader(CommandType.Text, "select * from TipoSangre");
                while (dbManager.DataReader.Read())
                {
                    CatalogosDTO tipodesangre = new CatalogosDTO();
                    tipodesangre.Codigo = dbManager.DataReader.SafeGetInt32(0);
                    tipodesangre.Descripcion = dbManager.DataReader.SafeGetString(1);
                    listCatalogos.Add(tipodesangre);
                }
                return listCatalogos;
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
        #endregion

        #region ObtenerNacionalidad
        public List<CatalogosDTO> ObtenerNacionalidad()
        {
            List<CatalogosDTO> listCatalogos = new List<CatalogosDTO>();
            try
            {
                dbManager.Open();
                dbManager.Command.CommandTimeout = 0;
                dbManager.ExecuteReader(CommandType.Text, "select * from Nacionalidad");
                while (dbManager.DataReader.Read())
                {
                    CatalogosDTO nacionalidad = new CatalogosDTO();
                    nacionalidad.Codigo = dbManager.DataReader.SafeGetInt32(0);
                    nacionalidad.Descripcion = dbManager.DataReader.SafeGetString(1);
                    listCatalogos.Add(nacionalidad);
                }
                return listCatalogos;
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
        #endregion

        #region ObtenerPaisNacimiento
        public List<PaisDTO> ObtenerPaisNacimiento()
        {
            List<PaisDTO> listCatalogos = new List<PaisDTO>();
            try
            {
                dbManager.Open();
                dbManager.Command.CommandTimeout = 0;
                dbManager.ExecuteReader(CommandType.Text, "select * from Pais");
                while (dbManager.DataReader.Read())
                {
                    PaisDTO paisnacimiento = new PaisDTO();
                    paisnacimiento.Codigo = dbManager.DataReader.SafeGetString(0);
                    paisnacimiento.Descripcion = dbManager.DataReader.SafeGetString(1);
                    listCatalogos.Add(paisnacimiento);
                }
                return listCatalogos;
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
        #endregion

        #region ObtenerProvincia
        public List<CatalogosDTO> ObtenerProvincia()
        {
            List<CatalogosDTO> listCatalogos = new List<CatalogosDTO>();
            try
            {
                dbManager.Open();
                dbManager.Command.CommandTimeout = 0;
                dbManager.ExecuteReader(CommandType.Text, "select * from Provincia ");
                while (dbManager.DataReader.Read())
                {
                    CatalogosDTO provincia = new CatalogosDTO();
                    provincia.Codigo = dbManager.DataReader.SafeGetInt32(0);
                    provincia.Descripcion = dbManager.DataReader.SafeGetString(1);
                    provincia.CodigoRelacion= dbManager.DataReader.SafeGetString(2);
                    listCatalogos.Add(provincia);
                }
                return listCatalogos;
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
        #endregion

        #region ObtenerCanton
        public List<CatalogosDTO> ObtenerCanton()
        {
            List<CatalogosDTO> listCatalogos = new List<CatalogosDTO>();
            try
            {
                dbManager.Open();
                dbManager.Command.CommandTimeout = 0;
                dbManager.ExecuteReader(CommandType.Text, "select * from Canton");
                while (dbManager.DataReader.Read())
                {
                    CatalogosDTO canton = new CatalogosDTO();
                    canton.Codigo = dbManager.DataReader.SafeGetInt32(0);
                    canton.Descripcion = dbManager.DataReader.SafeGetString(1);
                    canton.CodigoPadre = dbManager.DataReader.SafeGetInt32(3);
                    listCatalogos.Add(canton);
                }
                return listCatalogos;
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
        #endregion

        #region ObtenerParroquia
        public List<CatalogosDTO> ObtenerParroquia()
        {
            List<CatalogosDTO> listCatalogos = new List<CatalogosDTO>();
            try
            {
                dbManager.Open();
                dbManager.Command.CommandTimeout = 0;
                dbManager.ExecuteReader(CommandType.Text, "select * from Distrito");
                while (dbManager.DataReader.Read())
                {
                    CatalogosDTO parroquia = new CatalogosDTO();
                    parroquia.Codigo = dbManager.DataReader.SafeGetInt32(0);
                    parroquia.Descripcion = dbManager.DataReader.SafeGetString(1);
                    parroquia.CodigoPadre = dbManager.DataReader.SafeGetInt32(2);
                    listCatalogos.Add(parroquia);
                }
                return listCatalogos;
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
        #endregion

        #region ObtenerTiposPrestamo
        public List<CatalogosDTO> ObtenerTiposPrestamo()
        {
            List<CatalogosDTO> listCatalogos = new List<CatalogosDTO>();
            try
            {
                dbManager.Open();
                dbManager.Command.CommandTimeout = 0;
                dbManager.ExecuteReader(CommandType.Text, "select * from TipoPrestamo where EstadoTipoPrestamo = 1 and MnemonicoMovimiento in('&PrestamoEmpleado','&AnticiposVarios')");
                while (dbManager.DataReader.Read())
                {
                    CatalogosDTO tipoprestamo = new CatalogosDTO();
                    tipoprestamo.Codigo = dbManager.DataReader.SafeGetInt32(0);
                    tipoprestamo.Descripcion = dbManager.DataReader.SafeGetString(1);
                    listCatalogos.Add(tipoprestamo);
                }
                return listCatalogos;
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
        #endregion

        #region ObtenerFrecuencia
        public List<CatalogosDTO> ObtenerFrecuencia()
        {
            List<CatalogosDTO> listCatalogos = new List<CatalogosDTO>();
            try
            {
                dbManager.Open();
                dbManager.Command.CommandTimeout = 0;
                dbManager.ExecuteReader(CommandType.Text, "select * from Frecuencia where AplicaPrestamo = 1");
                //dbManager.ExecuteReader(CommandType.Text, "select CodigoFrecuencia, NombreFrecuencia, CodigoTipoPrestamo from Frecuencia where AplicaPrestamo = 1");
                while (dbManager.DataReader.Read())
                {
                    CatalogosDTO frecuencia = new CatalogosDTO();
                    frecuencia.Codigo = dbManager.DataReader.SafeGetInt32(0);
                    frecuencia.Descripcion = dbManager.DataReader.SafeGetString(1);
                    //frecuencia.CodigoPadre = dbManager.DataReader.SafeGetInt32(2);
                    listCatalogos.Add(frecuencia);
                }
                return listCatalogos;
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
        #endregion

        #region ObtenerTiposAnticipo
        public List<CatalogosDTO> ObtenerTiposAnticipo()
        {
            List<CatalogosDTO> listCatalogos = new List<CatalogosDTO>();
            try
            {
                dbManager.Open();
                dbManager.Command.CommandTimeout = 0;
                dbManager.ExecuteReader(CommandType.Text, "select CodigoMovimiento, NombreMovimiento from Movimiento where CodigoMovimientoCalculo = '&IngresoAnticipoDos'");
                while (dbManager.DataReader.Read())
                {
                    CatalogosDTO tipoanticipo = new CatalogosDTO();
                    tipoanticipo.Codigo = dbManager.DataReader.SafeGetInt32(0);
                    tipoanticipo.Descripcion = dbManager.DataReader.SafeGetString(1);
                    listCatalogos.Add(tipoanticipo);
                }
                return listCatalogos;
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
        #endregion

        #region ObtenerTiposPrestamo
        public List<CatalogosDTO> ObtenerTiposPermiso()
        {
            List<CatalogosDTO> listCatalogos = new List<CatalogosDTO>();
            try
            {
                dbManager.Open();
                dbManager.Command.CommandTimeout = 0;
                dbManager.ExecuteReader(CommandType.Text, "select * from TipoPermiso where EstadoTipoPermiso = 1");
                while (dbManager.DataReader.Read())
                {
                    CatalogosDTO tipoprestamo = new CatalogosDTO();
                    tipoprestamo.Codigo = dbManager.DataReader.SafeGetInt32(0);
                    tipoprestamo.Descripcion = dbManager.DataReader.SafeGetString(1);
                    listCatalogos.Add(tipoprestamo);
                }
                return listCatalogos;
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
        #endregion

    }
}
