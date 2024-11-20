using AutoMapper;
using BUE.Inscriptions.Infrastructure.Interfaces;
using BUE.Inscriptions.Shared.Utils;
using Microsoft.EntityFrameworkCore;
using BUE.Inscriptions.Domain.Inscriptions.Entities;
using BUE.Inscriptions.Domain.Inscriptions.DTO;


namespace BUE.Inscriptions.Infrastructure.Repository
{
    public class PaymentInformationRepository : BaseRepository, IPaymentInformationRepository
    {
        private readonly PortalMatriculasDBContext _db;
        private readonly IMapper _mapper;
        public string Message { get; set; }
        public string StatusCode { get; set; }

        public PaymentInformationRepository(PortalMatriculasDBContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }
       

        public async Task<PaymentInformationDTO> CreateAsync(PaymentInformationDTO entity)
        {
            try
            {
                PaymentInformation paymentInformation = _mapper.Map<PaymentInformationDTO, PaymentInformation>(entity);
                _db.PaymentInformation.Add(paymentInformation);
                await _db.SaveChangesAsync();
                return _mapper.Map<PaymentInformation, PaymentInformationDTO>(paymentInformation);

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
                PaymentInformation paymentInformation = await _db.PaymentInformation.FindAsync(id);
                if (paymentInformation == null)
                {
                    return false;
                }
                _db.PaymentInformation.Remove(paymentInformation);
                await _db.SaveChangesAsync();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<IEnumerable<PaymentInformationDTO>> GetAsync()
        {
            var paymentInformations = await _db.PaymentInformation
                   .ToListAsync();
            return _mapper.Map<IEnumerable<PaymentInformationDTO>>(paymentInformations);

        }

        public async Task<PaymentInformationDTO> GetByIdAsync(int id)
        {
            var paymentInformation = await _db.PaymentInformation
                .FirstOrDefaultAsync(e => e.Id == id);
            return _mapper.Map<PaymentInformationDTO>(paymentInformation);
        }

        public async Task<PaymentInformationDTO> UpdateAsync(int id, PaymentInformationDTO entity)
        {
            try
            {
                var paymentInformation = _mapper.Map<PaymentInformationDTO, PaymentInformation>(entity);
                var existingPaymentInformation = await _db.PaymentInformation.FindAsync(id);

                if (existingPaymentInformation == null)
                {
                    throw new NullReferenceException(MessageUtil.Instance.NotFound);
                }
                var currentPaymentInformation = MapProperties(paymentInformation, existingPaymentInformation);
                _db.PaymentInformation.Update(currentPaymentInformation);
                await _db.SaveChangesAsync();
                return _mapper.Map<PaymentInformation, PaymentInformationDTO>(currentPaymentInformation);
            }
            catch (Exception ex)
            {
                HandlerOperationException(ex);
                Message = this.MessageException;
                StatusCode = this.CodeException;
                return null;
            }
        }
    }
}

