using System;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField]
    protected int startingHealth = 100;

    private int health;

    public event Action OnDied;

    private void Awake()
    {
        health = startingHealth;
    }

    public void TakeDamage(int amount)
    {
        health -= amount;
        if (health <= 0)
            Die();
    }

    private void Die()
    {
        if (OnDied != null)
            OnDied();

        //Destroy(gameObject);
    }
}
