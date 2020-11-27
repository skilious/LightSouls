using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TacklingObj : EnemyAI
{
    private enum Abilities
    {
        basic_tackle,
        consecutive_tackle
    };

    private Abilities ability;

    private CapsuleCollider col;
    private Rigidbody rb;

    [SerializeField] private float dashSpeed = 0.0f;

    protected override void Start()
    {
        base.Start();
        rb = GetComponent<Rigidbody>();
        col = GetComponent<CapsuleCollider>();
    }
    public void StartAttack()
    {
        InvokeRepeating("Attack", attackTimer, repeatTimer);
    }
    public void StopAttack()
    {
        //Cancel attack once it reaches a certain distance.
        CancelInvoke("Attack");
    }

    void Attack()
    {
        StartCoroutine(TacklingAttack());
    }
    private IEnumerator TacklingAttack()
    {
        GameObject obj = gameObject.transform.GetChild(0).gameObject;
        print("Its currently targeting");
        float abilityTarget = Random.Range(0, 2);
        ability = (Abilities)abilityTarget;
        yield return new WaitForSeconds(0.5f);
        switch (ability)
        {
            case Abilities.basic_tackle:
                {
                    print("Go for it");
                    obj.SetActive(true);
                    col.isTrigger = true;
                    // transform.position = Vector3.MoveTowards(transform.position, getPlayerPos, 7.5f * Time.fixedDeltaTime);
                    rb.AddForce(transform.forward * dashSpeed, ForceMode.Acceleration);
                    //I hate this part the most of this whole script.
                    //Calling two Raycasts in one if statement for both back and forth of the GameObject.
                    yield return new WaitForSeconds(0.5f);
                    print("Conditions have met and have stopped tackling");
                    rb.velocity = Vector3.zero;
                    obj.SetActive(false);
                    col.isTrigger = false;
                    break;
                }
            case Abilities.consecutive_tackle:
                {
                    for (int i = 0; i < 3; i++)
                    {
                        print("Go for it");
                        obj.SetActive(true);
                        col.isTrigger = true;
                        // transform.position = Vector3.MoveTowards(transform.position, getPlayerPos, 7.5f * Time.fixedDeltaTime);
                        rb.AddForce(transform.forward * dashSpeed, ForceMode.Acceleration);
                        //I hate this part the most of this whole script.
                        //Calling two Raycasts in one if statement for both back and forth of the GameObject.
                        yield return new WaitForSeconds(0.3f);
                        print("Conditions have met and have stopped tackling");
                        rb.velocity = Vector3.zero;
                        obj.SetActive(false);
                        col.isTrigger = false;
                    }
                    break;
                }
        }
    }

}

