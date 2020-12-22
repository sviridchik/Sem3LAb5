using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileManager
{
    class Archive
    {
        public static string MyArchive(string path, string compressedFile)
        {
            string PreviousSizeAndCompressedSize;
            using (FileStream sourceStream = new FileStream(path, FileMode.OpenOrCreate))
            {
                // поток для записи сжатого файла
                using (FileStream targetStream = File.Create(compressedFile))
                {
                    // поток архивации
                    using (GZipStream compressionStream = new GZipStream(targetStream, CompressionMode.Compress))
                    {
                        sourceStream.CopyTo(compressionStream); // копируем байты из одного потока в другой
                        PreviousSizeAndCompressedSize = sourceStream.Length.ToString()+" " + targetStream.Length.ToString();

                    }
                }
            }
            return PreviousSizeAndCompressedSize;
        }
    }
}
