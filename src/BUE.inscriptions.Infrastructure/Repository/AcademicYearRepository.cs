using System.Reflection;
using AutoMapper;
using BUE.Inscriptions.Domain.Inscriptions.DTO;
using BUE.Inscriptions.Domain.Inscriptions.Entities;
using BUE.Inscriptions.Infrastructure.Interfaces;
using BUE.Inscriptions.Shared.Utils;
using Microsoft.EntityFrameworkCore;


namespace BUE.Inscriptions.Infrastructure.Repository
{
    public class AcademicYearRepository : BaseRepository, IAcademicYearRepository
    {
        private readonly PortalMatriculasDBContext _db;
        private readonly IMapper _mapper;

        public AcademicYearRepository(PortalMatriculasDBContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<AcademicYearDTO> CreateAsync(AcademicYearDTO entity)
        {
            AcademicYear academicYear = _mapper.Map<AcademicYearDTO, AcademicYear>(entity);
            _db.AcademicYears.Add(academicYear);
            await _db.SaveChangesAsync();

            return _mapper.Map<AcademicYear, AcademicYearDTO>(academicYear);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                AcademicYear academicYear = await _db.AcademicYears.FindAsync(id);
                if (academicYear == null)
                {
                    return false;
                }

                // Your implementation for deleting an AcademicYear
                _db.AcademicYears.Remove(academicYear);
                await _db.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                LogManagement.Instance.writeAssembly(MethodBase.GetCurrentMethod().DeclaringType, $"StackTrace: {ex.StackTrace} Error: {ex.Message}", ".Error");
                return false;
            }
        }

        public async Task<IEnumerable<AcademicYearDTO>> GetAsync()
        {
            var academicYears = await _db.AcademicYears.ToListAsync();
            return _mapper.Map<IEnumerable<AcademicYearDTO>>(academicYears);
        }

        public async Task<AcademicYearDTO> GetByIdAsync(int id)
        {
            var academicYear = await _db.AcademicYears.FindAsync(id);
            return _mapper.Map<AcademicYearDTO>(academicYear);
        }

        public async Task<AcademicYearDTO> UpdateAsync(int id, AcademicYearDTO entity)
        {
            AcademicYear academicYear = _mapper.Map<AcademicYearDTO, AcademicYear>(entity);
            var existingAcademicYear = await _db.AcademicYears.FindAsync(id);

            if (existingAcademicYear == null)
            {
                throw new NullReferenceException(MessageUtil.Instance.NotFound);
            }
            MapProperties(academicYear, existingAcademicYear);
            await _db.SaveChangesAsync();
            return _mapper.Map<AcademicYear, AcademicYearDTO>(existingAcademicYear);
        }
    }
}
