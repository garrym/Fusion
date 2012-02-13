namespace Fusion.Core.Authentication
{
    public class AuthenticationKey : IAuthenticationKey
    {
        public AuthenticationKey(int userId, string key)
        {
            UserId = userId;
            Key = key;
        }

        public string Key { get; private set; }
        public int UserId { get; private set; }
    }
}