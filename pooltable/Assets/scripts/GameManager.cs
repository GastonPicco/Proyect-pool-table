using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public bool GameOn;
    public static GameManager data;
    public GameObject Stick,WhiteBall,ballIn;
    public Vector3 stickPosition,_Direction,_RealStickPos;
    public bool mainTurn;
    public int firstTurn;
    public bool firstBall;
    public bool Error;
    public GameObject[] balls;
    public float[] speeds,minSpeeds;
    public float ballspeeds;
    public LineRenderer lineRenderer;
    public GameObject Line;
    public Vector3 pos1, pos2;
    public bool nextDobleTurn = false , dobleTurn = false;
    public GameObject RedPanel, RedPanel2, BluePanel, BluePanel2;
    public GameObject firstBallOfTurn;
    public int tipeOfBall;
    public bool hit ,oneCanblack, twoCanblack,MOB;
    public int countFlat, countGrated;
    public GameObject Checker1, Checker2;
    public GameObject win1, win2;
    public bool shootCD;

    private void Awake()
    {
        if (data == null)
        {
            data = this;
        }
        else
        {

            if(data != this)
            {
                Destroy(gameObject);
            }
        }
    }
    void Start()
    {
        GameOn = true;
        countGrated = 7;
        countFlat = 7;
        balls = new GameObject[16];
        speeds = new float[16];
        firstTurn = Random.Range(0, 2);
        Debug.Log(firstTurn);
        if (firstTurn == 0)
        {
            mainTurn = true;
            Debug.Log(".");
            Debug.Log("|||||||||||||||||||||||||||||||||||||||||");
            Debug.Log("turno jugador 2 .43");
        }
        else
        {
            mainTurn = false;
            Debug.Log(".");
            Debug.Log("|||||||||||||||||||||||||||||||||||||||||");
            Debug.Log("turno jugador 1 . 50");
        }
        lineRenderer.positionCount = 2;
    }
    void Update()
    {
        _Direction = WhiteBall.transform.position - stickPosition;
        lineRenderer.SetPosition(0, pos1);
        lineRenderer.SetPosition(1, pos2);
        Debug.DrawLine(pos1,pos2);
     
    }
    private void FixedUpdate()
    {
        if (GameOn)
        {
            if (mainTurn == false)
            {
                if(tipeOfBall == 0 || tipeOfBall == 1)
                {
                    BluePanel.SetActive(true);
                    RedPanel.SetActive(false);
                    BluePanel2.SetActive(false);
                    RedPanel2.SetActive(false);
                }
                else if(tipeOfBall == 2)
                {
                    BluePanel.SetActive(false);
                    RedPanel.SetActive(false);
                    BluePanel2.SetActive(true);
                    RedPanel2.SetActive(false);
                }
            }
            else
            {
                if(tipeOfBall == 0 || tipeOfBall == 2)
                {
                    BluePanel.SetActive(false);
                    RedPanel.SetActive(true);
                    BluePanel2.SetActive(false);
                    RedPanel2.SetActive(false);
                }
                else if (tipeOfBall == 1)
                {
                    BluePanel.SetActive(false);
                    RedPanel.SetActive(false);
                    BluePanel2.SetActive(false);
                    RedPanel2.SetActive(true);
                }
                
            }
            HideStick();
        }
        
    }
    public void NextTurn()
    {
        if (GameOn)
        {
            if (mainTurn)
            {
                if (!hit)
                {
                    dobleTurn = false;
                    nextDobleTurn = true;
                }
                if (!dobleTurn)
                {
                    mainTurn = false;
                    Debug.Log(".");
                    Debug.Log("|||||||||||||||||||||||||||||||||||||||||");
                    Debug.Log("turno jugador 1");
                }
                else
                {
                    Debug.Log("OTRO TIRO!!!");
                    dobleTurn = false;
                }
                if (nextDobleTurn)
                {
                    nextDobleTurn = false;
                    dobleTurn = true;
                }

            }


            else if (!mainTurn)
            {
                if (!hit)
                {
                    dobleTurn = false;
                    nextDobleTurn = true;
                }
                if (!dobleTurn)
                {
                    mainTurn = true;
                    Debug.Log(".");
                    Debug.Log("|||||||||||||||||||||||||||||||||||||||||");
                    Debug.Log("turno jugador 2");
                }
                else
                {
                    Debug.Log("OTRO TIRO!!!");
                    dobleTurn = false;
                }
                if (nextDobleTurn)
                {
                    nextDobleTurn = false;
                    dobleTurn = true;
                }
            }
        }
    }
    public void AsignBall()
    {
        if (mainTurn)
        {
            if(ballIn.tag == "FlatBall")
            {
                Debug.Log(".");
                Debug.Log("|||||||||||||||||||||||||||||||||||||||||");
                Debug.Log("Al jugado 2 se le asigna las bolas lisas");
                tipeOfBall = 2;
            }
            else if(ballIn.tag == "GratedBall")
            {
                Debug.Log(".");
                Debug.Log("|||||||||||||||||||||||||||||||||||||||||");
                Debug.Log("al jugador 2 se le asignan las bolas rayadas");
                tipeOfBall = 1;
            }
        }
        else if (!mainTurn)
        {
            if (ballIn.tag == "FlatBall")
            {
                Debug.Log(".");
                Debug.Log("|||||||||||||||||||||||||||||||||||||||||");
                Debug.Log("Al jugado 1 se le asigna las bolas lisas");
                tipeOfBall = 1;
            }
            else if (ballIn.tag == "GratedBall")
            {
                Debug.Log(".");
                Debug.Log("|||||||||||||||||||||||||||||||||||||||||");
                Debug.Log("al jugador 1 se le asignan las bolas rayadas");
                tipeOfBall = 2;
            }
        }

    }
    void HideStick()
    {
        if (GameOn)
        {
            if (speeds[0] > 0 || speeds[1] > 0 || speeds[2] > 0 || speeds[3] > 0 || speeds[4] > 0 || speeds[5] > 0 || speeds[6] > 0 || speeds[7] > 0 || speeds[8] > 0 || speeds[9] > 0 || speeds[10] > 0 || speeds[11] > 0 || speeds[12] > 0 || speeds[13] > 0 || speeds[14] > 0 || speeds[15] > 0)
            {
                Stick.SetActive(false);
                Line.SetActive(false);
            }
            else
            {
                Stick.SetActive(true);
                Line.SetActive(true);

            }
        }
        
    }
    public void BallTipe()
    {
        if (GameOn)
        {
            if (firstBallOfTurn.tag == "FlatBall" && !mainTurn && tipeOfBall == 2)
            {
                dobleTurn = false;
                nextDobleTurn = true;
                Debug.Log("Esa bola no es tuya");
            }
            if (firstBallOfTurn.tag == "GratedBall" && !mainTurn && tipeOfBall == 1)
            {
                dobleTurn = false;
                nextDobleTurn = true;
                Debug.Log("Esa bola no es tuya");
            }
            if (firstBallOfTurn.tag == "FlatBall" && mainTurn && tipeOfBall == 1)
            {
                dobleTurn = false;
                nextDobleTurn = true;
                Debug.Log("Esa bola no es tuya");
            }
            if (firstBallOfTurn.tag == "GratedBall" && mainTurn && tipeOfBall == 2)
            {
                dobleTurn = false;
                nextDobleTurn = true;
                Debug.Log("Esa bola no es tuya");
            }
            if (firstBallOfTurn.tag == "BlackBall" && !oneCanblack && !mainTurn || firstBallOfTurn.tag == "BlackBall" && !twoCanblack && mainTurn)
            {
                dobleTurn = false;
                nextDobleTurn = true;
            }
        }
        
    }
    public void BallIn()
    {
        if (GameOn)
        {
            if (ballIn.tag == "FlatBall" && tipeOfBall == 1 && !mainTurn)
            {
                dobleTurn = true;
            }
            if (ballIn.tag == "GratedBall" && tipeOfBall == 2 && !mainTurn)
            {
                dobleTurn = true;
            }
            if (ballIn.tag == "FlatBall" && tipeOfBall == 2 && mainTurn)
            {
                dobleTurn = true;
            }
            if (ballIn.tag == "GratedBall" && tipeOfBall == 1 && mainTurn)
            {
                dobleTurn = true;
            }
        }    
    }

    public void ActiveBlackBall()
    {
        if (GameOn)
        {
            if (tipeOfBall == 1 && !balls[1].activeInHierarchy && !balls[3].activeInHierarchy && !balls[4].activeInHierarchy && !balls[5].activeInHierarchy && !balls[6].activeInHierarchy && !balls[2].activeInHierarchy && !balls[7].activeInHierarchy)
            {
                oneCanblack = true;
            }
            if (tipeOfBall == 2 && !balls[1].activeInHierarchy && !balls[3].activeInHierarchy && !balls[4].activeInHierarchy && !balls[5].activeInHierarchy && !balls[6].activeInHierarchy && !balls[2].activeInHierarchy && !balls[7].activeInHierarchy)
            {
                twoCanblack = true;
            }
            if (tipeOfBall == 2 && !balls[9].activeInHierarchy && !balls[10].activeInHierarchy && !balls[11].activeInHierarchy && !balls[12].activeInHierarchy && !balls[13].activeInHierarchy && !balls[14].activeInHierarchy && !balls[15].activeInHierarchy)
            {
                oneCanblack = true;
            }
            if (tipeOfBall == 1 && !balls[9].activeInHierarchy && !balls[10].activeInHierarchy && !balls[11].activeInHierarchy && !balls[12].activeInHierarchy && !balls[13].activeInHierarchy && !balls[14].activeInHierarchy && !balls[15].activeInHierarchy)
            {
                twoCanblack = true;
            }
        }
        
    }
    public void asignateHole(Vector3 HoleBallPos)
    {
        if (GameOn)
        {
            if (!mainTurn)
            {
                Instantiate(Checker1, HoleBallPos, Quaternion.identity);
            }
            else
            {
                Instantiate(Checker2, HoleBallPos, Quaternion.identity);
            }
        }
    }
    public void BlackBallIn(int evento)
    {
        if (GameOn)
        {
            if (evento == 0 && !mainTurn)
            {
                Debug.Log("Pierde jugador 1");
                GameOn = false;
                win2.SetActive(true);
            }
            else if (evento == 0 && mainTurn)
            {
                Debug.Log("Pierde jugado 2");
                GameOn = false;
                win1.SetActive(true);
            }
            else if (evento == 1)
            {
                Debug.Log("Jugador 1 gana");
                GameOn = false;
                win1.SetActive(true);
            }
            else if (evento == 2)
            {
                Debug.Log("Jugador 2 gana");
                GameOn = false;
                win2.SetActive(true);
            }
        }
        
    }
    public void MouseOnButton()
    {
        MOB = true;
    }
    public void MouseOutButton()
    {
        MOB = false;
    }

}
