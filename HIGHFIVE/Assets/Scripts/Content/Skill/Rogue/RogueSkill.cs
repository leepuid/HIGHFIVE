public class RogueSkill : CharacterSkill
{
    protected override void Start()
    {
        base.Start();
        FirstSkill = new Assassination();
        FirstSkill.Init();
        SecondSkill = new StunShot();
        SecondSkill.Init();
        ThirdSkill = new Flash();
        ThirdSkill.Init();
    }
}