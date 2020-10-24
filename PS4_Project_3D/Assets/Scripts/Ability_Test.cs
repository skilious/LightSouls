using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ability_Test : MonoBehaviour
{
    //Instantiate atleast 3-5 projectiles behind the character
    //Once 3-5 projectiles have all been spawned with their own offsets,
    //start yeeting the projectiles to the location that has been provided from the cursor.
    //It'll despawn after a few seconds if it didn't hit anything.

    public int spawnCount;

    public float secondsEverySpawn;
    public GameObject spawnProjectile;
    public Vector3 offset, targetPos;
    public LayerMask clickable;
    private float offsetBetweenProj = -2.0f;
    private int countProjSpawn = 0;

    protected private float timer = 0f;
    private void Update()
    {
        Ray rayToCursor = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit rayHit;
        if (Physics.Raycast(rayToCursor, out rayHit, clickable))
        {
            targetPos = new Vector3(rayHit.point.x, transform.position.y, rayHit.point.z);
        }
        //Debug.DrawRay(transform.position, rayHit.point, Color.green);

        //Once Alpha2 on keyboard is pressed down.
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            StartCoroutine(spawnProj());
        }
    }

    IEnumerator spawnProj()
    {
        for (int i = 0; i < spawnCount; i++)
        {
            offset = transform.position - (transform.forward * 3.0f) + (transform.right * offsetBetweenProj);
            GameObject spawnClone = Object_Pooling.SharedInstance.GetPooledObject("Projectile");
            spawnClone.transform.position = offset;
            spawnClone.transform.rotation = Quaternion.identity;
            spawnClone.SetActive(true);
            spawnClone.transform.LookAt(targetPos);
            offsetBetweenProj += 1.0f;
            yield return new WaitForSeconds(0.2f);
            Rigidbody cloneRB = spawnClone.GetComponent<Rigidbody>();
            countProjSpawn++;
            if (countProjSpawn >= spawnCount)
            {
                offsetBetweenProj = -2.0f;
                countProjSpawn = 0;
            }
            cloneRB.AddForce(spawnClone.transform.forward * 500.0f, ForceMode.Acceleration);
        }
    }
}
