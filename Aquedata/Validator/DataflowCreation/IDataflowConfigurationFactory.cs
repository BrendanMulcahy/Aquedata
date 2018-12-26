namespace Aquedata.Validator.DataflowCreation
{
    public interface IDataflowConfigurationFactory
    {
        DataflowConfiguration GetConfiguration(string format);
    }
}