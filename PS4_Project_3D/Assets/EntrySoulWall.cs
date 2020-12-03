using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Tai's script for EntrySoulWall inheriting the Wave System.
public class EntrySoulWall : Wave_System
{
    private ParticleSystem ps;
    private bool stopSpawn = false;
    private void Start()
    {
        ps = GetComponent<ParticleSystem>();
    }
    private IEnumerator OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player") && !stopSpawn)
        {
            beginWave = true;
            stopSpawn = true;
            yield return new WaitForSeconds(1.0f);
            gameObject.GetComponent<BoxCollider>().isTrigger = false;
            ps.Play();
        }
    }
}
