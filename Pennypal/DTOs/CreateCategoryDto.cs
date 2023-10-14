using System.ComponentModel.DataAnnotations;

namespace Pennypal.DTOs;

public class CreateCategoryDto
{
    [Required]
    public string Name { get; set; }
    [Required]
    public string Description { get; set; }
}