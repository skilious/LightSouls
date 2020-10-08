using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_Projectile : MonoBehaviour
{
    Transform player;
    [SerializeField]
    private float fire_Rate = 0;

    [SerializeField]
    private float maxDistance;
    private float timer;
    public int NumOfProjectiles;
    public int min, max;
    [SerializeField]
    private GameObject projectile;

    private Vector3 startPoint;

    private const float radius = 1.0f;
    public enum shootingType
    {
        basic_projectile,
        circular_projecitle,
        test3
    };

    public shootingType shoot_type;
    private void Awake()
    {
        player = GameObject.Find("CharacterTest").GetComponent<Transform>();
    }
    private void Update()
    {
        if (shoot_type == shootingType.basic_projectile)
        {
            timer -= Time.deltaTime;
            float dist = Vector3.Distance(player.position, transform.position);
            Vector2 direction = player.position - transform.position;
            if (dist < maxDistance && timer < 0)
            {
                GameObject clone = Instantiate(projectile, transform.position, Quaternion.identity);
                Rigidbody2D cloneRB = clone.gameObject.GetComponent<Rigidbody2D>();
                cloneRB.velocity = direction.normalized * 2.0f;
                float AngleRad = Mathf.Atan2(cloneRB.velocity.y, cloneRB.velocity.x);

                float AngleDeg = (180 / Mathf.PI) * AngleRad;

                clone.transform.rotation = Quaternion.Euler(0, 0, AngleDeg);
                Destroy(clone, 3.0f);
                timer = fire_Rate;
            }
        }

        if (shoot_type == shootingType.circular_projecitle)
        {
            timer -= Time.deltaTime;
            float dist = Vector3.Distance(player.position, transform.position);
            if (dist < maxDistance && timer < 0)
            {
                NumOfProjectiles = 10;//Random.Range(min, max);
                SpawnProjectiles(NumOfProjectiles);
                timer = fire_Rate;
            }
        }
    }

    private void SpawnProjectiles(int _numOfProjectiles)
    {
        //360 a whole circle dividing the amount of projectiles.
        float angleStep = 360 / _numOfProjectiles;
        float angle = 0;
        for (int i = 0; i <= _numOfProjectiles - 1; i++)
        {
            float projDirXPos = startPoint.x + Mathf.Sin(angle * Mathf.PI / 180) * radius;
            float projDirYPos = startPoint.y + Mathf.Cos(angle * Mathf.PI / 180) * radius;
            Vector3 projectileVector = new Vector3(projDirXPos, projDirYPos);
            Vector3 projMoveDirection = (projectileVector - startPoint).normalized * 2.5f;

            GameObject clone = Instantiate(projectile, transform.position, Quaternion.identity);
            Rigidbody2D cloneRB = clone.gameObject.GetComponent<Rigidbody2D>();
            cloneRB.velocity = new Vector3(projMoveDirection.x, projMoveDirection.y, 0);

            float AngleRad = Mathf.Atan2(cloneRB.velocity.y, cloneRB.velocity.x);

            float AngleDeg = (180 / Mathf.PI) * AngleRad;

            clone.transform.rotation = Quaternion.Euler(0, 0, AngleDeg);
            Destroy(clone, 3.0f);
            angle += angleStep;
        }
    }
}
