using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfMauiLibrary.Interfaces {
    public interface IDataAccessService<T> {

        public string ConnectionString { get; set; }
        void CreateTable();
        List<T> GetAll();
        T GetOne(int id);
        void InsertOne(T obj);
        void UpdateOne(T obj);
        void DeleteOne(int id);
    }
}
