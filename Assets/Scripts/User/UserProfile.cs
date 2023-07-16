using System.Collections.Generic;
using Hero;

namespace User
{
    public class UserProfile
    {
        public int Id { get; set; }
        public string JwtToken { get; set; }
        public string DeviceId { get; set; }
        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public string Salt { get; set; }
        public int Money { get; set; }
        public int Gems { get; set; }

        public List<HeroesSettings> HeroesSettings { get; } = new();
    }
}