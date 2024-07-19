using Microsoft.AspNetCore.Identity;

namespace VehicleApi.Persistance
{
    public class UserInfo : IdentityUser
    {

        public string NickName { get; set; }

        public string Phone { get; set; }
    }
}
