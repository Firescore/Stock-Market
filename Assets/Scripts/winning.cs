using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class winning : MonoBehaviour
{
    public GameObject SkateBoard;
    public GameObject MainCAm;
    public CinemachineVirtualCamera WinCAm;
    public GameObject skate;
    public GameObject Strip;
    public Animator Player;
    public ParticleSystem[] confetti;
    public GameObject name;


    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            StartCoroutine(Win(other));
        }
    }

    IEnumerator Win(Collider other)
    {
        Strip.SetActive(false);
        //other.gameObject.transform.Find("Male").gameObject.GetComponent<Animator>().SetTrigger("jump");
        //other.gameObject.transform.Find("Male").gameObject.GetComponent<PlayerMovement>().FinalMove();

        // yield return new WaitForSeconds(0.6f);
        // SkateBoard.SetActive(true);
        // other.gameObject.SetActive(false);
        WinCAm.Priority = 100;
        //ConfettiPlay();
        // Lea
        GameObject gameObject = Player.gameObject.transform.parent.gameObject;
        gameObject.GetComponent<PlayerMovement>().SetMove(false);
        Player.SetBool("run", false);
        Player.SetBool("up", false);
        Player.SetBool("down", false);
        name.SetActive(false);
        LeanTween.rotateY(Player.gameObject, 180, 0.5f);
        yield return new WaitForSeconds(0.5f);
        
        
        Player.SetTrigger("dance");
        yield return new WaitForSeconds(0.8f);
        ConfettiPlay();

        //MainCAm.SetActive(false);

        //skate.SetActive(false);


    }

    public void ConfettiPlay()
    {
        foreach (ParticleSystem p in confetti)
        {
            p.Play();
        }
    }
}
