using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TripleBossProj : EnemyAI
{
    /*TODO: 
    Create three projectiles at three different locations
    Shoot in their own projectiles at their own locations
    Aim directly towards the player's last location.
    Adjustable firerates

    OBJ_Pooling support - Reference three GameObjects as Projectiles.
    */
    [SerializeField] private Vector3[] projectilePosition;

    [SerializeField] private Transform[] projectileSpawn;

    protected override void Start()
    {
        base.Start();
    }
    public void StartAttack()
    {
        InvokeRepeating("Attack", attackTimer, repeatTimer);
    }

    public void StopAttack()
    {
        CancelInvoke("Attack");
    }

    protected override void Update()
    {
        base.Update();
    }

    private void Attack()
    {
        StartCoroutine(Projectile());
    }

    private IEnumerator Projectile()
    {
        WaitForSeconds wait = new WaitForSeconds(0.2f);

        for (int i = 0; i < projectileSpawn.Length; i++)
        {
            projectilePosition[i] = projectileSpawn[i].position;
            switch (i)
            {
                case 0:
                    {
                        GameObject obj = Object_Pooling.SharedInstance.GetPooledObject("EnemyProjectile");
                        obj.SetActive(true);
                        obj.transform.position = projectilePosition[i];
                        obj.transform.LookAt(player.transform.position);
                        Rigidbody cloneRB = obj.GetComponent<Rigidbody>();
                        cloneRB.AddForce(obj.transform.forward * 350.0f, ForceMode.Acceleration);
                        break;
                    }
                case 1:
                    {
                        GameObject obj = Object_Pooling.SharedInstance.GetPooledObject("EnemyProjectile");
                        obj.SetActive(true);
                        obj.transform.position = projectilePosition[i];
                        obj.transform.LookAt(player.transform.position);
                        Rigidbody cloneRB = obj.GetComponent<Rigidbody>();
                        cloneRB.AddForce(obj.transform.forward * 350.0f, ForceMode.Acceleration);
                        break;
                    }
                case 2:
                    {
                        GameObject obj = Object_Pooling.SharedInstance.GetPooledObject("EnemyProjectile");
                        obj.SetActive(true);
                        obj.transform.position = projectilePosition[i];
                        obj.transform.LookAt(player.transform.position);
                        Rigidbody cloneRB = obj.GetComponent<Rigidbody>();
                        cloneRB.AddForce(obj.transform.forward * 350.0f, ForceMode.Acceleration);
                        break;
                    }
            }
            yield return wait;
        }

    }
}
