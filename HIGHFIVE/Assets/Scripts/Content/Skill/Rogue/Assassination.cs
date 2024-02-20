using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Assassination : BaseSkill
{
    private SkillDBEntity _assassinationData;
    private BuffDBEntity _assassinationBuffData;
    private GameObject _targetObject;
    public override void Init()
    {
        base.Init();
        if (Main.DataManager.SkillDict.TryGetValue("암살", out SkillDBEntity assassinationData))
        {
            _assassinationData = assassinationData;
        }
        if (Main.DataManager.BuffDict.TryGetValue("출혈", out BuffDBEntity assassinationBuffData))
        {
            _assassinationBuffData = assassinationBuffData;
        }
        //나중에 데이터 매니저에서 받아오기
        skillData.skillName = _assassinationData.name;
        skillData.info = $"적에게 {_assassinationData.damage + (int)(Main.GameManager.SpawnedCharacter.stat.Attack * _assassinationData.damageRatio)} " +
            $"만큼의 피해를 가하고 {assassinationBuffData.durationTime}초 동안" +
            $" {assassinationBuffData.trueDamage + Main.GameManager.SpawnedCharacter.stat.Attack}만큼의 고정 피해를 주는 출혈을 일으킨다.";
        skillData.skillSprite = Main.ResourceManager.Load<Sprite>("Sprites/SkillIcon/Assessination");
        skillData.coolTime = _assassinationData.coolTime;
        skillData.curTime = skillData.coolTime;
        skillData.animTime = _assassinationData.animTime;
        skillData.isUse = true;
        skillData.skillRange = _assassinationData.range;
        skillData.damage = _assassinationData.damage + (int)(Main.GameManager.SpawnedCharacter.stat.Attack * _assassinationData.damageRatio); ;
    }

    public override bool CanUseSkill()
    {
        if (!skillData.isUse) return false;
        if (Keyboard.current.qKey.wasPressedThisFrame)
        {
            SetTarget();
        }
        if (!CheckRange()) return false;
        return true;
    }
    public override void Execute()
    {
        Character myCharacter = Main.GameManager.SpawnedCharacter;
        skillData.isUse = false;
        Main.ResourceManager.Instantiate("SkillEffect/AssessinationEffect", myCharacter.targetObject.transform.Find("EffectTarget").position, syncRequired: true);
        DamageToTarget(_targetObject, myCharacter);
        myCharacter.Animator.SetBool(myCharacter.PlayerAnimationData.SkillDelayTimeHash, true);
        myCharacter.SkillController.CallSkillExecute(myCharacter.CharacterSkill.FirstSkill);
        myCharacter.SkillController.CallSkillDelay(myCharacter.CharacterSkill.FirstSkill.skillData);
    }

    private void DamageToTarget(GameObject target, Character shooter)
    {
        target.GetComponent<Stat>().TakeDamage(skillData.damage, shooter.gameObject);
        BaseBuff assassinationBuff = new AssassinationBuff();
        if (target.GetComponent<Character>())
        {
            PhotonView targetPv = target.GetComponent<PhotonView>();
            if (Main.NetworkManager.photonPlayer.TryGetValue(targetPv.ViewID, out Player targetPlayer))
            {
                shooter.GetComponent<PhotonView>().RPC("ReceiveBuff", RpcTarget.Others, targetPv.ViewID, Define.Buff.Assassination);
            }
        }
        else { target.GetComponent<Creature>().BuffController?.AddBuff(assassinationBuff, shooter.gameObject); }
    }

    private bool CheckRange()
    {
        Character myCharacter = Main.GameManager.SpawnedCharacter;

        if (myCharacter.targetObject != null)
        {
            float distance = (myCharacter.targetObject.transform.position - myCharacter.transform.position).magnitude;

            if (skillData.skillRange >= distance) { return true; }
            else { return false; }
        }
        else
        {
            return false;
        }
    }

    private void SetTarget()
    {
        Character myCharacter = Main.GameManager.SpawnedCharacter;
        Vector2 mousePoint = myCharacter.Input._playerActions.Move.ReadValue<Vector2>();
        Vector2 raymousePoint = Camera.main.ScreenToWorldPoint(mousePoint);

        int mask = (1 << (int)Define.Layer.Monster) | (1 << (Main.GameManager.SelectedCamp == Define.Camp.Red ? (int)Define.Layer.Blue : (int)Define.Layer.Red));

        RaycastHit2D hit = Physics2D.Raycast(raymousePoint, Camera.main.transform.forward, 10.0f, mask);

        if (hit.collider?.gameObject != null)
        {
            myCharacter.targetObject = hit.collider.gameObject;
            _targetObject = myCharacter.targetObject;
            float distance = (hit.collider.transform.position - myCharacter.transform.position).magnitude;

            if (skillData.skillRange >= distance)
            {
                myCharacter._playerStateMachine.ChangeState(myCharacter._playerStateMachine.PlayerFirstSkillState);
            }
            else
            {
                myCharacter._playerStateMachine.ChangeState(myCharacter._playerStateMachine._playerMoveState);
            }
        }
    }
}
