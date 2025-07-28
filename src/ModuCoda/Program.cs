using System.Net;
using System.Text;
using Yarp.ReverseProxy.Configuration;
using Yarp.ReverseProxy.Forwarder;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<BackendProcessDiscoveryService>();
builder.Services.AddSingleton<IProcessManagerFactory, ProcessManagerFactory>();
builder.Services.AddHostedService<BackendProcessManager>();

builder.Services.AddReverseProxy().LoadFromMemory(
    [
        new RouteConfig
        {
            RouteId = "ttyd-proxy",
            ClusterId = "ttyd",
            Match = new RouteMatch { Path = "/ttyd/{**remainder}" },
            Transforms =
            [
                new Dictionary<string, string>
                {
                    { "PathPattern", "/{**remainder}" },
                },
                new Dictionary<string, string>
                {
                    { "RequestHeadersCopy", "true" },
                },
                new Dictionary<string, string>
                {
                    { "RequestHeader", "Connection" },
                    { "Set", "Upgrade" },
                }
            ]
        },
        new RouteConfig
        {
            RouteId = "code-proxy",
            ClusterId = "code",
            Match = new RouteMatch { Path = "/code/{**remainder}" },
            Transforms =
            [
                new Dictionary<string, string>
                {
                    { "PathPattern", "/{**remainder}" },
                },
                new Dictionary<string, string>
                {
                    { "RequestHeadersCopy", "true" },
                },
                new Dictionary<string, string>
                {
                    { "RequestHeader", "Connection" },
                    { "Set", "Upgrade" },
                }
            ],
        },
    ],
    [
        new ClusterConfig
        {
            ClusterId = "ttyd",
            HttpRequest = new ForwarderRequestConfig
            {
                Version = HttpVersion.Version11,
                VersionPolicy = HttpVersionPolicy.RequestVersionExact,
            },
            Destinations = new Dictionary<string, DestinationConfig>
            {
                { "target", new DestinationConfig { Address = "http://127.0.0.1:7681/", } },
            },
        },
        new ClusterConfig
        {
            ClusterId = "code",
            HttpRequest = new ForwarderRequestConfig
            {
                Version = HttpVersion.Version11,
                VersionPolicy = HttpVersionPolicy.RequestVersionExact,
            },
            Destinations = new Dictionary<string, DestinationConfig>
            {
                { "target", new DestinationConfig { Address = "http://127.0.0.1:8000/", } },
            },
        },
    ]);

var app = builder.Build();
var preferreEncoding = new UTF8Encoding(false);

app.MapGet("/", (HttpContext ctx) =>
{
    return Results.Content(
        Templates.RenderLayoutPage(),
        "text/html", preferreEncoding);
});

app.MapGet("/instructions", (HttpContext ctx) =>
{
    return Results.Content(
        Templates.InstructionPage(),
        "text/html", preferreEncoding);
});

app.MapReverseProxy();
app.Run();
