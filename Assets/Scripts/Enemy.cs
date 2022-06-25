using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] GameObject deathVFX;
    void OnParticleCollision(GameObject other)
    {
        Instantiate(deathVFX, transform.position, Quaternion.identity);
        Debug.Log($"{name} I'm hit! by {other.gameObject.name}");
        Destroy(gameObject);
    }
}
