using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBallEffect : MonoBehaviour
{
    private void DestroySkillEffect()
    {
        Main.ResourceManager.Destroy(gameObject);
    }
}
