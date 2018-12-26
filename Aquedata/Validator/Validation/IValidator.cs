namespace Aquedata.Validator.Validation
{
    public interface IValidator<T>
    {
        Validity<T> Validate(T item);
    }
}