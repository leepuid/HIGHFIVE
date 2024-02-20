using UnityEngine;

public class RedBuff : BaseBuff
{
    private BuffDBEntity _redBuffData;
    public override void Init()
    {
        base.Init();
        if (Main.DataManager.BuffDict.TryGetValue("레드", out BuffDBEntity redBuffData))
        {
            _redBuffData = redBuffData;
        }
        //나중에 데이터 매니저에서 받아오기
        buffData.buffSprite = Main.ResourceManager.Load<Sprite>("Sprites/BuffIcon/Red");
        buffData.type = typeof(RedBuff);
        buffData.duration = _redBuffData.durationTime;
        buffData.curTime = 0;
        buffData.isSustainBuff = _redBuffData.isSustain; //유지여부
    }

    public override void Activation()
    {
        myCharacter.stat.Attack += _redBuffData.atk;
    }

    public override void Deactivation()
    {
        myCharacter.stat.Attack -= _redBuffData.atk;
    }
}