using AutoMapper;
using BUE.Inscriptions.Domain.Entity.DTO;
using BUE.Inscriptions.Domain.Entity;
using BUE.Inscriptions.Infrastructure.Interfaces;
using BUE.Inscriptions.Shared.Utils;
using Microsoft.EntityFrameworkCore;

namespace BUE.Inscriptions.Infrastructure.Repository
{
    public class MedicalRecordRepository : BaseRepository, IMedicalRecordRepository
    {
        private readonly PortalMatriculasDBContext _db;
        private readonly IMapper _mapper;
        private readonly IAwsS3UploaderRepository _awsS3;

        public MedicalRecordRepository(PortalMatriculasDBContext db, IMapper mapper, IAwsS3UploaderRepository awsS3)
        {
            _db = db;
            _mapper = mapper;
            _awsS3 = awsS3;
        }

        public async Task AttachFiles(MedicalRecord medicalRecord, MedicalRecordDTO entity)
        {
            string s3Result = "";
            if (entity.DisqualifiedSportFile is not null && entity.DisqualifiedSportFile.Length > 0)
            {
                string subPathDocS3 = String.Format(@"students/{0}/MedicalRecord/Disqualified", entity.RequestId);
                s3Result = await _awsS3.UploadBucketFileAsync(subPathDocS3, "medical_record_disqualified" + entity.RequestId.ToString().Trim() + ".pdf", entity.DisqualifiedSportFile);
                medicalRecord.DisqualifiedSportUrlFile = s3Result;
            }
            if (entity.VaccineFile is not null && entity.VaccineFile.Length > 0)
            {
                string subPathDocS3 = String.Format(@"students/{0}/MedicalRecord/Vaccines", entity.RequestId);
                s3Result = await _awsS3.UploadBucketFileAsync(subPathDocS3, "medical_record_vaccines" + entity.RequestId.ToString().Trim() + ".pdf", entity.VaccineFile);
                medicalRecord.VaccineUrlFile = s3Result;
            }
            if (entity.HasIncapacityFile is not null && entity.HasIncapacityFile.Length > 0)
            {
                string subPathDocS3 = String.Format(@"students/{0}/MedicalRecord/Vaccines", entity.RequestId);
                s3Result = await _awsS3.UploadBucketFileAsync(subPathDocS3, "medical_record_descapacidad" + entity.RequestId.ToString().Trim() + ".pdf", entity.HasIncapacityFile);
                medicalRecord.HasIncapacityUrlFile = s3Result;
            }
        }
        public async Task<MedicalRecordDTO> CreateAsync(MedicalRecordDTO entity)
        {
            var request = await _db.PortalRequests.FindAsync(entity.RequestId);
            if (request == null)
                throw new NullReferenceException(MessageUtil.Instance.NotFound);
            MedicalRecord medicalRecord = _mapper.Map<MedicalRecordDTO, MedicalRecord>(entity);
            await AttachFiles(medicalRecord, entity);
            _db.MedicalRecords.Add(medicalRecord);
            await _db.SaveChangesAsync();

            return _mapper.Map<MedicalRecord, MedicalRecordDTO>(medicalRecord);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                MedicalRecord medicalRecord = await _db.MedicalRecords.FindAsync(id);
                if (medicalRecord == null)
                {
                    return false;
                }

                _db.MedicalRecords.Remove(medicalRecord);
                await _db.SaveChangesAsync();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<IEnumerable<MedicalRecordDTO>> GetAsync()
        {
            var medicalRecords = await _db.MedicalRecords.ToListAsync();
            return _mapper.Map<IEnumerable<MedicalRecordDTO>>(medicalRecords);
        }

        public async Task<MedicalRecordDTO> GetByIdAsync(int id)
        {
            var medicalRecord = await _db.MedicalRecords.FindAsync(id);
            return _mapper.Map<MedicalRecordDTO>(medicalRecord);
        }

        public async Task<MedicalRecordDTO> UpdateAsync(int id, MedicalRecordDTO entity)
        {
            var request = await _db.PortalRequests.FindAsync(entity.RequestId);
            if (request == null)
                throw new NullReferenceException(MessageUtil.Instance.NotFound);
            MedicalRecord medicalRecord = _mapper.Map<MedicalRecordDTO, MedicalRecord>(entity);
            var existingMedicalRecord = await _db.MedicalRecords.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
            if (existingMedicalRecord == null)
                throw new NullReferenceException(MessageUtil.Instance.NotFound);

            await AttachFiles(medicalRecord, entity);
            MapProperties(medicalRecord, existingMedicalRecord);
            _db.MedicalRecords.Update(medicalRecord);
            await _db.SaveChangesAsync();
            return _mapper.Map<MedicalRecord, MedicalRecordDTO>(medicalRecord);
        }
    }
}
