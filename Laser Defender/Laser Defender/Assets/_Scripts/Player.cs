using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    // config parameters
    [Header("Player")]
    [SerializeField] float playerSpeed = 10f;
    [SerializeField] float padding = 0.5f;
    [SerializeField] int health = 200;
    [SerializeField] Text textHealth;
    //[SerializeField] LevelManager levelManager;


    [Header("Projectile")]
    [SerializeField] GameObject laser;
    [SerializeField] float laserSpeed = 10f;
    [SerializeField] float firePeriod = 0.1f;

    [Header("Sound")]
    [SerializeField] AudioClip dieSound;
    [SerializeField][Range(0,1)] float dieSoundVolume = 0.7f;
    [SerializeField] AudioClip shootSound;
    [SerializeField][Range(0,1)] float shootSoundVolume = 0.7f;

    private float xMin;
    private float xMax;
    private float yMin;
    private float yMax;
    private Coroutine firingCoroutine;

    // Start is called before the first frame update
    void Start()
    {
        SetUpMoveBoundries();
        textHealth.text = this.health.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        CheckMove();
        CheckFire();
    }

    IEnumerator Fire()
    {
        while (true)
        {
            GameObject missile = Instantiate(laser, transform.position, Quaternion.identity) as GameObject;
            missile.GetComponent<Rigidbody2D>().velocity = new Vector2(0, laserSpeed);
            AudioSource.PlayClipAtPoint(shootSound, Camera.main.transform.position, shootSoundVolume);
            yield return new WaitForSeconds(firePeriod);
        }
    }

    private void CheckFire()
    {
        if(Input.GetButtonDown("Fire1")) 
        {
            firingCoroutine = StartCoroutine(Fire());
        }
        else if (Input.GetButtonUp("Fire1"))
        {
            StopCoroutine(firingCoroutine);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        DamageDealer damageDealer = other.GetComponent<DamageDealer>();
        if(damageDealer)
        {
            DealDamage(damageDealer);
        }
    }

    private void DealDamage(DamageDealer damageDealer)
    {
        health -= damageDealer.GetDamage();
        textHealth.text = health.ToString();
        damageDealer.Hit();
        if (health <= 0)
        {
            PlayerDie();
        }
    }

    private void PlayerDie()
    {
        AudioSource.PlayClipAtPoint(dieSound, Camera.main.transform.position, dieSoundVolume);

        FindObjectOfType<LevelManager>().GoLoseScene();

        Destroy(gameObject);
    }

    private void CheckMove() 
    {
        var deltaX = Input.GetAxis("Horizontal") * Time.deltaTime * playerSpeed;
        var deltaY = Input.GetAxis("Vertical") * Time.deltaTime * playerSpeed;

        var newXPos = Mathf.Clamp(transform.position.x + deltaX, xMin, xMax);
        var newYPos = Mathf.Clamp(transform.position.y + deltaY, yMin, yMax);
        transform.position = new Vector2(newXPos, newYPos);
    }

    private void SetUpMoveBoundries()
    {
        Camera gameCamera = Camera.main;
        xMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x + padding;
        xMax = gameCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x - padding;
        yMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y + padding;
        yMax = gameCamera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y - padding;
    }
}
