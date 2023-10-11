using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Cinemachine.DocumentationSortingAttribute;
using TMPro;

public class ActionPlayer : MonoBehaviour
{
 //   public GameObject cloud;

    //[SerializeField] Transform deadZone;
    [SerializeField] Transform lifeZone;
    [SerializeField] TextMeshProUGUI scoreOut; 
    public KeyCode leftKey;
    public KeyCode rightKey;

    public float xAccel;
    public float gravity;
    public float bounceVel;
    public float spawnVel;
    public float gameStartCountDown = 30f;
    public float castDist;

    float respawnCounterMax = 10f;
    float respawnCounter;
    float score; 

    bool goLeft;
    bool goRight;
    bool bounce = true;
    bool startCountDown;
    bool killed;
    bool stopCollisionsLeft;
    bool stopCollisionsRight;
    bool dead; 

    Rigidbody2D myBody;

    private void Start()
    {
        myBody = GetComponent<Rigidbody2D>();
        startCountDown = true;
        respawnCounter = respawnCounterMax;
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
            
            if (dead)
            {
                respawnCounter--; 
                if (respawnCounter <= 0)
                {
                    this.transform.position = lifeZone.transform.position;
                    killed = false;
                    score--; 
                    dead = false;
                }
            } else if (!dead) { respawnCounter = respawnCounterMax; }
        }
    }

    private void FixedUpdate()
    {
       
        scoreOut.text = score.ToString(); 

        float playerY = transform.position.y;
        Vector3 newVel = myBody.velocity;
        if (!startCountDown)
        {
            #region movement
            

            newVel.x *= 0.9f;
            newVel.y += gravity;

            if (bounce)
            {
                newVel.y += bounceVel;
                bounce = false;
            }

            if (goLeft && !stopCollisionsLeft)
            {
                newVel.x -= xAccel;
            }
            else if (goRight && !stopCollisionsRight)
            {
                newVel.x += xAccel;
            }

            myBody.velocity = newVel;
            #endregion
        }

        /*
        if (killed)
        {
            newVel.y--;
            myBody.velocity = newVel;
        }
        */
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
                //Debug.Log(killed);
            } else if (collision.gameObject.transform.position.y < this.transform.position.y)
            {
                score++; 
            }

        }

        if (collision.gameObject.tag == "BarrierLeft")
        {
            stopCollisionsLeft = true;
        }
        if (collision.gameObject.tag == "BarrierRight")
        {
            stopCollisionsRight = true;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "BarrierLeft")
        {
            stopCollisionsLeft = false;
        }
        if (collision.gameObject.tag == "BarrierRight")
        {
            stopCollisionsRight = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Respawn")
        {
            dead = true; 
        }
    }
}
