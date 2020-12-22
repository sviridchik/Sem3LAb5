using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileManager
{
    public class Dearchive
    {

        public static void DeArchieves(string targetPath,string targetPathDecompressed)
        {
            using (FileStream sourceStream = new FileStream(targetPath, FileMode.OpenOrCreate))
            {
                // поток для записи восстановленного файла
                using (FileStream targetStream = File.Create(targetPathDecompressed))
                {
                    // поток разархивации
                    using (GZipStream decompressionStream = new GZipStream(sourceStream, CompressionMode.Decompress))
                    {
                        decompressionStream.CopyTo(targetStream);
                       // RecordEntryForAction(String.Format("File recovered: {0}", targetPathDecompressed));
                    }
                }
            }
        }
    }
}
