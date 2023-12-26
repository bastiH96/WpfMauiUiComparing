
namespace WpfMauiLibrary.HelperClasses; 
public static class Constants
{
    public static readonly List<string> PriorityColors = new List<string>()
    {
        "#FF5964", // red
        "#FFD449", // yellow
        "#5DFDCB" // green
    };

    // public const string DbFileName = "MauiAppDb.db";
    public const string DbFileNameMaui = "NewMauiDb3.db";
    public const string DbFileNameWpf = "NewWpfDb.db";

    // public const string DbFolder = "WpfMauiDbs";
    public const string DbFolder = "WpfMauiDatenbanken"; // FilePath for Mac

    public static readonly string DirectoryPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), DbFolder);

    public static readonly string DbFullPathMaui = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), DbFolder, DbFileNameMaui);
    public static readonly string DbFullPathWpf = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), DbFolder, DbFileNameWpf);
}
