using System.ComponentModel;

namespace Plumsail.NaughtyCat.Common.Enums
{
    public enum PriorityEnum
    {
        [Description(nameof(PriorityEnum.Worst))]
        Worst = 1,
        [Description(nameof(PriorityEnum.RatherNot))]
        RatherNot,
        [Description(nameof(PriorityEnum.Average))]
        Average,
        [Description(nameof(PriorityEnum.NotBad))]
        NotBad,
        [Description(nameof(PriorityEnum.Highest))]
        Highest,
    }
}
