using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private int health;
    [SerializeField] private ParticleSystem hurtParticle;
    [SerializeField] private bool debug;

    private int currentHealth;
    private Vector3 startPos;

    private void Start()
    {
        currentHealth = health;
        startPos = transform.position;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        Instantiate(hurtParticle, this.transform);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        print("boom ded");

        Respawn(startPos);
    }

    private void Respawn(Vector3 position)
    {
        transform.position = position;
        currentHealth = health;
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && debug)
        {
            TakeDamage(20);
        }
    }
}
