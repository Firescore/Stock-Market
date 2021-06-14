using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlagMovement : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] Transform[] targetPos;
    [SerializeField] float[] speed;

    int targetPosIdx;
    int speedIdx;
    void Start()
    {
        targetPosIdx = 0;
        speedIdx = 0;
        Move();
    }

    // Update is called once per frame
    void Update()
    {
        
        
    }

    public void Move()
    {
        if (targetPosIdx < 4 && speedIdx < 4)
        {
            LeanTween.moveX(gameObject, targetPos[targetPosIdx].position.x, speed[speedIdx]);
            targetPosIdx++;
            speedIdx++;
        }
    }
}
