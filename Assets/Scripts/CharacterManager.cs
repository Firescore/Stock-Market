using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterManager : MonoBehaviour
{
    public GameObject Player;
    public GameObject Strip;
    public Transform[] Positons;
    public Transform TppCamPos;
    public Transform FailPos;
    public GameObject TPPCamera;
    public Animator[] Door;
    public Animator CorrectText;
    public Animator WrongText;

    public RectTransform[] IconPosition;
    public RectTransform icon;
    public ParticleSystem[] confetti;


    public static int count;

    private void Start()
    {
        count = 0;
        LeanTween.moveLocal(Player, Positons[count].position,3f);

        LeanTween.moveX(icon, IconPosition[count].anchoredPosition.x, 3f);
    }


    public void RightAnswer()
    {

        StartCoroutine(Right());
    }


    IEnumerator Right()
    {

        ConfettiPlay();
        CorrectText.SetTrigger("text");
        yield return new WaitForSeconds(1f);
        Door[count].SetTrigger("open");
        yield return new WaitForSeconds(1f);
        Strip.SetActive(true);
        Player.GetComponent<Animator>().SetTrigger("run");
        LeanTween.moveLocal(Player, Positons[++count].position, 3f);
        LeanTween.moveX(icon, IconPosition[count].anchoredPosition.x, 3f);
        LeanTween.moveLocal(TPPCamera, TppCamPos.position, 1f);
        LeanTween.rotateLocal(TPPCamera, TppCamPos.rotation.eulerAngles, 1f);
        TPPCamera.GetComponent<CameraFollow>().enabled = true;

    }

    public void ConfettiPlay()
    {
        foreach (ParticleSystem p in confetti)
        {
            p.Play();
        }
    }

  


    public void WrongAnswer()
    {

        StartCoroutine(Wrong());
    }


    IEnumerator Wrong()
    {
        WrongText.SetTrigger("text");
        yield return new WaitForSeconds(1f);
    }


    public void ChangeColor(Image image)
    {
        image.color = Color.gray;
    }


    public void ChangeGreen(Image image)
    {
        StartCoroutine(Green(image));
    }

    IEnumerator Green(Image image)
    {
        image.color = Color.green;
        yield return new WaitForSeconds(0.2f);
        image.color = Color.white;
        yield return new WaitForSeconds(0.2f);

        image.color = Color.green;
        yield return new WaitForSeconds(0.2f);
        image.color = Color.white;
        yield return new WaitForSeconds(0.2f);

        image.color = Color.green;
        yield return new WaitForSeconds(0.2f);
        image.color = Color.white;
        yield return new WaitForSeconds(0.2f);
        image.color = Color.green;
    }


    public void DisableGB(GameObject Panel)
    {
        StartCoroutine(disable(Panel));
    }

    IEnumerator disable(GameObject Panel)
    {
        yield return new WaitForSeconds(2f);
        Panel.SetActive(false);
    }
}
