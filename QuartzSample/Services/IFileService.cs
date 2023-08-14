using System.Threading.Tasks;

namespace QuartzSample.Services
{
    internal interface IFileService
    {
        Task SaveTextsToFile(string filePath, string text);
    }
}