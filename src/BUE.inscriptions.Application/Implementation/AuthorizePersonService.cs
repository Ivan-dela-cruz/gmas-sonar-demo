using BUE.Inscriptions.Infrastructure.Interfaces;
using BUE.Inscriptions.Domain.Entity.DTO;
using BUE.Inscriptions.Domain.Paging;
using BUE.Inscriptions.Domain.Response;
using BUE.Inscriptions.Application.Interfaces;
using BUE.Inscriptions.Shared.Utils;

namespace BUE.Inscriptions.Application.Implementation
{
    public class AuthorizePersonService : IAuthorizePersonService
    {
        private IAuthorizePersonRepository _personAuthRep;
        public AuthorizePersonService(IAuthorizePersonRepository personAuth) => _personAuthRep = personAuth;
        public async Task<IBaseResponse<AuthorizePeopleDTO>> CreateServiceAsync(AuthorizePeopleDTO model)
        {
            var baseResponse = new BaseResponse<AuthorizePeopleDTO>();
            var AuthorizePerson = await _personAuthRep.CreateAsync(model);
            if (AuthorizePerson is null)
            {
                baseResponse.Message = MessageUtil.Instance.Warning;
                baseResponse.status = false;
            }
            baseResponse.Data = AuthorizePerson;
            return baseResponse;
        }
        public async Task<IBaseResponse<PagedList<AuthorizePeopleDTO>>> StoreServiceAsync(IEnumerable<AuthorizePeopleDTO> entities)
        {
            var baseResponse = new BaseResponse<PagedList<AuthorizePeopleDTO>>();
            var AuthorizePersons = await _personAuthRep.StoreMany(entities);
            if (AuthorizePersons is null)
            {
                baseResponse.Message = MessageUtil.Instance.Warning;
                baseResponse.status = false;
            }
            baseResponse.Data = PagedList<AuthorizePeopleDTO>.ToPagedList(AuthorizePersons, 1, 100);
            return baseResponse;
        }
        public async Task<IBaseResponse<bool>> DeleteServiceAsync(int id)
        {
            var baseResponse = new BaseResponse<bool>();
            bool IsSuccess = await _personAuthRep.DeleteAsync(id);
            if (!IsSuccess)
            {
                baseResponse.Message = MessageUtil.Instance.NotFound;
                baseResponse.status = false;
            }
            baseResponse.Data = IsSuccess;
            return baseResponse;
        }
        public async Task<IBaseResponse<AuthorizePeopleDTO>> GetByIdServiceAsync(int id)
        {
            var baseResponse = new BaseResponse<AuthorizePeopleDTO>();
            AuthorizePeopleDTO AuthorizePerson = await _personAuthRep.GetByIdAsync(id);
            if (AuthorizePerson is null)
            {
                baseResponse.Message = MessageUtil.Instance.NotFound;
                baseResponse.status = false;
            }
            baseResponse.Data = AuthorizePerson;
            return baseResponse;
        }
        public async Task<IBaseResponse<AuthorizePeopleDTO>> GetByIdentificationServiceAsync(string identification, int currentYearSchool, int requestCode)
        {
            var baseResponse = new BaseResponse<AuthorizePeopleDTO>();
            AuthorizePeopleDTO AuthorizePerson = await _personAuthRep.GetByIdentificationAsync(identification, currentYearSchool, requestCode);
            if (AuthorizePerson is null)
            {
                baseResponse.Message = MessageUtil.Instance.NotFound;
                baseResponse.status = false;
            }
            baseResponse.Data = AuthorizePerson;
            return baseResponse;
        }
        public async Task<IBaseResponse<PagedList<AuthorizePeopleDTO>>> GetByRequestIdServiceAsync(int id)
        {
            var baseResponse = new BaseResponse<PagedList<AuthorizePeopleDTO>>();
            var AuthorizePersons = await _personAuthRep.GetByRequestIdAsync(id);
            if (AuthorizePersons is null)
            {
                baseResponse.Message = MessageUtil.Instance.NotFound;
                baseResponse.status = false;
            }
            baseResponse.Data = PagedList<AuthorizePeopleDTO>.ToPagedList(AuthorizePersons, 1, 100);
            return baseResponse;
        }
        public async Task<IBaseResponse<PagedList<AuthorizePeopleDTO>>> GetServiceAsync(PagingQueryParameters paging)
        {
            var baseResponse = new BaseResponse<PagedList<AuthorizePeopleDTO>>();
            var AuthorizePersons = await _personAuthRep.GetAsync();
            if (AuthorizePersons is null)
            {
                baseResponse.Message = MessageUtil.Instance.NotFound;
                baseResponse.status = false;
            }
            baseResponse.Data = PagedList<AuthorizePeopleDTO>.ToPagedList(AuthorizePersons, paging.PageNumber, paging.PageSize);
            return baseResponse;
        }
        public async Task<IBaseResponse<AuthorizePeopleDTO>> UpdateServiceAsync(int id, AuthorizePeopleDTO model)
        {

            var baseResponse = new BaseResponse<AuthorizePeopleDTO>();
            AuthorizePeopleDTO AuthorizePerson = await _personAuthRep.UpdateAsync(id, model);
            baseResponse.Message = MessageUtil.Instance.Updated;
            baseResponse.Data = AuthorizePerson;
            return baseResponse;
        }
    }
}
