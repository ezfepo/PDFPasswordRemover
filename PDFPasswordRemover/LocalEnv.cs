namespace PDFPasswordRemover;

/// <summary>
/// Reads KEY=VALUE pairs from an optional local ".env" file (gitignored) so developers
/// can prefill values, like a password, without committing them. Never required for the app to run.
/// </summary>
public static class LocalEnv
{
    private static readonly Lazy<Dictionary<string, string>> Values = new(Load);

    public static string? Get(string key) => Values.Value.GetValueOrDefault(key);

    private static Dictionary<string, string> Load()
    {
        var values = new Dictionary<string, string>();

        string? envPath = FindEnvFile();
        if (envPath is null)
        {
            return values;
        }

        foreach (string line in File.ReadAllLines(envPath))
        {
            string trimmed = line.Trim();
            if (trimmed.Length == 0 || trimmed.StartsWith('#'))
            {
                continue;
            }

            int separatorIndex = trimmed.IndexOf('=');
            if (separatorIndex <= 0)
            {
                continue;
            }

            string key = trimmed[..separatorIndex].Trim();
            string value = trimmed[(separatorIndex + 1)..].Trim();
            values[key] = value;
        }

        return values;
    }

    private static string? FindEnvFile()
    {
        string[] candidateDirectories =
        [
            AppContext.BaseDirectory,
            Directory.GetCurrentDirectory(),
        ];

        foreach (string directory in candidateDirectories)
        {
            string candidate = Path.Combine(directory, ".env");
            if (File.Exists(candidate))
            {
                return candidate;
            }
        }

        return null;
    }
}
