using ModuCoda.Contracts;

namespace ModuCoda.Services;

public class ProcessManagerFactory : IProcessManagerFactory
{
    public IProcessManager Create()
    {
        return new ProcessManager();
    }
}
