using UnityEngine;

public class ConditionComponent : MonoBehaviour
{
    [SerializeField]
    int maxHealth = 3;
    int health;
    Rigidbody rigidbody;

    bool isDead = false;

    private void Start()
    {
        health = maxHealth;
        rigidbody = GetComponent<Rigidbody>();
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
    public void MoveUp()
    {
            rigidbody.AddForce(transform.up * 8, ForceMode.VelocityChange);
            rigidbody.AddForce(-transform.forward * 50, ForceMode.VelocityChange);
    }
    void Dead()
    {
        print("Dead");
    }
}
