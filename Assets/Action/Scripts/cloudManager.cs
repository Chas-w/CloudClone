using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cloudManager : MonoBehaviour
{
    [SerializeField] float destroyTimerMax;
    //[SerializeField] GameObject cloudParent;
    float destroyTimer;

    bool destroy;
    // Start is called before the first frame update
    void Start()
    {
        destroyTimer = destroyTimerMax;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            destroy = true;
            //Destroy(this.gameObject);
        }
    }
 
    // Update is called once per frame
    void FixedUpdate()
    {
     if (destroy)
        {
            destroyTimer--;
        }   

     if (destroyTimer <= 0)
        {
            Destroy(this.gameObject);
        }
    }
}
