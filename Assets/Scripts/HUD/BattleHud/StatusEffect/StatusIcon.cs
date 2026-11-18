using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class StatusIcon : MonoBehaviour
{
    public StatusEffectData statusEffectData { get; private set; }
    [SerializeField] Image iconImage;
    [SerializeField] TMP_Text stacks;
    void Start()
    {
        
    }

    
    void Update()
    {
        
    }

    public void SetEffect(StatusEffectData effectData)
    {
        statusEffectData = effectData;
        iconImage.sprite = statusEffectData.sfSO.icon;
        stacks.text = statusEffectData.stacks.ToString();
        UpdateStacks();
    }

    public void UpdateStacks()
    {
        if (statusEffectData == null) return;
        if(statusEffectData.stacks <= 0)
        {
            Destroy(gameObject);
            return;
        }
        stacks.text = statusEffectData.stacks.ToString();
    }
}
