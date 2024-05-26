namespace WeChatCacheImageRecover;

public static class Program
{
    private static void Main()
    {
        /*
            Program for recovering images from WeChat cache folder.
            This program copies files from the specified source directory 
            and its subdirectories to the specified destination directory. 
            If a file is missing an extension, it adds a .jpg extension.
        */
        
        // Path to Android/data/com.tencent.mm folder.
        const string sourceFolderPath = @"..\Android\data\com.tencent.mm";
        const string outputFolderPath = @"..\Desktop\Output";

        if (!Directory.Exists(sourceFolderPath))
        {
            Console.WriteLine($"Source folder '{sourceFolderPath}' does not exist.");
            return;
        }

        if (!Directory.Exists(outputFolderPath))
        {
            Directory.CreateDirectory(outputFolderPath);
        }

        try
        {
            CopyFiles(sourceFolderPath, outputFolderPath);
            Console.WriteLine("All files moved successfully.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"{ex.Message}");
        }
    }

    private static void CopyFiles(string sourcePath, string destinationPath)
    {
        // Move files in the current directory.
        foreach (var filePath in Directory.GetFiles(sourcePath))
        {
            var fileName = Path.GetFileName(filePath);
            var destFilePath = Path.Combine(destinationPath, fileName);

            // Add .jpg extension if the file is missing an extension.
            if (string.IsNullOrEmpty(Path.GetExtension(fileName)))
            {
                destFilePath += ".jpg";
            }
            
            // Check if the file already exists at the destination.
            if (File.Exists(destFilePath))
            {
                Console.WriteLine($"File '{fileName}' already exists in the destination. Skipping...");
                continue;
            }

            File.Copy(filePath, destFilePath);
            Console.WriteLine($"Moved: {filePath} -> {destFilePath}");
        }

        // C files in subdirectories.
        foreach (var directoryPath in Directory.GetDirectories(sourcePath))
        {
            CopyFiles(directoryPath, destinationPath);
        }
    }
}