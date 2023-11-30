using SQLite;
using System.ComponentModel.DataAnnotations.Schema;

namespace WpfMauiLibrary.Models;

[SQLite.Table("Task")]
public class ToDoTaskModel
{
    [AutoIncrement, PrimaryKey]
    public int Id { get; set; }
    public string Content { get; set; }
    public bool IsCompleted { get; set; } = false;
    public DateTime? DueDate { get; set; }
    public int? Priority { get; set; }
    [ForeignKey("CategoryId")]
    public int CategoryId { get; set; }

    public ToDoTaskModel()
    {
        
    }

    public ToDoTaskModel(string content, DateTime? dueDate, int? priority, int categoryId)
    {
        Content = content;
        DueDate = dueDate;
        Priority = priority;    
        CategoryId = categoryId;
    }
}
