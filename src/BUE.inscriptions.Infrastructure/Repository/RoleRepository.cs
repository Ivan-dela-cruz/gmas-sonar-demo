using AutoMapper;
using BUE.Inscriptions.Infrastructure.Interfaces;
using BUE.Inscriptions.Domain.Entity;
using BUE.Inscriptions.Domain.Entity.DTO;
using BUE.Inscriptions.Shared.Utils;
using Microsoft.EntityFrameworkCore;

namespace BUE.Inscriptions.Infrastructure.Repository
{
    public class RoleRepository : IRoleRepository
    {
        private readonly PortalMatriculasDBContext _db;
        private IMapper _mapper;
        public RoleRepository(PortalMatriculasDBContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }
        public async Task<RoleDTO> CreateAsync(RoleDTO entity)
        {
            Role role = _mapper.Map<RoleDTO, Role>(entity);
            _db.Role.Add(role);
            await _db.SaveChangesAsync();
            return _mapper.Map<Role, RoleDTO>(role);
        }
        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                Role? role = await _db.Role.FirstOrDefaultAsync(x => x.code == id);
                if (role is null)
                {
                    return false;
                }
                role.DeletedAt = DateTime.Now;
                _db.Role.Update(role);
                //_db.Role.Remove(role);
                await _db.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public async Task<IEnumerable<RoleDTO>> GetAsync() => _mapper.Map<IEnumerable<RoleDTO>>(await _db.Role.ToListAsync());

        public async Task<IEnumerable<RoleDTO>> GetPermissionsAsync()
        {
            var rolesAndPermissions = await _db.Set<RolePermission>().FromSqlRaw("EXECUTE [dbo].[gmas_sp_RolePermisos]").ToListAsync();
            var groupedByRole = rolesAndPermissions.GroupBy(x => x.CodigoRol)
                                      .Select(group => new
                                      {
                                          Role = group.Key,
                                          Permissions = group.ToList()
                                      });

            foreach (var result in rolesAndPermissions)
            {
                Console.WriteLine("CodigoRol: " + result.CodigoRol);
                Console.WriteLine("NombreRol: " + result.NombreRol);
                Console.WriteLine("DescripcionRol: " + result.DescripcionRol);
                Console.WriteLine("EstadoRol: " + result.EstadoRol);
                Console.WriteLine("CreatedAtRol: " + result.CreatedAtRol);
                Console.WriteLine("UpdatedAtRol: " + result.UpdatedAtRol);
                Console.WriteLine("DeletedAtRol: " + result.DeletedAtRol);
                Console.WriteLine("codigo: " + result.codigo);
                Console.WriteLine("Nombre: " + result.Nombre);
                Console.WriteLine("Idioma: " + result.Idioma);
                Console.WriteLine("Estado: " + result.Estado);
                Console.WriteLine("CreatedAt: " + result.CreatedAt);
                Console.WriteLine("UpdatedAt: " + result.UpdatedAt);
                Console.WriteLine("DeletedAt: " + result.DeletedAt);
                Console.WriteLine("Filtro: " + result.Filtro);
                Console.WriteLine("TipoObjeto: " + result.TipoObjeto);
                Console.WriteLine("Objeto: " + result.Objeto);
                Console.WriteLine("Configuracion: " + result.Configuracion);
                Console.WriteLine("");
            }

            return _mapper.Map<IEnumerable<RoleDTO>>(await _db.Role.ToListAsync());
        }



        public async Task<RoleDTO> GetByIdAsync(int id) =>

            _mapper.Map<RoleDTO>(await _db.Role.FirstOrDefaultAsync(x => x.code == id));

        public async Task<RoleDTO> UpdateAsync(int id, RoleDTO entity)
        {
            Role role = _mapper.Map<RoleDTO, Role>(entity);
            var roleAfter = await _db.Role.AsNoTracking().FirstOrDefaultAsync(x => x.code == id);
            if (roleAfter is null || role.code != id)
            {
                throw new NullReferenceException(MessageUtil.Instance.NotFound);
            }
            _db.Role.Update(role);
            await _db.SaveChangesAsync();
            return _mapper.Map<Role, RoleDTO>(role);
        }

        public async Task<IEnumerable<PermissionDTO>> GetAllPermissionsAsync() =>

           _mapper.Map<IEnumerable<PermissionDTO>>(await _db.Permissions.Where(x => x.status == true).ToListAsync());


    }
}
