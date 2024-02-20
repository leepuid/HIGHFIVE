using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MageSkills : CharacterSkill
{
    protected override void Start()
    {
        base.Start();
        FirstSkill = new FireBall();
        FirstSkill.Init();
        SecondSkill = new StunShot();
        SecondSkill.Init();
        ThirdSkill = new Flash();
        ThirdSkill.Init();
    }
}
