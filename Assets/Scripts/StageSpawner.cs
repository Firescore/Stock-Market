using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageSpawner : MonoBehaviour
{
    public static int count = 0;

    public GameObject[] Stages;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Stages[count].SetActive(true);
            count++;
        }
    }
}
