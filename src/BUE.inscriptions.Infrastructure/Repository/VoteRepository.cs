using AutoMapper;
using BUE.Inscriptions.Domain.Elecctions.DTO;
using BUE.Inscriptions.Domain.Elecctions.Entities;
using BUE.Inscriptions.Domain.Entity;
using BUE.Inscriptions.Infrastructure.Interfaces;
using BUE.Inscriptions.Shared.Utils;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Utilities;


namespace BUE.Inscriptions.Infrastructure.Repository
{
    public class VoteRepository : BaseRepository, IVoteRepository
    {
        private readonly PortalMatriculasDBContext _db;
        private readonly IMapper _mapper;

        public VoteRepository(PortalMatriculasDBContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<VoteDTO> CreateAsync(VoteDTO entity)
        {
            Vote vote = _mapper.Map<VoteDTO, Vote>(entity);
            _db.Votes.Add(vote);
            await _db.SaveChangesAsync();

            return _mapper.Map<Vote, VoteDTO>(vote);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                Vote vote = await _db.Votes.FindAsync(id);
                if (vote == null)
                {
                    return false;
                }

                // Your implementation for deleting an Vote
                _db.Votes.Remove(vote);
                await _db.SaveChangesAsync();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<IEnumerable<VoteDTO>> GetAsync()
        {
            var votes = await _db.Votes.ToListAsync();
            return _mapper.Map<IEnumerable<VoteDTO>>(votes);
        }

        public async Task<VoteDTO> GetByIdAsync(int id)
        {
            var vote = await _db.Votes.FindAsync(id);
            return _mapper.Map<VoteDTO>(vote);
        }
        public async Task<IEnumerable<VoteDTO>> GetByUserIdAsync(int id)
        {
            var votes = await _db.Votes.Where(x => x.UserId == id)
                 .Include(e => e.CandidateElection)
                 .Include(e => e.CandidateElection.Election)
                .ToListAsync();
            return _mapper.Map<IEnumerable<VoteDTO>>(votes);
        }
        public async Task<VoteDTO> GetByElectionAndUserIdAsync(int ElectionId, int UserId)
        {
            var vote = await _db.Votes.FirstOrDefaultAsync(x => x.UserId == UserId && x.CandidateElection.ElectionId == ElectionId);
            return _mapper.Map<VoteDTO>(vote);
        }

        public async Task<VoteDTO> UpdateAsync(int id, VoteDTO entity)
        {
            Vote vote = _mapper.Map<VoteDTO, Vote>(entity);
            var existingVote = await _db.Votes.FindAsync(id);

            if (existingVote == null)
            {
                throw new NullReferenceException(MessageUtil.Instance.NotFound);
            }
            MapProperties(vote, existingVote);
            await _db.SaveChangesAsync();
            return _mapper.Map<Vote, VoteDTO>(existingVote);
        }

        public async Task<IEnumerable<VoteCountDTO>> GetVoteCountsByCandidateElectionAsync(int ElectionId)
        {

            var voteCounts = await _db.Votes
                .Where(x => x.CandidateElection.ElectionId == ElectionId && x.CandidateElection.Status == true)
                .GroupBy(x => x.CandidateElectionId)
                .Select(g => new VoteCountDTO
                {
                    ElectionId = ElectionId,
                    CandidateElectionId = g.Key,
                    VoteCount = g.Count(),
                    Candidate = _mapper.Map<Candidate, CandidateDTO>(g.FirstOrDefault().CandidateElection.Candidate),
                    Election = _mapper.Map<Election, ElectionDTO>(g.FirstOrDefault().CandidateElection.Election),
                    Position = _mapper.Map<Position, PositionDTO>(g.FirstOrDefault().CandidateElection.Position)

                })
                .ToListAsync();

            var candidateIdsWithVotes = voteCounts.Select(vc => vc.CandidateElectionId);
            var candidatesWithoutVotes = await _db.CandidateElections
                .Include(ce => ce.Candidate)
                .Include(ce => ce.Election)
                .Include(ce => ce.Position)
                .Where(ce => ce.ElectionId == ElectionId && ce.Status == true && !candidateIdsWithVotes.Contains(ce.Id))
                .ToListAsync();

            foreach (var candidate in candidatesWithoutVotes)
            {
                var votesZero = new VoteCountDTO
                {
                    ElectionId = ElectionId,
                    CandidateElectionId = candidate.Id,
                    VoteCount = 0,
                    Candidate = _mapper.Map<Candidate, CandidateDTO>(candidate.Candidate),
                    Election = _mapper.Map<Election, ElectionDTO>(candidate.Election),
                    Position = _mapper.Map<Position, PositionDTO>(candidate.Position)
                };
                voteCounts.Add(votesZero);
            }

            return voteCounts;
        }
    }
}
