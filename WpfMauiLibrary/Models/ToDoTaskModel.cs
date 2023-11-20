
namespace WpfMauiLibrary.Models;

public class ToDoTaskModel
{
    public int Id { get; set; }
    public string Content { get; set; }
    public bool IsCompleted { get; set; } = false;
    public DateTime? DueDate { get; set; }
    public string? Importance { get; set; }

    public ToDoTaskModel(string content, DateTime? dueDate, string? importance)
    {
        Content = content;
        DueDate = dueDate;
        Importance = importance;    
    }
}
