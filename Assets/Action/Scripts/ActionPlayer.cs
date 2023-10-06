using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionPlayer : MonoBehaviour
{
    public KeyCode leftKey;
    public KeyCode rightKey;

    public float xAccel;
    public float gravity;
    public float bounceVel;

    bool goLeft;
    bool goRight;
    bool bounce = true; 
    
    Rigidbody2D myBody;

    private void Start()
    {
        myBody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        #region move input
        if (Input.GetKey(leftKey))
        {
            goLeft = true;
        } else { goLeft = false; }
        if (Input.GetKey(rightKey))
        {
            goRight = true;
        } else { goRight = false; }
        #endregion
    }

    private void FixedUpdate()
    {
        #region movement
        Vector3 newVel = myBody.velocity;

        newVel.x *= 0.9f;
        newVel.y += gravity;

        if(bounce)
        {
            newVel.y += bounceVel;
            bounce = false; 
        }

        if (goLeft)
        {
            newVel.x -= xAccel;
        }
        else if (goRight)
        {
            newVel.x += xAccel;
        }

        myBody.velocity = newVel;
        #endregion
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Cloud")
        {
            bounce = true;
        }
    }
}
