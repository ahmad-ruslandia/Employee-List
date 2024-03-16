using Foundation;

namespace EmployeeList
{
    [Register("AppDelegate")]
    public class AppDelegate : MauiUIApplicationDelegate
    {
        protected override MauiApp CreateMauiApp() => EmployeeProgram.CreateMauiApp();
    }
}