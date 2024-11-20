using AutoMapper;
using BUE.Inscriptions.Domain.Elecctions.DTO;
using BUE.Inscriptions.Domain.Elecctions.Entities;
using BUE.Inscriptions.Domain.Entity.DTO;
using BUE.Inscriptions.Domain.Entity;
using BUE.Inscriptions.Infrastructure.Interfaces;
using BUE.Inscriptions.Shared.Utils;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;


namespace BUE.Inscriptions.Infrastructure.Repository
{
    public class CandidateRepository : BaseRepository, ICandidateRepository
    {
        private readonly PortalMatriculasDBContext _db;
        private readonly IMapper _mapper;
        private readonly IAwsS3UploaderRepository _awsS3;

        public CandidateRepository(PortalMatriculasDBContext db, IMapper mapper, IAwsS3UploaderRepository awsS3)
        {
            _db = db;
            _mapper = mapper;
            _awsS3 = awsS3;
        }
        public async Task AttachFiles(Candidate candidate, CandidateDTO entityDto)
        {
            if (entityDto.FilesStorage != null)
            {
                foreach (var row in entityDto.FilesStorage)
                {
                    string fileName = candidate.Id + "_" + row.FileName + "." + row.FileType;
                    if (row.FileBytes is not null && row.FileBytes.Length > 0)
                    {
                        var s3Result = await _awsS3.UploadBucketFileAsync(row.FilePath, fileName, row.FileBytes);
                        if (!string.IsNullOrEmpty(s3Result))
                        {
                            switch (row.FieldName.ToLower())
                            {
                                case "image":
                                    candidate.Image = s3Result;
                                    break;
                                
                            }
                        }
                    }
                }
            }
        }

        public async Task<CandidateDTO> CreateAsync(CandidateDTO entity)
        {
            Candidate candidate = _mapper.Map<CandidateDTO, Candidate>(entity);
            _db.Candidates.Add(candidate);
            await _db.SaveChangesAsync();
            await AttachFiles(candidate, entity);
            await _db.SaveChangesAsync();
            return _mapper.Map<Candidate, CandidateDTO>(candidate);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                Candidate candidate = await _db.Candidates.FindAsync(id);
                if (candidate == null)
                {
                    return false;
                }
                _db.Candidates.Remove(candidate);
                await _db.SaveChangesAsync();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<IEnumerable<CandidateDTO>> GetAsync()
        {
            var candidates = await _db.Candidates
                .ToListAsync();
            return _mapper.Map<IEnumerable<CandidateDTO>>(candidates);
        }

        public async Task<CandidateDTO> GetByIdAsync(int id)
        {
            var candidate = await _db.Candidates
                .FirstOrDefaultAsync(e => e.Id == id);
            return _mapper.Map<CandidateDTO>(candidate);
        }
        public async Task<IEnumerable<CandidateDTO>> GetByElectionIdAsync(int id)
        {
            var candidates = await _db.Candidates
                .Where(e => e.Id == id)
                .ToListAsync();
            return _mapper.Map<IEnumerable<CandidateDTO>>(candidates);
        }

        public async Task<CandidateDTO> UpdateAsync(int id, CandidateDTO entity)
        {
            var candidate = _mapper.Map<CandidateDTO, Candidate>(entity);
            var existingCandidate = await _db.Candidates.FindAsync(id);

            if (existingCandidate == null)
            {
                throw new NullReferenceException(MessageUtil.Instance.NotFound);
            }
            await AttachFiles(candidate, entity);
            var currentCandidate =  MapProperties(candidate, existingCandidate);
            _db.Candidates.Update(currentCandidate);
            await _db.SaveChangesAsync();
            return _mapper.Map<Candidate, CandidateDTO>(currentCandidate);
        }
    }
}
