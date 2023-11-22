
using SQLite;
using WpfMauiLibrary.Interfaces;
using WpfMauiLibrary.Models;

namespace WpfMauiLibrary.Services
{
    public class CategoryDataAccess : IDataAccessService {
        public string ConnectionString { get ; set ; }

        public CategoryDataAccess(string connectionString)
        {
            ConnectionString = connectionString ;
        }

        public List<CategoryModel> GetAll<CategoryModel>() {
            var connection = new SQLiteConnection(ConnectionString);
            var result = connection.Query<CategoryModel>("SELECT * FROM Category").ToList();
            connection.Close();
            return result;
            
        }
        public T GetOne<T>(int id) {
            throw new NotImplementedException();
        }
        public void InsertOne<T>(T obj) {
            throw new NotImplementedException();
        }

        public void DeleteOne<T>(int id) {
            throw new NotImplementedException();
        }

        public void UpdateOne<T>(T obj) {
            throw new NotImplementedException();
        }
    }
}
