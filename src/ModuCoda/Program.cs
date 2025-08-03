using ModuCoda.Contracts;
using ModuCoda.HealthChecks;
using ModuCoda.Services;
using System.Net;
using System.Text;
using Yarp.ReverseProxy.Configuration;
using Yarp.ReverseProxy.Forwarder;

var builder = WebApplication.CreateBuilder(args);
var config = new Configurations(builder.Configuration);

builder.Services.AddSingleton(config);
builder.Services.AddSingleton<UtilityService>();
builder.Services.AddSingleton<TtydDiscoveryService>();
builder.Services.AddSingleton<VsCodeDiscoveryService>();
builder.Services.AddSingleton<IProcessManagerFactory, ProcessManagerFactory>();
builder.Services.AddHostedService<BackendProcessManager>();
builder.Services.AddHttpClient();

builder.Services.AddHealthChecks()
    .AddTypeActivatedCheck<TtydHealthChecks>("ttydHealthCheck")
    .AddTypeActivatedCheck<VsCodeHealthChecks>("vscodeHealthCheck");

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
            RouteId = "vscode-proxy",
            ClusterId = "vscode",
            Match = new RouteMatch { Path = "/vscode/{**remainder}" },
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
                { "target", new DestinationConfig { Address = config.TtydAddress.AbsoluteUri, } },
            },
        },
        new ClusterConfig
        {
            ClusterId = "vscode",
            HttpRequest = new ForwarderRequestConfig
            {
                Version = HttpVersion.Version11,
                VersionPolicy = HttpVersionPolicy.RequestVersionExact,
            },
            Destinations = new Dictionary<string, DestinationConfig>
            {
                { "target", new DestinationConfig { Address = config.VsCodeAddress.AbsoluteUri, } },
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

app.MapHealthChecks("/healthz");
app.MapReverseProxy();
app.Run();
