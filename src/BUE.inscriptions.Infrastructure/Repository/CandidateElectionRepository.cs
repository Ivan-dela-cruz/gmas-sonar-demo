using AutoMapper;
using BUE.Inscriptions.Domain.Elecctions.DTO;
using BUE.Inscriptions.Domain.Elecctions.Entities;
using BUE.Inscriptions.Infrastructure.Interfaces;
using BUE.Inscriptions.Shared.Utils;
using Microsoft.EntityFrameworkCore;


namespace BUE.Inscriptions.Infrastructure.Repository
{
    public class CandidateElectionRepository : BaseRepository, ICandidateElectionRepository
    {
        private readonly PortalMatriculasDBContext _db;
        private readonly IMapper _mapper;

        public CandidateElectionRepository(PortalMatriculasDBContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<CandidateElectionDTO> CreateAsync(CandidateElectionDTO entity)
        {
           CandidateElection candidateElection = _mapper.Map<CandidateElectionDTO, CandidateElection>(entity);
            _db.CandidateElections.Add(candidateElection);
            await _db.SaveChangesAsync();

            return _mapper.Map<CandidateElection, CandidateElectionDTO>(candidateElection);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
               CandidateElection candidateElection = await _db.CandidateElections.FindAsync(id);
                if (candidateElection == null)
                {
                    return false;
                }
                _db.CandidateElections.Remove(candidateElection);
                await _db.SaveChangesAsync();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<IEnumerable<CandidateElectionDTO>> GetAsync()
        {
            var candidates = await _db.CandidateElections.Include(e => e.AcademicYear)
                .Include(e => e.Position)
                .Include(e => e.Election)
                .Include(e => e.Candidate)
                .ToListAsync();
            return _mapper.Map<IEnumerable<CandidateElectionDTO>>(candidates);
        }

        public async Task<CandidateElectionDTO> GetByIdAsync(int id)
        {
            var candidate = await _db.CandidateElections
                .Include(e=> e.AcademicYear)
                .Include(e=> e.Position)
                .Include(e=> e.Election)
                .Include(e=> e.Candidate)
                .FirstOrDefaultAsync(e => e.Id == id); 
            return _mapper.Map<CandidateElectionDTO>(candidate);
        }
        public async Task<IEnumerable<CandidateElectionDTO>> GetByElectionIdAsync(int id)
        {
            var candidates = await _db.CandidateElections
                .Include(e => e.AcademicYear)
                .Include(e => e.Position)
                .Include(e => e.Election)
               .Include(e => e.Candidate)
                .Where(e => e.ElectionId == id)
                .ToListAsync();
            return _mapper.Map<IEnumerable<CandidateElectionDTO>>(candidates);
        }

        public async Task<CandidateElectionDTO> UpdateAsync(int id, CandidateElectionDTO entity)
        {
           CandidateElection candidateElection = _mapper.Map<CandidateElectionDTO, CandidateElection>(entity);
            var existingCandidate = await _db.CandidateElections.FindAsync(id);

            if (existingCandidate == null)
            {
                throw new NullReferenceException(MessageUtil.Instance.NotFound);
            }
            MapProperties(candidateElection, existingCandidate);
            await _db.SaveChangesAsync();
            return _mapper.Map<CandidateElection, CandidateElectionDTO>(existingCandidate);
        }
    }
}
