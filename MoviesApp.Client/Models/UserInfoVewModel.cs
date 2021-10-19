namespace MoviesApp.Client.Models
{
    public class UserInfoVewModel
    {
        public Dictionary<string, string> UserInfoDictionary { get; private set; } = null;

        public UserInfoVewModel(Dictionary<string, string> userInfoDictionary)
        {
            UserInfoDictionary = userInfoDictionary;
        }      
    }
}
