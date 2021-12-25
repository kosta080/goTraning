using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EventManager : MonoBehaviour
{

    public static Action StartTraining;
    public static Action Reset5Tar;
    public static Action ResetMovingTar;
    public static Action PouseGame;

    public static void startTraining()
    {
        StartTraining?.Invoke();
    }
    public static void reset5Tar()
    {
        Reset5Tar?.Invoke();
    }
    public static void resetMovingTar()
    {
        ResetMovingTar?.Invoke();
    }
    public static void pouseGame()
    {
        PouseGame?.Invoke();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            pouseGame();
        }
    }
    public void doExitGame()
    {
        Application.Quit();
    }
}
