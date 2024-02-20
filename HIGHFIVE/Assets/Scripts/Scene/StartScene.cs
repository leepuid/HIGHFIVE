using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartScene : BaseScene
{
    [SerializeField] private HIGHFIVE_Data HIGHFIVE_data; // 인스펙터창에 직접 엑셀데이터 대입 방식(엑셀파일말고 SO형식인 HIGHFIVE_Data.asset을 넣어줘야함)
    private CharacterDBEntity warrior; //전사 정보가 들어갈 변수
    protected override void Init()
    {
        base.Init();
        //DebugTest();
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Click");
            Main.SoundManager.PlaySFX("SFX_Click", 0.01f);
        }
    }

    private void DebugTest()
    {
        Debug.Log(HIGHFIVE_data.Characters[0].job); // 엑셀데이터를 대입해준 HIGHFIVE_data에서 정보를 가져옴 (전사 출력)

        // DataManager에서 만들어둔 CharacterDict 에서 "전사"key의 value( (전사), (전사정보), atk...)를 불러와서 warrior변수에 담아준다.(DataManager에서 확인해볼것)
        // 밑에 방식을 쓰면 HIGHFIVE_data가 이미 DataManager 에 있으므로 위에 [SerializeField] private HIGHFIVE_Data HIGHFIVE_data;를 선언해줄필요가 없음
        // (참고)TryGetValue는 bool값을 반환한다.
        Main.DataManager.CharacterDict.TryGetValue("도적", out CharacterDBEntity rogue); // 이렇게 out에서 바로 변수 선언해줄수도있음
        Main.DataManager.CharacterDict.TryGetValue("전사", out warrior); //"전사"정보를 찾아보고 찾는데 성공하면 warrior에 정보 넣어주기
        
        Debug.Log(rogue.job);
        Debug.Log(warrior.def);
    }
}
