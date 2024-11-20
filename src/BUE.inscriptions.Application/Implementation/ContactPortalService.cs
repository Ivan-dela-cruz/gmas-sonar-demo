using BUE.Inscriptions.Infrastructure.Interfaces;
using BUE.Inscriptions.Domain.Entity.DTO;
using BUE.Inscriptions.Domain.Paging;
using BUE.Inscriptions.Domain.Response;
using BUE.Inscriptions.Application.Interfaces;
using BUE.Inscriptions.Shared.Utils;

namespace BUE.Inscriptions.Application.Implementation
{
    public class ContactPortalService : IContactPortalService
    {
        private IContactPortalRepository _contactRep;
        private IUserRepository _userRep;
        public ContactPortalService(IContactPortalRepository contactRep, IUserRepository userRep)
        {
            _contactRep = contactRep;
            _userRep = userRep;

        }
        public async Task<IBaseResponse<ContactPortalDTO>> CreateServiceAsync(ContactPortalDTO model)
        {
            var BackModel = model;
            var baseResponse = new BaseResponse<ContactPortalDTO>();
            var contact = await _contactRep.CreateAsync(model);
            if (contact is null)
            {
                baseResponse.Message = MessageUtil.Instance.Warning;
                baseResponse.status = false;
            }
            if (model.isLegalRepresentative != null && model.isLegalRepresentative == true && model.userCode != null)
            {
                if(BackModel.ContactCodeBue == null)
                    await _userRep.UpdateUserWithContacAsync((int)contact.userCode, (int)contact.code);
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
        public async Task<IBaseResponse<ContactPortalDTO>> GetByIdServiceAsync(int id)
        {
            var baseResponse = new BaseResponse<ContactPortalDTO>();
            ContactPortalDTO contact = await _contactRep.GetByIdAsync(id);
            if (contact is null)
            {
                baseResponse.Message = MessageUtil.Instance.NotFound;
                baseResponse.status = false;
            }
            baseResponse.Data = contact;
            return baseResponse;
        }
        public async Task<IBaseResponse<ContactPortalDTO>> GetByCodeContactCurrentSchoolAsync(int codeContact, int currentYearSchool)
        {
            var baseResponse = new BaseResponse<ContactPortalDTO>();
            ContactPortalDTO contact = await _contactRep.GetByCodeContactCurrentSchoolAsync(codeContact, currentYearSchool);
            if (contact is null)
            {
                baseResponse.Message = MessageUtil.Instance.NotFound;
                baseResponse.status = false;
            }
            baseResponse.Data = contact;
            return baseResponse;
        }
        public async Task<IBaseResponse<PagedList<ContactPortalDTO>>> GetServiceAsync(PagingQueryParameters paging)
        {
            var baseResponse = new BaseResponse<PagedList<ContactPortalDTO>>();
            var contacts = await _contactRep.GetAsync();
            if (contacts is null)
            {
                baseResponse.Message = MessageUtil.Instance.NotFound;
                baseResponse.status = false;
            }
            baseResponse.Data = PagedList<ContactPortalDTO>.ToPagedList(contacts, paging.PageNumber, paging.PageSize);
            return baseResponse;
        }
        public async Task<IBaseResponse<PagedList<ContactPortalDTO>>> GetListBasicServiceAsync(PagingQueryParameters paging)
        {
            var baseResponse = new BaseResponse<PagedList<ContactPortalDTO>>();
            var contacts = await _contactRep.GetlistBasicAsync();
            if (contacts is null)
            {
                baseResponse.Message = MessageUtil.Instance.NotFound;
                baseResponse.status = false;
            }
            baseResponse.Data = PagedList<ContactPortalDTO>.ToPagedList(contacts, paging.PageNumber, paging.PageSize);
            return baseResponse;
        }
        public async Task<IBaseResponse<ContactPortalDTO>> UpdateServiceAsync(int id, ContactPortalDTO model)
        {

            var baseResponse = new BaseResponse<ContactPortalDTO>();
            ContactPortalDTO contact = await _contactRep.UpdateAsync(id, model);
            contact.photo = null;
            contact.documentFile = null;
            baseResponse.Message = MessageUtil.Instance.Updated;
            baseResponse.Data = contact;
            return baseResponse;
        }
        public async Task<IBaseResponse<bool>> GetByIdenditicationServiceAsync(string documentNumber)
        {
            var baseResponse = new BaseResponse<bool>();
            ContactPortalDTO contact = await _contactRep.GetByIdentificactionAsync(documentNumber);
            baseResponse.Data = true;
            baseResponse.Message = MessageUtil.Instance.Found;
            baseResponse.status = false;
            if (contact is null)
            {
                baseResponse.Data = false;
                baseResponse.Message = MessageUtil.Instance.NotFound;
                baseResponse.status = true;
            }
            return baseResponse;
        }
        public async Task<IBaseResponse<bool>> GetByEmailServiceAsync(string email)
        {
            var baseResponse = new BaseResponse<bool>();
            ContactPortalDTO contact = await _contactRep.GetByIdEmailAsync(email);
            baseResponse.Data = true;
            baseResponse.Message = MessageUtil.Instance.Found;
            baseResponse.status = false;
            if (contact is null)
            {
                baseResponse.Data = false;
                baseResponse.Message = MessageUtil.Instance.NotFound;
                baseResponse.status = true;
            }
            return baseResponse;
        }
    }
}
