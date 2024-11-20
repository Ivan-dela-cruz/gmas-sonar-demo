using AutoMapper;
using BUE.Inscriptions.Infrastructure.Interfaces;
using BUE.Inscriptions.Domain.Entity.DTO;
using Microsoft.EntityFrameworkCore;
using BUE.Inscriptions.Domain.Reports;
using BUE.Inscriptions.Domain.Entity;

namespace BUE.Inscriptions.Infrastructure.Repository
{
    public class GenaralPortalRepository : IGenaralPortalRepository
    {
        private readonly PortalMatriculasDBContext _portalDB;
        private IMapper _mapper;
        public GenaralPortalRepository(PortalMatriculasDBContext portalDB, IMapper mapper)
        {
            _portalDB = portalDB;
            _mapper = mapper;
        }

        public async Task<IEnumerable<StatusDTO>> getStatusAsync() =>
            _mapper.Map<IEnumerable<StatusDTO>>(await _portalDB.Status.Where(o => o.status).ToListAsync());
        public async Task<IEnumerable<SchoolYearDTO>> getSchoolYearsAsync() =>
           _mapper.Map<IEnumerable<SchoolYearDTO>>(await _portalDB.SchoolYear.Where(o => o.status).ToListAsync());
        public async Task<IEnumerable<BankDTO>> getBankAsync() =>
           _mapper.Map<IEnumerable<BankDTO>>(await _portalDB.Bank.Where(o => o.status).ToListAsync());
        public async Task<IEnumerable<PaymentMethodDTO>> getPaymentMethodAsync() =>
          _mapper.Map<IEnumerable<PaymentMethodDTO>>(await _portalDB.PaymentMethod.ToListAsync());
        public async Task<IEnumerable<DebitTypeDTO>> getDebitTypeAsync() =>
          _mapper.Map<IEnumerable<DebitTypeDTO>>(await _portalDB.DebitType.Where(o => o.status).ToListAsync());
        public async Task<IEnumerable<RelationShipDTO>> getRelationShipsAsync() =>
           _mapper.Map<IEnumerable<RelationShipDTO>>(await _portalDB.RelationsShip.Where(o => o.status).ToListAsync());
        public async Task<IEnumerable<CreditCardDTO>> getCredtCardAsync() =>
           _mapper.Map<IEnumerable<CreditCardDTO>>(await _portalDB.CreditCard.Where(o => o.status).ToListAsync());
        public async Task<IEnumerable<CivilStatusDTO>> getCivilStatusAsync() =>
          _mapper.Map<IEnumerable<CivilStatusDTO>>(await _portalDB.CivilStatus.ToListAsync());
        public async Task<SchoolYearDTO> getSchoolYearByCodeAsync(int code) =>
          _mapper.Map<SchoolYearDTO>(await _portalDB.SchoolYear.FindAsync(code));
        public async Task<IEnumerable<RecordStudentReport>> getRecordStudentReportAsync(int code, int studentCode,int requestCode)
        {
            var res = await _portalDB.RecordStudentReport
             .FromSqlInterpolated($"EXEC [dbo].[gmas_sp_rptFichaRegistro] @codigoAnioLectivo = {code}, @CodigoEstudianteAnioLectivo = {studentCode}")
             .AsNoTracking()
             .ToListAsync();
            var response = _mapper.Map<IEnumerable<RecordStudentReport>>(res);

            var peopleAuth = await getRecordAuthPeople(requestCode);
            var peopleAuthMap = _mapper.Map<IEnumerable<RecordAuthPeople>>(peopleAuth);

            var medicalRecord = await _portalDB.MedicalRecords.Where(x => x.RequestId == requestCode).FirstOrDefaultAsync();


            foreach (var  item in response)
            {
                item.personasAutorizadas = peopleAuthMap;
                item.medicalRecord = medicalRecord;
            }

            return response;
        }
        public async Task<IEnumerable<RecordAuthPeople>> getRecordAuthPeople(int code)
        {
            var res = await _portalDB.RecordAuthPeople
             .FromSqlInterpolated($"EXEC [dbo].[gmas_sp_rptPersonaAutorizadasFicha] @CodigoSolicitud = {code}")
             .AsNoTracking()
             .ToListAsync();
            var response = _mapper.Map<IEnumerable<RecordAuthPeople>>(res);
            return response;
        }


    }
}
