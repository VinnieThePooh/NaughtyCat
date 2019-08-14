using System.ComponentModel;

namespace Plumsail.NaughtyCat.Core.Enums
{
    public enum RoleEnum
    {
        [Description(nameof(Administrator))]
        Administrator = 1,
        [Description(nameof(BasicUser))]
        BasicUser,
    }
}
