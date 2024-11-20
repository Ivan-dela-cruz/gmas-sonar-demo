using AutoMapper;
using BUE.Inscriptions.Infrastructure.Interfaces;
using BUE.Inscriptions.Shared.Utils;
using Microsoft.EntityFrameworkCore;
using BUE.Inscriptions.Domain.Inscriptions.Entities;
using BUE.Inscriptions.Domain.Inscriptions.DTO;


namespace BUE.Inscriptions.Infrastructure.Repository
{
    public class PersonRepository : BaseRepository, IPersonRepository
    {
        private readonly PortalMatriculasDBContext _db;
        private readonly IMapper _mapper;
        private readonly IAwsS3UploaderRepository _awsS3;
        public string Message { get; set; }
        public string StatusCode { get; set; }

        public PersonRepository(PortalMatriculasDBContext db, IMapper mapper, IAwsS3UploaderRepository awsS3)
        {
            _db = db;
            _mapper = mapper;
            _awsS3 = awsS3;
        }
        public async Task AttachFiles(Person person, PersonDTO entityDto)
        {
            if (entityDto.FilesStorage != null)
            {
                foreach (var row in entityDto.FilesStorage)
                {
                    string fileName = person.Id + "_" + row.FileName + "." + row.FileType;
                    if (row.FileBytes is not null && row.FileBytes.Length > 0)
                    {
                        var s3Result = await _awsS3.UploadBucketFileAsync(row.FilePath, fileName, row.FileBytes);
                        if (!string.IsNullOrEmpty(s3Result))
                        {
                            switch (row.FieldName.ToLower())
                            {
                                case "image":
                                    person.Image = s3Result;
                                    break;
                                case "documentidentification":
                                    person.DocumentIdentification = s3Result;
                                    break;
                            }
                        }
                    }
                }
            }
        }

        public async Task<PersonDTO> CreateAsync(PersonDTO entity)
        {
            try
            {
                Person person = _mapper.Map<PersonDTO, Person>(entity);
                _db.Person.Add(person);
                await _db.SaveChangesAsync();
                await AttachFiles(person, entity);
                await _db.SaveChangesAsync();
                return _mapper.Map<Person, PersonDTO>(person);

            }
            catch (Exception ex)
            {
                HandlerOperationException(ex);
                Message = this.MessageException;
                StatusCode = this.CodeException;
                return null;

            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                Person person = await _db.Person.FindAsync(id);
                if (person == null)
                {
                    return false;
                }
                _db.Person.Remove(person);
                await _db.SaveChangesAsync();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<IEnumerable<PersonDTO>> GetAsync()
        {
            var persons = await _db.Person
                .ToListAsync();
            return _mapper.Map<IEnumerable<PersonDTO>>(persons);
        }

        public async Task<PersonDTO> GetByIdAsync(int id)
        {
            var person = await _db.Person
                .FirstOrDefaultAsync(e => e.Id == id);
            return _mapper.Map<PersonDTO>(person);
        }
        public async Task<PersonDTO> GetByIdentificationAsync(string identification)
        {
            var person = await _db.Person
                .FirstOrDefaultAsync(e => e.Identification.Trim() == identification);
            return _mapper.Map<PersonDTO>(person);
        }

        public async Task<PersonDTO> UpdateAsync(int id, PersonDTO entity)
        {
            try
            {
                var person = _mapper.Map<PersonDTO, Person>(entity);
                var existingPerson = await _db.Person.FindAsync(id);
                if (existingPerson == null)
                {
                    throw new NullReferenceException(MessageUtil.Instance.NotFound);
                }
                await AttachFiles(person, entity);
                var currentPerson = MapProperties(person, existingPerson);
                currentPerson.UserId = existingPerson.UserId;
                _db.Person.Update(currentPerson);
                await _db.SaveChangesAsync();
                return _mapper.Map<Person, PersonDTO>(currentPerson);
            }
            catch (Exception ex) {
                HandlerOperationException(ex);
                Message = this.MessageException;
                StatusCode = this.CodeException;
                return null;
            }
           
        }
    }
}
