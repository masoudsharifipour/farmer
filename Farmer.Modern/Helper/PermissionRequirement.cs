using Microsoft.AspNetCore.Authorization;

namespace Farmer.Modern.Helper
{
    public class PermissionRequirement: IAuthorizationRequirement
    {
        public string Permission { get; private set; }

        public PermissionRequirement(string permission)
        {
            Permission = permission;
        }
    }
}

