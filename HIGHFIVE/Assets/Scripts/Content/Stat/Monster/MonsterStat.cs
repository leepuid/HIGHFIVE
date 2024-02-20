using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterStat : Stat
{
    protected override void Init()
    {
        base.Init();
    }

    protected void StatSet(string name)
    {
        if (Main.DataManager.MonsterDict.TryGetValue(name, out MonsterDBEntity monster))
        {
            Attack = monster.atk;
            Defence = monster.def;
            AttackRange = monster.atkRange;
            AttackSpeed = monster.atkSpeed;
            MoveSpeed = monster.movSpeed;
            MaxHp = monster.maxHp;
            CurHp = monster.curHp;
            SightRange = monster.sight;
            Exp = monster.exp;
        }
    }
}
