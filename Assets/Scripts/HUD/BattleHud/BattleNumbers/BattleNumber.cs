using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;
public class BattleNumber : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI text;
    public string value;
    void Awake ()
    {

    }

    // Update is called once per frame
    void Update()
    {
            
    }

    public void UpdateText(float t)
    {
        value = Mathf.RoundToInt(t).ToString();
        text.text = value;
        StartCoroutine(MoveText());
    }

    public void SetColor(TMP_ColorGradient color)
    {
        text.colorGradientPreset = color;
    }

    public IEnumerator MoveText()
    {
        Vector2 StartPos = transform.position;
        Vector2 EndPos = (Vector2)transform.position + Vector2.up * 3;
        float t = 0;
        float dur = 1.2f;
        while(t < dur)
        {
            t += Time.deltaTime;
            transform.position = Vector2.Lerp(StartPos, EndPos, t / dur);
            yield return null;
        }


    }
}
