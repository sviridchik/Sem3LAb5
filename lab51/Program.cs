using System;
using System.Configuration;
using System.Threading;
using System.Threading.Tasks;
using DataAccessLayer;
using FileManager;
using ServiceLayer;

namespace lab41
{
    class Program
    {
        static void Main(string[] args)
        {


            var logger = new Logger();

            CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
            AccessAndConnection serviceLayer = new AccessAndConnection(cancellationTokenSource);

            Thread loggerThread = new Thread(new ThreadStart(logger.Start));
            loggerThread.Start();

            serviceLayer.StartAccessAsync(cancellationTokenSource.Token);
        }
    }
}
