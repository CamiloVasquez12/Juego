using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Movement : MonoBehaviour
{
    public float speed;
    private bool isMoving;
    public Animator animator;

    private Vector3 direction;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (!isMoving)
        {

            float horizontal = Input.GetAxisRaw("Horizontal");
            float vertical = Input.GetAxisRaw("Vertical");

            direction = new Vector3(horizontal, vertical);

            AnimateMovement(direction);

            
        }
    }

    private void FixedUpdate()
    {
        this.transform.position += direction.normalized * speed * Time.deltaTime;
    }

    void AnimateMovement(Vector3 direction)
    {
        if(animator != null)
        {
            if (animator.runtimeAnimatorController != null)
            {
                if (direction.magnitude > 0)
                {
                    animator.SetBool("isMoving", true);
                    animator.SetFloat("horizontal", direction.x);
                    animator.SetFloat("vertical", direction.y);
                }
                else
                {
                    animator.SetBool("isMoving", false);
                }
            }                
        }
    }

}
