using UnityEngine;

public class AttackHitbox : MonoBehaviour
{
    public SwordAttack sword;

    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Enemy"))
        {
            Monster monster = other.GetComponent<Monster>();
            if (monster != null)
            {
                monster.TakeDamage(sword.attackDamage);
            }
        }
    }
}