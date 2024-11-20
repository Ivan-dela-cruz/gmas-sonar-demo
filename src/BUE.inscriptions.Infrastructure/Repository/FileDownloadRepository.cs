using AutoMapper;
using BUE.Inscriptions.Infrastructure.Interfaces;
using BUE.Inscriptions.Domain.Entity;
using BUE.Inscriptions.Domain.Entity.DTO;
using BUE.Inscriptions.Shared.Utils;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace BUE.Inscriptions.Infrastructure.Repository
{
    public class FileDownloadRepository : IFileDownloadRepository
    {
        private readonly PortalMatriculasDBContext _db;
        private readonly IAwsS3UploaderRepository _awsS3;
        protected readonly IConfiguration _configuration;
        private IMapper _mapper;
        public FileDownloadRepository(PortalMatriculasDBContext db, IMapper mapper, IConfiguration configuration, IAwsS3UploaderRepository awsS3)
        {
            _db = db;
            _mapper = mapper;
            _awsS3 = awsS3;
            _configuration = configuration;
        }
        public async Task AttachFiles(FileDownloadDTO fileDownload, FileDownloadDTO entityDto)
        {
            if (entityDto.lang.fileES is not null && entityDto.lang.fileES.Length > 0)
            {
                string subPathS3 = "filedownloads/pdf";
                fileDownload.lang.urlES = await _awsS3.UploadBucketFileAsync(subPathS3, entityDto.lang.fileNameES + entityDto.lang.fileExtentionES, entityDto.lang.fileES);
                fileDownload.lang.fileES = null;
            }
            if (entityDto.lang.fileEN is not null && entityDto.lang.fileEN.Length > 0)
            {
                string subPathS3 = "filedownloads/pdf";
                fileDownload.lang.urlEN = await _awsS3.UploadBucketFileAsync(subPathS3, entityDto.lang.fileNameEN + entityDto.lang.fileExtentionEN, entityDto.lang.fileEN);
                fileDownload.lang.fileEN = null;
            }
            if (entityDto.lang.fileFR is not null && entityDto.lang.fileFR.Length > 0)
            {
                string subPathS3 = "filedownloads/pdf";
                fileDownload.lang.urlFR = await _awsS3.UploadBucketFileAsync(subPathS3, entityDto.lang.fileNameFR + entityDto.lang.fileExtentionFR, entityDto.lang.fileFR);
                fileDownload.lang.fileFR = null;
            }
        }

        public async Task<FileDownloadDTO> CreateAsync(FileDownloadDTO entity)
        {
            await AttachFiles(entity, entity);
            string langFiles = JsonConvert.SerializeObject(entity.lang);
            FileDownload fileDownload = _mapper.Map<FileDownloadDTO, FileDownload>(entity);
            fileDownload.langNames = langFiles;

            _db.Files.Add(fileDownload);
            await _db.SaveChangesAsync();
            return _mapper.Map<FileDownload, FileDownloadDTO>(fileDownload);
        }
        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                FileDownload? fileDownload = await _db.Files.FirstOrDefaultAsync(x => x.code == id);
                if (fileDownload is null)
                {
                    return false;
                }
                fileDownload.DeletedAt = DateTime.Now;
                _db.Files.Update(fileDownload);
                //_db.Files.Remove(fileDownload);
                await _db.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public async Task<IEnumerable<FileDownloadDTO>> GetAsync()
        {
            var list = _mapper.Map<IEnumerable<FileDownloadDTO>>(await _db.Files.ToListAsync());
            foreach (var fileDownload in list)
            {
                fileDownload.lang = JsonConverterResponse(fileDownload.langNames);
            }
            return list;
        }

        public LangFilesDTO JsonConverterResponse(string objectString)
        {
            try
            {
                var result = JsonConvert.DeserializeObject<LangFilesDTO>(objectString);
                return result;
            }
            catch (Exception ex)
            {
                return null;
            }
        }


        public async Task<FileDownloadDTO> GetByIdentificationAsync(string identification) =>

            _mapper.Map<FileDownloadDTO>(await _db.Files.FirstOrDefaultAsync(x => x.identification == identification));
       
        public async Task<IEnumerable<FileDownloadDTO>> GetByModuleAsync(string module)
        {
            var list = _mapper.Map<IEnumerable<FileDownloadDTO>>(await _db.Files.Where(x => x.Module == module).ToListAsync());
            foreach (var fileDownload in list)
            {
                fileDownload.lang = JsonConverterResponse(fileDownload.langNames);
            }
            return list;
        }


        public async Task<FileDownloadDTO> UpdateAsync(int id, FileDownloadDTO entity)
        {

            var fileDownloadAfter = await _db.Files.AsNoTracking().FirstOrDefaultAsync(x => x.code == id);
            if (fileDownloadAfter is null)
            {
                throw new NullReferenceException(MessageUtil.Instance.NotFound);
            }
            await AttachFiles(entity, entity);
            string langFiles = JsonConvert.SerializeObject(entity.lang);
            FileDownload fileDownload = _mapper.Map<FileDownloadDTO, FileDownload>(entity);
            fileDownload.code = id;
            fileDownload.langNames = langFiles;
            _db.Files.Update(fileDownload);
            await _db.SaveChangesAsync();
            return _mapper.Map<FileDownload, FileDownloadDTO>(fileDownload);
        }
    }
}
