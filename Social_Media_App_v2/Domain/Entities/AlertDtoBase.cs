namespace Domain.Entities;
public abstract class AlertDtoBase
{
    public Guid Id { get; set; }
    public string Url { get; set; }
    public string Description { get; set; }
    public DateTime Date { get; set; }
}