using Foundation;

namespace EmployeeProfile
{
    [Register("AppDelegate")]
    public class AppDelegate : MauiUIApplicationDelegate
    {
        protected override MauiApp CreateMauiApp() => EmployeeProgram.CreateMauiApp();
    }
}