
using System.Text;

namespace InvalidCharacters
{

    public class InvalidCharacters : IInvalidCharacters
    {
        private List<string>? ListOfInvalidCharacters;
        private string? InvalidCharactersFound = null;

        public InvalidCharacters() 
        {
            ListOfInvalidCharacters = [];

            ListOfInvalidCharacters.Add("'");
            ListOfInvalidCharacters.Add("\\");
            ListOfInvalidCharacters.Add("/");
            ListOfInvalidCharacters.Add(":");
            ListOfInvalidCharacters.Add("*");
            ListOfInvalidCharacters.Add("?");
            ListOfInvalidCharacters.Add("<");
            ListOfInvalidCharacters.Add(">");
            ListOfInvalidCharacters.Add("|");
            ListOfInvalidCharacters.Add("-");
        }

        public void Dispose()
        {
            ListOfInvalidCharacters?.Clear();
            GC.SuppressFinalize(this);
        }

        public string? FailMessage()
        {
            return $"Invalid entry - please re-enter (Invalid characters found ' ,\\ ,/ ,: ,* ,? ,< ,> ,| ,- ).";
        }

        public string? FailMessageError(Exception err)
        {
            return $"Error: {err.Message} -- {err.InnerException?.Message}";
        }

        public string? Result(string? input)
        {
            if (input == null)
                return null;

            try
            {
                StringBuilder InvalidCharactersFoundSB = new();

                if (ListOfInvalidCharacters != null && ListOfInvalidCharacters.Count > 0)
                    foreach (var invalidCharacters in ListOfInvalidCharacters)
                        if (!string.IsNullOrWhiteSpace(invalidCharacters))
                            if (input.Contains(invalidCharacters))
                                return FailMessage();

                return null;
            }
            catch (Exception err)
            {
                return FailMessageError(err);
            }
        }

        public List<string>? ShowCheckedValues()
        {
            return ListOfInvalidCharacters;
        }

        public string? SuccessMessage()
        {
            return "";
        }

        public string Version()
        {
            return "Version 1.5.2";
        }
    }
}
