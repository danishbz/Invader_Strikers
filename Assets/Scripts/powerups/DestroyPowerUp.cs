using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyPowerUp : MonoBehaviour
{
    public float destroyRadius = 5f; // Adjust the radius

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            CollectPowerUp();
        }
    }

    private void CollectPowerUp()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, destroyRadius);

        foreach (Collider2D collider in colliders)
        {
            if (collider.CompareTag("Enemy"))
            {
                Destroy(collider.gameObject);
            }
        }

        Destroy(gameObject);
    }
}