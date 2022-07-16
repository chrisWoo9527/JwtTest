using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 托管服务
{
    internal class Class1 : BackgroundService
    {
        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            Task.Delay(500000);
            Console.WriteLine("一个后台托管服务");
            return Task.CompletedTask;
        }
    }
}
