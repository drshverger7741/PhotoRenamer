using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utility
{
    public class PhotoCollector
    {
        private string sourceDirectory;
        private string destinationDirectory;

        public PhotoCollector(string sourceDir, string destDir)
        {
            sourceDirectory = sourceDir;
            destinationDirectory = destDir;
        }

        public async Task CollectPhotos()
        {
            await CollectPhotosFromFolder(sourceDirectory);
        }

        private async Task CollectPhotosFromFolder(string folderPath)
        {
            try
            {
                // Получаем все файлы в текущей папке
                var files = Directory.EnumerateFiles(folderPath);

                foreach (var file in files)
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
                        int counter = 0;
                        while (File.Exists(destinationPath))
                        {
                            counter++;
                            string counterString = counter.ToString();
                            newFileName = $"{creationDate:dd-MM-yyyy HHmmss}_{folderName}{counterString}{extension}";
                            destinationPath = Path.Combine(destinationDirectory, newFileName);
                        }

                        // Копируем файл в целевую папку с новым именем
                        File.Copy(file, destinationPath);
                    }
                }

                // Рекурсивно вызываем этот метод для всех подпапок текущей папки
                var directories = Directory.EnumerateDirectories(folderPath);
                foreach (var directory in directories)
                {
                    await CollectPhotosFromFolder(directory);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при обработке папки {folderPath}: {ex.Message}");
            }
        }
    }


}
