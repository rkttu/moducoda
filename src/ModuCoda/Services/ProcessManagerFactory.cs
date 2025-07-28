public class ProcessManagerFactory : IProcessManagerFactory
{
    public IProcessManager Create()
    {
        return new ProcessManager();
    }
}
