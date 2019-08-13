using System.ComponentModel;

namespace Plumsail.NaughtyCat.Common.Enums
{
    public enum DelicacyEnum
    {
        [Description(nameof(LotOfBones))]
        LotOfBones,
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
