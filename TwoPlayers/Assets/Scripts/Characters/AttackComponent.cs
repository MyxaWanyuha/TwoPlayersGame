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

    [SerializeField] AudioClip[] clips;
    public void StartAttack()
    {
        if (movement.IsGrounded == false) return;
        IsAttack = true;
        movement.IsCanMovingJumping = false;
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Attack01")) return;
        
        animator.Play("Attack01");

        if (tag == "Player")
        {
            GameController.GetInstance().PlaySound(clips);
        }
        var e = colliders[0];
        Ray ray = new Ray(e.transform.position - 0.5f * transform.forward, transform.forward);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 1) && hit.collider.CompareTag("Enemy"))
        {
            hit.collider.GetComponent<ConditionComponent>().TakeDamage(damage);
        }
    }

    public void StopAttack()
    {
        IsAttack = false;
        movement.IsCanMovingJumping = true;
    }
}
