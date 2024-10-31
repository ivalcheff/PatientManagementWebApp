
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static PatientManagementApp.Common.ModelValidationConstraints.FileUpload;


namespace PatientManagementApp.Data.Models
{
    public class FileUpload
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        [MaxLength(FileNameMaxLength)]
        public string FileName { get; set; } = null!;

        [Required]
        [MaxLength(ContentTypeMaxLength)]
        public string ContentType { get; set; } = null!;

        // Binary data of the file
        public byte[] Data { get; set; } = null!;

        public Guid PatientId { get; set; }
        [ForeignKey(nameof(PatientId))]
        public virtual Patient Patient { get; set; } = null!;
    }
}
