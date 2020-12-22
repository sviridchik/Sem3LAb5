using ConfugurationManager;
using DataAccessLayer;
using FileManager;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ServiceLayer
{
    class FileTransferService : IFileTransferService
    {
        public string content;
        public FileTransferService(string content)
        {
            this.content = content;
        }


        // XmlGeneratorService xmlEntity = new XmlGeneratorService();

        public string GetContent()
        {
            return content;
        }


        public async Task LogContentAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            Type resFinder = OptionsManager.GetOptions<FinderInfo>();
            FileInfo tempFile = new FileInfo(Path.GetTempFileName());
            var writer = tempFile.AppendText();
            await writer.WriteAsync(new StringBuilder(content), cancellationToken);
            writer.Close();

            string sourcePathFromStorage = "C:\\" + Logger.GetValueByNameField(resFinder, "SourceDirectory").ToString()
                          .Replace("\"", "").Replace(" ", "");


            FileInfo fileInfo = new FileInfo(Path.Combine(sourcePathFromStorage, "output.xml"));
            if (fileInfo.Exists)
                fileInfo.Delete();

            tempFile.MoveTo(fileInfo.FullName);
        }

    }
}
