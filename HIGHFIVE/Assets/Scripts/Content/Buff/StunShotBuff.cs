using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StunShotBuff : BaseBuff
{
    private BuffDBEntity _stunShotBuffData;

    public override void Init()
    {
        base.Init();
        if (Main.DataManager.BuffDict.TryGetValue("스턴샷", out BuffDBEntity stunShotBuffData))
        {
            _stunShotBuffData = stunShotBuffData;
        }
        //나중에 데이터 매니저에서 받아오기
        buffData.buffSprite = Main.ResourceManager.Load<Sprite>("Sprites/SkillIcon/StunShot");
        buffData.type = typeof(AssassinationBuff);
        buffData.duration = _stunShotBuffData.durationTime;
        buffData.curTime = 0;
        buffData.effectTime = 0;
        buffData.isSustainBuff = _stunShotBuffData.isSustain;
    }

    public override void Activation() 
    {

    }

    public override void Deactivation()
    {
        buffData.effectTime = buffData.duration;
    }

    public override IEnumerator ApplyEffect(GameObject target, GameObject shooter = null)
    {
        yield return base.ApplyEffect(target);
        if (Main.GameManager.SpawnedCharacter.stat.CurHp > 0)
        {
            target.GetComponent<Character>()._playerStateMachine.ChangeState(target.GetComponent<Character>()._playerStateMachine.PlayerConfuseState);
            yield return new WaitForSeconds(buffData.duration);
        }
        if (Main.GameManager.SpawnedCharacter.stat.CurHp > 0)
        {
            target.GetComponent<Character>()._playerStateMachine.ChangeState(target.GetComponent<Character>()._playerStateMachine._playerIdleState);
        }
        buffData.effectTime = 0;
    }
}
