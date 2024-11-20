using BUE.Inscriptions.Application.Interfaces;
using BUE.Inscriptions.Domain.Elecctions.DTO;
using BUE.Inscriptions.Domain.Paging;
using BUE.Inscriptions.Domain.Response;
using BUE.Inscriptions.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BUE.Inscriptions.Unit.Test.UnitTest.PublicApi.OrganizationVote
{
    public class OrganizationVoteServiceFake 
    {
        private readonly IOrganizationVoteRepository _voteRepository;

        public OrganizationVoteServiceFake(IOrganizationVoteRepository voteRepository)
        {
            _voteRepository = voteRepository;
            FakePopulateRun().Wait();
        }

        public async Task<IBaseResponse<bool>> FakePopulateRun()
        {
            var baseResponse = new BaseResponse<bool>();
            for (int i = 11447; i >= 12436; i++)
            {
                var organizationVoteDTO = new OrganizationVoteDTO()
                {
                    OrganizationElectionId = 9,
                    UserId = i
                };
                var vote = await _voteRepository.CreateAsync(organizationVoteDTO);

            }
            return baseResponse;
        }

        public Task<IBaseResponse<OrganizationVoteDTO>> CreateServiceAsync(OrganizationVoteDTO entity)
        {
            throw new NotImplementedException();
        }

        public Task<IBaseResponse<bool>> DeleteServiceAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IBaseResponse<OrganizationVoteDTO>> GetByElectionAndUserIdAsync(int ElectionId, int UserId)
        {
            throw new NotImplementedException();
        }

        public Task<IBaseResponse<OrganizationVoteDTO>> GetByIdServiceAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IBaseResponse<PagedList<OrganizationVoteDTO>>> GetByUserIdServiceAsync(int id, PagingQueryParameters paging)
        {
            throw new NotImplementedException();
        }

        public Task<IBaseResponse<PagedList<OrganizationVoteDTO>>> GetServiceAsync(PagingQueryParameters paging)
        {
            throw new NotImplementedException();
        }

        public Task<IBaseResponse<PagedList<VoteCountDTO>>> GetVotesByOrganizationElectionAsync(PagingQueryParameters paging, int ElectionId)
        {
            throw new NotImplementedException();
        }

        public Task<IBaseResponse<OrganizationVoteDTO>> UpdateServiceAsync(int id, OrganizationVoteDTO entity)
        {
            throw new NotImplementedException();
        }
    }
}
