using AutoMapper;
using BUE.Inscriptions.Domain.Elecctions.DTO;
using BUE.Inscriptions.Domain.Elecctions.Entities;
using BUE.Inscriptions.Infrastructure.Interfaces;
using BUE.Inscriptions.Shared.Utils;
using Microsoft.EntityFrameworkCore;


namespace BUE.Inscriptions.Infrastructure.Repository
{
    public class CandidateOrganizationRepository : BaseRepository, ICandidateOrganizationRepository
    {
        private readonly PortalMatriculasDBContext _db;
        private readonly IMapper _mapper;

        public CandidateOrganizationRepository(PortalMatriculasDBContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<CandidateOrganizationDTO> CreateAsync(CandidateOrganizationDTO entity)
        {
            CandidateOrganization candidateElection = _mapper.Map<CandidateOrganizationDTO, CandidateOrganization>(entity);
            _db.CandidateOrganizations.Add(candidateElection);
            await _db.SaveChangesAsync();

            return _mapper.Map<CandidateOrganization, CandidateOrganizationDTO>(candidateElection);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                CandidateOrganization candidateElection = await _db.CandidateOrganizations.FindAsync(id);
                if (candidateElection == null)
                {
                    return false;
                }
                _db.CandidateOrganizations.Remove(candidateElection);
                await _db.SaveChangesAsync();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public async Task<bool> DeleteOrganizationCandidateAsync(int organizationId, int candidateId)
        {
            try
            {
                var candidateElection = await _db.CandidateOrganizations.Where(x => x.OrganizationId == organizationId && x.UserId == candidateId).FirstOrDefaultAsync();
                if (candidateElection == null)
                {
                    return false;
                }
                _db.CandidateOrganizations.Remove(candidateElection);
                await _db.SaveChangesAsync();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<IEnumerable<CandidateOrganizationDTO>> GetAsync()
        {
            var candidates = await _db.CandidateOrganizations
                .Include(e => e.Organization)
                .Include(e => e.User)
                .ToListAsync();
            return _mapper.Map<IEnumerable<CandidateOrganizationDTO>>(candidates);
        }

        public async Task<CandidateOrganizationDTO> GetByIdAsync(int id)
        {
            var candidate = await _db.CandidateOrganizations
                .Include(e => e.Organization)
                .Include(e => e.User)
                .FirstOrDefaultAsync(e => e.Id == id);
            return _mapper.Map<CandidateOrganizationDTO>(candidate);
        }
        public async Task<IEnumerable<CandidateOrganizationDTO>> GetByOrganizationIdAsync(int id)
        {
            var candidates = await _db.CandidateOrganizations
                .Include(e => e.Organization)
                .Include(e => e.User)
                .Where(e => e.OrganizationId == id)
                .ToListAsync();
            return _mapper.Map<IEnumerable<CandidateOrganizationDTO>>(candidates);
        }

        public async Task<CandidateOrganizationDTO> UpdateAsync(int id, CandidateOrganizationDTO entity)
        {
            CandidateOrganization candidateElection = _mapper.Map<CandidateOrganizationDTO, CandidateOrganization>(entity);
            var existingCandidate = await _db.CandidateOrganizations.FindAsync(id);

            if (existingCandidate == null)
            {
                throw new NullReferenceException(MessageUtil.Instance.NotFound);
            }
            MapProperties(candidateElection, existingCandidate);
            await _db.SaveChangesAsync();
            return _mapper.Map<CandidateOrganization, CandidateOrganizationDTO>(existingCandidate);
        }
    }
}
