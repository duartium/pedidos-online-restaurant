using System.IO;

namespace Neutrinodevs.PedidosOnline.Domain.Contracts.Repositories
{
    public interface IImageRepository
    {
        void SaveImageToDirectory(string path, Stream stream);
    }
}
