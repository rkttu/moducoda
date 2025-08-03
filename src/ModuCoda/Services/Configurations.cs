namespace ModuCoda.Services;

public sealed class Configurations
{
    private readonly IConfiguration _configuration;

    [ActivatorUtilitiesConstructor]
    public Configurations(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public string? TtydPath => _configuration["ttydPath"];
    public string? TtydCredential => _configuration["credential"];

    public string? VsCodePath => _configuration["codePath"];
}
