using System;
using System.IO;
using SQLite;

namespace BooksServiceSampleHost.DataBaseConf
{
    public static class Contants
    {
        public const string DatabaseFilename = "BookChaptersService.db";
        public const SQLiteOpenFlags Flags =
            SQLiteOpenFlags.ReadWrite |
            SQLiteOpenFlags.Create |
            SQLiteOpenFlags.SharedCache;
        public static string DatabasePath
        {
            get
            {
                var basePath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
                return Path.Combine(basePath,DatabaseFilename);
            }
        }
    }
}
