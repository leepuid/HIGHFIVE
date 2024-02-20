using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.TextCore.Text;

public class FireBall : BaseSkill
{
    private SkillDBEntity _fireBallData;
    public override void Init()
    {
        base.Init();
        if (Main.DataManager.SkillDict.TryGetValue("파이어볼", out SkillDBEntity firballData))
        {
            _fireBallData = firballData;
        }
        //나중에 데이터 매니저에서 받아오기
        skillData.skillName = _fireBallData.name;
        skillData.info = $"스킬 사용 시 커서 방향으로 구체를 날리고 해당 구체에 맞은 적은 " +
            $"{_fireBallData.damage + (int)(Main.GameManager.SpawnedCharacter.stat.Attack * _fireBallData.damageRatio)}만큼의 피해를 입힌다";
        skillData.skillSprite = Main.ResourceManager.Load<Sprite>("Sprites/SkillIcon/FireBall");
        skillData.coolTime = _fireBallData.coolTime;
        skillData.curTime = skillData.coolTime;
        skillData.animTime = 0.5f;
        skillData.isUse = true;
        skillData.loadTime = _fireBallData.castingTime;
        skillData.damage = _fireBallData.damage + (int)(Main.GameManager.SpawnedCharacter.stat.Attack * _fireBallData.damageRatio);
        skillData.skillRange = _fireBallData.range;
    }

    public override bool CanUseSkill()
    {
        if (!skillData.isUse) return false;
        return true;
    }
    public override void Execute()
    {
        Character myCharacter = Main.GameManager.SpawnedCharacter;

        skillData.isUse = false;
        myCharacter.Animator.SetBool(myCharacter.PlayerAnimationData.SkillDelayTimeHash, true);
        myCharacter.SkillController.CallSkillExecute(myCharacter.CharacterSkill.FirstSkill);
        myCharacter.SkillController.CallSkillDelay(myCharacter.CharacterSkill.FirstSkill.skillData);

        InstantiateAfterLoad();
    }

    private async void InstantiateAfterLoad()
    {
        Vector2 dir = GetDir().normalized;
        await Task.Delay(TimeSpan.FromMilliseconds(skillData.loadTime));
        Character myCharacter = Main.GameManager.SpawnedCharacter;
        GameObject sphere = Main.ResourceManager.Instantiate("SkillEffect/FireBallEffect", myCharacter.transform.position, syncRequired: true);
        SetRotation(sphere, dir);
        SetSpeed(sphere, dir);

        sphere.GetComponent<ShooterInfoController>().CallShooterInfoEvent(myCharacter.gameObject);
    }

    private void SetRotation(GameObject go, Vector2 dir)
    {
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        go.transform.rotation = rotation;
    }

    private void SetSpeed(GameObject go, Vector2 dir)
    {
        Rigidbody2D rigidbody = go.GetComponent<Rigidbody2D>();
        rigidbody.velocity = dir.normalized * 10.0f;
    }

    private Vector2 GetDir()
    {
        Vector2 point = Camera.main.ScreenToWorldPoint(Main.GameManager.SpawnedCharacter.Input._playerActions.Move.ReadValue<Vector2>());
        return (point - (Vector2)Main.GameManager.SpawnedCharacter.transform.position);
    }
}