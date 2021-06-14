using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PlayerMovement : MonoBehaviour
{
    private float speed;
    public float gravity = -9.8f;
    public float groundDistance = 0.4f;

    public Transform groundCheck;

    public LayerMask layer;

    [SerializeField] float[] speedArray;
    [SerializeField] float[] questionBlockWaitTime;
    [SerializeField] bool isPlayer = false;
    [SerializeField] CinemachineVirtualCamera stockCam;
    [SerializeField] CinemachineVirtualCamera buyStockCam;
    [SerializeField] CinemachineVirtualCamera sellStockCam;
    [SerializeField] GameObject[] stockMarketCanvas;
    [SerializeField] StairCreator stairCreator;
    [SerializeField] int[] botBuySell;

    [SerializeField] FlagMovement flagMovement;
    [SerializeField] Animator[] animators;
    [SerializeField] GameObject skatePlayer;
    private CharacterController controller;
    private Animator anime;
    private StairCreator sTC;
    private float yAxis, zAxis;

    private Vector3 velocity;
    private bool isGrounded = false;
    private bool _move = true;
    private int speedIdx = 0;
    private int botBuySellIdx = 0;
    private int questionBlockWaitTimeIdx = 0;
    private int stockMarketCanvasIdx = 0;

    private int stockStat = 0;
    float timer = 0f;
    
    // Start is called before the first frame update
    void Start()
    {
        controller = transform.GetComponent<CharacterController>();
        anime = transform.GetChild(0).GetComponent<Animator>();
        sTC = GameObject.FindGameObjectWithTag("StairCreator").GetComponent<StairCreator>();
        speed = speedArray[speedIdx];
        timer = 0f;
       
            anime.SetBool("run", true);
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, layer);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = 0f;
        }

        if(_move)
          move();
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

       /* if (isPlayer)
        {

            if (Input.GetKeyDown("b"))
            {
                stairCreator.StairSpawn(false);
                stairCreator.BuyStock();
            }

            else if (Input.GetKeyDown("s"))
            {
                stairCreator.SellStock();
            }
        }*/

    }

    private void OnTriggerEnter(Collider collider)
    {
        _move = false;
        // Debug.Log(timer);
        if (!collider.gameObject.CompareTag("Finish"))
        {
            _move = false;

            //stairCreator.StairSpawn(false);


            if (isPlayer)
            {
                anime.SetBool("up", false);
                anime.SetBool("run", false);
                anime.SetBool("down", false);
                if (stockStat == 0)
                {
                    stockCam.Priority = 20;

                }

                else if (stockStat == 1)
                {
                    buyStockCam.Priority = 20;
                }
                else
                {
                    sellStockCam.Priority = 20;
                }

                StartCoroutine(ActivateStockMarketCanvas(true, 1f));

            }

            else
            {
                anime.SetBool("up", false);
                anime.SetBool("run", false);
                anime.SetBool("down", false);
                stairCreator.BotBuySell(botBuySell[botBuySellIdx]);
                botBuySellIdx++;
                StartCoroutine(BotMoveAgain());

            }

            speedIdx++;
            if (speedIdx < 4)
                speed = speedArray[speedIdx];
        }
        // controller.Move(Vector3.zero);
    }

    private IEnumerator BotMoveAgain()
    {
        yield return new WaitForSeconds(questionBlockWaitTime[questionBlockWaitTimeIdx]);
        questionBlockWaitTimeIdx++;
        Punch();
        yield return new WaitForSeconds(0.1f);
        stairCreator.BotDestroyQuestionBlock();
        yield return new WaitForSeconds(1f);
        timer = 0;
        flagMovement.Move();
        stairCreator.StairSpawn(true);

        SetAnim();
        _move = true;

        

    }

    void move()
    {
       // anime.SetBool("run", true);
        controller.Move(transform.forward * speed * Time.deltaTime);
    }

   

    public void SetPriority(int priority)
    {
        if(stockStat == 0)
            stockCam.Priority = priority;

        // Invoke("ActivateStockMarketCanvas", 0.5f);


        else if (stockStat == 1)
             buyStockCam.Priority = priority;

         else
             sellStockCam.Priority = priority;

        StartCoroutine(ActivateStockMarketCanvas(false, 0f));
    }

    private IEnumerator ActivateStockMarketCanvas(bool val, float time)
    {
        yield return new WaitForSeconds(time);
        stockMarketCanvas[stockMarketCanvasIdx].SetActive(val);
        if (val)
        {
            foreach (Animator animator in animators)
            {
                animator.SetTrigger("PopUp");
            }
        }

        else
            stockMarketCanvasIdx++;
    }

    public void SetStockStat(int num)
    {
        stockStat = num;
    }

    public void SetMove(bool val)
    {
        timer = 0;
        flagMovement.Move();
        SetAnim();
        _move = val;
        
    }

    private void SetAnim()
    {
        if (!isPlayer)
        {
            if (stockStat == 0)
            {
                anime.SetBool("up", false);
                anime.SetBool("down", false);
                anime.SetBool("run", true);

            }

            else if (stockStat == 1)
            {
                anime.SetBool("up", false);
                anime.SetBool("run", false);
                anime.SetBool("down", true);
            }

            else
            {
                anime.SetBool("down", false);
                anime.SetBool("run", false);
                anime.SetBool("up", true);
            }
        }

        else
        {
            if (stockStat == 0)
            {
                anime.SetBool("up", false);
                anime.SetBool("down", false);
                anime.SetBool("run", true);

            }

            else if (stockStat == -1)
            {
                anime.SetBool("up", false);
                anime.SetBool("run", false);
                anime.SetBool("down", true);
            }

            else
            {
                anime.SetBool("down", false);
                anime.SetBool("run", false);
                anime.SetBool("up", true);
            }
        }
    }

    public void Punch()
    {
        anime.SetTrigger("punch");
    }


}
