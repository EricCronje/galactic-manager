namespace Auth
{
    public interface IAuth
    {
        public bool CheckIfAuthorized(string userName, string passWord);
    }
}