using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StairCreator : MonoBehaviour
{
    public GameObject stairPieces;
    public bool PlacingUP = false;

    public float yAxis, zAxis;
    public float spwam;
    public float spwanSpeed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        spwam -= Time.deltaTime;
        spwnPieces();

    }
    private GameObject pieces;
    void spwnPieces()
    {
        if (spwam <= 0)
        {
            pieces = Instantiate(stairPieces, new Vector3(transform.position.x, yAxis, zAxis), Quaternion.identity);
            pieces.transform.parent = transform;

            zAxis += 0.12f;

            if (!PlacingUP)
                yAxis += 0.1f;

            else if (PlacingUP)
                yAxis -= 0.1f;

            spwam = spwanSpeed;
        }
        
    }
}
