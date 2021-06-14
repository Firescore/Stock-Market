using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    [SerializeField] GameObject player;
    private float offsetz;
    private float offsety;
    private Vector3 pos;

    void Start()
    {
        pos = transform.position;
        offsetz = transform.position.z - player.transform.position.z;
        offsety = transform.position.y - player.transform.position.y;
    }

    void Update()
    {
        pos.z = player.transform.position.z + offsetz;
        pos.y = player.transform.position.y + offsety;
        transform.position = pos;

    }
}
