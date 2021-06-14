using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class coin : MonoBehaviour
{
    public GameObject Points;
    public GameObject Player;
    private void OnTriggerEnter(Collider other)
    {
       // Debug.Log("fsdfsdf");
        gameObject.SetActive(false);
        GameObject.Instantiate(Points, Points.transform.position, Points.transform.rotation);
        GameObject.Instantiate(Points, Player.transform);
        
    }
}
