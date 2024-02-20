using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.TextCore.Text;

public class Stabbing : BaseSkill
{
    private SkillDBEntity _stabbingData;
    private BuffDBEntity _stabbingBuffData;
    private GameObject _targetObject;
    public override void Init()
    {
        base.Init();
        if (Main.DataManager.SkillDict.TryGetValue("광폭화", out SkillDBEntity stabbingData))
        {
            _stabbingData = stabbingData;
        }
        if (Main.DataManager.BuffDict.TryGetValue("광폭화", out BuffDBEntity stabbingBuffData))
        {
            _stabbingBuffData = stabbingBuffData;
        }
        //나중에 데이터 매니저에서 받아오기
        skillData.info = $"자신의 이동속도가 {_stabbingBuffData.effectTime}초 동안 {_stabbingBuffData.moveSpd}증가하고" +
            $"{_stabbingBuffData.durationTime}초 동안 {_stabbingBuffData.damage + Main.GameManager.SpawnedCharacter.stat.Attack}데미지만큼 평타 강화가 된다";
        skillData.skillSprite = Main.ResourceManager.Load<Sprite>("Sprites/SkillIcon/Berserk");
        skillData.coolTime = _stabbingData.coolTime;
        skillData.curTime = skillData.coolTime;
        skillData.animTime = 0.5f;
        skillData.isUse = true;
        skillData.loadTime = 0;
        skillData.skillName = _stabbingBuffData.name;
    }

    public override bool CanUseSkill()
    {
        if (!skillData.isUse) return false;
        return true;
    }
    public override void Execute()
    {
        Character myCharacter = Main.GameManager.SpawnedCharacter;
        if (!skillData.isUse) return;
        skillData.isUse = false;
        skillData.coolTimeicon.fillAmount = 1;
        BaseBuff stabbingBuff = new StabbingBuff();
        myCharacter.BuffController.AddBuff(stabbingBuff);
        myCharacter.Animator.SetBool(myCharacter.PlayerAnimationData.SkillDelayTimeHash, true);
        myCharacter.SkillController.CallSkillDelay(myCharacter.CharacterSkill.FirstSkill.skillData);
    }
}
