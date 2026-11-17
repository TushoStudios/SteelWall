using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using TMPro;
public class ShowBattleNumbers : MonoBehaviour
{
    List<BattleNumber> battleNumbers = new List<BattleNumber>();
    [SerializeField] GameObject pref;
    [SerializeField] TMP_ColorGradient heal, damage;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowNumbers(List<GenericBattleValue> v)
    {
        foreach(GenericBattleValue gv in v)
        {
            Vector2 xoffset = gv.target is PjController ? Vector2.right * 1.5f : Vector2.right * -1.5f;
            Vector2 yoffset = Vector2.up * 4.25f;
            Vector2 offset = xoffset + yoffset;
            TMP_ColorGradient color = gv is DamageInfo ? damage : heal;
            GameObject number = Instantiate(pref, (Vector2)gv.target.transform.position + offset, Quaternion.identity);
            number.transform.SetParent(this.transform);
            number.transform.localScale = Vector3.one;
            BattleNumber battleNumber = number.GetComponent<BattleNumber>();
            battleNumber.UpdateText(gv.value);
            battleNumber.SetColor(color);
            battleNumbers.Add(battleNumber);
        }
        StartCoroutine(DeleteNumbers());
    }

    public IEnumerator DeleteNumbers()
    {
        yield return new WaitForSeconds(1.2f);
        foreach(BattleNumber battleNumber in battleNumbers)
        {
            Destroy(battleNumber.gameObject);
        }
        battleNumbers.Clear();
    }
}
