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
    private void Update()
    {
        Ray rayToCursor = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit rayHit;
        if (Physics.Raycast(rayToCursor, out rayHit, clickable))
        {
            targetPos = new Vector3(rayHit.point.x, transform.position.y, rayHit.point.z);
        }
        Debug.DrawRay(transform.position, rayHit.point, Color.green);

        //Once Alpha2 on keyboard is pressed down.
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            float offsetBetweenProj = -2.0f;
            for (int i = 0; i < spawnCount; i++)
            {
                offset = transform.position - (transform.forward * 3.0f) + (transform.right * offsetBetweenProj);
                GameObject spawnClone = Instantiate(spawnProjectile, offset, Quaternion.identity);

                spawnClone.transform.LookAt(targetPos);
                offsetBetweenProj += 1.0f;
                Rigidbody cloneRB = spawnClone.GetComponent<Rigidbody>();
                cloneRB.AddForce(spawnClone.transform.forward * 500.0f, ForceMode.Acceleration);

                Destroy(spawnClone, 2.0f);
                if (i >= spawnCount - 1)
                {
                    offsetBetweenProj = -2.0f;
                }
            }
        }
    }
}
