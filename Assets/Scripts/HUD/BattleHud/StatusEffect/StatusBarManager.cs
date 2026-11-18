using UnityEngine;
using System.Collections.Generic;

public class StatusBarManager : MonoBehaviour
{
    [SerializeField] GameObject pref;
    [SerializeField] List<StatusBar> statusBars = new List<StatusBar>();
    [SerializeField] Vector2 offset;
    void Start()
    {
        
    } 

    void Update()
    {
        
    }

    public void CreateStatusBar(BattleChar battleChar)
    {
        GameObject newStatusBar = Instantiate(pref, battleChar.transform.position + (Vector3)offset, Quaternion.identity, transform);
        StatusBar statusBar = newStatusBar.GetComponent<StatusBar>();
        statusBar.SetBattleChar(battleChar);
        statusBars.Add(statusBar);
    }

    public void DeleteBar(BattleChar battleChar)
    {
        foreach(StatusBar bar in statusBars)
        {
            if(bar.character == battleChar)
            {
                statusBars.Remove(bar);
                Destroy(bar.gameObject);
                break;
            }
        }
    }

    public void HideBars()
    {
        gameObject.SetActive(false);
    }

    public void ShowBars()
    {
        gameObject.SetActive(true);
    }

}
