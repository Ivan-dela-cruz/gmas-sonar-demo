using AutoMapper;
using BUE.Inscriptions.Infrastructure.Interfaces;
using BUE.Inscriptions.Domain.Entity;
using BUE.Inscriptions.Domain.Entity.DTO;
using BUE.Inscriptions.Shared.Utils;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace BUE.Inscriptions.Infrastructure.Repository
{
    public class ContactRepository : BaseRepository, IContactRepository
    {
        private readonly BueDBContext _db;
        private IMapper _mapper;
        public ContactRepository(BueDBContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }
        public async Task<ContactDTO> CreateAsync(ContactDTO entity)
        {
            Contact contact = _mapper.Map<ContactDTO, Contact>(entity);
            _db.Contacts.Add(contact);
            await _db.SaveChangesAsync();
            return _mapper.Map<Contact, ContactDTO>(contact);
        }
        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                Contact? contact = await _db.Contacts.FirstOrDefaultAsync(x => x.code == id);
                if (contact is null)
                {
                    return false;
                }
                //contact.DeletedAt = DateTime.Now;
                _db.Contacts.Update(contact);
                //_db.Contacts.Remove(contact);
                await _db.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                LogManagement.Instance.writeAssembly(MethodBase.GetCurrentMethod().DeclaringType, $"StackTrace: {ex.StackTrace} Error: {ex.Message}", ".ErrorS3");
                return false;
            }
        }
        public async Task<IEnumerable<ContactDTO>> GetAsync() =>

            _mapper.Map<IEnumerable<ContactDTO>>(await _db.Contacts.ToListAsync());



        public async Task<ContactDTO> GetByIdAsync(int id) =>

            _mapper.Map<ContactDTO>(await _db.Contacts.FirstOrDefaultAsync(x => x.code == id));
        public async Task<ContactDTO> GetRepresentativeByContactAsync(int contactCode)
        {
            var contact = await _db.Contacts.Include(u => u.studentRepresentatives).FirstOrDefaultAsync(i => i.code == contactCode);
            return _mapper.Map<Contact, ContactDTO>(contact);
        }


        public async Task<ContactDTO> UpdateAsync(int id, ContactDTO entity)
        {
            Contact contact = _mapper.Map<ContactDTO, Contact>(entity);
            var contactAfter = await _db.Contacts.AsNoTracking().FirstOrDefaultAsync(x => x.code == id);
            if (contactAfter is null || contact.code != id)
            {
                throw new NullReferenceException(MessageUtil.Instance.NotFound);
            }
            var currentEntity = MapProperties(contact, contactAfter);

            _db.Contacts.Update(currentEntity);
            await _db.SaveChangesAsync();
            return _mapper.Map<Contact, ContactDTO>(contact);
        }
    }
}
