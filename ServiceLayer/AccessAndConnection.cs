using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ConfugurationManager;
using DataAccessLayer;
using FileManager;

namespace ServiceLayer
{
    public class AccessAndConnection
    {


        static Type resDataOptions = OptionsManager.GetOptions<DataOptions>();
        public static string ser = Logger.GetValueByNameField(resDataOptions, "Server").ToString();
        public static string db = Logger.GetValueByNameField(resDataOptions, "Database").ToString();
        public static bool con = (bool)Logger.GetValueByNameField(resDataOptions, "Trusted_Connection");
        DataAccessLayerClass dbAccess;
        CancellationTokenSource cancellationTokenSource;
        public AccessAndConnection(CancellationTokenSource cancellationTokenSource)
        {
            this.cancellationTokenSource = cancellationTokenSource;
            Type resDataOptions = OptionsManager.GetOptions<DataOptions>();
            dbAccess = new DataAccessLayerClass(ser, db, con);
            ConfugurationManager.OptionsManager.DataAccess = dbAccess;
        }


        public string resultOutput;
        public async void StartAccessAsync(CancellationToken cancellationToken)
        {
            var e = dbAccess.StartDbAsync(cancellationToken);
            e.Wait();
            if (cancellationToken.IsCancellationRequested)
                return;
            XmlGeneratorService xmlData = new XmlGeneratorService();
            resultOutput = await xmlData.CreateContentAsync(await dbAccess.GetAddAsync("GETALLADDRESS", cancellationToken), cancellationToken);
            if (cancellationToken.IsCancellationRequested)
                return;
            FileTransferService fts = new FileTransferService(resultOutput);
            await fts.LogContentAsync(cancellationToken);
            if (cancellationToken.IsCancellationRequested)
                return;
            Thread.Sleep(10000);
        }
        public void StopAccess()
        {
            cancellationTokenSource.Cancel();
            Thread.Sleep(1000);
        }
    }


}

