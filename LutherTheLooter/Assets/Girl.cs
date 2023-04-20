using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Girl : MonoBehaviour
{
    // Start is called before the first frame update

    private Rigidbody2D rb;
    private Animator anim;
    [SerializeField] float speed;
    [SerializeField] int direction;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        anim.SetBool("isRun", true);
    }

    // Update is called once per frame
    void Update()
    {
        //refAnim();
        
    }
    private void FixedUpdate()
    {
        GetComponent<Transform>().position=new Vector2(transform.position.x+speed*Time.deltaTime, transform.position.y);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        if(collision.gameObject.tag.Equals("Wall"))
        {
            Debug.Log("Hit wall");
            speed *= -1;
            direction *= -1;
            
            GetComponent<SpriteRenderer>().flipX = !GetComponent<SpriteRenderer>().flipX;
            transform.Rotate(new Vector3(0, direction*180, 0));

        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {


            if (collision.gameObject.tag.Equals("Wall"))
            {
                Debug.Log("Hit wall");
                speed *= -1;
                direction *= -1;

                GetComponent<SpriteRenderer>().flipX = !GetComponent<SpriteRenderer>().flipX;
                transform.Rotate(new Vector3(0, direction * 180, 0));

            }
        
    }
    void refAnim()
    {


        if(Input.GetKeyDown(KeyCode.X))
        {
            anim.SetTrigger("attack");
        } 
        if(Input.GetKeyDown(KeyCode.K))
        {
            anim.SetBool("isKickBoard", true);
        }
        if(Input.GetKeyDown(KeyCode.D))
        {
            anim.SetBool("isRun", true);
        }
        if(Input.GetKeyDown(KeyCode.Space))
        {
            anim.SetBool("isJump", true);
        }

        if (Input.GetKeyUp(KeyCode.X))
        {
            anim.ResetTrigger("attack");
        }
        if (Input.GetKeyUp(KeyCode.K))
        {
            anim.SetBool("isKickBoard", false);
        }
        if (Input.GetKeyUp(KeyCode.D))
        {
            anim.SetBool("isRun", false);
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            anim.SetBool("isJump", false);
        }
    }

}
