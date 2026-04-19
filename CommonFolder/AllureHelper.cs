using Allure.Net.Commons;
using System.IO;

public static class AllureHelper
{
    public static void AttachScreenshot(string path)
    {
        using var stream = File.OpenRead(path);
        AllureLifecycle.Instance.AddAttachment(
            "Failure Screenshot",
            "image/png",
            stream
        );
    }
}