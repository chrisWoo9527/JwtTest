using System.IO;

namespace 托管服务Api
{
    public class HostedServiceTest1 : BackgroundService
    {
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
          await   File.WriteAllTextAsync(@"C:\Users\Administrator\Desktop\1123\1.txt", "sdsdqweqwe");
        }
    }
}
