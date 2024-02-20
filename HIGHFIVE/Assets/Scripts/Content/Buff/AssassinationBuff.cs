using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static Define;

public class AssassinationBuff : BaseBuff
{
    private BuffDBEntity _assassinationBuffData;

    public override void Init()
    {
        base.Init();
        if (Main.DataManager.BuffDict.TryGetValue("출혈", out BuffDBEntity assassinationBuffData))
        {
            _assassinationBuffData = assassinationBuffData;
        }
        //나중에 데이터 매니저에서 받아오기
        buffData.buffSprite = Main.ResourceManager.Load<Sprite>("Sprites/SkillIcon/Assessination");
        buffData.type = typeof(AssassinationBuff);
        buffData.duration = _assassinationBuffData.durationTime;
        buffData.curTime = 0;
        buffData.effectTime = 0;
        buffData.trueDamage = _assassinationBuffData.trueDamage + Main.GameManager.SpawnedCharacter.stat.Attack;
        buffData.isSustainBuff = _assassinationBuffData.isSustain;
    }

    public override void Activation() { }   

    public override void Deactivation()
    {
        buffData.effectTime = buffData.duration;
    }

    public override IEnumerator ApplyEffect(GameObject target, GameObject shooter)
    {
        yield return base.ApplyEffect(target);

        while (buffData.effectTime < buffData.duration)
        {
            buffData.effectTime += 1f;
            target.GetComponent<Stat>().TakeDamage(buffData.trueDamage, shooter, isTrueDamage: true);
            yield return new WaitForSeconds(1f);
        }
        buffData.effectTime = 0;
    }
}
