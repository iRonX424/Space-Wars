using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] float health = 100;
    [SerializeField] int scoreValue;

    [SerializeField] float shotCounter;
    [SerializeField] float minShootingTime = 0.1f;
    [SerializeField] float maxShootingTime = 5f;
    [SerializeField] float firingSpeed = 10f;

    [SerializeField] GameObject laserPrefab;
    [SerializeField] AudioClip LaserSound;
    [SerializeField] [Range(0, 1)] float LaserSoundVol = 0.2f;

    [SerializeField] GameObject EnemyHitVfx;
    [SerializeField] GameObject EnemyDeathVFX;

    [SerializeField] AudioClip DeathSound;
    [SerializeField] [Range(0, 1)] float DeathSoundVol = 0.5f;
 
    void Start()
    {
        shotCounter = Random.Range(minShootingTime,maxShootingTime);
    }
  
    void Update()
    {
        countdownAndShoot();
    }

    private void countdownAndShoot()
    {
        shotCounter -= Time.deltaTime;

        if(shotCounter<=0f)
        {
            fire();
            shotCounter = Random.Range(minShootingTime, maxShootingTime);
        }
    }

    private void fire()
    {
        GameObject laser = Instantiate(laserPrefab, gameObject.transform.position, Quaternion.identity);
        laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0f, -firingSpeed);

        AudioSource.PlayClipAtPoint(LaserSound,Camera.main.transform.position,LaserSoundVol);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        DamageDealer damageDealer = other.gameObject.GetComponent<DamageDealer>();
        if(!damageDealer) { return; }
        processHit(damageDealer);
    }

    private void processHit(DamageDealer damageDealer)
    {
        health -= damageDealer.getDamage();
        damageDealer.hit();
        if (health <= 0)
        {
            Destroy(gameObject);
            Instantiate(EnemyDeathVFX, transform.position, transform.rotation);
            AudioSource.PlayClipAtPoint(DeathSound,Camera.main.transform.position,DeathSoundVol);
            FindObjectOfType<GameSession>().AddToScore(scoreValue);
        }
        else
        {
            Instantiate(EnemyHitVfx,transform.position,transform.rotation);
        }
    }
}
