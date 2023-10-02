using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;
    [HideInInspector] public Rigidbody2D rb2D;
    [HideInInspector] public Vector2 moveDirection;
    public float moveSpeed;
    private float friction;
    public float moveFriction;
    public float brakeFriction;
    public float oxygenPoint = 100f;
    public float damageCooldownInterval;
    [HideInInspector] public bool damageLock = false;

    public List<Collider2D> nearbyBodies;
    public GameObject nearestBody;
    public float nearestBodyDistance;
    public TextMeshPro bodyDistanceDisplay;

    public List<Collider2D> nearbyDangers;
    public GameObject nearestDanger;
    public float nearestDangerDistance;
    public TextMeshPro dangerDistanceDisplay;

    public SpriteRenderer fireSpriteRenderer;
    public GameObject playerSprite;

    public TextMeshPro playerLine;
    public List<string> lines;

    public List<AudioClip> spraySounds;

    void Start()
    {
        instance = this;
        rb2D = GetComponent<Rigidbody2D>();
        friction = moveFriction;
    }

    void Update()
    {
        moveDirection = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")).normalized;
        if (rb2D.velocity != Vector2.zero)
        {
            playerSprite.transform.rotation = Quaternion.LookRotation(Vector3.forward, rb2D.velocity);
        }
        Brake();
        DetectNearby();


        if (nearestBodyDistance < 99)
        {
            bodyDistanceDisplay.text = nearestBodyDistance.ToString("F");
        }
        else
        {
            bodyDistanceDisplay.text = "";
        }
        
        if (nearestDangerDistance < 40)
        {
            dangerDistanceDisplay.text = nearestDangerDistance.ToString("F");
        }
        else
        {
            dangerDistanceDisplay.text = "";
        }

        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D) ||
            Input.GetKeyDown(KeyCode.F) || Input.GetKeyDown(KeyCode.LeftArrow) ||
            Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.UpArrow) ||
            Input.GetKeyDown(KeyCode.DownArrow)) 
        {
            AudioSource audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.clip = spraySounds[Random.Range(0, spraySounds.Count)];
            audioSource.spatialBlend = 0;
            audioSource.Play();
            StartCoroutine(SelfDestroy(audioSource));
        }
    }

    void FixedUpdate()
    {
        PlayerControl();
        rb2D.velocity *= friction;
    }

    void PlayerControl()
    {
        rb2D.AddForce(moveDirection * moveSpeed);
    }

    void Brake()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            friction = brakeFriction;
            fireSpriteRenderer.enabled = true;
            moveDirection = Vector2.zero;
        }
        else
        {
            fireSpriteRenderer.enabled = false;
        }
        
        if(Input.GetKeyUp(KeyCode.Space))
        {
            friction = moveFriction;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            AudioSource audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.clip = spraySounds[Random.Range(0, spraySounds.Count)];
            audioSource.spatialBlend = 0;
            audioSource.Play();
            StartCoroutine(SelfDestroy(audioSource));
        }
    }

    IEnumerator SelfDestroy(AudioSource audioSource)
    {
        yield return new WaitForSeconds(2f);
        Destroy(audioSource);
    }

    public void TakeDamage(float damage)
    {
        if (!damageLock)
        {
            oxygenPoint -= damage;
            damageLock = true;
            Invoke("ResetDamageLock", damageCooldownInterval);
        }

        if (oxygenPoint < 0) GameManager.Instance.ChangeState(GameManager.GameState.GameOver);
    }

    void ResetDamageLock()
    {
        damageLock = false;
    }

  

    void DetectNearby()
    {
        nearbyBodies.Clear();
        nearbyDangers.Clear();
        
        nearestBodyDistance = Single.PositiveInfinity;
        nearestDangerDistance = Single.PositiveInfinity;
        
        List<Collider2D> potentialNearbyBodies = Physics2D.OverlapCircleAll(transform.position, 100f).ToList();
        foreach (var potentialBodyCollider2D in potentialNearbyBodies)
        {
            if (potentialBodyCollider2D.CompareTag("Heart"))
            {
                nearbyBodies.Add(potentialBodyCollider2D);
            }

            if (potentialBodyCollider2D.CompareTag("Danger"))
            {
                nearbyDangers.Add(potentialBodyCollider2D);
            }
        }

        foreach (var bodyCollider2D in nearbyBodies)
        {
            float distance = (bodyCollider2D.gameObject.transform.position - transform.position).magnitude;
            if (distance < nearestBodyDistance)
            {
                nearestBodyDistance = distance;
                nearestBody = bodyCollider2D.gameObject;
            }
        }

        foreach (var dangerCollider2D in nearbyDangers)
        {
            float distance = (dangerCollider2D.gameObject.transform.position - transform.position).magnitude;
            if (distance < nearestDangerDistance)
            {
                nearestDangerDistance = distance;
                nearestDanger = dangerCollider2D.gameObject;
            }
        }
    }
}
