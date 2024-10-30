namespace BusinessLayer.Dtos;

public class CreateTranslationJobDto
{
    public int Id { get; set; }
    public string CustomerName { get; set; }
    public string OriginalContent { get; set; }
    public double Price { get; set; }
}
