using AutoMapper;
using BUE.Inscriptions.Infrastructure.Interfaces;
using BUE.Inscriptions.Domain.Entity;
using BUE.Inscriptions.Domain.Entity.DTO;
using BUE.Inscriptions.Shared.Utils;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using System.Xaml.Permissions;
using System.Linq;
using System.Reflection;

namespace BUE.Inscriptions.Infrastructure.Repository
{
    public class StudentRepresentativeRepository : IStudentRepresentativeRepository
    {

        private readonly BueDBContext _dbBue;
        private IMapper _mapper;
        public StudentRepresentativeRepository(BueDBContext dbBue, IMapper mapper)
        {
            _dbBue = dbBue;
            _mapper = mapper;
        }

        public async Task<IEnumerable<StudentBUEDTO>> getContactStudentsAsyncBUE(int contactCode, int currentSchoolYear)
        {
            List<StudentBUE> students = new List<StudentBUE>();
            try
            {
                List<int> codes = new List<int>();
                var listRep = await _dbBue.studentRepresentatives.Where(x => x.contactCode == contactCode && x.isLegalRepresentative).ToListAsync();
                foreach (var item in listRep)
                {
                    codes.Add(item.studentCodeSchoolYear);
                }
                students = await _dbBue.Students
                .Where(x => codes.Contains((int)x.studentCodeSchoolYear) && x.currentSchoolYear == currentSchoolYear)
                .ToListAsync();

                return _mapper.Map<IEnumerable<StudentBUEDTO>>(students);
            }
            catch (Exception ex)
            {
                LogManagement.Instance.writeAssembly(MethodBase.GetCurrentMethod().DeclaringType, $"StackTrace: {ex.StackTrace} Error: {ex.Message}", ".Error");
                var remap = _mapper.Map<IEnumerable<StudentBUEDTO>>(students);
                return remap;
            }
        }
        public async Task<IEnumerable<ContactDTO>> GetAuthPeopleByStudentAsync(int studentCode, int currentSchoolYear)
        {
            List<Contact> contacts = new List<Contact>();
            try
            {
                var student = await _dbBue.Students.FirstOrDefaultAsync(x => x.studentCode == studentCode && x.currentSchoolYear == currentSchoolYear);
                if (student == null) return null;
                List<int> codes = new List<int>();
                var listRep = await _dbBue.studentRepresentatives.Where(x => x.studentCodeSchoolYear == student.studentCodeSchoolYear && x.relationshipStudentCode > 2 && x.isResponsible == true).ToListAsync();
                foreach (var item in listRep)
                {
                    codes.Add(item.contactCode);
                }
                contacts = await _dbBue.Contacts
                .Where(x => codes.Contains((int)x.code))
                .ToListAsync();
                return _mapper.Map<IEnumerable<ContactDTO>>(contacts);
            }
            catch (Exception ex)
            {
                LogManagement.Instance.writeAssembly(MethodBase.GetCurrentMethod().DeclaringType, $"StackTrace: {ex.StackTrace} Error: {ex.Message}", ".Error");
                var remap = _mapper.Map<IEnumerable<ContactDTO>>(contacts);
                return remap;
            }
        }
        public async Task<ContactDTO> GetSecondContactByStudentAsync(int studentCode, int currentSchoolYear, int currentFirstRepresentative)
        {
            try
            {
                List<int> relationsShips = new List<int> { 1, 2 };
                var contactExist = await _dbBue.Contacts.Where(x => x.code == currentFirstRepresentative).FirstOrDefaultAsync();
                if (contactExist == null) return null;
                var student = await _dbBue.Students.FirstOrDefaultAsync(x => x.studentCode == studentCode && x.currentSchoolYear == currentSchoolYear);
                if (student == null) return null;
                // busca padres y madres
                var secondRepresentative = await _dbBue.studentRepresentatives.Where(x => x.studentCodeSchoolYear == student.studentCodeSchoolYear && x.isResponsible == true && x.contactCode != currentFirstRepresentative && relationsShips.Contains(x.relationshipStudentCode)).FirstOrDefaultAsync();
                if (secondRepresentative == null) // si no encuentra parde o padre buscara otro
                    secondRepresentative = await _dbBue.studentRepresentatives.Where(x => x.studentCodeSchoolYear == student.studentCodeSchoolYear && x.isResponsible == true && x.contactCode != currentFirstRepresentative).FirstOrDefaultAsync();
                if (secondRepresentative == null) return null;

                var contact = await _dbBue.Contacts
                .Where(x => x.code == secondRepresentative.contactCode)
                .FirstOrDefaultAsync();
                if (contact == null) return null;
                return _mapper.Map<ContactDTO>(contact);
            }
            catch (Exception ex)
            {
                LogManagement.Instance.writeAssembly(MethodBase.GetCurrentMethod().DeclaringType, $"StackTrace: {ex.StackTrace} Error: {ex.Message}", ".Error");
                return null;
            }
        }
    }
}
