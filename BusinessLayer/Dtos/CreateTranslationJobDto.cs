using System.ComponentModel.DataAnnotations;

namespace BusinessLayer.Dtos;

public class CreateTranslationJobDto
{
    [Required] public string CustomerName { get; set; }

    [Required] public string OriginalContent { get; set; }
}
