using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Monster : MonoBehaviour
{
    [Header("References")]
    public Transform target;
    public Animator animator;
    public GameObject deathEffect;
    public Slider healthBar;

    [Header("Audio")]
    public AudioClip HitSound;
    public AudioClip deathSoundClip;
    public AudioSource audioSource;

    [Header("Follow Settings")]
    public float followDistance = 20f;
    public float stopDistance = 2f;
    public float wanderSpeed = 2.5f;
    public float chaseSpeed = 4f;

    [Header("Wander Settings")]
    public float wanderRadius = 10f;
    public float wanderInterval = 3f;

    [Header("Stats")]
    public float maxHealth = 100f;
    public float attackDamage = 10f;
    public float attackRate = 1f;
    public float attackRange = 2f;

    private float currentHealth;
    private float wanderTimer;
    private NavMeshAgent agent;
    private Vector3 spawnPosition;
    private float lastAttackTime;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        spawnPosition = transform.position;
        wanderTimer = wanderInterval;
        currentHealth = maxHealth;
        healthBar.maxValue = maxHealth;

        if (target == null)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
                target = player.transform;
        }
    }

    void Update()
    {
        if (target == null) return;

        float distance = Vector3.Distance(transform.position, target.position);

        if (distance <= followDistance)
        {
            agent.stoppingDistance = stopDistance;
            agent.SetDestination(target.position);
            agent.speed = chaseSpeed;
        }
        else
        {
            agent.stoppingDistance = 0f;
            wanderTimer += Time.deltaTime;
            agent.speed = wanderSpeed;

            if (wanderTimer >= wanderInterval || agent.remainingDistance <= 0.5f)
            {
                Vector3 wanderPoint = GetRandomPointNearSpawn();
                agent.SetDestination(wanderPoint);
                wanderTimer = 0f;
            }
        }

        if(distance <= attackRange)
        {
            animator.SetTrigger("Attack");
            if (Time.time >= lastAttackTime + 1f / attackRate)
            {
                target.GetComponent<PlayerHealth>()?.TakeDamage(attackDamage);
                lastAttackTime = Time.time;
            }       
        }

        animator.speed = agent.speed / 4;
    }

    Vector3 GetRandomPointNearSpawn()
    {
        Vector3 randomDirection = Random.insideUnitSphere * wanderRadius;
        randomDirection += spawnPosition;

        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomDirection, out hit, wanderRadius, NavMesh.AllAreas))
        {
            return hit.position;
        }

        return spawnPosition;
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        healthBar.value = currentHealth;
        if(currentHealth <= 0)
        {
            Die();
        }
        else
        {
            audioSource.pitch = Random.Range(0.5f, 1.5f);
            audioSource.clip = HitSound;
            audioSource.Play();
        }
    }

    void Die()
    {
        audioSource.pitch = Random.Range(0.5f, 1.5f);
        audioSource.clip = deathSoundClip;
        audioSource.Play();
        agent.isStopped = true;
        GameObject deathEffectObj = Instantiate(deathEffect, transform.position, transform.rotation);
        deathEffectObj.GetComponentInChildren<Rigidbody>().AddForce(new Vector3(Random.Range(-20, 20), Random.Range(-20, 20), Random.Range(-20, 20)), ForceMode.Impulse);
        Destroy(gameObject);
    }
}