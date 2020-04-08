using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace LawAgendaApi.Helpers
{
    public static class Tools
    {
        public static async Task<string> SaveFile(long fileId, IFormFile file)
        {
            var path = GetUploadingPath(fileId);
            var fileName = GetFileName(fileId, file.FileName);
            await using (var fileStream = new FileStream(Path.Combine(path, fileName), FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }

            var httpPath = GetHttpPath(path, fileName);
            return httpPath;
        }

        private static string GetUploadingPath(long fileId)
        {
            var path = Path.Combine(@"C:\LawyersAgenda", "Avatars");

            var division = fileId / 10000;
            var multi = division * 10000;
            var summation = multi + 9999;
            var section1 = $"{multi:000000000000}";
            var section2 = $"{summation:000000000000}";

            var name = $"{section1}-{section2}";

            path = Path.Combine(path, name);

            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
            return path;
        }

        private static string GetFileName(long fileId, string fileName)
        {
            var sections = fileName.Split('.');

            var name = $"{fileId}.{sections[1]}";

            return name;
        }

        private static string GetHttpPath(string path, string fileName)
        {
            var index = path.LastIndexOf("\\") + 1;
            var trimmed = path.Substring(index);
            var httpPath = $"http://agenda.e-iraq.net/files/avatars/{trimmed.Replace('\\', '/')}/{fileName}";
            return httpPath;
        }

    }
}