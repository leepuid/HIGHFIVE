using UnityEngine;

public class EliteBuff : BaseBuff
{
    private BuffDBEntity _eliteBuffData;
    public override void Init()
    {
        base.Init();
        if (Main.DataManager.BuffDict.TryGetValue("엘리트", out BuffDBEntity eliteBuffData))
        {
            _eliteBuffData = eliteBuffData;
        }
        //나중에 데이터 매니저에서 받아오기
        buffData.buffSprite = Main.ResourceManager.Load<Sprite>("Sprites/BuffIcon/BlueBuff");
        buffData.type = typeof(EliteBuff);
        buffData.duration = _eliteBuffData.durationTime;
        buffData.curTime = 0;
        buffData.isSustainBuff = _eliteBuffData.isSustain; //유지여부
    }

    public override void Activation()
    {
        myCharacter.stat.Attack += _eliteBuffData.atk;
        myCharacter.stat.Defence += _eliteBuffData.def;
        myCharacter.stat.MoveSpeed += _eliteBuffData.moveSpd;
    }

    public override void Deactivation()
    {
        myCharacter.stat.Attack -= _eliteBuffData.atk;
        myCharacter.stat.Defence -= _eliteBuffData.def;
        myCharacter.stat.MoveSpeed -= _eliteBuffData.moveSpd;
    }
}