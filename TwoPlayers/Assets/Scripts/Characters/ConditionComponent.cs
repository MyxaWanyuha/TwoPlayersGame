using UnityEngine;

public class ConditionComponent : MonoBehaviour
{
    [SerializeField]
    int maxHealth = 3;
    int health;

    bool isDead = false;

    private void Start()
    {
        health = maxHealth;
    }

    public void TakeDamage(int damage)
    {
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
