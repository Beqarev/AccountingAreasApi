namespace AccountingAreasApi.Models;

public class Area
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string Location { get; set; }
    public Region Region { get; set; }
    public Category Category { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
}