﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiUiApp.HelperClasses;

public static class Constants
{
    public const string DbFileName = "MauiAppDb.db";
    public const string DbFolder = "WpfMauiComparing";
    public static readonly string DirectoryPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), DbFolder);
    public static readonly string DbFullPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), DbFolder, DbFileName);
}
