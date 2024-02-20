using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillTimer : MonoBehaviour
{
    protected SkillController _skillController;
    protected virtual void Start()
    {
        _skillController = GetComponent<SkillController>();
        _skillController.skillExecuteEvent += StartCoolDown;
        _skillController.skillDelayEvent += StarSkillDelay;
    }

    private void StartCoolDown(BaseSkill skill)
    {
        StartCoroutine(StartCoolCoroutine(skill));
    }

    private void StarSkillDelay(SkillData skillData)
    {
        StartCoroutine(StartSkillDelayCoroutine(skillData));
    }

    IEnumerator StartCoolCoroutine(BaseSkill skill)
    {
        while (skill.skillData.curTime > 0)
        {
            skill.skillData.curTime -= 0.1f;
            skill.skillData.coolTimeicon.fillAmount = skill.skillData.curTime / skill.skillData.coolTime;
            yield return new WaitForSeconds(0.1f);
        }
        skill.skillData.coolTimeicon.fillAmount = 0;
        skill.skillData.curTime = skill.skillData.coolTime;
        skill.skillData.isUse = true;
    }
    IEnumerator StartSkillDelayCoroutine(SkillData skillData)
    {
        yield return new WaitForSeconds(skillData.animTime);
        Character myCharacter = Main.GameManager.SpawnedCharacter;
        myCharacter.Animator.SetBool(myCharacter.PlayerAnimationData.SkillDelayTimeHash, false);
    }
}
