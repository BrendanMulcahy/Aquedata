namespace Aquedata.Dataflow
{
    public interface IDataflowConfigurationFactory
    {
        DataflowConfiguration GetConfiguration(string format);
    }
}