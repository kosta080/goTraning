using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movingTargets : MonoBehaviour
{
    public Animator targetsAnimator;
    public TextMesh readyText;
    public int targetsOnScene;



    void Start()
    {
        EventManager.ResetMovingTar += restartRound;
        EventManager.PouseGame += stopRound;
    }

    private void restartRound()
    {
        stopRound();
        beginCountDown();
    }
    private void stopRound()
    {
        targetsAnimator.SetTrigger("stop");
        targetsOnScene = 0;
        readyText.text = "";
    }
    private void beginCountDown()
    {
        targetsAnimator.SetTrigger("stop");
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
        //devlareTarget();
        startAtrgetMovemeny();
    }

    private void startAtrgetMovemeny()
    {
        targetsAnimator.SetTrigger("start");
        targetsAnimator.speed = 1;
    }
    public void calcMovingTargetsScore(List<int> playerHits)
    {
        
        targetsAnimator.speed = 0;
        int totalRoundScore = 0;
        for (var i = 0; i < playerHits.Count; i++)
        {
            totalRoundScore += playerHits[i];
        }
        readyText.text = "your score is " + totalRoundScore + " / " + "50";
    }
}
