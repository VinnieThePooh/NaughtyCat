using System.ComponentModel;

namespace Plumsail.NaughtyCat.Domain.Enums
{
    public enum PriorityEnum
    {
        [Description(nameof(Worst))]
        Worst = 1,
        [Description(nameof(RatherNot))]
        RatherNot,
        [Description(nameof(Average))]
        Average,
        [Description(nameof(NotBad))]
        NotBad,
        [Description(nameof(Highest))]
        Highest
    }
}
