using System.ComponentModel;

namespace Plumsail.NaughtyCat.Common.Enums
{
    public enum DelicacyEnum
    {
        [Description(nameof(LotOfBones))]
        LotOfBones = 1,
        [Description(nameof(GonnaStruggle))]
        GonnaStruggle,
        [Description(nameof(PrettyGood))]
        PrettyGood,
        [Description(nameof(NiceToHave))]
        NiceToHave,
        [Description(nameof(SuperTasty))]
        SuperTasty
    }
}
