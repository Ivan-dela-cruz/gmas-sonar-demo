using BUE.Inscriptions.Application.Interfaces;
using BUE.Inscriptions.Domain.Elecctions.DTO;
using BUE.Inscriptions.Domain.Paging;
using BUE.Inscriptions.Domain.Response;
using BUE.Inscriptions.Infrastructure.Interfaces;
using BUE.Inscriptions.Shared.Utils;


namespace BUE.Inscriptions.Application.Implementation
{
    public class OrganizationVoteService : IOrganizationVoteService
    {
        private readonly IOrganizationVoteRepository _voteRepository;
        private readonly IElectionRepository _electionRepository;
        private readonly ICandidateOrganizationRepository _candidateOrganizationRepository;

        public OrganizationVoteService(IOrganizationVoteRepository voteRepository, IElectionRepository electionRepository, ICandidateOrganizationRepository candidateOrganizationRepository)
        {
            _voteRepository = voteRepository;
            _electionRepository = electionRepository;
            _candidateOrganizationRepository = candidateOrganizationRepository;
        }

        public async Task<IBaseResponse<OrganizationVoteDTO>> CreateServiceAsync(OrganizationVoteDTO model)
        {
            var baseResponse = new BaseResponse<OrganizationVoteDTO>();
            var vote = await _voteRepository.CreateAsync(model);
            baseResponse.Data = vote;
            return baseResponse;
        }

        public async Task<IBaseResponse<bool>> DeleteServiceAsync(int id)
        {
            var baseResponse = new BaseResponse<bool>();
            bool isSuccess = await _voteRepository.DeleteAsync(id);

            if (!isSuccess)
            {
                baseResponse.Message = MessageUtil.Instance.NotFound;
                baseResponse.status = false;
            }
            baseResponse.Data = isSuccess;
            return baseResponse;
        }

        public async Task<IBaseResponse<OrganizationVoteDTO>> GetByIdServiceAsync(int id)
        {
            var baseResponse = new BaseResponse<OrganizationVoteDTO>();
            OrganizationVoteDTO vote = await _voteRepository.GetByIdAsync(id);

            if (vote == null)
            {
                baseResponse.Message = MessageUtil.Instance.NotFound;
                baseResponse.status = false;
                return baseResponse;
            }
            baseResponse.Data = vote;
            return baseResponse;
        }
        public async Task<IBaseResponse<PagedList<OrganizationVoteDTO>>> GetByUserIdServiceAsync(int id, PagingQueryParameters paging)
        {
            var baseResponse = new BaseResponse<PagedList<OrganizationVoteDTO>>();
            var votes = await _voteRepository.GetByUserIdAsync(id);

            if (votes == null)
            {
                baseResponse.Message = MessageUtil.Instance.NotFound;
                baseResponse.status = false;
            }
            baseResponse.Data = PagedList<OrganizationVoteDTO>.ToPagedList(votes, paging.PageNumber, paging.PageSize);
            return baseResponse;
        }
        public async Task<IBaseResponse<OrganizationVoteDTO>> GetByElectionAndUserIdAsync(int ElectionId, int UserId)
        {
            var baseResponse = new BaseResponse<OrganizationVoteDTO>();
            OrganizationVoteDTO vote = await _voteRepository.GetByElectionAndUserIdAsync(ElectionId, UserId);

            if (vote == null)
            {
                baseResponse.Message = MessageUtil.Instance.NotFound;
                baseResponse.status = false;
                return baseResponse;
            }
            baseResponse.Data = vote;
            return baseResponse;
        }

        public async Task<IBaseResponse<PagedList<OrganizationVoteDTO>>> GetServiceAsync(PagingQueryParameters paging)
        {
            var baseResponse = new BaseResponse<PagedList<OrganizationVoteDTO>>();
            var votes = await _voteRepository.GetAsync();

            if (votes == null)
            {
                baseResponse.Message = MessageUtil.Instance.NotFound;
                baseResponse.status = false;
            }
            baseResponse.Data = PagedList<OrganizationVoteDTO>.ToPagedList(votes, paging.PageNumber, paging.PageSize);
            return baseResponse;
        }

