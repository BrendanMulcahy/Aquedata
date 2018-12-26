namespace Aquedata.Validator.Validation
{
    public class Validity<T>
    {
        public Validity(T data)
        {
            IsValid = true;
            Data = data;
        }

        public Validity(string invalidReason)
        {
            IsValid = false;
            InvalidReason = invalidReason;
        }

        public bool IsValid { get; }

        public T Data { get; }

        public string InvalidReason { get; }
    }
}