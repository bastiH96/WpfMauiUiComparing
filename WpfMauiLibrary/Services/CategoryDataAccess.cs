
using SQLite;
using WpfMauiLibrary.Interfaces;
using WpfMauiLibrary.Models;

namespace WpfMauiLibrary.Services
{
    public class CategoryDataAccess : IDataAccessService<CategoryModel>
    {
        public string ConnectionString { get; set; }

        public CategoryDataAccess(string connectionstring)
        {
            ConnectionString = connectionstring;
        }

        public void CreateTable()
        {
            var connection = new SQLiteConnection(ConnectionString);
            connection.CreateTable<CategoryModel>();
            connection.Close();
        }

        public List<CategoryModel> GetAll()
        {
            var connection = new SQLiteConnection(ConnectionString);
            var result = connection.Query<CategoryModel>("SELECT * FROM Category");
            connection.Close();
            return result.ToList();
        }

        public CategoryModel GetOne(int id)
        {
            var connection = new SQLiteConnection(ConnectionString);
            var result = connection.Query<CategoryModel>($"SELECT * FROM Category WHERE Id = {id}");
            connection.Close();
            return result[0];
        }

        public void InsertOne(CategoryModel category)
        {
            var connection = new SQLiteConnection(ConnectionString);
            connection.Insert(category);
            connection.Close();
        }

        public void DeleteOne(int id)
        {
            var connection = new SQLiteConnection(ConnectionString);
            connection.Delete<CategoryModel>(id);
            connection.Close();
        }

        public void UpdateOne(CategoryModel category)
        {
            var connection = new SQLiteConnection(ConnectionString);
            connection.Update(category);
            connection.Close();
        }

        public int GetLastImplementedId() {
            var connection = new SQLiteConnection(ConnectionString);
            var result = connection.Query<int>("SELECT Id FROM Category");
            connection.Close();
            return result.ToList().Last();
        }

    }
}
