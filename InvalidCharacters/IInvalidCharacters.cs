namespace InvalidCharacters
{
    public interface IInvalidCharacters : IDisposable
    {
        public string? Result(string? input);
        public string? FailMessage();
        public string? FailMessageError(Exception err);
        public string? SuccessMessage();
        public List<string>? ShowCheckedValues();
        public string Version();
    }
}
