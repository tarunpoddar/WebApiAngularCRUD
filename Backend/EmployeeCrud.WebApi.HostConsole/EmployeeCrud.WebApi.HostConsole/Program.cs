// See https://aka.ms/new-console-template for more information



using System.Web.Http.SelfHost;

Console.WriteLine("Hello, World!");

var config = new HttpSelfHostConfiguration("http://localhost:1234");

var server = new HttpSelfHostServer(config, new MyWebAPIMessageHandler());
var task = server.OpenAsync();
task.Wait();

Console.WriteLine("Web API Server has started at http://localhost:1234");
Console.ReadLine();

class MyWebAPIMessageHandler : HttpMessageHandler
{
    protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, System.Threading.CancellationToken cancellationToken)
    {
        var task = new Task<HttpResponseMessage>(() => {
            var resMsg = new HttpResponseMessage();
            resMsg.Content = new StringContent("Hello World!");
            return resMsg;
        });

        task.Start();
        return task;
    }
}
