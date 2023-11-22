using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfMauiLibrary.Interfaces {
    public interface IDataAccessService {

        public string ConnectionString { get; set; }
        List<T> GetAll<T>();
        T GetOne<T>(int id);
        void InsertOne<T>(T obj);
        void UpdateOne<T>(T obj);
        void DeleteOne<T>(int id);
    }
}
