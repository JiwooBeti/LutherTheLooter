using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultEnemy : MonoBehaviour
{
    [SerializeField] List<Transform> waypoints;
    [SerializeField] int index = 0;
    [SerializeField] float speed;
    [SerializeField] float maxRotation;
    [SerializeField] float smallestRotation=.5f;
    [SerializeField] float proximity;
    Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 newLoc = Vector2.MoveTowards(transition.position, waypoints[index].position, .3f);
        transform.position = newLoc;

        if (MathF.Abs(transform.position.x - waypoints[index].position.x) < .01f && Mathf.Abs(transform.position.y- waypoints[index].position.y)< .01f)
        {
            index = (index + 1) % waypoints.Count;
        }
    }
}
