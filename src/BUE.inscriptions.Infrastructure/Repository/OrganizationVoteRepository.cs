using AutoMapper;
using BUE.Inscriptions.Domain.Elecctions.DTO;
using BUE.Inscriptions.Domain.Elecctions.Entities;
using BUE.Inscriptions.Infrastructure.Interfaces;
using BUE.Inscriptions.Shared.Utils;
using Microsoft.EntityFrameworkCore;


namespace BUE.Inscriptions.Infrastructure.Repository
{
    public class OrganizationVoteRepository : BaseRepository, IOrganizationVoteRepository
    {
        private readonly PortalMatriculasDBContext _db;
        private readonly IMapper _mapper;

        public OrganizationVoteRepository(PortalMatriculasDBContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<OrganizationVoteDTO> CreateAsync(OrganizationVoteDTO entity)
        {
            OrganizationVote vote = _mapper.Map<OrganizationVoteDTO, OrganizationVote>(entity);
            _db.OrganizationVotes.Add(vote);
            await _db.SaveChangesAsync();

            return _mapper.Map<OrganizationVote, OrganizationVoteDTO>(vote);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                OrganizationVote vote = await _db.OrganizationVotes.FindAsync(id);
                if (vote == null)
                {
                    return false;
                }
                _db.OrganizationVotes.Remove(vote);
                await _db.SaveChangesAsync();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<IEnumerable<OrganizationVoteDTO>> GetAsync()
        {
            var votes = await _db.OrganizationVotes.ToListAsync();
            return _mapper.Map<IEnumerable<OrganizationVoteDTO>>(votes);
        }

        public async Task<OrganizationVoteDTO> GetByIdAsync(int id)
        {
            var vote = await _db.OrganizationVotes.FindAsync(id);
            return _mapper.Map<OrganizationVoteDTO>(vote);
        }
        public async Task<IEnumerable<OrganizationVoteDTO>> GetByUserIdAsync(int id)
        {
            var votes = await _db.OrganizationVotes.Where(x => x.UserId == id)
                 .Include(e => e.OrganizationElection)
                 .Include(e => e.OrganizationElection.Election)
                .ToListAsync();
            return _mapper.Map<IEnumerable<OrganizationVoteDTO>>(votes);
        }
        public async Task<OrganizationVoteDTO> GetByElectionAndUserIdAsync(int ElectionId, int UserId)
        {
            var vote = await _db.OrganizationVotes.FirstOrDefaultAsync(x => x.UserId == UserId && x.OrganizationElection.ElectionId == ElectionId);
            return _mapper.Map<OrganizationVoteDTO>(vote);
        }

        public async Task<OrganizationVoteDTO> UpdateAsync(int id, OrganizationVoteDTO entity)
        {
            OrganizationVote vote = _mapper.Map<OrganizationVoteDTO, OrganizationVote>(entity);
            var existingOrganizationVote = await _db.OrganizationVotes.FindAsync(id);

            if (existingOrganizationVote == null)
            {
                throw new NullReferenceException(MessageUtil.Instance.NotFound);
            }
            MapProperties(vote, existingOrganizationVote);
            await _db.SaveChangesAsync();
            return _mapper.Map<OrganizationVote, OrganizationVoteDTO>(existingOrganizationVote);
        }
        public async Task<int> GeOrganizationBlankVoteByElectionAsync(int ElectionId)
        {
            var voteCounts = await _db.OrganizationVotes
                .Where(x => x.OrganizationElection.ElectionId == ElectionId && x.OrganizationElectionId == null).ToListAsync();
            return voteCounts.Count;
        }

            public async Task<IEnumerable<VoteCountDTO>> GetCountsOrganizationVoteByElectionAsync(int ElectionId)
        {

            var voteCounts = await _db.OrganizationVotes
                .Where(x => x.OrganizationElection.ElectionId == ElectionId && x.OrganizationElectionId != null )
                .GroupBy(x => x.OrganizationElectionId)
                .Select(g => new VoteCountDTO
                {
                    ElectionId = ElectionId,
                    OrganizationElectionId = g.Key,
                    VoteCount = g.Count(),
                    Organization = _mapper.Map<Organization, OrganizationDTO>(g.FirstOrDefault().OrganizationElection.Organization),
                    Election = _mapper.Map<Election, ElectionDTO>(g.FirstOrDefault().OrganizationElection.Election)

                })
                .ToListAsync();

            var organizationIdsWithOrganizationVotes = voteCounts.Select(vc => vc.OrganizationElectionId);
            var organizationsWithoutOrganizationVotes = await _db.OrganizationElections
                .Include(ce => ce.Organization)
                .Include(ce => ce.Election)
                .Where(ce => ce.ElectionId == ElectionId && ce.Status == true && !organizationIdsWithOrganizationVotes.Contains(ce.Id))
                .ToListAsync();

            foreach (var organization in organizationsWithoutOrganizationVotes)
            {
                var votesZero = new VoteCountDTO
                {
                    ElectionId = ElectionId,
                    OrganizationElectionId = organization.Id,
                    VoteCount = 0,
                    Organization = _mapper.Map<Organization, OrganizationDTO>(organization.Organization),
                    Election = _mapper.Map<Election, ElectionDTO>(organization.Election)
                };
                voteCounts.Add(votesZero);
            }

            return voteCounts;
        }
    }
}
