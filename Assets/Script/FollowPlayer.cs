using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    [SerializeField] Transform followGameObject;

    float deltaY;
    // Start is called before the first frame update
    void Start()
    {
        deltaY = transform.position.y - followGameObject.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(transform.position.x,
            followGameObject.position.y,
            followGameObject.position.z);
    }
}
