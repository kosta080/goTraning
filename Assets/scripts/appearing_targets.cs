using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class appearing_targets : MonoBehaviour
{
    public GameObject targetElement;
    public int targetCount = 10;
    public float targetsXspread = 70;
    public float targetsScale = 0.1f;
    public float appearInterval = 1f;
    public float targetLifespen = 3f;
    public TextMesh readyText;
    private int roundScore;
    public List<GameObject> Targets;
    Vector3 targetPlace;
    private int targetsOnScene = 0;

    private bool roundActive = false;

    void Start()
    {
        //StartCoroutine(CountDown());
        //EventManager.StartTraining += beginCountDown;
        EventManager.Reset5Tar += restartRound;
        EventManager.ResetMovingTar += stopRound;
        EventManager.PouseGame += stopRound;
    }
    private void beginCountDown()
    {
        StartCoroutine(CountDown());
    }

    IEnumerator CountDown()
    {
        string[] texts = new string[] { "ready?", "3", "2", "1", "shoot" };
        for (int i = 0; i < texts.Length; i++)
        {
            readyText.text = texts[i];
            yield return new WaitForSeconds(1f);
        }
        Debug.Log("shoot");
        roundActive = true;
        devlareTarget();
    }
    private void devlareTarget()
    {
        if (!roundActive) return;
        dropTarget(targetsOnScene, targetCount);
        if (targetsOnScene < targetCount-1)
        {
            targetsOnScene++;
            Invoke("devlareTarget", appearInterval);
        }
        else
        {
            targetsOnScene = 0;
            Invoke("calcRoundScore", 2f);
        }
    }

    private void dropTarget(int targetI, int targetsTotal)
    {
        float basePositionX = ((targetsXspread / targetCount) * targetI) - (targetsXspread / 2);
        targetPlace = new Vector3(basePositionX + Random.Range(-0.7f, 0.7f), Random.Range(-0.5f, 0.5f), targetI * -0.1f);
        //targetPlace = new Vector3(basePositionX , 0f, 0f);
        targetPlace += transform.position;

        GameObject newtarget;
        newtarget = Instantiate(targetElement, targetPlace, Quaternion.identity);
        //newtarget.transform.SetParent(transform);
        //newtarget.transform.localPosition = targetPlace;
        newtarget.transform.eulerAngles = new Vector3(-90f, 0f, 0f);
        newtarget.transform.localScale = new Vector3(targetsScale, targetsScale, targetsScale);

        newtarget.GetComponent<targetLife>().setLifeSpen(targetLifespen);

        Targets.Add(newtarget);
    }
    
    private void calcRoundScore()
    {
        roundScore = 0;
        for (var i=0;i< Targets.Count; i++){
            targetLife currentTarget = Targets[i].GetComponent<targetLife>();
            if (currentTarget.state == "active"){
                Invoke("calcRoundScore", 2f);
                break;
            }
            roundScore += currentTarget.Score;
        }
        readyText.text = "your score is " + roundScore + " / "+ targetCount*10;
    }
    public void stopRound()
    {
        roundActive = false;
        for (var i = 0; i < Targets.Count; i++)
        {
            Destroy(Targets[i]);
        }
        targetsOnScene = 0;
        Targets = new List<GameObject> { };
        readyText.text = "";
    }
    public void restartRound()
    {
        stopRound();
        beginCountDown();
    }
}