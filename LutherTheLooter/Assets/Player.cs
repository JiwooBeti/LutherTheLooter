using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    Animator animator;
    [SerializeField] bool left, right, space, up, down, escalatorButton, elevatorButton, paperButton, computerButton;
    [SerializeField] float xSpeed, ySpeed;
    [SerializeField] bool touchingLadder;
    [SerializeField] GameObject touchedDoor;
    [SerializeField] bool upKey;
    [SerializeField] GameObject touchedEscalator = null, touchedElevator = null, touchedComputer = null, touchedFurniture=null;
    [SerializeField] bool onElevator = false;
    [SerializeField] bool onEscalator = false;
    [SerializeField] Text text, scoreText;
    [SerializeField] GameObject bigPaper;
    [SerializeField] bool oneDown, twoDown, threeDown, fourDown, fiveDown, sixDown, sevenDown, eightDown, nineDown, zeroDown;
    [SerializeField] string inputCode = "";
    [SerializeField] bool touchingCPU;
    [SerializeField] bool touchingLocked;
    [SerializeField] int score = 0;
    [SerializeField] int id = -1;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        touchedDoor = null;
        text.text = "";
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
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            upKey = true;
        }
        if (Input.GetKeyUp(KeyCode.UpArrow))
        {
            upKey = false;
        }
        if(Input.GetKeyDown(KeyCode.Q))
        {
                escalatorButton = true;
        }
        if (Input.GetKeyUp(KeyCode.Q))
        {
            escalatorButton = false;
        }
        if(Input.GetKeyDown(KeyCode.E))
        {
            elevatorButton = true;
        }
        if(Input.GetKeyUp(KeyCode.E))
        {
            elevatorButton = false;
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            paperButton = true;
        }
        if (Input.GetKeyUp(KeyCode.P))
        {
            paperButton = false;
        }
        if(Input.GetKeyDown(KeyCode.C))
        {
            computerButton = true;
        }
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            computerButton = false;
        }
        if (touchingCPU && computerButton)
        {
            GatherNums();
        }

    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "ladder")
        {
            touchingLadder = true;
        }
        if (collision.gameObject.tag == "Door")
        {
            touchedDoor = collision.gameObject;
        }
        if(collision.gameObject.tag=="Escalator")
        {
            touchedEscalator = collision.gameObject;
        }
        if (collision.gameObject.tag == "Elevator")
        {
            touchedElevator = collision.gameObject;
        }
        if(collision.gameObject.tag=="Paper")
        {
            text.text="Door Code: "+collision.gameObject.GetComponent<Paper>().getText();
            //Debug.Log("New Text: " + text.text);
        }
        if(collision.gameObject.tag=="Computer")
        {
            touchedComputer= collision.gameObject;  
            touchingCPU = true;
            text.text = "Enter Door Code: ";
        }
        if(collision.gameObject.tag=="Locked")
        {
            touchingLocked = true;
        }
        if (collision.gameObject.tag == "Furniture")
        {
            id = collision.gameObject.GetComponent<Furniture>().id;
            touchedFurniture = collision.gameObject;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Locked")
        {
            touchingLocked = false;
        }
        if (collision.gameObject.tag == "ladder")
        {
            touchingLadder = false;

            float newY = transform.position.y;

            if (transform.position.y > -8.06)
            {
                newY = -7.46f;
            }
            else if (transform.position.y < -20.7)
            {
                newY = -21.92f;
            } else if (transform.position.y < -15.8)
            {
                newY = -17.32f;
            } else if (transform.position.y > -13.38)
            {
                newY = -12.35f;
            }
            transform.position = new Vector2(transform.position.x, newY);
        }
        if (collision.gameObject.tag == "Door")
        {
            touchedDoor = null;
        }
        if (collision.gameObject.tag == "Escalator")
        {
            touchedEscalator = null;
        }
        if (collision.gameObject.tag == "Elevator")
        {
            touchedElevator = null;
        }
        if (collision.gameObject.tag == "Paper")
        {
            text.text = "";
        }
        if (collision.gameObject.tag == "Computer")
        {
            touchedComputer = null;
            text.text = "";
            touchingCPU = false;
            inputCode = "";
        }

        if (collision.gameObject.tag == "Furniture"&&collision.gameObject.GetComponent<Furniture>().id==id)
        {
            id = -1;
            touchedFurniture = null;
        }



    }
    private void FixedUpdate()
    {
        //Debug.Log(transform.position.y);

        animator.speed = 1;
        if(upKey&&touchedFurniture!=null&&!touchedFurniture.GetComponent<Furniture>().used)
        {
            touchedFurniture.GetComponent<Furniture>().used = true;
            score += touchedFurniture.GetComponent<Furniture>().GetValue();
            scoreText.text = "Score: " + score;

        }

        if (touchingCPU&&computerButton)
        {
            bigPaper.SetActive(true);
            text.enabled = true;
            return;
        }
        else if(!text.text.Equals("")&&paperButton)
        {
            bigPaper.SetActive(true);
            text.enabled = true;
            return;
        } else
        {
            //Debug.Log("should not see papers");
            bigPaper.SetActive(false);
            text.enabled = false;
        }
        if (up && touchingLadder)
        {
            animator.SetBool("land", true);
            transform.position = new Vector2(transform.position.x, Mathf.Abs(ySpeed) * Time.deltaTime + transform.position.y);
            return;

        }
        else if (down && touchingLadder)
        {
            animator.SetBool("land", true);
            transform.position = new Vector2(transform.position.x, -1 * Mathf.Abs(ySpeed) * Time.deltaTime + transform.position.y);
            return;

        }
        else
        {
            animator.SetBool("land", false);
        }
        if (up&&touchedDoor!=null)
        {

            transform.position = new Vector2(touchedDoor.GetComponent<Door>().getDestX(), touchedDoor.GetComponent<Door>().getDestY());

        }
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
        

        if(escalatorButton&&touchedEscalator!=null&&!onEscalator&&!touchingLocked)
        {
            Debug.Log("Going to : " + touchedEscalator.GetComponent<Escalator>().getY());
            GetComponent<SpriteRenderer>().enabled = false;
            transform.position = new Vector2(transform.position.x, touchedEscalator.GetComponent<Escalator>().getY());
            Debug.Log("Should be at : " + transform.position.y);
            onEscalator = true;
            escalatorButton = false;
            Invoke("showPlayer", 0.5f);
        }

        if(elevatorButton&&touchedElevator!=null&&!onElevator)
        {
            GetComponent<SpriteRenderer>().enabled = false;
            transform.position = new Vector2(transform.position.x, touchedElevator.GetComponent<Escalator>().getY());
            onElevator = true;
            elevatorButton = false;
            Invoke("showPlayer", 1.5f) ;
        }
        


        }
        
    void showPlayer()
    {
        onElevator = false;
        onEscalator = false;
        GetComponent<SpriteRenderer>().enabled = true;
    }

    void GatherNums()
    {
        if(inputCode.Length>=4)
        {
            //call the computer to check if the two strings are equal
            string s = touchedComputer.GetComponent<Computer>().GetPaper().GetComponent<Paper>().getText();
            //if equal the computer runs the method to make a locked door unlocked
            Debug.Log(s + " : " + inputCode);
            if(inputCode.Equals(s))
            {
                touchedComputer.GetComponent<Computer>().Unlock();
                inputCode = "";
                touchingLocked = false;
            } else
            {
                inputCode = "";
            }
            return;
        }
        if(!oneDown&&Input.GetKeyDown(KeyCode.Alpha1)){oneDown = true;inputCode += 1; }
        if (!twoDown && Input.GetKeyDown(KeyCode.Alpha2)){twoDown = true;inputCode += 2; }
        if (!threeDown && Input.GetKeyDown(KeyCode.Alpha3)){threeDown = true; inputCode += 3; }
        if (!fourDown && Input.GetKeyDown(KeyCode.Alpha4)){fourDown = true; inputCode += 4; }
        if (!fiveDown && Input.GetKeyDown(KeyCode.Alpha5)){fiveDown = true; inputCode += 5; }
        if (!sixDown && Input.GetKeyDown(KeyCode.Alpha6)){sixDown = true; inputCode += 6; }
        if (!sevenDown && Input.GetKeyDown(KeyCode.Alpha7)){sevenDown = true; inputCode += 7; }
        if (!eightDown && Input.GetKeyDown(KeyCode.Alpha8)){eightDown = true; inputCode += 8; }
        if (!nineDown && Input.GetKeyDown(KeyCode.Alpha9)){nineDown = true; inputCode += 9; }
        if (!zeroDown && Input.GetKeyDown(KeyCode.Alpha0)){zeroDown = true; inputCode += 0; }
        if (Input.GetKeyUp(KeyCode.Alpha1)) { oneDown = false; }
        if (Input.GetKeyUp(KeyCode.Alpha2)) { twoDown = false; }
        if (Input.GetKeyUp(KeyCode.Alpha3)) { threeDown = false; }
        if (Input.GetKeyUp(KeyCode.Alpha4)) { fourDown = false; }
        if (Input.GetKeyUp(KeyCode.Alpha5)) { fiveDown = false; }
        if (Input.GetKeyUp(KeyCode.Alpha6)) { sixDown = false; }
        if (Input.GetKeyUp(KeyCode.Alpha7)) { sevenDown = false; }
        if (Input.GetKeyUp(KeyCode.Alpha8)) { eightDown = false; }
        if (Input.GetKeyUp(KeyCode.Alpha9)) { nineDown = false; }
        if (Input.GetKeyUp(KeyCode.Alpha0)) { zeroDown = false; }

        /*if (oneDown) { inputCode += 1; }
        if (twoDown) { inputCode += 2; }
        if (threeDown) { inputCode += 3; }
        if (fourDown) { inputCode += 4; }
        if (fiveDown) { inputCode += 5; }
        if (sixDown) { inputCode += 6; }
        if (sevenDown) { inputCode += 7; }
        if (eightDown) { inputCode += 8; }
        if (nineDown) { inputCode += 9; }
        if (zeroDown) { inputCode += 0; }*/
        text.text = "Enter Door Code: " + inputCode;


    }
}
