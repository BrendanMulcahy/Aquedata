namespace Aquedata.Validator.Sinks
{
    public interface ISink<in T>
    {
        void Persist(T[] item);
    }
}