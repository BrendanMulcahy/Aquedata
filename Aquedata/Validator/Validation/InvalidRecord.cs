namespace Aquedata.Validator.Validation
{
    public class InvalidRecord
    {
        public InvalidRecord(int id, string reason)
        {
            Id = id;
            Reason = reason;
        }

        public int Id { get; }

        public string Reason { get; }  
    }
}