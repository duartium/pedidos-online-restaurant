using Microsoft.Extensions.Logging;
using Neutrinodevs.PedidosOnline.Domain.Contracts.Repositories;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Neutrinodevs.PedidosOnline.Infraestructure.Repositories
{
    public class ImageRepository : IImageRepository
    {
        private readonly ILogger<ImageRepository> _logger;
        public ImageRepository(ILogger<ImageRepository> logger)
        {
            _logger = logger;
        }
        public void SaveImageToDirectory(string path, Stream stream)
        {
            try
            {
                if (stream == null) return;
                if (stream.Length == 0) return;

                //using (Image imagen = Image.FromStream(stream))
                //    imagen.Save(path);

                byte[] fileData = null;
                using (var binaryReader = new BinaryReader(stream))
                {
                    fileData = binaryReader.ReadBytes((int)stream.Length);
                }

                //ImageConverter imageConverter = new ImageConverter();
                //Image image = imageConverter.ConvertFrom(fileData) as Image;
                //image.Save(path, System.Drawing.Imaging.ImageFormat.Jpeg);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
            }
        }
    }
}
