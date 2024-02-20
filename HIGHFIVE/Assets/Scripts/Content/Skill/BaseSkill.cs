using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public struct SkillData
{
    public string skillName;
    public string info;
    public float coolTime;
    public float curTime;
    public float animTime;
    public float durationTime;
    public int damage;
    public float skillRange;
    public double loadTime;
    public bool isUse;
    public Image coolTimeicon;
    public Sprite skillSprite;
    public GameObject skillPrefab;
}

public abstract class BaseSkill
{
    public SkillData skillData;
    public virtual void Init() { }

    public abstract void Execute();
    public abstract bool CanUseSkill();
}
