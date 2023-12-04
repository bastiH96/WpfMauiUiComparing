using SQLite;
using WpfMauiLibrary.Interfaces;
using WpfMauiLibrary.Models;

namespace WpfMauiLibrary.Services;
public class ToDoTaskDataAccess : IDataAccessService<ToDoTaskModel> {
    public string ConnectionString { get; set; }

    public ToDoTaskDataAccess(string connectionstring)
    {
        ConnectionString = connectionstring;
    }

    public void CreateTable() {
        string query = @"CREATE TABLE IF NOT EXISTS Task (
                        Id INTEGER NOT NULL,
                        Content TEXT NOT NULL,
                        IsCompleted INTEGER NOT NULL,
                        DueDate bigint,
                        Priority INTEGER,
                        CategoryId INTEGER NOT NULL,
                        PriorityColor TEXT NOT NULL,
                        FOREIGN KEY(CategoryId) REFERENCES Category(Id),
                        PRIMARY KEY(Id AUTOINCREMENT))";
        var connection = new SQLiteConnection(ConnectionString);
        connection.Execute(query);
        connection.Close();
    }
    public List<ToDoTaskModel> GetAll() {
        var connection = new SQLiteConnection(ConnectionString);
        var result = connection.Query<ToDoTaskModel>("SELECT * FROM Task");
        connection.Close();
        return result.ToList();
    }

    public ToDoTaskModel GetOne(int id) {
        var connection = new SQLiteConnection(ConnectionString);
        var result = connection.Query<ToDoTaskModel>($"SELECT * FROM Task WHERE Id = {id}");
        connection.Close();
        return result[0];
    }

    public List<ToDoTaskModel> GetAllByCategoryIdAndOpen(int categoryId) {
        var connection = new SQLiteConnection(ConnectionString);
        var result = connection.Query<ToDoTaskModel>($"SELECT * FROM Task WHERE CategoryId = {categoryId} AND IsCompleted = 0");
        connection.Close();
        return result.ToList();
    }
    public List<ToDoTaskModel> GetAllByCategoryIdAndCompleted(int categoryId) {
        var connection = new SQLiteConnection(ConnectionString);
        var result = connection.Query<ToDoTaskModel>($"SELECT * FROM Task WHERE CategoryId = {categoryId} AND IsCompleted = 1");
        connection.Close();
        return result.ToList();
    }

    public void InsertOne(ToDoTaskModel toDoTask) {
        var connection = new SQLiteConnection(ConnectionString);
        connection.Insert(toDoTask);
        connection.Close();
    }

    public void DeleteOne(int id) {
        var connection = new SQLiteConnection(ConnectionString);
        connection.Delete<ToDoTaskModel>(id);
        connection.Close();
    }

    public void UpdateOne(ToDoTaskModel toDoTask) {
        var connection = new SQLiteConnection(ConnectionString);
        connection.Update(toDoTask);
        connection.Close();
    }
}
