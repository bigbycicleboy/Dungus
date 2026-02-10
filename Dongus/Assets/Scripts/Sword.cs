using UnityEngine;

public class SwordAttack : MonoBehaviour
{
    [Header("References")]
    public Animator swordAnimator;
    public AudioSource swordAudioSource;
    

    [Header("Attack Settings")]
    public float attackDuration = 0.3f;
    public float attackCooldown = 0.4f;
    public float attackDamage = 10f;

    bool isAttacking;

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !isAttacking)
        {
            Attack();
        }
    }

    void Attack()
    {
        isAttacking = true;

        swordAudioSource.Play();

        swordAnimator.SetBool("Attack", true);

        Invoke(nameof(ResetAttack), attackCooldown);
    }

    void ResetAttack()
    {
        swordAnimator.SetBool("Attack", false);
        isAttacking = false;
    }
}