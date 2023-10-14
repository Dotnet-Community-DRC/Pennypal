using System.ComponentModel.DataAnnotations;

namespace Pennypal.DTOs;

public class UpdateCategoryDto
{
    public Guid Id { get; set; }
    [Required]
    public string Name { get; set; }
    [Required]
    public string Description { get; set; }
}