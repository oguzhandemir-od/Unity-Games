using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordAttack : MonoBehaviour
{
    Collider2D swordCollider;
    public float damage = 10f;
    public AttackDirection attackDirection;
    Vector2 attackOffset;
    public float attackDuration = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        swordCollider = GetComponent<Collider2D>();
        attackOffset = transform.position;
        swordCollider.enabled = false;
    }

    public void Attack()
    {
        swordCollider.enabled = true;

        switch (attackDirection)
        {
            case AttackDirection.left:
                AttackLeft();
                break;
            case AttackDirection.right:
                AttackRight();
                break;
            case AttackDirection.up:
                AttackUp();
                break;
            case AttackDirection.down:
                AttackDown();
                break;
            default:
                break;

        }
        Invoke(nameof(StopAttack), attackDuration);

    }
    // Update is called once per frame
    void Update()
    {

    }

    public enum AttackDirection
    {
        left, right, up, down
    }

    public void AttackRight()
    {
        swordCollider.offset = new Vector2(0.1f, 0);
    }
    public void AttackLeft()
    {
        swordCollider.offset = new Vector2(-0.1f, 0);
    }
    public void AttackUp()
    {
        swordCollider.offset = new Vector2(0, 0.1f);
    }
    public void AttackDown()
    {
        swordCollider.offset = new Vector2(0, -0.1f);
    }

    public void StopAttack()
    {
        swordCollider.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            print("Düşmana temas var");
            EnemyController enemy=collision.GetComponent<EnemyController>();

            if(enemy!=null)
            {
                enemy.Health=enemy.Health-damage;
            }
        }
    }
}
