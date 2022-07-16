// See https://aka.ms/new-console-template for more information

using Microsoft.Extensions.Hosting;
using 托管服务;

using var service = new Class1();
await service.StartAsync(CancellationToken.None);