using UnityEngine;

public class OriginalCharPos
{
    public BattleChar character;
    public Vector2 originalPos;

    public OriginalCharPos(BattleChar character, Vector2 originalPos)
    {
        this.character = character;
        this.originalPos = originalPos;
    }
}
