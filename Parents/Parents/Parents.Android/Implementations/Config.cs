using Xamarin.Forms;

[assembly: Dependency(typeof(Parents.Droid.Implementations.Config))]

namespace Parents.Droid.Implementations
{
    using Interfaces;
    using SQLite;
    using SQLite.Net.Interop;

    public class Config : IConfig
    {
        string directoryDB;
        private SQLite.Net.Interop.ISQLitePlatform platform;

        public string DirectoryDB
        {
            get
            {
                if (string.IsNullOrEmpty(directoryDB))
                {
                    directoryDB = System.Environment.GetFolderPath(
                        System.Environment.SpecialFolder.Personal);
                }

                return directoryDB;
            }
        }

        public ISQLitePlatform Platform
        {
            get
            {
                if (platform == null)
                {
                    platform =
                        new SQLite.Net.Platform.XamarinAndroid.SQLitePlatformAndroid();
                }

                return platform;
            }
        }
    }
}
