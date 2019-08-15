using System.ComponentModel;

namespace Plumsail.NaughtyCat.Domain.Enums
{
    public enum RoleEnum
    {
        [Description(nameof(Administrator))]
        Administrator = 1,
        [Description(nameof(BasicUser))]
        BasicUser,
    }
}
