namespace ModuCoda.Services;

public sealed class Configurations
{
    private readonly IConfiguration _configuration;

    [ActivatorUtilitiesConstructor]
    public Configurations(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public static readonly Uri DefaultTtydAddress =
        new Uri("http://localhost:7681/", UriKind.Absolute);
    public static readonly Uri DefaultVsCodeAddress =
        new Uri("http://localhost:8000/", UriKind.Absolute);

    public string? TtydPath => _configuration["ttydPath"];
    public string? TtydCredential => _configuration["credential"];
    public Uri TtydAddress => Uri.TryCreate(_configuration["ttydAddress"], UriKind.Absolute, out var uri) ? uri : DefaultTtydAddress;

    public string? VsCodePath => _configuration["codePath"];
    public Uri VsCodeAddress => Uri.TryCreate(_configuration["codeAddress"], UriKind.Absolute, out var uri) ? uri : DefaultVsCodeAddress;
}
