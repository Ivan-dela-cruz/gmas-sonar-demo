using BUE.Inscriptions.Domain.Entity.DTO;
using BUE.Inscriptions.Domain.Reports;

namespace BUE.Inscriptions.Infrastructure.Interfaces
{
    public interface IGenaralBueRepository
    {
        Task<IEnumerable<LevelDTO>> getLevelAsync();
        Task<IEnumerable<CourseGradeDTO>> getCoursesAsync();
        Task<IEnumerable<ParallelClassDTO>> getParallesAsync();
        Task<IEnumerable<SpecialtyDTO>> getSpecialtiesAsync();
        Task<IEnumerable<NationalityDTO>> getNationalitiesAsync();
        Task<IEnumerable<CountryDTO>> getCountriesAsync();
        Task<IEnumerable<ProvinceDTO>> getProvincesAsync();
        Task<IEnumerable<CantonDTO>> getCantonsAsync();
        Task<IEnumerable<ParishDTO>> getParishAsync();
        Task<IEnumerable<RelationShipDTO>> getRelationShipsAsync();
        Task<IEnumerable<ProfessionDTO>> getProfessionsAsync();
        Task<IEnumerable<SchoolYearDTO>> getSchoolYearsAsync();
        Task<IEnumerable<TransactionStudent>> getTransactionByStudentAsync(int studentCode);
        Task<IEnumerable<TransactionStudent>> getTransactionsAllStudentsAsync();
        Task<IEnumerable<TransactionDetailStudent>> getTransactionDetailByStudentAsync(int studentCode);
        Task<IEnumerable<TransactionDetailStudent>> getTransactionDetailsAllStudentsAsync();
    }
}
