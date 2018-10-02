using System.Collections.Generic;

namespace WebUI.Models.UsersAndRoles
{
    public class UsersForAddingIntoRoleViewModel
    {
        public IEnumerable<UserViewModel> Users { get; set; }
        public PagingInfoView PagingInfo { get; set; }
        public string RoleId { get; set; }
        public string ReturnUrl { get; set; }
    }
}