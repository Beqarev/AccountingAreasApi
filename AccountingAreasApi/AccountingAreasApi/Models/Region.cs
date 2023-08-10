using System.ComponentModel.DataAnnotations;

namespace AccountingAreasApi.Models;

public class Region
{
    public int Id { get; set; }
    [MaxLength (50)]
    public string Name { get; set; }
}