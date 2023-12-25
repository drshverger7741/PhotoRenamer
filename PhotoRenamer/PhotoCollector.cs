public class PhotoCollector
{
    private string sourceDirectory;
    private string destinationDirectory;

    public PhotoCollector(string sourceDir, string destDir)
    {
        sourceDirectory = sourceDir;
        destinationDirectory = destDir;
    }

    public void CollectPhotos()
    {
        CollectPhotosFromFolder(sourceDirectory);
        Console.WriteLine("Фотографии успешно собраны.");
    }

    private void CollectPhotosFromFolder(string folderPath)
    {
        try
        {
            // Проходимся по всем файлам в текущей папке
            foreach (var file in Directory.GetFiles(folderPath))
            {
                // Проверяем расширение файла - только фотографии будут обработаны
                var extension = Path.GetExtension(file).ToLower();
                if (extension == ".jpg" || extension == ".jpeg" ||
                    extension == ".png" || extension == ".gif")
                {
                    // Получаем дату создания файла из его метаданных
                    var creationDate = File.GetCreationTime(file);
                    string folderName = new DirectoryInfo(folderPath).Name;

                    // Формируем новое имя файла на основе даты создания
                    var newFileName = $"{creationDate:dd-MM-yyyy HHmmss}_{folderName}{extension}";

                    // Конструируем полный путь для сохранения файла в целевой папке назначения
                    var destinationPath = Path.Combine(destinationDirectory, newFileName);
                    int counte = 0;
                    while (File.Exists(destinationPath))
                    {
                        counte++;
                        string counteString= counte.ToString();
                        newFileName = $"{creationDate:dd-MM-yyyy HHmmss}_{folderName}{counteString}{extension}";
                        destinationPath = Path.Combine(destinationDirectory, newFileName);
                    }

                    // Копируем файл в целевую папку с новым именем
                    File.Copy(file, destinationPath);
                }
            }

            // Рекурсивно вызываем этот метод для всех подпапок текущей папки
            foreach (var subfolder in Directory.GetDirectories(folderPath))
            {
                CollectPhotosFromFolder(subfolder);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка при обработке папки {folderPath}: {ex.Message}");
        }
    }
}