using System.Threading.Tasks;

namespace LawAgendaApi.Repositories.FileRepo
{
    public interface IFileRepo
    {
        Task<Data.Entities.File> CreateFile(Data.Entities.File file);
    }
}