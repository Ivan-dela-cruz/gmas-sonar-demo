using AutoMapper;
using BUE.Inscriptions.Domain.Elecctions.DTO;
using BUE.Inscriptions.Domain.Elecctions.Entities;
using BUE.Inscriptions.Infrastructure.Interfaces;
using BUE.Inscriptions.Shared.Utils;
using Microsoft.EntityFrameworkCore;


namespace BUE.Inscriptions.Infrastructure.Repository
{
    public class ElectionRepository : BaseRepository, IElectionRepository
    {
        private readonly PortalMatriculasDBContext _db;
        private readonly IMapper _mapper;
        private readonly IAwsS3UploaderRepository _awsS3;

        public ElectionRepository(PortalMatriculasDBContext db, IMapper mapper, IAwsS3UploaderRepository awsS3)
        {
            _db = db;
            _mapper = mapper;
            _awsS3 = awsS3;
        }
        public async Task AttachFiles(Election election, ElectionDTO entityDto)
        {
            string s3Result = "";
            string fileName = election.Title.ToString().Trim() + "_" + election.Id;
            if (entityDto.CoverImageFile is not null && entityDto.CoverImageFile.Length > 0)
            {
                string subPathS3 = "canditates/images";
                s3Result = await _awsS3.UploadBucketFileAsync(subPathS3, "photo_election_" + fileName + ".png", entityDto.CoverImageFile);
                election.CoverImage = s3Result ?? "";
            }
        }
        public async Task<ElectionDTO> CreateAsync(ElectionDTO entity)
        {
            Election election = _mapper.Map<ElectionDTO, Election>(entity);
            await AttachFiles(election, entity);
            _db.Elections.Add(election);
            await _db.SaveChangesAsync();

            return _mapper.Map<Election, ElectionDTO>(election);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                Election election = await _db.Elections.FindAsync(id);
                if (election == null)
                {
                    return false;
                }

                // Your implementation for deleting an Election
                _db.Elections.Remove(election);
                await _db.SaveChangesAsync();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<IEnumerable<ElectionDTO>> GetAsync()
        {
            var elections = await _db.Elections
             .Include(e => e.AcademicYear)
             .ToListAsync();
            return _mapper.Map<IEnumerable<ElectionDTO>>(elections);
        }

        public async Task<ElectionDTO> GetByIdAsync(int id)
        {
            var election = await _db.Elections
             .Include(e => e.AcademicYear)
             .FirstOrDefaultAsync(e => e.Id == id);
            return _mapper.Map<ElectionDTO>(election);
        }

        public async Task<ElectionDTO> UpdateAsync(int id, ElectionDTO entity)
        {
            Election election = _mapper.Map<ElectionDTO, Election>(entity);
            var existingElection = await _db.Elections.FindAsync(id);

            if (existingElection == null)
            {
                throw new NullReferenceException(MessageUtil.Instance.NotFound);
            }
            await AttachFiles(election, entity);
            MapProperties(election, existingElection);
            await _db.SaveChangesAsync();
            return _mapper.Map<Election, ElectionDTO>(existingElection);
        }
        public async Task<int> GetTotalUsersAsync(int electionId)
        {
            List<int> secondaryCodes = new List<int>() { 13, 14, 15 };
            int totalUser = 0;

            var election = await _db.Elections.FirstOrDefaultAsync(e => e.Id == electionId);

            if (election != null && election.Group == 1)
            {
                totalUser = await _db.Padron.Where(e => !secondaryCodes.Contains((int)e.CourseId))
                                   .Select(e => e.UserId)
                                   .Distinct()
                                   .CountAsync();
            }
            else
            {
                totalUser = await _db.Padron
                                  .Select(e => e.UserId)
                                  .Distinct()
                                  .CountAsync();

            }


            return totalUser;
        }

        public async Task<IEnumerable<int>> GetUserLevelElectionAsync(int userId)
        {
            List<int> levels = new List<int>();
            var users = await _db.Users.FirstOrDefaultAsync(x => x.Code == userId);
            var relatives = await _db.Padron.Where(x => x.UserId == userId).ToListAsync();

            if (relatives == null || !relatives.Any())
            {
                return levels;
            }
            var levelse = relatives.Select(x => x.CourseId).Distinct().ToList();
            foreach (var row in levelse)
            {
                levels.Add((int)row);
            }
            return levels;
        }
    }
}

