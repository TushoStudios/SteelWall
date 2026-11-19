using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using TMPro;
public class ShowBattleNumbers : MonoBehaviour
{
    List<BattleNumber> battleNumbers = new List<BattleNumber>();
    [SerializeField] GameObject pref;
    [SerializeField] TMP_ColorGradient heal, damage, dodge, miss;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowNumbers(BattleResult result)
    {
        foreach(GenericBattleValue gv in result.values)
        {
            Vector2 xoffset = gv.target is PjController ? Vector2.right * 1.5f : Vector2.right * -1.5f;
            Vector2 yoffset = Vector2.up * 4.25f;
            Vector2 offset = xoffset + yoffset;
            TMP_ColorGradient color = gv is DamageInfo ? damage : heal;
            GameObject number = Instantiate(pref, (Vector2)gv.target.transform.position + offset, Quaternion.identity);
            number.transform.SetParent(this.transform);
            number.transform.localScale = Vector3.one;
            BattleNumber battleNumber = number.GetComponent<BattleNumber>();
            battleNumber.UpdateText(Mathf.RoundToInt(gv.value).ToString());
            battleNumber.SetColor(color);
            battleNumbers.Add(battleNumber);
        }
        foreach(BattleChar dodge in result.dodge)
        {
            Vector2 xoffset = dodge is PjController ? Vector2.right * 1.5f : Vector2.right * -1.5f;
            Vector2 yoffset = Vector2.up * 4.25f;
            Vector2 offset = xoffset + yoffset;
            TMP_ColorGradient color = this.dodge;
            GameObject number = Instantiate(pref, (Vector2)dodge.transform.position + offset, Quaternion.identity);
            number.transform.SetParent(this.transform);
            number.transform.localScale = Vector3.one;
            BattleNumber battleNumber = number.GetComponent<BattleNumber>();
            battleNumber.UpdateText("Desviou");
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
