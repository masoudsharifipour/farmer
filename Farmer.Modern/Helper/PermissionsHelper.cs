using System.Collections.Generic;

namespace Farmer.Modern.Helper
{
    public static class Permissions
    {
        public static List<string> GeneratePermissionsForModule(string module)
        {
            return new List<string>()
            {
                $"Permissions.{module}.Create",
                $"Permissions.{module}.View",
                $"Permissions.{module}.Edit",
                $"Permissions.{module}.Delete",
            };
        }

        public static class PermissionsValue
        {
            public const string View = "Permissions.View";
            public const string Create = "Permissions.Create";
            public const string Edit = "Permissions.Edit";
            public const string Delete = "Permissions.Delete";
        }
    }
}

