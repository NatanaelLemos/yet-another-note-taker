using System.ComponentModel.DataAnnotations;

namespace YetAnotherNoteTaker.Common.Dtos
{
    public class NotebookDto
    {
        public string Key { get; set; }

        [Required]
        [MinLength(2)]
        [MaxLength(255)]
        public string Name { get; set; }
    }
}
