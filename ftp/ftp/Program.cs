using System;
using System.Net;
using System.IO;
using System.Collections.Generic;

class FtpClient
{
    static string ftpServer = "ftp://127.0.0.1/"; // FTP-сервер
    static string username = "user";         // логин
    static string password = "12345";        // пароль

    static void Main()
    {
        Console.WriteLine("Содержимое FTP-сервера:");
        List<string> items = ListDirectory(ftpServer);

        Console.WriteLine("\nВ обратном порядке:");
        for (int i = items.Count - 1; i >= 0; i--)
        {
            Console.WriteLine(items[i]);
        }
        Console.Write("\nВведите место для копирования: ");
        string lPath = Console.ReadLine();
        Console.Write("\nВведите имя файла или папки для копирования: ");
        string item = Console.ReadLine();
        string localPath = Path.Combine(lPath, item);
        DownloadFile(ftpServer + item, localPath);
    }

    static List<string> ListDirectory(string ftpPath)
    {
        List<string> items = new List<string>();
        try
        {
            FtpWebRequest request = (FtpWebRequest)WebRequest.Create(ftpPath);
            request.Method = WebRequestMethods.Ftp.ListDirectory;
            request.Credentials = new NetworkCredential(username, password);
            using (FtpWebResponse response = (FtpWebResponse)request.GetResponse())
            using (Stream responseStream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(responseStream))
            {
                while (!reader.EndOfStream)
                {
                    items.Add(reader.ReadLine());
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Ошибка при получении списка: " + ex.Message);
        }
        return items;
    }

    static void DownloadFile(string ftpFile, string localPath)
    {
        try
        {

            FtpWebRequest request = (FtpWebRequest)WebRequest.Create(ftpFile);
            request.Method = WebRequestMethods.Ftp.DownloadFile;
            request.Credentials = new NetworkCredential(username, password);
            using (FtpWebResponse response = (FtpWebResponse)request.GetResponse())
            using (Stream responseStream = response.GetResponseStream())
            using (FileStream outputStream = new FileStream(localPath, FileMode.Create))
            {
                responseStream.CopyTo(outputStream);
            }
            Console.WriteLine($"Файл сохранен: {localPath}");
        }
        catch (Exception ex)
        {
            Console.WriteLine("Ошибка при загрузке файла: " + ex.Message);
        }
    }
}
