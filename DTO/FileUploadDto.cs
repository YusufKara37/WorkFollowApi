using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace WorkFvApi.DTO.FileUploadDto
{
    public class FileUploadDto
    {
        [Required]
        public IFormFile File { get; set; }  // PDF dosyası

        [Required]
        public int WorkId { get; set; }  // İş ID'si
    }
}
