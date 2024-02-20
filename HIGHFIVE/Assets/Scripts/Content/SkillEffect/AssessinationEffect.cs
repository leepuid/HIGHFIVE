using UnityEngine;

public class AssessinationEffect : MonoBehaviour
{
    private void DestroySkillEffect()
    {
        Main.ResourceManager.Destroy(gameObject);
    }
}