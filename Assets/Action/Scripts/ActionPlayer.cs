using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Cinemachine.DocumentationSortingAttribute;

public class ActionPlayer : MonoBehaviour
{
 //   public GameObject cloud;

    [SerializeField] Transform deadZone;
    [SerializeField] Transform lifeZone;
    public KeyCode leftKey;
    public KeyCode rightKey;

    public float xAccel;
    public float gravity;
    public float bounceVel;
    public float spawnVel;
    public float gameStartCountDown = 30f;

    bool goLeft;
    bool goRight;
    bool bounce = true;
    bool startCountDown;
    bool killed; 

    Rigidbody2D myBody;

    private void Start()
    {
        myBody = GetComponent<Rigidbody2D>();
        startCountDown = true;
    }

    private void Update()
    {
        if (!startCountDown)
        {
            #region move input
            if (Input.GetKey(leftKey))
            {
                goLeft = true;
            }
            else { goLeft = false; }
            if (Input.GetKey(rightKey))
            {
                goRight = true;
            }
            else { goRight = false; }
            #endregion
        }
    }

    private void FixedUpdate()
    {
        float playerY = transform.position.y;
        if (!startCountDown)
        {
            #region movement
            Vector3 newVel = myBody.velocity;

            newVel.x *= 0.9f;
            newVel.y += gravity;

            if (bounce)
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

        if (startCountDown)
        {
           // Vector3 newVel = myBody.velocity;
           // newVel.y += spawnVel;
            gameStartCountDown--;
           //myBody.velocity = newVel;
            if (gameStartCountDown <= 0)
            {
                startCountDown = false;
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!startCountDown) 
        { 
            if (collision.gameObject.tag == "Cloud")
            {
                if (collision.gameObject.transform.position.y < this.transform.position.y) { bounce = true; }
                if (collision.gameObject.transform.position.y >= this.transform.position.y) { Physics.IgnoreCollision(collision.gameObject.GetComponent<Collider>(), GetComponent<Collider>()); }
            }
        }
        
        if (collision.gameObject.tag == "Player")
        {
            if (collision.gameObject.transform.position.y > this.transform.position.y)
            {
                killed = true;
            }
        }
    }
}
