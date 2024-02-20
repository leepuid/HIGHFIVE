public class WarriorSkill : CharacterSkill
{
    protected override void Start()
    {
        base.Start();
        FirstSkill = new Stabbing();
        FirstSkill.Init();
        SecondSkill = new StunShot();
        SecondSkill.Init();
        ThirdSkill = new Flash();
        ThirdSkill.Init();
    }
}