using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Computer : MonoBehaviour
{
    [SerializeField] GameObject paper, lockedDoor, unlockedDoor;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public GameObject GetPaper()
    {
        return paper;
    }
    public void Unlock()
    {
        lockedDoor.SetActive(false);
        unlockedDoor.SetActive(true);
    }

}
