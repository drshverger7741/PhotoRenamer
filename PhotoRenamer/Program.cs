
Console.WriteLine("Привет! Введиите путь папки где хранятся фотографии.");

string sourceDir = Console.ReadLine();

Console.WriteLine($"Путь папки где хранятся фотографии, {sourceDir}!");

Console.WriteLine("Укажите путь куда сохранятся фотографии.");

string destDir = Console.ReadLine();

Console.WriteLine($"Путь куда сохранятся фотографии: {destDir}!");

// Для ожидания нажатия клавиши Enter перед завершением программы:
// Console.ReadLine();
//string sourceDir = @"C:\path\to\source\directory";
//string destDir = @"C:\path\to\destination\directory";

PhotoCollector collector = new PhotoCollector(sourceDir, destDir);
collector.CollectPhotos();

Console.WriteLine("Дело сделано :)");

Console.ReadLine();