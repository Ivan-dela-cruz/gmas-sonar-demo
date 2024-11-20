using BUE.Inscriptions.Application.Interfaces;
using BUE.Inscriptions.Domain.Entity.DTO;
using BUE.Inscriptions.Domain.Paging;
using BUE.Inscriptions.Domain.Response;
using BUE.Inscriptions.Infrastructure.Interfaces;
using BUE.Inscriptions.Shared.Utils;
namespace BUE.Inscriptions.Application.Implementation
{
    public class MedicalRecordService : IMedicalRecordService
    {
        private readonly IMedicalRecordRepository _medicalRecordRepository;

        public MedicalRecordService(IMedicalRecordRepository medicalRecordRepository)
        {
            _medicalRecordRepository = medicalRecordRepository;
        }

        public async Task<IBaseResponse<MedicalRecordDTO>> CreateServiceAsync(MedicalRecordDTO model)
        {
            var baseResponse = new BaseResponse<MedicalRecordDTO>();
            var medicalRecord = await _medicalRecordRepository.CreateAsync(model);
            baseResponse.Data = medicalRecord;
            return baseResponse;
        }

        public async Task<IBaseResponse<bool>> DeleteServiceAsync(int id)
        {
            var baseResponse = new BaseResponse<bool>();
            bool isSuccess = await _medicalRecordRepository.DeleteAsync(id);

            if (!isSuccess)
            {
                baseResponse.Message = MessageUtil.Instance.NotFound;
                baseResponse.status = false;
            }
            baseResponse.Data = isSuccess;
            return baseResponse;
        }
        public async Task<IBaseResponse<PagedList<MedicalRecordDTO>>> GetServiceAsync(PagingQueryParameters paging)
        {
            var baseResponse = new BaseResponse<PagedList<MedicalRecordDTO>>();
            var academicYears = await _medicalRecordRepository.GetAsync();

            if (academicYears == null)
            {
                baseResponse.Message = MessageUtil.Instance.NotFound;
                baseResponse.status = false;
            }
            baseResponse.Data = PagedList<MedicalRecordDTO>.ToPagedList(academicYears, paging.PageNumber, paging.PageSize);
            return baseResponse;
        }

        public async Task<IBaseResponse<MedicalRecordDTO>> GetByIdServiceAsync(int id)
        {
            var baseResponse = new BaseResponse<MedicalRecordDTO>();
            MedicalRecordDTO medicalRecord = await _medicalRecordRepository.GetByIdAsync(id);

            if (medicalRecord == null)
            {
                baseResponse.Message = MessageUtil.Instance.NotFound;
                baseResponse.status = false;
                return baseResponse;
            }
            baseResponse.Data = medicalRecord;
            return baseResponse;
        }

        public async Task<IBaseResponse<IEnumerable<MedicalRecordDTO>>> GetServiceAsync()
        {
            var baseResponse = new BaseResponse<IEnumerable<MedicalRecordDTO>>();
            var medicalRecords = await _medicalRecordRepository.GetAsync();

            if (medicalRecords == null)
            {
                baseResponse.Message = MessageUtil.Instance.NotFound;
                baseResponse.status = false;
            }
            baseResponse.Data = medicalRecords;
            return baseResponse;
        }

        public async Task<IBaseResponse<MedicalRecordDTO>> UpdateServiceAsync(int id, MedicalRecordDTO model)
        {
            var baseResponse = new BaseResponse<MedicalRecordDTO>();
            MedicalRecordDTO medicalRecord = await _medicalRecordRepository.UpdateAsync(id, model);
            baseResponse.Message = MessageUtil.Instance.Updated;
            baseResponse.Data = medicalRecord;
            return baseResponse;
        }
    }
}
