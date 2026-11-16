using UnityEngine;

public class EnemyBattleHUD : StatsPanel
{
    [SerializeField] ShowText text;

    public override void UpdateChildren()
    {
        if(character == null) return;   
        base.UpdateChildren();
        text.UpdateText(character.name);
        text.UpdateTMP();
    }
}
