using UnityEngine;

public class ConditionComponent : MonoBehaviour
{
    [SerializeField]
    int maxHealth = 3;
    int health;
    Rigidbody rb;
    Animator animator;
    bool isDead = false;
    public bool IsCanGetDamage { get; set; }

    private void Start()
    {
        health = maxHealth;
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        IsCanGetDamage = true;
    }

    public void TakeDamage(int damage)
    {
        if (IsCanGetDamage == false)
        {
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

    public void MoveUp()
    {
            rb.AddForce(transform.up * 8, ForceMode.VelocityChange);
            rb.AddForce(-transform.forward * 50, ForceMode.VelocityChange);
    }

    void Dead()
    {
        print("Dead");
    }
}