        public async Task<IBaseResponse<OrganizationVoteDTO>> UpdateServiceAsync(int id, OrganizationVoteDTO model)
        {
            var baseResponse = new BaseResponse<OrganizationVoteDTO>();
            OrganizationVoteDTO vote = await _voteRepository.UpdateAsync(id, model);
            baseResponse.Message = MessageUtil.Instance.Updated;
            baseResponse.Data = vote;
            return baseResponse;
        }

        public async Task<IBaseResponse<PagedList<VoteCountDTO>>> GetVotesByOrganizationElectionAsync(PagingQueryParameters paging, int ElectionId)
        {
            var baseResponse = new BaseResponse<PagedList<VoteCountDTO>>();
            var votes = await _voteRepository.GetCountsOrganizationVoteByElectionAsync(ElectionId);

            if (votes == null)
            {
                baseResponse.Message = MessageUtil.Instance.NotFound;
                baseResponse.status = false;
            }
            baseResponse.Data = PagedList<VoteCountDTO>.ToPagedList(votes, paging.PageNumber, paging.PageSize);
            return baseResponse;
        }
        public async Task<IBaseResponse<ElectionDTO>> GetDHontResultOrganizationElectionAsync(int ElectionId)
        {
            var summary = new VoteSummaryDTO<VoteCountDTO>();
            int totalVotes = 0;
            var baseResponse = new BaseResponse<ElectionDTO>();
            List<VoteDistributionDTO<VoteCountDTO>> result = new List<VoteDistributionDTO<VoteCountDTO>>();

            var totalUser = await _electionRepository.GetTotalUsersAsync(ElectionId);
            var election = await _electionRepository.GetByIdAsync(ElectionId);
            var blankVotes = await _voteRepository.GeOrganizationBlankVoteByElectionAsync(ElectionId);
            summary.BlankVotes = blankVotes;

            var votes = await _voteRepository.GetCountsOrganizationVoteByElectionAsync(ElectionId);
            if (votes == null)
            {
                baseResponse.Message = MessageUtil.Instance.NotFound;
                baseResponse.status = false;
            }
            var firstRow = votes.First();
            foreach (var row in votes)
            {
             
                List<CandidateMembersDTO> members = new List<CandidateMembersDTO>();
                var newRow = new VoteDistributionDTO<VoteCountDTO>(row.Organization.Name, row.VoteCount);
                newRow.OrganizationId = row.Organization.Id;
                newRow.Reference = row;
                totalVotes += row.VoteCount;
                var candidates = await _candidateOrganizationRepository.GetByOrganizationIdAsync(row.Organization.Id);
                foreach(var item in candidates)
                {
                    members.Add(new CandidateMembersDTO()
                    {
                        Name= $"{item.User.FirstLastName} {item.User.Names}",
                        Identification = item.User.Identification,
                        Email = item.User.Email,
                        Phone = item.User.Cellphone,
                    });
                }
                newRow.Members = members;
                result.Add(newRow);
            }

            CalculateDhontSeats(result, firstRow.Election.Seats);
            foreach(var item in result)
            {
                List<CandidateMembersDTO> distributions = item.Members.Take(item.SeatsWon).ToList();
                item.Distribution = distributions;
            }
           
            summary.TotalVotes = blankVotes + totalVotes;
            summary.ValidVotes = totalVotes;
            summary.VoteDistributions = result;
            summary.Quota = totalVotes / election.Seats;
            summary.Inscriptions = totalUser;
            election.Summary = summary;

            baseResponse.Data = election;
            return baseResponse;
        }
        public void CalculateDhontSeats(List<VoteDistributionDTO<VoteCountDTO>> DHontResultList, int totalSeats)
        {
            for (int i = 0; i < totalSeats; i++)
            {
                var currentDHontResultDTO = DHontResultList.OrderByDescending(x => x.Votes / (x.SeatsWon + 1)).First();
                currentDHontResultDTO.SeatsWon++;
            }
        }
    }
}
