using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cam : MonoBehaviour
{

    // Start is called before the first frame update
    //public Vector2 target;
    //public float smoothTime;
    //public Vector2 velocity = Vector3.zero;
    private Vector2 vel;
    public GameObject player;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {



    }

    private void FixedUpdate()
    {
        float destX = player.transform.position.x;
        float destY = player.transform.position.y;



        Vector2 newPos = Vector2.SmoothDamp(new Vector2(transform.position.x, transform.position.y), new Vector2(destX, destY), ref vel, 0.5f);

        //0 0
        //-56 30





        transform.position = new Vector3(newPos.x, newPos.y, transform.position.z);
    }
}
