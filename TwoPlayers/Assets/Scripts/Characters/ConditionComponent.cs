using UnityEngine;

public class ConditionComponent : MonoBehaviour
{
    [SerializeField]
    int maxHealth = 3;
    int health;
    public int Health => health;
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
            health = 0;
            isDead = true;
            Dead();
        
        }
    }

    public bool Healing(int heal = 1)
    {
        var oldHealth = health;
        heal += health;
        heal = Mathf.Min(heal, maxHealth);
        health = heal;
        return oldHealth != health;
    }

    void Dead()
    {
        print("Dead");
        if(gameObject.CompareTag("Enemy"))
        {
            GameController.GetInstance().AddPoints(10);
        }
    }
}
