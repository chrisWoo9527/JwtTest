
using Identity.Sql.Data;

namespace 托管服务Api
{
    public class SechemServices : BackgroundService
    {
        private readonly IServiceScope serviceScope;

        public SechemServices(IServiceScopeFactory factory)
        {
            this.serviceScope = factory.CreateScope();
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            // 一般这种DBcontext必须设置为瞬时服务
            MirDbContext mirDbContext = serviceScope.ServiceProvider.GetRequiredService<MirDbContext>();
            int i = 0;

            while (!stoppingToken.IsCancellationRequested)
            {

                long v = mirDbContext.Users.LongCount();
                Console.WriteLine($"循环第{i++}次 获得条目数{v}");
                await File.WriteAllTextAsync(@"C:\Users\Administrator\Desktop\1123\1.txt", $"循环第{i++}次 获得条目数{v}");
                await Task.Delay(3000);
            }
        }


        public override void Dispose()
        {
            base.Dispose();
        }
    }
}
