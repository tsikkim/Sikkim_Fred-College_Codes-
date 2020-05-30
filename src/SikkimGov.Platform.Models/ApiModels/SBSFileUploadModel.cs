using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace SikkimGov.Platform.Models.ApiModels
{
    public class SBSFileUploadModel
    {
        [Required]
        [EnumDataType(typeof(SBSFileType))]
        public string FileType { get; set; }

        [Required]
        public IFormFile File { get; set; }
    }
}

public enum SBSFileType
{
    Payment = 1,
    Receipt = 2
}