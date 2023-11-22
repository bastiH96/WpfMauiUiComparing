
using SQLite;

namespace WpfMauiLibrary.Models;

[Table("Category")]

public class CategoryModel
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }
    public string Name { get; set; }
    public string? Color { get; set; }
    public string? Icon { get; set; }
    [Ignore]
    public List<ToDoTaskModel> ToDoList { get; set; } = new List<ToDoTaskModel>();

    public CategoryModel()
    {
    }

    /*
    public CategoryModel(string name, string? color, string? icon)
    {
        Name = name;
        Color = color;
        Icon = icon;
    }*/
}
