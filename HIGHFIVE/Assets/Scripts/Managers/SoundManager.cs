using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class SoundManager : MonoBehaviour
{
    // 오디오 소스
    private AudioSource bgmPlayer;
    private List<AudioSource> sfxPlayer = new List<AudioSource>();
    private AudioMixer audioMixer;

    public void SoundInit()
    {
        GameObject musicObject = GameObject.Find("SoundManager");

        if (musicObject == null)
        {
            musicObject = new GameObject("SoundManager");
            musicObject.GetOrAddComponent<AudioSource>();
        }

        Object.DontDestroyOnLoad(musicObject);
        bgmPlayer = Util.GetOrAddComponent<AudioSource>(musicObject);

        for (int i = 0; i < 10; i++)
        {
            AudioSource temp = musicObject.AddComponent<AudioSource>();
            sfxPlayer.Add(temp);
        }

        PlayBGM("Town_Castle_01", 0.1f);
        BGM();
    }

    public void SoundUpdate()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Main.SoundManager.PlayEffect("SFX_Click", 1f);
        }
    }

    public void BGM()
    {
        if (SceneManager.GetActiveScene().name == "IntroScene")
        { PlayBGM("Town_Castle_01", 0.02f); } 

        //else if (SceneManager.GetActiveScene().name == "RoomScene")
        //{ PlayBGM("Town_Castle_01", 0.02f); } 

        else if (SceneManager.GetActiveScene().name == "SelectScene")
        { PlayBGM("Battle_Normal_EW02", 0.02f); }

        else if (SceneManager.GetActiveScene().name == "GameScene")
        { PlayBGM("Battle_Boss_07", 0.02f); }
    }

    public void PlayBGM(string bgmName, float volume)
    {
        bgmPlayer.clip =  Resources.Load<AudioClip>($"Sounds/BGM/{bgmName}");
        if (bgmPlayer.clip != null)
        {
            bgmPlayer.volume = volume;
            bgmPlayer.Play();
        }
    }

    public void PlayEffect(string efxName, float volume)
    {
        AudioClip clip = Resources.Load<AudioClip>($"Sounds/SFX/{efxName}");
        //Debug.Log(effectPlayer.Count);
        for (int i = 0; i < sfxPlayer.Count; i++)
        {
            if (sfxPlayer[i].isPlaying)
            {
                //Debug.Log(i);
                continue;
            }
            else
            {
                sfxPlayer[i].volume = volume;
                sfxPlayer[i].PlayOneShot(clip);
            }
        }
        //예외처리 필요, 10개보다 더 늘어날경우
    }

    // 음소거
    void Mute()
    {
        bgmPlayer.mute = !bgmPlayer.mute;
    }

    //사용법
    //SoundManager.instance.PlayEffect("효과음", 1f);
    //Main.SoundManager.PlayEffect("파일이름", 볼륨); => 아직 미연결
}

//using System.Collections.Generic;
//using Unity.VisualScripting;
//using UnityEngine;
//public class SoundManager
//{
//    // 오디오 소스
//    private AudioSource bgmPlayer;
//    private List<AudioSource> effectPlayer = new List<AudioSource>();
//    public void SoundInit()
//    {
//        GameObject testObject = GameObject.Find("SoundManager");
//        if (testObject == null)
//        {
//            testObject = new GameObject("SoundManager");
//            testObject.GetOrAddComponent<AudioSource>();
//        }
//        Object.DontDestroyOnLoad(testObject);
//        bgmPlayer = Util.GetOrAddComponent<AudioSource>(testObject);
//        for (int i = 0; i < 10; i++)
//        {
//            AudioSource temp = testObject.AddComponent<AudioSource>();
//            effectPlayer.Add(temp);
//        }
//        /*var t = testObject.GetComponent<SoundManager>();
//        if (t == Main.SoundManager)
//        {
//            Debug.Log("일치");
//        }
//        else
//        {
//            Debug.Log("불일치");
//        }*/
//        //PlayBGM("BGM_Powerful", 0.01f);
//        PlayBGM("KBF_3m_Town_Castle_01", 0.01f);
//    }
//    public void SoundUpdate()
//    {
//        if (Input.GetMouseButtonDown(0))
//        {
//            Main.SoundManager.PlayEffect("SFX_Click", 1f);
//        }
//    }
//    public void PlayBGM(string bgmName, float volume)
//    {
//        bgmPlayer.clip = Resources.Load<AudioClip>($"Sounds/BGM/{bgmName}");
//        if (bgmPlayer.clip != null)
//        {
//            bgmPlayer.volume = volume;
//            bgmPlayer.Play();
//        }
//    }
//    public void PlayEffect(string efxName, float volume)
//    {
//        AudioClip clip = Resources.Load<AudioClip>($"Sounds/SFX/{efxName}");
//        //Debug.Log(effectPlayer.Count);
//        for (int i = 0; i < effectPlayer.Count; i++)
//        {
//            if (effectPlayer[i].isPlaying)
//            {
//                //Debug.Log(i);
//                continue;
//            }
//            else
//            {
//                effectPlayer[i].volume = volume;
//                effectPlayer[i].PlayOneShot(clip);
//            }
//        }
//        //예외처리 필요, 10개보다 더 늘어날경우
//    }
//    // 음소거
//    void Mute()
//    {
//        bgmPlayer.mute = !bgmPlayer.mute;
//    }
//    //사용법
//    //SoundManager.instance.PlayEffect("효과음", 1f);
//    //Main.SounManager.PlayEffect("파일이름", 볼륨); => 아직 연결안됌
//}
