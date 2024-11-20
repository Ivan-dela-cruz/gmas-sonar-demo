using AutoMapper;
using BUE.Inscriptions.Infrastructure.Interfaces;
using BUE.Inscriptions.Domain.Entity;
using BUE.Inscriptions.Domain.Entity.DTO;
using BUE.Inscriptions.Shared.Utils;
using Microsoft.EntityFrameworkCore;

namespace BUE.Inscriptions.Infrastructure.Repository
{
    public class FinanceInformationRepository : IFinanceInformationRepository
    {
        private readonly PortalMatriculasDBContext _db;
        private IMapper _mapper;
        public FinanceInformationRepository(PortalMatriculasDBContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }
        public async Task<FinanceInformationDTO> CreateAsync(FinanceInformationDTO entity)
        {
            FinanceInformation financeInformation = _mapper.Map<FinanceInformationDTO, FinanceInformation>(entity);
            _db.FinanceInformation.Add(financeInformation);
            await _db.SaveChangesAsync();
            return _mapper.Map<FinanceInformation, FinanceInformationDTO>(financeInformation);
        }
        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                FinanceInformation? financeInformation = await _db.FinanceInformation.FirstOrDefaultAsync(x => x.code == id);
                if (financeInformation is null)
                {
                    return false;
                }
                financeInformation.DeletedAt = DateTime.Now;
                _db.FinanceInformation.Update(financeInformation);
                //_db.FinanceInformation.Remove(financeInformation);
                await _db.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public async Task<IEnumerable<FinanceInformationDTO>> GetAsync() =>

            _mapper.Map<IEnumerable<FinanceInformationDTO>>(await _db.FinanceInformation.ToListAsync());



        public async Task<FinanceInformationDTO> GetByIdAsync(int id) =>

            _mapper.Map<FinanceInformationDTO>(await _db.FinanceInformation.FirstOrDefaultAsync(x => x.code == id));

        public async Task<FinanceInformationDTO> GetByStudentIdAsync(int id) =>

             _mapper.Map<FinanceInformationDTO>(await _db.FinanceInformation.FirstOrDefaultAsync(x => x.studentCodeSchoolYear == id));


        public async Task<FinanceInformationDTO> UpdateAsync(int id, FinanceInformationDTO entity)
        {
            FinanceInformation financeInformation = _mapper.Map<FinanceInformationDTO, FinanceInformation>(entity);
            var financeInformationBefore = await _db.FinanceInformation.AsNoTracking().FirstOrDefaultAsync(x => x.code == id);
            if (financeInformationBefore is null || financeInformation.code != id)
            {
                throw new NullReferenceException(MessageUtil.Instance.NotFound);
            }
            var currentEntity = MapperNewValues(financeInformation, financeInformationBefore);
            _db.FinanceInformation.Update(currentEntity);
            await _db.SaveChangesAsync();
            return _mapper.Map<FinanceInformation, FinanceInformationDTO>(currentEntity);
        }

        private FinanceInformation MapperNewValues(FinanceInformation newEntity, FinanceInformation olderEntity)
        {
            var currentEntity = new FinanceInformation();
            currentEntity.code = newEntity.code != null && newEntity.code != 0 ? newEntity.code : olderEntity.code;
            currentEntity.checkbookCode = newEntity.checkbookCode != null ? newEntity.checkbookCode : olderEntity.checkbookCode;
            currentEntity.debitTypeCode = newEntity.debitTypeCode != null ? newEntity.debitTypeCode : olderEntity.debitTypeCode;
            currentEntity.bankCode = newEntity.bankCode != null ? newEntity.bankCode : olderEntity.bankCode;
            currentEntity.contactCode = newEntity.contactCode != null ? newEntity.contactCode : olderEntity.contactCode;
            currentEntity.accountNumber = newEntity.accountNumber != null ? newEntity.accountNumber : olderEntity.accountNumber;
            currentEntity.typeIdentification = newEntity.typeIdentification != null ? newEntity.typeIdentification : olderEntity.typeIdentification;
            currentEntity.documentNumber = newEntity.documentNumber != null ? newEntity.documentNumber : olderEntity.documentNumber;
            currentEntity.namePaymentReference = newEntity.namePaymentReference != null ? newEntity.namePaymentReference : olderEntity.namePaymentReference;
            currentEntity.email = newEntity.email != null ? newEntity.email : olderEntity.email;
            currentEntity.studentCodeSchoolYear = newEntity.studentCodeSchoolYear != null ? newEntity.studentCodeSchoolYear : olderEntity.studentCodeSchoolYear;
            currentEntity.studentCodeSchoolYearBue = newEntity.studentCodeSchoolYearBue != null ? newEntity.studentCodeSchoolYearBue : olderEntity.studentCodeSchoolYearBue;
            currentEntity.requestCode = newEntity.requestCode != null ? newEntity.requestCode : olderEntity.requestCode;
            currentEntity.lastName = newEntity.lastName != null ? newEntity.lastName : olderEntity.lastName;
            currentEntity.secondName = newEntity.secondName != null ? newEntity.secondName : olderEntity.secondName;
            currentEntity.names = newEntity.names != null ? newEntity.names : olderEntity.names;
            currentEntity.status = newEntity.status != null ? newEntity.status : olderEntity.status;
            currentEntity.integrationStatus = newEntity.integrationStatus != null ? newEntity.integrationStatus : olderEntity.integrationStatus;
            currentEntity.paymentReference = newEntity.paymentReference != null ? newEntity.paymentReference : olderEntity.paymentReference;
            currentEntity.paymentCode = newEntity.paymentCode != null ? newEntity.paymentCode : olderEntity.paymentCode;
            currentEntity.companyScholarShip = newEntity.companyScholarShip != null ? newEntity.companyScholarShip : olderEntity.companyScholarShip;
            currentEntity.isScholarShip = newEntity.isScholarShip != null ? newEntity.isScholarShip : olderEntity.isScholarShip;
            currentEntity.creditCardCode = newEntity.creditCardCode != null ? newEntity.creditCardCode : olderEntity.creditCardCode;
            return currentEntity;
        }

    }
}
