
namespace EmployeeProfile.Utilities
{
    public static class ConnectionDB
    {
        public static string ReturnPath(string databaseName)
        {
            string databasePath = string.Empty;
            if (DeviceInfo.Platform == DevicePlatform.Android)
            {
                databasePath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
                databasePath = Path.Combine(databasePath,databaseName);
            }else if(DeviceInfo.Platform == DevicePlatform.iOS)
            {
                databasePath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                databasePath = Path.Combine(databasePath,"..","Library",databaseName);
            }

            return databasePath;
        }
    }
}
