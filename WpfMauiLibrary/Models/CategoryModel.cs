
namespace WpfMauiLibrary.Models;

public class CategoryModel
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string? Color { get; set; }
    public string? Icon { get; set; }
    public List<ToDoTaskModel> ToDoList { get; set; } = new List<ToDoTaskModel>();

    public CategoryModel(string name, string? color, string? icon)
    {
        Name = name;
        Color = color;
        Icon = icon;
    }
}
