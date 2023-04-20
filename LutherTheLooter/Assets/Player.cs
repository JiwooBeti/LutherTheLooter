using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Animator animator;
    [SerializeField] bool left, right, space;
    [SerializeField] float speed;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.D))
        {
            right = true;
        }
        if(Input.GetKeyDown(KeyCode.A))
        {
            left = true;
        }
        if(Input.GetKeyUp(KeyCode.D))
        {
            right = false;
        }
        if(Input.GetKeyUp(KeyCode.A))
        {
            left = false;
        }
        if(Input.GetKeyDown(KeyCode.Space))
        {
            space = true;
        }
        if(Input.GetKeyUp(KeyCode.Space))
        {
            space = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(collision.gameObject.tag);
    }
    private void FixedUpdate()
    {
        animator.speed = 1;
        if(right)
        {
            if(!animator.GetBool("run"))
            {
                animator.speed = 0;
            }
            transform.position = new Vector2(transform.position.x + speed*Time.deltaTime, transform.position.y);
            animator.SetBool("run", true);
            GetComponent<SpriteRenderer>().flipX = false;
            
        }

        if (left)
        {
            if (!animator.GetBool("run"))
            {
                animator.speed = 0;
            }
            transform.position = new Vector2(transform.position.x - speed*Time.deltaTime, transform.position.y);
            animator.SetBool("run", true);
            GetComponent<SpriteRenderer>().flipX = true;
        }
        if(!right&&!left)
        {
            animator.SetBool("run", false);
        }
        if(space)
        {
            if (!animator.GetBool("bat"))
            {
                animator.speed = 0;
            }
            animator.SetBool("bat", true);
        } else
        {
            animator.SetBool("bat", false);
        }


        }
    }
