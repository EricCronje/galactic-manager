using ArchCorpUtilities.Models;

namespace Auth
{
    public class Auth : IAuth
    {
        MockEntityRepositoryAuth<User>? Repository { get; set; }

        public bool CheckIfAuthorized(string userName, string passWord)
        {
            if (string.IsNullOrWhiteSpace(userName))
            {
                throw new ArgumentException($"'{nameof(userName)}' cannot be null or whitespace.", nameof(userName));
            }
            if (string.IsNullOrWhiteSpace(passWord))
            {
                throw new ArgumentException($"'{nameof(passWord)}' cannot be null or whitespace.", nameof(passWord));
            }

            //Make sure the user name and password has valid characters @ and . are permitted - numbers and alphanumerics only!.
            //Find the user name in the user repository.
            //If found decrypt the password
            //Check if the password is the same as on the repository.
            return false;
        }


    }
}
