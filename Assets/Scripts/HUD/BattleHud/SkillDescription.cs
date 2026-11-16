using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;
public class SkillDescription : MonoBehaviour
{
    [SerializeField] Image tooltip;
    [SerializeField] TMP_Text skillName, skillDescription;
    [SerializeField] List<Image> Ally, Enemy;
    [SerializeField] Sprite Null, ally, enemy, Pos;
    public void UpdateTooltip(SkillData skill)
    {
        tooltip.gameObject.SetActive(true);
        skillName.text = skill.original_skill.Name;
        skillDescription.text = skill.original_skill.Description;
        UsePosTooltip(skill);
        PosTooltip(skill);
    }

    public void HideTooltip()
    {
        tooltip.gameObject.SetActive(false);
        ClearTooltip();
    }

    void UsePosTooltip(SkillData skill)
    {
        if (skill == null) return;

        foreach(int pos in skill.original_skill.use_pos)
        {
            if (pos >= Ally.Count) continue;
            Ally[pos].sprite = Pos;
        }
    }

    void PosTooltip (SkillData skill)
    {
        if (skill == null) return;
        Sprite spr = skill.original_skill.affect_enemy ? enemy : ally;
        foreach (int pos in skill.original_skill.pos)
        {
            if (pos >= Enemy.Count) continue;
            Enemy[pos].sprite = spr;
        }
    }

    public void ClearTooltip()
    {
        foreach (Image img in Ally)
        {
            img.sprite = Null;
        }
        foreach (Image img in Enemy)
        {
            img.sprite = Null;
        }
    }
}
