using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BUE.Inscriptions.Domain.Entity.DTO
{
    public class FileStorageDTO
    {
        [Required(ErrorMessage = "The FieldName is required")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "The FieldName must be between 3 and 100 characters")]
        public string  FieldName{ get; set; }
        [Required(ErrorMessage = "The FileName is required")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "The FileName must be between 3 and 100 characters")]
        public string  FileName{ get; set; }
        [Required(ErrorMessage = "The FileType is required")]
        [StringLength(20, MinimumLength = 2, ErrorMessage = "The FileType must be between 2 and 20 characters")]
        public string  FileType{ get; set; }
        [Required(ErrorMessage = "The FilePath is required")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "The FilePath must be between 3 and 50 characters")]
        public string  FilePath{ get; set; }
        [Required(ErrorMessage = "The FileBytes is required")]
        public byte[] FileBytes { get; set; }
    }
    public class AdditionalFile
    {
        public string FieldName { get; set; }
        public string Content { get; set; }
    }
}
