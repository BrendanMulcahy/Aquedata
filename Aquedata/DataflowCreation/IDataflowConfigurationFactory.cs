namespace Aquedata.DataflowCreation
{
    public interface IDataflowConfigurationFactory
    {
        DataflowConfiguration GetConfiguration(string format);
    }
}