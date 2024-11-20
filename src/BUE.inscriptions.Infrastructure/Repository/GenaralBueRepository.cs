using AutoMapper;
using BUE.Inscriptions.Infrastructure.Interfaces;
using BUE.Inscriptions.Domain.Entity.DTO;
using Microsoft.EntityFrameworkCore;
using BUE.Inscriptions.Domain.Reports;

namespace BUE.Inscriptions.Infrastructure.Repository
{
    public class GenaralBueRepository : IGenaralBueRepository
    {
        private readonly BueDBContext _dbBue;
        private IMapper _mapper;
        public GenaralBueRepository(BueDBContext dbBue, IMapper mapper)
        {
            _dbBue = dbBue;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CourseGradeDTO>> getCoursesAsync() =>
            _mapper.Map<IEnumerable<CourseGradeDTO>>(await _dbBue.CourseGrades.Where(o => o.Status).ToListAsync());

        public async Task<IEnumerable<LevelDTO>> getLevelAsync() =>
         _mapper.Map<IEnumerable<LevelDTO>>(await _dbBue.Levels.Where(o => o.Status).ToListAsync());

        public async Task<IEnumerable<ParallelClassDTO>> getParallesAsync() =>
            _mapper.Map<IEnumerable<ParallelClassDTO>>(await _dbBue.Parallels.Where(o => o.Status).ToListAsync());

        public async Task<IEnumerable<SpecialtyDTO>> getSpecialtiesAsync() =>
            _mapper.Map<IEnumerable<SpecialtyDTO>>(await _dbBue.Specialties.Where(o => o.Status).ToListAsync());
        public async Task<IEnumerable<NationalityDTO>> getNationalitiesAsync() =>
            _mapper.Map<IEnumerable<NationalityDTO>>(await _dbBue.Nationalities.Where(o => o.status).ToListAsync());
        public async Task<IEnumerable<CountryDTO>> getCountriesAsync() =>
            _mapper.Map<IEnumerable<CountryDTO>>(await _dbBue.Countries.Where(o => o.status).ToListAsync());
        public async Task<IEnumerable<ProvinceDTO>> getProvincesAsync() =>
           _mapper.Map<IEnumerable<ProvinceDTO>>(await _dbBue.Provinces.Where(o => o.status).ToListAsync());
        public async Task<IEnumerable<CantonDTO>> getCantonsAsync() =>
           _mapper.Map<IEnumerable<CantonDTO>>(await _dbBue.Cantons.Where(o => o.status).ToListAsync());
        public async Task<IEnumerable<ParishDTO>> getParishAsync() =>
           _mapper.Map<IEnumerable<ParishDTO>>(await _dbBue.Parishes.Where(o => o.status).ToListAsync());
        public async Task<IEnumerable<RelationShipDTO>> getRelationShipsAsync() =>
           _mapper.Map<IEnumerable<RelationShipDTO>>(await _dbBue.RelationsShip.Where(o => o.status).ToListAsync());
        public async Task<IEnumerable<ProfessionDTO>> getProfessionsAsync() =>
           _mapper.Map<IEnumerable<ProfessionDTO>>(await _dbBue.Professions.Where(o => o.status).ToListAsync());
        public async Task<IEnumerable<SchoolYearDTO>> getSchoolYearsAsync() =>
           _mapper.Map<IEnumerable<SchoolYearDTO>>(await _dbBue.SchoolYears.ToListAsync());
        public async Task<IEnumerable<TransactionDetailStudent>> getTransactionDetailByStudentAsync(int studentCode)
        {
            var res = await _dbBue.TransactionDetailStudent
             .FromSqlInterpolated($"EXEC [dbo].[gmas_sp_rptDuedasDetalleEstudiante] @CodigoEstudiante = {studentCode}")
             .AsNoTracking()
             .ToListAsync();
            return res;
        }
        public async Task<IEnumerable<TransactionDetailStudent>> getTransactionDetailsAllStudentsAsync()
        {
            var res = await _dbBue.TransactionDetailStudent
             .FromSqlInterpolated($"EXEC [dbo].[gmas_sp_rptDuedasDetalleEstudiante]")
             .AsNoTracking()
             .ToListAsync();
            return res;
        }
        public async Task<IEnumerable<TransactionStudent>> getTransactionByStudentAsync(int studentCode)
        {
            var res = await _dbBue.TransactionStudent
             .FromSqlInterpolated($"EXEC [dbo].[gmas_sp_rptDuedasEstudiante] @CodigoEstudiante = {studentCode}")
             .AsNoTracking()
             .ToListAsync();
            return res;
        }
        public async Task<IEnumerable<TransactionStudent>> getTransactionsAllStudentsAsync()
        {
            var res = await _dbBue.TransactionStudent
             .FromSqlInterpolated($"EXEC [dbo].[gmas_sp_rptDuedasEstudiante]")
             .AsNoTracking()
             .ToListAsync();
            return res;
        }

    }
}
