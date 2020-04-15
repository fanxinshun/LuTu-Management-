using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using System.Net;

namespace Coldairarrow.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseUrls("http://*:5000")
                .UseUrls("http://*:443")
                .UseStartup<Startup>();
        //.UseKestrel(options =>//设置Kestrel服务器
        //{
        //    options.Listen(IPAddress.Parse("172.16.0.10"), 5002, listenOptions =>
        //    {
        //        //填入之前iis中生成的pfx文件路径和指定的密码　　　　　　　　　　　　
        //        listenOptions.UseHttps(pfx, "123456");
        //    });
        //});
    }
}
