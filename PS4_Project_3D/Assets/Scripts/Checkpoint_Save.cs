using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Checkpoint_Save : MonoBehaviour
{
    [SerializeField] private ParticleSystem[] healEffects;
    private bool healing = false;
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            string sceneName = SceneManager.GetActiveScene().name;
            print(sceneName);
            GameManager.GMInstance.SavePosition(sceneName);
            healing = true;
            for (int i = 0; i < healEffects.Length; i++)
            {
                healEffects[i].Play();
            }

            if (Character_Status.instance.curHealth >= Character_Status.instance.maxHealth)
            {
                healing = false;
                Character_Status.instance.curHealth = Mathf.RoundToInt(Character_Status.instance.healthHit);
            }
            else if (healing)
            {
                float healthRegenerate = Mathf.Lerp(Character_Status.instance.healthHit, Character_Status.instance.maxHealth, 2.5f * Time.deltaTime);
                Character_Status.instance.healthHit = healthRegenerate;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            healing = false;
            Character_Status.instance.curHealth = Mathf.RoundToInt(Character_Status.instance.healthHit);
        }
    }
}
