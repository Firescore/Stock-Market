using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StairCreator : MonoBehaviour
{
    public GameObject stairPieces;
    public bool PlacingUP = false;
    [SerializeField] bool placingNormal = true;
    [SerializeField] PlayerMovement playerMovement;
    [SerializeField] GameObject initialQuestionBlock;
    [SerializeField] GameObject[] objectives;

    bool isInitialQuestionBlockActive = true;

    public float yAxis, zAxis;
    public float spwam;
    public float spwanSpeed;

    //[SerializeField] bool isPlayer = false;
    [SerializeField] GameObject[] questionBlockPrefab;
    //[SerializeField] StairCreator stairCreator;

    [SerializeField] float[] blockDistances;
    float blockDistance;
    int blockDistanceIdx;
    bool createStair = true;
    float distance;
    GameObject questionBlock;
    private int questionBlockCount = 1;
    private int questionBlockPrefabIdx = 0;
    private int objectiveIndex = 0;

    int finalBuySellCount = 1;
    // Start is called before the first frame update
    void Start()
    {
        distance = 0f;
        blockDistanceIdx = 0;
        blockDistance = blockDistances[blockDistanceIdx];
    }

    // Update is called once per frame
    void Update()
    {

        if (distance >= blockDistance && questionBlockCount <= 4)
        {
            if (!PlacingUP && !placingNormal && !isInitialQuestionBlockActive)
                questionBlock = Instantiate(questionBlockPrefab[questionBlockPrefabIdx], new Vector3(transform.position.x - 0.039f, yAxis + 0.08f - 0.08f, zAxis),
                    questionBlockPrefab[questionBlockPrefabIdx].transform.rotation);

            else if (PlacingUP && !placingNormal && !isInitialQuestionBlockActive)
                questionBlock = Instantiate(questionBlockPrefab[questionBlockPrefabIdx], new Vector3(transform.position.x - 0.039f, yAxis + 0.32f, zAxis),
                   questionBlockPrefab[questionBlockPrefabIdx].transform.rotation);

            else if(!isInitialQuestionBlockActive)
                questionBlock = Instantiate(questionBlockPrefab[questionBlockPrefabIdx], new Vector3(transform.position.x - 0.039f, yAxis + 0.18f - 0.08f, zAxis),
                    questionBlockPrefab[questionBlockPrefabIdx].transform.rotation);

            questionBlockCount++;
            questionBlockPrefabIdx++;
            //questionBlock.LeanRotateY(90, 0);
            //  questionBlock.transform.rotation = 
            //  Debug.Log(questionBlock.transform.rotation.ToEulerAngles());
            distance = 0;
            blockDistanceIdx++;

            if (blockDistanceIdx < 4)
                blockDistance = blockDistances[blockDistanceIdx];

            StairSpawn(false);

        }

        else if (distance >= blockDistance && questionBlockCount > 4)
        {
            createStair = false;
        }

        spwam -= Time.deltaTime;
        spwnPieces();

    }

    private GameObject pieces;

    void spwnPieces()
    {
        if (spwam <= 0 && createStair)
        {
            if (questionBlockCount > 1)
            {
                pieces = Instantiate(stairPieces, new Vector3(transform.position.x, yAxis, zAxis), Quaternion.identity);
                pieces.transform.parent = transform;
            }
            zAxis += 0.12f;

            distance += 0.12f;
            //distance += transform.position.z;

            if (!PlacingUP && !placingNormal)
                yAxis += 0.1f;

            else if (PlacingUP && !placingNormal)
                yAxis -= 0.1f;

            else
                yAxis += 0;

            spwam = spwanSpeed;     //dont touch it
        }

    }

    public void StairSpawn(bool val)
    {
        createStair = val;
    }

    public void AvoidBitCoin()
    {
        StartCoroutine(Avoid());
    }

    public void BuyStock()
    {
        // Debug.Log("done");
        StartCoroutine(Buy());
    }

    public void SellStock()
    {
        //Debug.Log("done");
        StartCoroutine(Sell());
    }

    private IEnumerator Avoid()
    {
        yield return new WaitForSeconds(1.2f);
        playerMovement.SetPriority(1);
        playerMovement.SetStockStat(0);

        StartCoroutine(DestroyQuestionBlock());
        yield return new WaitForSeconds(1.2f);
        PlacingUP = false;
        placingNormal = true;
        StairSpawn(true);
        playerMovement.SetMove(true);
    }

    private IEnumerator Buy()
    {
        yield return new WaitForSeconds(1.2f);
        playerMovement.SetPriority(1);
        playerMovement.SetStockStat(1);
        
        StartCoroutine(DestroyQuestionBlock());
        yield return new WaitForSeconds(1.2f);
        PlacingUP = false;
        placingNormal = false;
        StairSpawn(true);
        playerMovement.SetMove(true);
    }


    private IEnumerator Sell()
    {
        yield return new WaitForSeconds(1.2f);
        playerMovement.SetPriority(1);
        playerMovement.SetStockStat(-1);
       
        StartCoroutine(DestroyQuestionBlock());
                 
        yield return new WaitForSeconds(1.2f);
        PlacingUP = true;
        placingNormal = false;
        StairSpawn(true);
        playerMovement.SetMove(true);
    }
    /*
    public void FinalBuySell()//, GameObject nextObjective)
    {
        objectiveIndex++;
        StartCoroutine(LastBuySell(objectives[objectiveIndex-1], objectives[objectiveIndex]));//, nextObjective));
        
    }

    private IEnumerator LastBuySell(GameObject currObjective, GameObject nextObjective)
    {
       // if(finalBuySellCount == 1)
        yield return new WaitForSeconds(1.2f);

       

        currObjective.GetComponent<Animator>().SetTrigger("PopIn");
        GameObject level = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.transform.parent.gameObject;
        level.SetActive(false);

        yield return new WaitForSeconds(0.3f);

        currObjective.SetActive(false);

        //if(finalBuySellCount != 4)
        nextObjective.SetActive(true);
        nextObjective.GetComponent<Animator>().SetTrigger("PopUp");


    }*/
    
    public void FinalSell(GameObject curr)
    {
        StartCoroutine(LastSell(curr));
    }

    private IEnumerator LastSell(GameObject curr)
    {
        yield return new WaitForSeconds(1.2f);
        
        playerMovement.SetPriority(1);
        playerMovement.SetStockStat(1);

        StartCoroutine(DestroyQuestionBlock());
        yield return new WaitForSeconds(1.2f);
        PlacingUP = false;
        placingNormal = false;
        StairSpawn(true);
        playerMovement.SetMove(true);

    }

    public void ChangeGreen(Image image)
    {
        Color color1 = Color.white;
        Color color2 = Color.grey;
        StartCoroutine(ChangeColor(color1, color2, image));
    }

    public void ChangeOrange(Image image)
    {
        Color color1 = Color.white;
        Color color2 = Color.grey;
        StartCoroutine(ChangeColor(color1, color2, image));
    }

    IEnumerator ChangeColor(Color color1, Color color2, Image image)
    {
        

        image.color = color2;
        yield return new WaitForSeconds(0.24f);

        image.color = color1;
        yield return new WaitForSeconds(0.24f);

      
        image.color = color2;
        yield return new WaitForSeconds(0.24f);


        image.color = color1;
        yield return new WaitForSeconds(0.24f);

        image.color = color2;
        yield return new WaitForSeconds(0.24f);
        image.color = color1;
       

    }


    public void BotBuySell(int num)
    {
        switch(num)
        {
            case -1:
                PlacingUP = true;
                placingNormal = false;
                playerMovement.SetStockStat(1);
                break;
            case 0:
                placingNormal = true;
                playerMovement.SetStockStat(0);
                break;
            case 1:
                PlacingUP = false;
                placingNormal = false;
                playerMovement.SetStockStat(-1);
                break;

        }
    }

    public IEnumerator DestroyQuestionBlock()
    {
        playerMovement.Punch();
        if (isInitialQuestionBlockActive)
        {
            isInitialQuestionBlockActive = false;
            DestroyInitialQuestionBlock();
            yield return new WaitForSeconds(0.1f);

            

            GameObject fracuture = initialQuestionBlock.transform.GetChild(1).gameObject;
            initialQuestionBlock.GetComponent<MeshRenderer>().enabled = false;
            fracuture.SetActive(true);
            foreach (Transform child in fracuture.transform)
            {
                child.gameObject.AddComponent<Rigidbody>();
                // child.gameObject.GetComponent<Rigidbody>().AddForce(60f * Vector3.forward);
                child.gameObject.GetComponent<Rigidbody>().AddExplosionForce(100f, initialQuestionBlock.transform.position, 2f);
            }
            Destroy(initialQuestionBlock, 2f);
        }
        else
        {
          //  Destroy(initialQuestionBlock, 2f);
           // initialQuestionBlock.transform.GetChild(0).gameObject.SetActive(false);
            questionBlock.transform.GetChild(0).gameObject.SetActive(false);

            // int length = transform.GetChildCount();
            //GameObject img = questionBlock.transform.Find("Image").gameObject;
           // GameObject cube = questionBlock.transform.Find("Cube").gameObject;
            //cube.SetActive(false);
            //img.SetActive(false);

            yield return new WaitForSeconds(0.1f);

            GameObject fracuture = questionBlock.transform.GetChild(1).gameObject;
            questionBlock.GetComponent<MeshRenderer>().enabled = false;
            fracuture.SetActive(true);
            foreach (Transform child in fracuture.transform)
            {
                child.gameObject.AddComponent<Rigidbody>();
                // child.gameObject.GetComponent<Rigidbody>().AddForce(60f * Vector3.forward);
                child.gameObject.GetComponent<Rigidbody>().AddExplosionForce(100f, questionBlock.transform.position, 2f);
            }
            
            // questionBlock.GetComponent<Rigidbody>().AddExplosionForce(100f, questionBlock.transform.position, 4f);
            Destroy(questionBlock, 2f);
            //Destroy(questionBlock);

        }
    }

    public void BotDestroyQuestionBlock()
    {
        if (isInitialQuestionBlockActive)
        {
            isInitialQuestionBlockActive = false;
            DestroyInitialQuestionBlock();

            GameObject fracuture = initialQuestionBlock.transform.GetChild(1).gameObject;
            initialQuestionBlock.GetComponent<MeshRenderer>().enabled = false; ;
            fracuture.SetActive(true);
            foreach (Transform child in fracuture.transform)
            {
                child.gameObject.AddComponent<Rigidbody>();
                // child.gameObject.GetComponent<Rigidbody>().AddForce(60f * Vector3.forward);
                child.gameObject.GetComponent<Rigidbody>().AddExplosionForce(100f, initialQuestionBlock.transform.position, 2f);
            }
            Destroy(initialQuestionBlock, 2f);
        }


        // int length = transform.GetChildCount();
        else
        {
            questionBlock.transform.GetChild(0).gameObject.SetActive(false);

            //yield return new WaitForSeconds(0.1f);



            GameObject fracuture = questionBlock.transform.GetChild(1).gameObject;
            questionBlock.GetComponent<MeshRenderer>().enabled = false;
            fracuture.SetActive(true);
            foreach (Transform child in fracuture.transform)
            {
                child.gameObject.AddComponent<Rigidbody>();
                // child.gameObject.GetComponent<Rigidbody>().AddForce(60f * Vector3.forward);
                child.gameObject.GetComponent<Rigidbody>().AddExplosionForce(100f, questionBlock.transform.position, 2f);
            }
            Destroy(questionBlock, 2f);
        }
    }

    public void DestroyInitialQuestionBlock()
    {

        //Destroy(initialQuestionBlock, 2f);
        initialQuestionBlock.transform.GetChild(0).gameObject.SetActive(false);
       
    }
}
