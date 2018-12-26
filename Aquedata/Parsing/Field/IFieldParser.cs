namespace Aquedata.Parsing.Field
{
    public interface IFieldParser<out T>
    {
        T Parse(string field);
    }
}