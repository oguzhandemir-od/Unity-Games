using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 1f;
    public ContactFilter2D movementFilter;
    public Animator animator;


    private Vector2 movementInput;
    private Rigidbody2D rb;
    private List<RaycastHit2D> castCollisions = new List<RaycastHit2D>();

    private SpriteRenderer spriteRenderer;
    public SwordAttack swordAttack;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = rb.GetComponent<SpriteRenderer>();
    }


    private void Update()
    {
        Animate();
    }

    void FixedUpdate()
    {
        if (movementInput != Vector2.zero)
        {
            bool success = TryMove(movementInput);

            if (movementInput.x < 0)
            {
                spriteRenderer.flipX = true;
                swordAttack.attackDirection=SwordAttack.AttackDirection.left;
            }
            else if (movementInput.x > 0)
            {
                spriteRenderer.flipX = false;
                swordAttack.attackDirection=SwordAttack.AttackDirection.right;
            }
            else if(movementInput.y>0)
            {
                swordAttack.attackDirection=SwordAttack.AttackDirection.up;
            }
            else if(movementInput.y<0)
            {
                swordAttack.attackDirection=SwordAttack.AttackDirection.down;
            }

            if (!success)
            {
                success = TryMove(new Vector2(movementInput.x, 0));
            }
            if (!success)
            {
                success = TryMove(new Vector2(0, movementInput.y));
            }
        }
    }

    
    private bool TryMove(Vector2 direction)
    {
        // Hareket yönünde bir raycast yaparak çarpışmayı kontrol et
        int count = rb.Cast(
            direction,
            movementFilter,
            castCollisions,
            moveSpeed * Time.fixedDeltaTime
        );

        // Eğer herhangi bir çarpışma yoksa, karakteri hareket ettir
        if (count == 0)
        {
            Debug.Log("No Touch");
            rb.MovePosition(rb.position + direction * moveSpeed * Time.fixedDeltaTime);
            return true;
        }
        else
        {
            Debug.Log("Touch");
            return false;

        }
    }
    // Start Input System Funcs.
    void OnMove(InputValue movementValue)
    {
        movementInput = movementValue.Get<Vector2>();
    }
    // End Input System Funcs.

    void Animate()
    {
        if (movementInput != Vector2.zero)
        {
            animator.SetFloat("AnimX", movementInput.x);
            animator.SetFloat("AnimY", movementInput.y);
            animator.SetFloat("AnimMagnitude", movementInput.magnitude);

            // Flip
            if (movementInput.x < 0)
            {
                spriteRenderer.flipX = true;
            }
            else if (movementInput.x > 0)
            {
                spriteRenderer.flipX = false;
            }
        }
        else
        {
            animator.SetFloat("AnimMagnitude", 0);
        }
    }

    void OnAttack(InputValue value)
    {
        swordAttack.Attack();
        print("Vuruş");
        animator.SetTrigger("AttackTrigger");
    }
}
