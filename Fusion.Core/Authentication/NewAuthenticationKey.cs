namespace Fusion.Core.Authentication
{
    public class NewAuthenticationKey : IAuthenticationKey
    {
        public NewAuthenticationKey(int keyId, string vCode)
        {
            KeyID = keyId;
            VCode = vCode;
        }
        public int KeyID { get; set; }
        public string VCode { get; set; }
    }
}
