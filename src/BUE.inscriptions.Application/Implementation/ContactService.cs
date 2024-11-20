using BUE.Inscriptions.Infrastructure.Interfaces;
using BUE.Inscriptions.Domain.Entity.DTO;
using BUE.Inscriptions.Domain.Paging;
using BUE.Inscriptions.Domain.Response;
using BUE.Inscriptions.Application.Interfaces;
using BUE.Inscriptions.Shared.Utils;

namespace BUE.Inscriptions.Application.Implementation
{
    public class ContactService : IContactService
    {
        private IContactRepository _contactRep;
        public ContactService(IContactRepository contactRep) => _contactRep = contactRep;
        public async Task<IBaseResponse<ContactDTO>> CreateServiceAsync(ContactDTO model)
        {
            var baseResponse = new BaseResponse<ContactDTO>();
            var contact = await _contactRep.CreateAsync(model);
            if (contact is null)
            {
                baseResponse.Message = MessageUtil.Instance.Warning;
                baseResponse.status = false;
            }
            baseResponse.Data = contact;
            return baseResponse;
        }
        public async Task<IBaseResponse<bool>> DeleteServiceAsync(int id)
        {
            var baseResponse = new BaseResponse<bool>();
            bool IsSuccess = await _contactRep.DeleteAsync(id);
            if (!IsSuccess)
            {
                baseResponse.Message = MessageUtil.Instance.NotFound;
                baseResponse.status = false;
            }
            baseResponse.Data = IsSuccess;
            return baseResponse;
        }
        public async Task<IBaseResponse<ContactDTO>> GetByIdServiceAsync(int id)
        {
            var baseResponse = new BaseResponse<ContactDTO>();
            ContactDTO contact = await _contactRep.GetByIdAsync(id);
            if (contact is null)
            {
                baseResponse.Message = MessageUtil.Instance.NotFound;
                baseResponse.status = false;
            }
            baseResponse.Data = contact;
            return baseResponse;
        }
        public async Task<IBaseResponse<ContactDTO>> GetRepresentativeByContactAsync(int contactCode)
        {
            var baseResponse = new BaseResponse<ContactDTO>();
            ContactDTO contact = await _contactRep.GetRepresentativeByContactAsync(contactCode);
            if (contact is null)
            {
                baseResponse.Message = MessageUtil.Instance.NotFound;
                baseResponse.status = false;
            }
            baseResponse.Data = contact;
            return baseResponse;
        }
        public async Task<IBaseResponse<PagedList<ContactDTO>>> GetServiceAsync(PagingQueryParameters paging)
        {
            var baseResponse = new BaseResponse<PagedList<ContactDTO>>();
            var contacts = await _contactRep.GetAsync();
            if (contacts is null)
            {
                baseResponse.Message = MessageUtil.Instance.NotFound;
                baseResponse.status = false;
            }
            baseResponse.Data = PagedList<ContactDTO>.ToPagedList(contacts, paging.PageNumber, paging.PageSize);
            return baseResponse;
        }
        public async Task<IBaseResponse<ContactDTO>> UpdateServiceAsync(int id, ContactDTO model)
        {

            var baseResponse = new BaseResponse<ContactDTO>();
            ContactDTO contact = await _contactRep.UpdateAsync(id, model);
            baseResponse.Message = MessageUtil.Instance.Updated;
            baseResponse.Data = contact;
            return baseResponse;
        }
    }
}
