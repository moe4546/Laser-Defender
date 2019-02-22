using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] float health = 100f;
    [SerializeField] float shotCounter;
    [SerializeField] float minTimeBetweenShots = 0.2f;
    [SerializeField] float maxTimeBetweenShots = 3f;
    [SerializeField] float shootSpeed = 20f;
    [SerializeField] GameObject projectile;
    [SerializeField] GameObject explosionParticle;
    [SerializeField] int scoreValue = 50;

    [Header("Sound")]
    [SerializeField] AudioClip dieSound;
    [SerializeField] [Range(0, 1)] float dieSoundVolume = 0.7f;
    [SerializeField] AudioClip shootSound;
    [SerializeField] [Range(0, 1)] float shootSoundVolume = 0.7f;

    private void Start()
    {
        shotCounter = Random.Range(minTimeBetweenShots, maxTimeBetweenShots);
    }

    private void Update()
    {
        CountDownAndShoot();
    }

    private void CountDownAndShoot()
    {
        shotCounter -= Time.deltaTime;
        if(shotCounter <= 0f)
        {
            Fire();
            shotCounter = Random.Range(minTimeBetweenShots, maxTimeBetweenShots);
        }
    }

    private void Fire()
    {
        GameObject laser = Instantiate(projectile, gameObject.transform.position, Quaternion.identity) as GameObject;
        laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -shootSpeed);
        AudioSource.PlayClipAtPoint(shootSound, Camera.main.transform.position, shootSoundVolume);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        DamageDealer damageDealer = other.GetComponent<DamageDealer>();
        PrecessHit(damageDealer);
    }

    private void PrecessHit(DamageDealer damageDealer)
    {
        if (damageDealer)
        {
            health -= damageDealer.GetDamage();
            damageDealer.Hit();
            if (health <= 0)
            {
                Die();
            }
        }
    }

    private void Die()
    {
        AudioSource.PlayClipAtPoint(dieSound, Camera.main.transform.position, dieSoundVolume);
        FindObjectOfType<GameSession>().AddScore(scoreValue);
        GameObject explosion = Instantiate(explosionParticle, transform.position, Quaternion.identity) as GameObject;
        Destroy(explosion, 1f);
        Destroy(gameObject);
    }
}
