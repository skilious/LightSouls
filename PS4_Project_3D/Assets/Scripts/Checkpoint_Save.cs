using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Checkpoint_Save : MonoBehaviour
{
    [SerializeField] private ParticleSystem[] healEffects;
    private bool healing = false;

    [SerializeField] private bool allowSave = false;
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            healing = true;
            for (int i = 0; i < healEffects.Length; i++)
            {
                healEffects[i].Play();
            }

            if (other.GetComponent<Character_Status>().curHealth >= other.GetComponent<Character_Status>().maxHealth)
            {
                healing = false;
                other.GetComponent<Character_Status>().curHealth = Mathf.RoundToInt(other.GetComponent<Character_Status>().healthHit);
            }
            else if (healing)
            {
                float healthRegenerate = Mathf.Lerp(other.GetComponent<Character_Status>().healthHit, other.GetComponent<Character_Status>().maxHealth, 2.5f * Time.deltaTime);
                other.GetComponent<Character_Status>().healthHit = healthRegenerate;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            string sceneName = SceneManager.GetActiveScene().name;
            if (allowSave)
            {
                GameManager.GMInstance.SavePosition(sceneName);
            }

            healing = false;
            other.GetComponent<Character_Status>().curHealth = Mathf.RoundToInt(other.GetComponent<Character_Status>().healthHit);
        }
    }
}
