using ArchCorpUtilities.Utilities;

namespace TestInvalidCharacters
{
    using U = UniversalUtilities;
    public class InvalidCharacters
    {

        [Fact]
        public void ValidInput_OnInvalidCharacters()
        {
            string? result = U.ValidateInput("TestInput");
            Assert.True(result == null);
        }

        [Fact]
        public void InValidCharacters()
        {
            string? result = U.ValidateInput("TestInput\\");
            Assert.True(result == "Invalid entry - please re-enter (Invalid characters found ' ,\\ ,/ ,: ,* ,? ,< ,> ,| ,- ).");
        }


    }
}