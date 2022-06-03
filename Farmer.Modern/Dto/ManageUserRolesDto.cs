using System.Collections.Generic;

namespace Farmer.Modern.Dto
{
    public class ManageUserRolesDto
    {
        public string UserId { get; set; }
        public IList<UserRolesDto> UserRoles { get; set; }
    }

    public class UserRolesDto
    {
        public string RoleName { get; set; }
        public bool Selected { get; set; }
    }
}

