using UnityEngine;

public class ConditionComponent : MonoBehaviour
{
    [SerializeField]
    int maxHealth = 3;
    int health;

    Animator animator;

    public bool isDead = false;

    public bool IsCanGetDamage { get; set; }

    private void Start()
    {
        health = maxHealth;
        animator = GetComponent<Animator>();
        IsCanGetDamage = true;
    }

    public bool IsTryGetDamage = false;
    public void TakeDamage(int damage)
    {
        if (IsCanGetDamage == false)
        {
            IsTryGetDamage = true;
            print("Cant get damage");
            return;
        }
        IsCanGetDamage = false;
        animator.Play("GetHit");
        health -= damage;
        if (health <= 0)
        {
            isDead = true;
            Dead();
        }
    }

    public void Healing()
    {
        if (health < maxHealth)
            ++health;
    }

    void Dead()
    {
        print("Dead");
    }
}
