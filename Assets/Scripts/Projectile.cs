using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {

    public float damage = 100f;
    public GameObject damageEffect;

    void Start()
    {
        GetComponent<AudioSource>().Play();
    }

    public void hit()
    {
        GameObject effect = Instantiate(damageEffect, transform.position, Quaternion.identity) as GameObject;
        ParticleSystem.EmissionModule em = effect.GetComponent<ParticleSystem>().emission;
        ParticleSystem.MinMaxCurve rate = em.rate;
        rate.constantMax = damage;
        em.rate = rate;
        effect.GetComponent<ParticleSystem>().startColor = this.GetComponent<SpriteRenderer>().color;
        Destroy(gameObject);
    }

    public float GetDamage()
    {
        return damage;
    }
}
