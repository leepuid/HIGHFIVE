using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RogueSkill : CharacterSkill
{
    protected override void Start()
    {
        base.Start();
        FirstSkill = new Assassination();
        FirstSkill.Init();
    }
}
