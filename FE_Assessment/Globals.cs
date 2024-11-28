using Newtonsoft.Json.Linq;

namespace FE_Assessment
{
    public static class Globals
    {
        //DATA OBJECTS
        public static JObject _testData;
        public static JObject _userData;

        //PATHS
        public static string _seperator = Path.DirectorySeparatorChar.ToString();
        public static string _currentDir = Directory.GetCurrentDirectory();
        public static string _appDir = Path.GetFullPath(Path.Combine(_currentDir, @".." + _seperator + ".." + _seperator + ".." + _seperator));
        public static string _resourceDir = Path.GetFullPath(Path.Combine(_appDir + @"Resources" + _seperator));
        public static string _reportDir = Path.GetFullPath(Path.Combine(_appDir + @"Reports" + _seperator));

    }
}