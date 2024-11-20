using AutoMapper;
using BUE.Inscriptions.Domain.Elecctions.DTO;
using BUE.Inscriptions.Domain.Elecctions.Entities;
using BUE.Inscriptions.Infrastructure.Interfaces;
using BUE.Inscriptions.Shared.Utils;
using Microsoft.EntityFrameworkCore;


namespace BUE.Inscriptions.Infrastructure.Repository
{
    public class PositionRepository : BaseRepository, IPositionRepository
    {
        private readonly PortalMatriculasDBContext _db;
        private readonly IMapper _mapper;

        public PositionRepository(PortalMatriculasDBContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<PositionDTO> CreateAsync(PositionDTO entity)
        {
            Position position = _mapper.Map<PositionDTO, Position>(entity);
            _db.Positions.Add(position);
            await _db.SaveChangesAsync();

            return _mapper.Map<Position, PositionDTO>(position);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                Position position = await _db.Positions.FindAsync(id);
                if (position == null)
                {
                    return false;
                }

                // Your implementation for deleting an Position
                _db.Positions.Remove(position);
                await _db.SaveChangesAsync();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<IEnumerable<PositionDTO>> GetAsync()
        {
            var positions = await _db.Positions.ToListAsync();
            return _mapper.Map<IEnumerable<PositionDTO>>(positions);
        }

        public async Task<PositionDTO> GetByIdAsync(int id)
        {
            var position = await _db.Positions.FindAsync(id);
            return _mapper.Map<PositionDTO>(position);
        }

        public async Task<PositionDTO> UpdateAsync(int id, PositionDTO entity)
        {
            Position position = _mapper.Map<PositionDTO, Position>(entity);
            var existingPosition = await _db.Positions.FindAsync(id);

            if (existingPosition == null)
            {
                throw new NullReferenceException(MessageUtil.Instance.NotFound);
            }
            MapProperties(position, existingPosition);
            await _db.SaveChangesAsync();
            return _mapper.Map<Position, PositionDTO>(existingPosition);
        }
    }
}
