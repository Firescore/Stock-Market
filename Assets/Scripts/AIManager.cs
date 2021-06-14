using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIManager : MonoBehaviour
{
    public Transform[] position;
    public RectTransform[] IconPosition;
    public RectTransform icon;
    public Animator[] Door;
    int count;
    Animator animator;

    private void Awake()
    {
        count = 0;
        animator = gameObject.GetComponent<Animator>();
    }

    private void Start()
    {
        StartCoroutine(AIRun());
    }

    IEnumerator AIRun()
    {
        LeanTween.moveLocal(gameObject,new Vector3(gameObject.transform.position.x,gameObject.transform.position.y,position[count].position.z),3f);
        LeanTween.moveX(icon, IconPosition[count].anchoredPosition.x, 3f);
       
        yield return new WaitForSeconds(3f);
        animator.SetTrigger("idle");
         yield return new WaitForSeconds(Random.Range(3f, 7f));
        animator.SetTrigger("run");
        
        Door[count].SetTrigger("open");
        count++;
        yield return new WaitForSeconds(1f);
        LeanTween.moveLocal(gameObject, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, position[count].position.z), 3f);
        LeanTween.moveX(icon, IconPosition[count].anchoredPosition.x, 3f);
        yield return new WaitForSeconds(3f);
        animator.SetTrigger("idle");
        yield return new WaitForSeconds(Random.Range(3f, 7f));
        animator.SetTrigger("run");

        Door[count].SetTrigger("open");
        count++;
        yield return new WaitForSeconds(1f);
        LeanTween.moveLocal(gameObject, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, position[count].position.z), 3f);
        LeanTween.moveX(icon, IconPosition[count].anchoredPosition.x, 3f);
        yield return new WaitForSeconds(3f);
        animator.SetTrigger("idle");
        yield return new WaitForSeconds(Random.Range(3f, 7f));
        animator.SetTrigger("run");
        Door[count].SetTrigger("open");
        count++;
        yield return new WaitForSeconds(1f);
        LeanTween.moveLocal(gameObject, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, position[count].position.z), 3f);
        LeanTween.moveX(icon, IconPosition[count].anchoredPosition.x, 3f);
        yield return new WaitForSeconds(3f);
        animator.SetTrigger("idle");
        yield return new WaitForSeconds(Random.Range(3f, 7f));
        count++;
    }
}
