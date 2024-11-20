using BUE.Inscriptions.Application.Interfaces;
using BUE.Inscriptions.Domain.Entity.DTO;
using BUE.Inscriptions.Domain.Inscriptions.DTO;
using BUE.Inscriptions.Domain.Paging;
using BUE.Inscriptions.Domain.Response;
using BUE.Inscriptions.Infrastructure.Interfaces;
using BUE.Inscriptions.Shared.Utils;


namespace BUE.Inscriptions.Application.Implementation
{
    public class PersonService : IPersonService
    {
        private readonly IPersonRepository _personRepository;
   

        public PersonService(IPersonRepository personRepository)
        {
            _personRepository = personRepository;
        }

        public async Task<IBaseResponse<PersonDTO>> CreateServiceAsync(PersonDTO model)
        {
            #region create user
            //var newUser = new UserDTO()
            //{
            //    Identification = model.Identification,
            //    FirstLastName = model.LastName,
            //    SecondaryLastName = model.SecondLastName,
            //    Names = $"{model.FirstName} {model.MiddleName}",
            //    Email = $"{model.Email}",
            //    UserName = $"{model.FirstName} {model.LastName}",
            //    BirthDate = model.BirthDate,
            //    Address = model.Address,
            //    Cellphone = model.CellPhone,
            //    Activo = true,
            //    Status = true,
            //    EmailVerification = true,
            //    Password = model.Identification,
            //    ContactCode = model.ExternalId

            //};
            //var userResult = await _userService.CreateWithPassServiceAsync(newUser);
            //if (userResult.status == false)
            //{
            //    baseResponse.Data = null;
            //    baseResponse.status = false;
            //    baseResponse.statusCode = userResult.statusCode;
            //    baseResponse.Message = userResult.Message;
            //    return baseResponse;
            //}
            //model.UserId = (int)userResult.Data.Code;
            #endregion
            var baseResponse = new BaseResponse<PersonDTO>() { Message = MessageUtil.Instance.Created, statusCode = MessageUtil.Instance.SUCCESS };
            var person = await _personRepository.CreateAsync(model);
            if (person == null)
            {
                baseResponse.Data = null;
                baseResponse.status = false;
                baseResponse.statusCode = _personRepository.StatusCode;
                baseResponse.Message = _personRepository.Message;
            }
            baseResponse.Data = person;
            return baseResponse;
        }

        public async Task<IBaseResponse<bool>> DeleteServiceAsync(int id)
        {
            var baseResponse = new BaseResponse<bool>();
            bool isSuccess = await _personRepository.DeleteAsync(id);

            if (!isSuccess)
            {
                baseResponse.Message = MessageUtil.Instance.NotFound;
                baseResponse.status = false;
            }
            baseResponse.Data = isSuccess;
            return baseResponse;
        }

        public async Task<IBaseResponse<PersonDTO>> GetByIdServiceAsync(int id)
        {
            var baseResponse = new BaseResponse<PersonDTO>();
            PersonDTO person = await _personRepository.GetByIdAsync(id);

            if (person == null)
            {
                baseResponse.Message = MessageUtil.Instance.NotFound;
                baseResponse.status = false;
                return baseResponse;
            }
            baseResponse.Data = person;
            return baseResponse;
        }
        public async Task<IBaseResponse<PersonDTO>> GetByIdentificationServiceAsync(string identificaction)
        {
            var baseResponse = new BaseResponse<PersonDTO>();
            PersonDTO person = await _personRepository.GetByIdentificationAsync(identificaction);

            if (person == null)
            {
                baseResponse.Message = MessageUtil.Instance.NotFound;
                baseResponse.status = false;
                return baseResponse;
            }
            baseResponse.Data = person;
            return baseResponse;
        }

        public async Task<IBaseResponse<PagedList<PersonDTO>>> GetServiceAsync(PagingQueryParameters paging)
        {
            var baseResponse = new BaseResponse<PagedList<PersonDTO>>();
            var persons = await _personRepository.GetAsync();

            if (persons == null)
            {
                baseResponse.Message = MessageUtil.Instance.NotFound;
                baseResponse.status = false;
            }
            baseResponse.Data = PagedList<PersonDTO>.ToPagedList(persons, paging.PageNumber, paging.PageSize);
            return baseResponse;
        }

        public async Task<IBaseResponse<PersonDTO>> UpdateServiceAsync(int id, PersonDTO model)
        {
            var baseResponse = new BaseResponse<PersonDTO>() { Message = MessageUtil.Instance.Updated , statusCode = MessageUtil.Instance.SUCCESS };
            PersonDTO person = await _personRepository.UpdateAsync(id, model);
            if (person == null)
            {
                baseResponse.Data = null;
                baseResponse.status = false;
                baseResponse.statusCode = _personRepository.StatusCode;
                baseResponse.Message = _personRepository.Message;
            }
            baseResponse.Data = person;
            return baseResponse;
        }
    }
}
