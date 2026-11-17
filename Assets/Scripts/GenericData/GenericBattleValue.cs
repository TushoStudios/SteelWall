using UnityEngine;

public class GenericBattleValue
{
    public BattleChar dealer;
    public BattleChar target;
    public float value;

    public GenericBattleValue(BattleChar c, BattleChar t, float value)
    {
        this.dealer = c;
        this.target = t;
        this.value = value;
    }
}
