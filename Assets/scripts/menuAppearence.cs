using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class menuAppearence : MonoBehaviour
{
   
    public GameObject menuObject;
    void Start()
    {
        EventManager.PouseGame += showMenu;
        showMenu();
    }
    private void showMenu()
    {
        menuObject.SetActive(true);
    }
   
}
