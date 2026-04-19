using Allure.Net.Commons;

public static class AllureExtensions
{
    public static void AddAttachment(this AllureLifecycle lifecycle, string name, string type, Stream content)
    {
        if (lifecycle == null) throw new ArgumentNullException(nameof(lifecycle));
        if (content == null) throw new ArgumentNullException(nameof(content));

        var resultsDir = lifecycle.ResultsDirectory;
        if (string.IsNullOrEmpty(resultsDir))
        {
            resultsDir = Path.GetTempPath();
        }

        Directory.CreateDirectory(resultsDir);

        var ext = GetExtensionFromContentType(type);
        var fileName = $"{Guid.NewGuid():N}{ext}";
        var filePath = Path.Combine(resultsDir, fileName);

        if (content.CanSeek)
            content.Position = 0;

        using (var fileStream = File.Create(filePath))
        {
            content.CopyTo(fileStream);
        }

        try
        {
            var metaPath = Path.Combine(resultsDir, fileName + ".meta");
            File.WriteAllText(metaPath, $"{name}\n{type}");
        }
        catch
        {
            // Swallow metadata errors to avoid breaking attachment flow.
        }
    }

    static string GetExtensionFromContentType(string contentType)
    {
        if (string.IsNullOrWhiteSpace(contentType)) return string.Empty;
        return contentType.ToLowerInvariant() switch
        {
            "image/png" => ".png",
            "image/jpeg" => ".jpg",
            "image/jpg" => ".jpg",
            "image/gif" => ".gif",
            "image/bmp" => ".bmp",
            "text/plain" => ".txt",
            "application/json" => ".json",
            _ => string.Empty,
        };
    }
}
