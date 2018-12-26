namespace Aquedata.Validator.DataflowBuilder
{
    public interface IDataflowConfigurationFactory
    {
        DataflowBuilderConfiguration GetConfiguration(string format);
    }
}