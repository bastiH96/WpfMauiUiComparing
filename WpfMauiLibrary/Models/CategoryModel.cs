using SQLite;

namespace WpfMauiLibrary.Models;

[Table("Category")]

public class CategoryModel
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }
    public string Name { get; set; }
    [Ignore]
    public List<ToDoTaskModel> ToDoList { get; set; } = new List<ToDoTaskModel>();

    public CategoryModel()
    {
    }

    public CategoryModel(string name)
    {
        Name = name;
    }
}
