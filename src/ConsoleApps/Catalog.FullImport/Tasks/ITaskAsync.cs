using System.Threading;
using System.Threading.Tasks;

namespace Catalog.FullImport.Tasks
{
    public interface ITaskAsync
    {
        Task ExecuteAsync();
    }
}