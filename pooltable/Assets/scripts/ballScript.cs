using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ballScript : MonoBehaviour
{
    [SerializeField] private bool whiteball, flatColor, blackball; 
    private Rigidbody rb;
    [SerializeField] public float force = 1, ballspeed, timer;
    private Vector3 Direction, prevPos,RealStickPos;
    private GameObject PoolStick;
    private bool updateTurn, updateTurnTimer,ballCollConfirmed;
    private bool countspeed, asign = true, canCount;
    [SerializeField] int id;
    private bool hole1, hole2;
    [SerializeField] GameObject panel;
    
    
    void Start()
    {
        countspeed = true;
        rb = gameObject.GetComponent<Rigidbody>();
        PoolStick = GameManager.data.Stick;
        if (whiteball)
        {
           GameManager.data.WhiteBall = this.gameObject;
        }
        if (!whiteball)
        {
            canCount = true;
        }
        
    }
    void Update()
    {
        GameManager.data.speeds[id] = ballspeed;
        Holes();
        if (whiteball)
        { 
            StickAndShot();

            ForceControl();

            LineRenderer();

            TurnAlternator();      
        }
        if(asign == true)
        {
            GameManager.data.balls[id] = gameObject;
            asign = false;
        }
        

    }
    private void FixedUpdate()
    {
        if (countspeed)
        {
            FixBallSpeed();
        }       
    }
    void Holes()
    {
        if (transform.position.y < -0.0125f)
        {
            GameManager.data.ballIn = this.gameObject;
            GameManager.data.BallIn();
            if (whiteball)
            {              
                transform.position = new Vector3(0, 0.0369f*10, -0.622f*10);
                rb.Sleep();
                GameManager.data.dobleTurn = false;
                GameManager.data.nextDobleTurn = true;               
                Debug.Log(".");
                Debug.Log("|||||||||||||||||||||||||||||||||||||||||");
                Debug.Log("el siguiente jugador tiene un turno extra");
            }
            else if(id == 8 && !hole1 && !hole2)
            {
                GameManager.data.BlackBallIn(0);
            }
            else if(id == 8 && hole1)
            {
                GameManager.data.BlackBallIn(1);
            }
            else if(id == 8 && hole2)
            {
                GameManager.data.BlackBallIn(2);
            }
            else
            {
                if(this.gameObject.tag == "FlatBall" && canCount)
                {
                    canCount = false;
                    GameManager.data.countFlat -= 1;
                    if(GameManager.data.countFlat == 0)
                    {
                        GameManager.data.asignateHole(gameObject.transform.position);
                    }
                }
                else if (this.gameObject.tag == "GratedBall" && canCount)
                {
                    canCount = false;
                    GameManager.data.countGrated -= 1;
                    if(GameManager.data.countGrated == 0)
                    {
                        GameManager.data.asignateHole(gameObject.transform.position);
                    }
                }
                
                if (!GameManager.data.firstBall)
                {
                    GameManager.data.firstBall = true;
                    GameManager.data.AsignBall();
                }
                gameObject.transform.localScale = transform.localScale - new Vector3(0.3f * Time.deltaTime, 0.3f * Time.deltaTime, 0.3f * Time.deltaTime);
                if (transform.localScale.x < 0)
                {
                    countspeed = false;
                    ballspeed = 0.0f;
                    GameManager.data.speeds[id] = ballspeed;
                    gameObject.SetActive(false);
                }
            }
            
        }
    }
    void StickAndShot()
    {
        GameManager.data._RealStickPos = RealStickPos;

        RealStickPos = (GameManager.data.stickPosition - gameObject.transform.position).normalized;

        if (Input.GetKeyDown(KeyCode.Mouse0) && PoolStick.activeInHierarchy && !GameManager.data.MOB && !panel.activeInHierarchy)
        {
            GameManager.data.hit = false;
            GameManager.data.ActiveBlackBall();
            rb.AddForce(Direction.normalized * force * 10000);
            updateTurnTimer = true;
            ballCollConfirmed = false;
        }
        Direction = GameManager.data._Direction; //shot direction get from *GameManager*
    }

    void ForceControl()
    {
        if (Input.GetAxis("Mouse ScrollWheel") > 0 && force < 10)
        {
            force = force + 0.5f;
            //Debug.Log(force);
        }
        if (Input.GetAxis("Mouse ScrollWheel") < 0 && force > 1)
        {
            force = force - 0.5f;
            //Debug.Log(force);
        }
    }

    void FixBallSpeed()
    {
            ballspeed = (prevPos - transform.position).magnitude;
            prevPos = transform.position;      
    }

    void LineRenderer()
    {
        GameManager.data.pos1 = new Vector3(transform.position.x, 0, transform.position.z);
        GameManager.data.pos2 = (new Vector3(Direction.x, 0, Direction.z).normalized / 1.5f * (force * (force * 0.8f)) + new Vector3(Direction.x, 0, Direction.z).normalized / 1) + new Vector3(transform.position.x, 0, transform.position.z);
    }

    void TurnAlternator()
    {
        if (updateTurn && PoolStick.activeInHierarchy)
        {
            updateTurn = false;
            Debug.Log(".");
            Debug.Log("|||||||||||||||||||||||||||||||||||||||||");
            Debug.Log("cambio de turno");
            GameManager.data.NextTurn();
        }
        if (updateTurnTimer)
        {
            timer += Time.deltaTime;
            if (timer > 0.6)
            {
                updateTurn = true;
                updateTurnTimer = false;
                timer = 0;
            }
        }
    }

  


    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == ("FlatBall")&&!ballCollConfirmed && whiteball)
        {
            GameManager.data.hit = true;
            Debug.Log(".");
            Debug.Log("|||||||||||||||||||||||||||||||||||||||||");
            Debug.Log("tocaste bola lisa");
            GameManager.data.firstBallOfTurn = collision.gameObject;
            ballCollConfirmed = true;
            GameManager.data.BallTipe();
        }
        else if (collision.gameObject.tag == ("GratedBall")&&!ballCollConfirmed && whiteball)
        {
            GameManager.data.hit = true;
            Debug.Log(".");
            Debug.Log("|||||||||||||||||||||||||||||||||||||||||");
            Debug.Log("tocaste bola Rayada");
            GameManager.data.firstBallOfTurn = collision.gameObject;
            ballCollConfirmed = true;
            GameManager.data.BallTipe();
        }
        else if (collision.gameObject.tag == ("BlackBall") && !ballCollConfirmed && whiteball)
        {
            GameManager.data.hit = true;
            Debug.Log(".");
            Debug.Log("|||||||||||||||||||||||||||||||||||||||||");
            Debug.Log("tocaste bola Negra");
            GameManager.data.firstBallOfTurn = collision.gameObject;
            ballCollConfirmed = true;
            GameManager.data.BallTipe();
            
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag == "Checker1" && id == 8)
        {
            hole1 = true;
        }
        if (other.gameObject.tag == "Chacker2" && id == 8)
        {
            hole2 = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "Checker1" && id == 8)
        {
            hole1 = false;
        }
        if(other.gameObject.tag == "Chacker2" && id == 8)
        {
            hole2 = false;
        }
    }
}
