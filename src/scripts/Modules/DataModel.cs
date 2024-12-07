using System.ComponentModel.DataAnnotations;


namespace DBLaba6.Controllers
{
    public class DataModel
    {
        [Required(ErrorMessage = "Data ID is required.")]
        public int DataId { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        [StringLength(100, ErrorMessage = "Name cannot exceed 100 characters.")]
        public string Name { get; set; }

        [StringLength(500, ErrorMessage = "Description cannot exceed 500 characters.")]
        public string? Description { get; set; }

        [Required(ErrorMessage = "Format is required.")]
        [StringLength(50, ErrorMessage = "Format cannot exceed 50 characters.")]
        public string Format { get; set; }

        [Required(ErrorMessage = "Content is required.")]
        public string Content { get; set; }

        [Required(ErrorMessage = "Created At date is required.")]
        public DateTime CreatedAt { get; set; }

        [Required(ErrorMessage = "Updated date is required.")]
        public DateTime UpdatedAt { get; set; }

        [Required(ErrorMessage = "Category ID is required.")]
        public int CategoryId { get; set; }
    }
}
