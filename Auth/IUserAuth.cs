namespace Auth
{
    public interface IUserAuth
    {
        public string UserName { get; set; }
        public string PassWord { get; set; }
    }
}