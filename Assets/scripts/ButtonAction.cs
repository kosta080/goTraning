using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonAction : MonoBehaviour
{
    public AudioSource btnSound;
    public Animator btnAbinator;

   
    public enum ActionType { none, reset5Tar, movingTar };
    public ActionType actionType;


    public void buttonAction()
    {
        Debug.Log("--button Action--");
        btnSound.Play();
        btnAbinator.SetTrigger("press");


        if (actionType.ToString() == "reset5Tar")
        {
            EventManager.reset5Tar();
        }
        else if (actionType.ToString() == "movingTar")
        {
            EventManager.resetMovingTar();
        }
    }
}
