using UnityEngine;

public class ConditionComponent : MonoBehaviour
{
    [SerializeField]
    int maxHealth = 3;
    [SerializeField] int health;
    public int Health => health;
    Animator animator;

    public bool isDead = false;
    public Vector3 spawnPoint;
    public void SetSpawnPoint(Vector3 sp)
    {
        spawnPoint = sp;
        print(spawnPoint.x.ToString());
    }

    public bool IsCanGetDamage { get; set; }

    private void Start()
    {
        spawnPoint = this.transform.localPosition;
        health = maxHealth;
        animator = GetComponent<Animator>();
        IsCanGetDamage = true;
        SpawnTrigger.setSpawnPoint += SetSpawnPoint;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P)) 
        {
            TakeDamage(3);
        }
           
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
        if (animator !=null)
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
        
        if(gameObject.CompareTag("Enemy"))
        {
            GameController.GetInstance().AddPoints(10);
            animator.Play("Die");
            var info = animator.GetCurrentAnimatorStateInfo(0);
            Destroy(gameObject, info.length);
            return;
        }
        foreach (var player in GameController.GetInstance().players)
        {
            player.Respawn();
        }
    }

    private void Respawn()
    {
        //добавить обновление хп и тд
        Healing(maxHealth);
        transform.position = spawnPoint;
    }
}
