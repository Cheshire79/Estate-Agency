
namespace EstateAgency.BLL.Identity.Interface.Data
{
    public class UserDTO
    {
        public string Id { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string RoleByDefault { get; set; }
    }
}
