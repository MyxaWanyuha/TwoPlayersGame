using UnityEngine;

public class AttackComponent : MonoBehaviour
{
    [SerializeField] Collider[] colliders;
    [SerializeField] int damage = 1;
    public bool IsAttack { get; private set; }
    Animator animator;

    MovementComponent movement;

    private void Start()
    {
        animator = GetComponent<Animator>();
        movement = GetComponent<MovementComponent>();
        StopAttack();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            other.GetComponent<ConditionComponent>().TakeDamage(damage);
        }
    }

    public void StartAttack()
    {
        if (movement.IsGrounded == false) return;
        IsAttack = true;
        movement.IsCanMovingJumping = false;
        foreach (var e in colliders)
        {
            e.enabled = true;
            animator.Play("Attack01");
        }
    }

    public void StopAttack()
    {
        IsAttack = false;
        movement.IsCanMovingJumping = true;
        foreach (var e in colliders)
        {
            e.enabled = false;
        }
    }
}
