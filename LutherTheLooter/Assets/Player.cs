using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Animator animator;
    [SerializeField] bool left, right, space, up, down;
    [SerializeField] float xSpeed, ySpeed;
    [SerializeField] bool touchingLadder;
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
        if(Input.GetKeyDown(KeyCode.W))
        {
            up = true;
        }
        if(Input.GetKeyDown(KeyCode.S))
        {
            down = true;
        }
        if (Input.GetKeyUp(KeyCode.W))
        {
            up = false;
        }
        if (Input.GetKeyUp(KeyCode.S))
        {
            down = false;
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "ladder")
        {
            touchingLadder = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "ladder")
        {
            touchingLadder = false;
        }
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
            transform.position = new Vector2(transform.position.x + xSpeed*Time.deltaTime, transform.position.y);
            animator.SetBool("run", true);
            GetComponent<SpriteRenderer>().flipX = false;
            
        }

        if (left)
        {
            if (!animator.GetBool("run"))
            {
                animator.speed = 0;
            }
            transform.position = new Vector2(transform.position.x - xSpeed*Time.deltaTime, transform.position.y);
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
        if(up && touchingLadder)
        {
            animator.SetBool("land", true);
            transform.position = new Vector2(transform.position.x, Mathf.Abs(ySpeed) * Time.deltaTime + transform.position.y);

        }
        else if(down&&touchingLadder)
        {
            animator.SetBool("land", true);
            transform.position = new Vector2(transform.position.x, -1*Mathf.Abs(ySpeed) * Time.deltaTime+ transform.position.y);

        }
        else
        {
            animator.SetBool("land", false);
        }


        }
    }
