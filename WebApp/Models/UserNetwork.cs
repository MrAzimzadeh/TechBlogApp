namespace WebApp.Models
{
    public class UserNetwork
    {
        public int Id { get; set; }
        public string UserUrl { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public int SocialNetworkId { get; set; }
        public SocialNetwork SocialNetwork { get; set; }
    }
}
