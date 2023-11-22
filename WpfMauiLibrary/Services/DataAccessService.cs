
using SQLite;

namespace WpfMauiLibrary.Services
{
    public class DataAccessService
    {
        public string DbPath { get; set; }
        private SQLiteConnection _connection;

        public DataAccessService(string dbPath)
        {
            DbPath = dbPath;
            _connection = new(dbPath);
        }

        public List<T> GetAll<T>()
        {
            _connection.Query<T>()
        }


    }
}
