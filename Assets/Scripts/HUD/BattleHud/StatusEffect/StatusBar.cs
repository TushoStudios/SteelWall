using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class StatusBar : MonoBehaviour
{
    public BattleChar character;
    [SerializeField] List<StatusIcon> statusEffects = new List<StatusIcon>();
    [SerializeField] GameObject pref;
    [SerializeField] Vector2 offset;
    void Start()
    {
        
    }

    
    void Update()
    {
        
    }

    public void SetBattleChar(BattleChar battleChar)
    {
        character = battleChar;
        
    }

    public void UpdateStatus()
    {
        if(character == null) return;
        foreach(StatusEffectData effect in character.status_effects)
        {
            bool exists = false;
            for(int i = 0; i < statusEffects.Count; i++)
            {
                if (statusEffects[i].statusEffectData.sfSO.effectName == effect.sfSO.effectName)
                {
                    statusEffects[i].UpdateStacks();
                    exists = true;
                    break;
                }
            }
            if (exists == false)
            {
                GameObject newStatus = Instantiate(pref, character.transform.position + (Vector3)offset, Quaternion.identity, transform);
                StatusIcon statusicon = newStatus.GetComponent<StatusIcon>();
                statusicon.SetEffect(effect);
                statusEffects.Add(statusicon);
            }
        }
    }
}