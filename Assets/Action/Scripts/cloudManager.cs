using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

public class cloudManager : MonoBehaviour
{
    [SerializeField] float destroyTimerMax;
    public float castDist = 2f;
    //[SerializeField] GameObject cloudParent;
    float destroyTimer;

    bool destroy;
    // Start is called before the first frame update
    void Start()
    {
        destroyTimer = destroyTimerMax;
    }

    private void Update()
    {
  
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, castDist); //holds the information from a raycast hit 

        //Debug.DrawRay(transform.position, Vector2.up * castDist, Color.blue);

        if (hit.collider != null && hit.transform.tag == "Player")
        {
            destroy = true;
        }

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
