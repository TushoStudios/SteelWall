using UnityEngine;

[System.Serializable]
public class DamageInfo : GenericBattleValue
{
    public bool ignore_prot = false;
    public DamageInfo(BattleChar c, BattleChar t, float value, bool ignore_prot) : base(c, t, value)
    {
        this.ignore_prot = ignore_prot;
    }

}