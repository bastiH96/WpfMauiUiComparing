using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfMauiLibrary.Services;

namespace WpfMauiLibrary.HelperClasses;

public static class DatabaseHelper
{
     public static void CreateDatabaseMaui()
     {
        CreateDirectoryIfNotExist();

        new CategoryDataAccess(Constants.DbFullPathMaui).CreateTable();
        new ToDoTaskDataAccess(Constants.DbFullPathMaui).CreateTable();
     }

    public static void CreateDatabaseWpf()
    {
        CreateDirectoryIfNotExist();

        new CategoryDataAccess(Constants.DbFullPathWpf).CreateTable();
        new ToDoTaskDataAccess(Constants.DbFullPathWpf).CreateTable();
    }

    private static void CreateDirectoryIfNotExist()
    {
        if (!Directory.Exists(Constants.DirectoryPath))
        {
            Directory.CreateDirectory(Constants.DirectoryPath);
        }
    }
}
