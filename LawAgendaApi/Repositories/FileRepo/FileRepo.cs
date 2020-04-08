using System.Threading.Tasks;
using LawAgendaApi.Data;

namespace LawAgendaApi.Repositories.FileRepo
{
    public class FileRepo : IFileRepo
    {
        private readonly DataContext _context;

        public FileRepo(DataContext context)
        {
            _context = context;
        }

        public async Task<Data.Entities.File> CreateFile(Data.Entities.File file)
        {
            var addedFile = await _context.Files.AddAsync(file);

            var isSaved = await _context.SaveChangesAsync() > 0;

            if (isSaved)
            {
                return addedFile.Entity;
            }

            return null;
        }
    }
}