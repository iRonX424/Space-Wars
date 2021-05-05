using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    //prefabs
    [Header("Projectile")]
    [SerializeField] GameObject laserPrefab;

    //laser projectile related
    [SerializeField] float firingSpeed = 10f;
    [SerializeField] float firingInterval = 0.2f;
    [SerializeField] AudioClip LaserSound;
    [SerializeField] [Range(0, 1)] float LaserSoundVol = 0.2f;

    Coroutine firingCoroutine;

    //movement speed
    [Header("Player")]
    [SerializeField] float movementSpeed = 6f;
    [SerializeField] int health = 500;
    //for movement boundaries
    [SerializeField] float padding = 1f;
    [SerializeField] GameObject PlayerHitVfx;
    [SerializeField] GameObject PlayerDeathVFX;

    [SerializeField] AudioClip HitSound;
    [SerializeField] AudioClip DeathSound;
    [SerializeField] [Range(0, 1)] float SoundVol = 0.5f;

    float xMin=0f;
    float xMax = 0f;

    float yMin = 0f;
    float yMax = 0f;
    void Start()
    {
        movementBoundaries();
    }

    void movementBoundaries()
    {
        Camera gameCamera = Camera.main;
        
        //for x axis
        xMin = gameCamera.ViewportToWorldPoint(new Vector3(0f,0f,0f)).x+padding;
        xMax = gameCamera.ViewportToWorldPoint(new Vector3(1f,0f,0f)).x-padding;

        //for y axis
        yMin = gameCamera.ViewportToWorldPoint(new Vector3(0f,0f,0f)).y+padding;
        yMax = gameCamera.ViewportToWorldPoint(new Vector3(0f,1f,0f)).y-padding;
    }


    void Update()
    {
        Move();
        Fire();
    }

    void Move()
    {
        //for x axis
        float deltaX = Input.GetAxisRaw("Horizontal")*movementSpeed*Time.deltaTime;
        float newXPos = Mathf.Clamp(transform.position.x + deltaX,xMin,xMax);

        //for y axis
        float deltaY = Input.GetAxisRaw("Vertical")*movementSpeed*Time.deltaTime;
        float newYPos = Mathf.Clamp(transform.position.y + deltaY,yMin,yMax);

        transform.position = new Vector2(newXPos,newYPos);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        DamageDealer damageDealer = other.gameObject.GetComponent<DamageDealer>();
        if (!damageDealer) { return; }
        processHit(damageDealer);
    }

    private void processHit(DamageDealer damageDealer)
    {
        health -= damageDealer.getDamage();
        damageDealer.hit();
        if (health <= 0)
        {
            Destroy(gameObject);
            Instantiate(PlayerDeathVFX,transform.position,transform.rotation);
            AudioSource.PlayClipAtPoint(DeathSound, Camera.main.transform.position, SoundVol);
            FindObjectOfType<Level>().loadGameOver();
        }
        else
        {
            GameObject hitVfx= Instantiate(PlayerHitVfx, transform.position, transform.rotation)as GameObject;
            hitVfx.transform.parent = GameObject.Find("Player").transform;
            AudioSource.PlayClipAtPoint(HitSound, Camera.main.transform.position, SoundVol);
        }
    }

    public int GetHealth()
    {
        return health;
    }

    void Fire()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            firingCoroutine=StartCoroutine(fireContinuous());
        }
        if(Input.GetButtonUp("Fire1"))
        {
            StopCoroutine(firingCoroutine);
        }
    }

    IEnumerator fireContinuous()
    {
        while (true)
        {
            GameObject laser = Instantiate(laserPrefab, transform.position, Quaternion.identity) as GameObject;
            laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0f, firingSpeed);

            AudioSource.PlayClipAtPoint(LaserSound,Camera.main.transform.position,LaserSoundVol);
            yield return new WaitForSeconds(firingInterval);
        }
    }
}
