using AutoMapper;
using BUE.Inscriptions.Domain.Elecctions.DTO;
using BUE.Inscriptions.Domain.Elecctions.Entities;
using BUE.Inscriptions.Infrastructure.Interfaces;
using BUE.Inscriptions.Shared.Utils;
using Microsoft.EntityFrameworkCore;


namespace BUE.Inscriptions.Infrastructure.Repository
{
    public class OrganizationElectionRepository : BaseRepository, IOrganizationElectionRepository
    {
        private readonly PortalMatriculasDBContext _db;
        private readonly IMapper _mapper;

        public OrganizationElectionRepository(PortalMatriculasDBContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<OrganizationElectionDTO> CreateAsync(OrganizationElectionDTO entity)
        {
            OrganizationElection organizationElection = _mapper.Map<OrganizationElectionDTO, OrganizationElection>(entity);
            _db.OrganizationElections.Add(organizationElection);
            await _db.SaveChangesAsync();

            return _mapper.Map<OrganizationElection, OrganizationElectionDTO>(organizationElection);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                OrganizationElection organizationElection = await _db.OrganizationElections.FindAsync(id);
                if (organizationElection == null)
                {
                    return false;
                }
                _db.OrganizationElections.Remove(organizationElection);
                await _db.SaveChangesAsync();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<IEnumerable<OrganizationElectionDTO>> GetAsync()
        {
            var candidates = await _db.OrganizationElections
                .Include(e => e.Election)
                .Include(e => e.Organization)
                .ToListAsync();
            return _mapper.Map<IEnumerable<OrganizationElectionDTO>>(candidates);
        }

        public async Task<OrganizationElectionDTO> GetByIdAsync(int id)
        {
            var candidate = await _db.OrganizationElections
                .Include(e => e.Election)
                .Include(e => e.Organization)
                .FirstOrDefaultAsync(e => e.Id == id);
            return _mapper.Map<OrganizationElectionDTO>(candidate);
        }
        public async Task<IEnumerable<OrganizationElectionDTO>> GetByElectionIdAsync(int id)
        {
            try
            {
                var candidates = await _db.OrganizationElections
                    .Include(e => e.Election)
                   .Include(e => e.Organization)
                    .Where(e => e.ElectionId == id)
                    .ToListAsync();
                return _mapper.Map<IEnumerable<OrganizationElectionDTO>>(candidates);

            }
            catch (Exception e)
            {
                return null;
            }
        }

        public async Task<OrganizationElectionDTO> UpdateAsync(int id, OrganizationElectionDTO entity)
        {
            OrganizationElection organizationElection = _mapper.Map<OrganizationElectionDTO, OrganizationElection>(entity);
            var existingCandidate = await _db.OrganizationElections.FindAsync(id);

            if (existingCandidate == null)
            {
                throw new NullReferenceException(MessageUtil.Instance.NotFound);
            }
            MapProperties(organizationElection, existingCandidate);
            await _db.SaveChangesAsync();
            return _mapper.Map<OrganizationElection, OrganizationElectionDTO>(existingCandidate);
        }
    }
}
