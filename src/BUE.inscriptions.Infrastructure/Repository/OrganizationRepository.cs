using AutoMapper;
using BUE.Inscriptions.Domain.Elecctions.DTO;
using BUE.Inscriptions.Domain.Elecctions.Entities;
using BUE.Inscriptions.Domain.Entity.DTO;
using BUE.Inscriptions.Infrastructure.Interfaces;
using BUE.Inscriptions.Shared.Utils;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;


namespace BUE.Inscriptions.Infrastructure.Repository
{
    public class OrganizationRepository : BaseRepository, IOrganizationRepository
    {
        private readonly PortalMatriculasDBContext _db;
        private readonly IMapper _mapper;
        private readonly IAwsS3UploaderRepository _awsS3;

        public OrganizationRepository(PortalMatriculasDBContext db, IMapper mapper, IAwsS3UploaderRepository awsS3)
        {
            _db = db;
            _mapper = mapper;
            _awsS3 = awsS3;
        }
        public async Task AttachFiles(Organization organization, OrganizationDTO entityDto)
        {
            if (entityDto.FilesStorage != null)
            {
                var additionalFiles = new List<AdditionalFile>();
                if (!string.IsNullOrEmpty(organization.AdditionalFiles))
                {
                    additionalFiles = JsonConvert.DeserializeObject<List<AdditionalFile>>(organization.AdditionalFiles);
                }
                foreach (var row in entityDto.FilesStorage)
                {
                    string fileName = organization.Id + "_" + row.FileName + "." + row.FileType;
                    if (row.FileBytes is not null && row.FileBytes.Length > 0)
                    {
                        var s3Result = await _awsS3.UploadBucketFileAsync(row.FilePath, fileName, row.FileBytes);
                        if (!string.IsNullOrEmpty(s3Result))
                        {
                            switch (row.FieldName.ToLower())
                            {
                                case "image":
                                    organization.Image = s3Result;
                                    break;
                                case "proposal":
                                    organization.Proposal = s3Result;
                                    break;
                                case "bannerimage":
                                    organization.BannerImage = s3Result;
                                    break;
                                default:
                                    var existingFile = additionalFiles.FirstOrDefault(f => f.FieldName == row.FieldName);
                                    if (existingFile != null)
                                        existingFile.Content = s3Result;
                                    else
                                        additionalFiles.Add(new AdditionalFile()
                                        {
                                            FieldName = row.FieldName,
                                            Content = s3Result
                                        });
                                    break;
                            }
                        }
                    }
                }
                organization.AdditionalFiles = JsonConvert.SerializeObject(additionalFiles);
            }
        }

        public async Task<OrganizationDTO> CreateAsync(OrganizationDTO entity)
        {
            Organization organization = _mapper.Map<OrganizationDTO, Organization>(entity);
            _db.Organizations.Add(organization);
            await _db.SaveChangesAsync();
            await AttachFiles(organization, entity);
            await _db.SaveChangesAsync();
            return _mapper.Map<Organization, OrganizationDTO>(organization);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                Organization organization = await _db.Organizations.FindAsync(id);
                if (organization == null)
                {
                    return false;
                }
                _db.Organizations.Remove(organization);
                await _db.SaveChangesAsync();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<IEnumerable<OrganizationDTO>> GetAsync()
        {
            var organizations = await _db.Organizations
                .ToListAsync();
            return _mapper.Map<IEnumerable<OrganizationDTO>>(organizations);
        }

        public async Task<OrganizationDTO> GetByIdAsync(int id)
        {
            var organization = await _db.Organizations
                .FirstOrDefaultAsync(e => e.Id == id);
            return _mapper.Map<OrganizationDTO>(organization);
        }
        public async Task<IEnumerable<OrganizationDTO>> GetByElectionIdAsync(int id)
        {
            var organizations = await _db.Organizations
                .Where(e => e.Id == id)
                .ToListAsync();
            return _mapper.Map<IEnumerable<OrganizationDTO>>(organizations);
        }

        public async Task<OrganizationDTO> UpdateAsync(int id, OrganizationDTO entity)
        {
            var existingOrganization = await _db.Organizations
                .FirstOrDefaultAsync(e => e.Id == id);
            var organization = _mapper.Map<OrganizationDTO, Organization>(entity);
            if (existingOrganization == null)
            {
                throw new NullReferenceException(MessageUtil.Instance.NotFound);
            }
            await AttachFiles(organization, entity);
            var currentOrganization = MapProperties(organization, existingOrganization);
            _db.Organizations.Update(currentOrganization);
            await _db.SaveChangesAsync();
            return _mapper.Map<Organization, OrganizationDTO>(currentOrganization);
        }
    }
}
