using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Warrior : Character
{
    protected override void Awake()
    {
        base.Awake();
        stat = GetComponent<Stat>();
        CharacterSkill = GetComponent<WarriorSkill>();
    }
    protected override void Start()
    {
        base.Start();
    }
    protected override void Update()
    {
        base.Update();
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
    }
}
