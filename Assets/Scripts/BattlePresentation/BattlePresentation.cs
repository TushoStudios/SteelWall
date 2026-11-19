using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
public class BattlePresentation : MonoBehaviour
{
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public IEnumerator PresentationCoroutine(BattleResult result)
    {
        // Your presentation logic here
        List<OriginalCharPos> originalPositions = new List<OriginalCharPos>();
        SetOriginalPos(result, originalPositions);
        float angle = result.attacker is PjController ? 5 : -5;
        StartCoroutine(RotateCamera(1.2f, angle));
        StartCoroutine(CharSize(result, 1.5f));
        StartCoroutine(SpriteChange(result, result.skill.anim, result.skill.reaction));
        StartCoroutine(CameraFacing(result, 1.2f));
        MoveAllChars(result);
        yield return new WaitForSeconds(1.21f);
        ReturnAllChars(originalPositions);

        yield return new WaitForSeconds(1f);
    }

    void MoveAllChars(BattleResult result)
    {
        float dir = result.attacker is PjController ? 1.5f : -1.5f;

        StartCoroutine(CharMove(result.attacker, dir, 1.2f));
        result.attacker.sprite_renderer.sortingOrder += 10;

        foreach (BattleChar c in result.targets)
        {
            if (c == result.attacker) continue;
            StartCoroutine(CharMove(c, dir, 1.2f));
            c.sprite_renderer.sortingOrder += 10;
        }
    }
    void ReturnAllChars(List<OriginalCharPos> pos)
    {
        foreach(OriginalCharPos p in pos)
        {
            StartCoroutine(CharReturnPos(p, 0.45f));
            StartCoroutine(CharReturnSize(p, 0.3f));
            StartCoroutine(CharReturnSprite(p,0.47f));
        }
        StartCoroutine(RotateCamera(0.45f, 0f));
    }
    void SetOriginalPos(BattleResult result, List<OriginalCharPos> pos)
    {
        pos.Add(new OriginalCharPos(result.attacker, result.attacker.transform.position));
        foreach(BattleChar c in result.targets)
        {
            if (c == result.attacker) continue;
            pos.Add(new OriginalCharPos(c, c.transform.position));
        }
    }

    IEnumerator RotateCamera(float duration, float targetAngle = 0)
    {
        float t = 0;
        while(t < duration)
        {
            t += Time.deltaTime;
            float angle = Mathf.Lerp(1.33f, targetAngle, t / duration);
            Camera.main.transform.rotation = Quaternion.Euler(0, 0, angle);
            yield return null;
        }
    }
    IEnumerator CharSize(BattleResult result, float size, bool lerp = false, float duration = 0)
    {
        List<BattleChar> chars = result.targets.ToList();
        if (!chars.Contains(result.attacker)) chars.Add(result.attacker);


        if (lerp)
        {
            float t = 0;
            while (t < duration)
            {
                t += Time.deltaTime;
                float s = Mathf.Lerp(1, size, t / duration);
                foreach (BattleChar c in chars)
                {
                    c.transform.localScale = new Vector3(s, s, 1);
                }
                yield return null;
            }
            
        }

        foreach (BattleChar c in chars)
        {
            c.transform.localScale = new Vector3(size, size, 1);
        }
    }
    IEnumerator SpriteChange(BattleResult result, Sprite atk, BattleSprites reaction)
    {
        result.attacker.sprite_renderer.sprite = atk;
        foreach (BattleChar c in result.targets)
        {
            if (c == result.attacker || c.spriteSO == null) continue;
            c.sprite_renderer.sprite = SwitchSprite(c, reaction);
        }
        yield return null;
    }
    IEnumerator CameraFacing(BattleResult result, float duration)
    {
        float t = 0;
        while (t < duration)
        {
            t += Time.deltaTime;
            result.attacker.transform.eulerAngles = Camera.main.transform.eulerAngles;
            foreach(BattleChar c in result.targets)
            {
                c.transform.eulerAngles = Camera.main.transform.eulerAngles;
            }
            yield return null;
        }
    }
    IEnumerator CharMove(BattleChar c, float dir, float duration)
    {
        float t = 0;
        Vector3 startAttackerPos = c.transform.position;
        while (t < duration)
        {
            t += Time.deltaTime;
            c.transform.position = Vector2.Lerp(startAttackerPos, startAttackerPos + new Vector3(dir, 0, 0), t / duration);
            yield return null;
        }
    }
    IEnumerator CharReturnPos(OriginalCharPos pos, float duration = 0.5f)
    {
        float t = 0;
        Vector2 startPos = pos.character.transform.position;
        Vector2 startAngle = pos.character.transform.eulerAngles;
        while (t < duration)
        {
            t += Time.deltaTime;
            pos.character.transform.position = Vector2.Lerp(startPos, pos.originalPos, t / duration);
            pos.character.transform.eulerAngles = Vector2.Lerp(startAngle, Vector3.zero, t / duration);
            yield return null;
        }
        pos.character.transform.position = pos.originalPos;
        pos.character.transform.eulerAngles = Vector3.zero;
        pos.character.sprite_renderer.sortingOrder -= 10;
    }
    IEnumerator CharReturnSize(OriginalCharPos pos, float duration = 0.5f)
    {
        float t = 0;
        Vector3 startScale = pos.character.transform.localScale;
        while (t < duration)
        {
            t += Time.deltaTime;
            pos.character.transform.localScale = Vector3.Lerp(startScale, new Vector3(1, 1, 1), t / duration);
            yield return null;
        }
        pos.character.transform.localScale = new Vector3(1, 1, 1);
    }
    IEnumerator CharReturnSprite(OriginalCharPos pos, float duration = 0.5f)
    {
        yield return new WaitForSeconds(duration);
        if(pos.character.spriteSO != null) pos.character.sprite_renderer.sprite = pos.character.spriteSO.idle;
    }

    Sprite SwitchSprite(BattleChar c, BattleSprites s)
    {
        if(c.spriteSO == null) return null;
        return s switch
        {
            BattleSprites.Idle => c.spriteSO.idle,
            BattleSprites.Hurt => c.spriteSO.hurt,
            BattleSprites.buff => c.spriteSO.buff,
            _=> c.spriteSO.idle
        };
    }
}
