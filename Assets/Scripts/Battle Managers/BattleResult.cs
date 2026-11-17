using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UIElements;

public class BattleResult
{
    public BattleChar attacker;
    public List<BattleChar> targets = new List<BattleChar>();
    public List<BattleChar> dodge = new List<BattleChar>();
    public List<BattleChar> hit = new List<BattleChar>();
    public List<GenericBattleValue> values = new List<GenericBattleValue>(); 
    public SkillSO skill;
    public int acc_base;
    public int acc_roll;
    public bool skill_fail = true;
    public bool crit = false;
  
}
