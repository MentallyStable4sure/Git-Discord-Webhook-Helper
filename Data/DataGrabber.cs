namespace MentallyStable.GitHelper.Data
{
    public sealed class DataGrabber
    {
        public const string CONTENT_FOLDER = "content";
        public const string CONFIGS_FOLDER = "configs";

        public static async Task<string> GrabFromConfigs(string configFile) => await GrabFromFile(CONFIGS_FOLDER, configFile);

        public static async Task<string> GrabFromContent(string contentFile) => await GrabFromFile(CONTENT_FOLDER, contentFile);

        public static async Task<string> GrabFromFile(string directory, string file)
        {
            var current = Directory.GetCurrentDirectory();
            var fullPath = Path.Combine(current, directory);

            if (!Directory.Exists(fullPath)) return string.Empty;
            fullPath = Path.Combine(fullPath, file);

            return await File.ReadAllTextAsync(fullPath);
        }

        public static FileStream GrabFromContentStream(string contentFile)
        {
            var current = Directory.GetCurrentDirectory();
            var fullPath = Path.Combine(current, CONTENT_FOLDER);

            if (!Directory.Exists(fullPath)) return (FileStream)FileStream.Null;

            return File.OpenRead(fullPath);
        }

        public static void CreateConfig(string rawJson, string filename = "rawConfig.json")
        {
            var current = Directory.GetCurrentDirectory();
            var fullPath = Path.Combine(current, CONFIGS_FOLDER);

            if (!Directory.Exists(fullPath)) Directory.CreateDirectory(fullPath);

            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            Console.WriteLine($"Updating path: {fullPath}.....");
            Console.ResetColor();

            File.WriteAllText(Path.Combine(fullPath, filename), rawJson);
        }
    }
}
