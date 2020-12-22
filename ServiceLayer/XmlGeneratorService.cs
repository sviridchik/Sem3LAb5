using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    class XmlGeneratorService : IXmlGeneratorService
    {
        public string resultOutput;
        public async Task<string> CreateContentAsync(DataEntity dataEntity, System.Threading.CancellationToken cancellationToken = new CancellationToken())
        {
            StringBuilder content = new StringBuilder(@"<?xml version =""1.0"" encoding=""utf-8""?>" + '\n');
            string rowName = dataEntity.names[0].Substring(0, dataEntity.names[0].Length - 2);
            content.AppendLine($"<XmlTable>");
            await Task.Run(() =>
            {
                for (int i = 0; i < dataEntity.values.Count; i++)
                {
                    if (cancellationToken.IsCancellationRequested)
                        return;
                    content.AppendLine($"\t<{rowName}>");
                    for (int j = 0; j < dataEntity.names.Count; j++)
                    {
                        if (j > 0)
                        {
                            content.Append("\n");
                        }
                        content.Append($"\t\t<{dataEntity.names[j]}>");
                        content.Append($"{dataEntity.values[i][j]}");
                        content.Append($"</{dataEntity.names[j]}>");
                    }
                    content.Append("\n");
                    content.AppendLine($"\t</{rowName}>");
                }
            });
            content.AppendLine($"</XmlTable>");
            return content.ToString();
        }
    }
}
