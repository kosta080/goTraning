using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class targetLife : MonoBehaviour
{

    public string state;
    public int Score;
    // state can be active / beaten / or disabled 
   
    void Start()
    {
        state = "active";
    }

    public void setLifeSpen(float seconds)
    {
        Invoke("disable", seconds);
    }
    public void setBeaten(int score)
    {
        if(state == "active")
        {
            state = "beaten";
            Score = score;
            transform.Find("_text").GetComponent<TextMesh>().text = score.ToString();
            transform.GetComponent<Renderer>().material.SetColor("_Color", Color.red);

            if (transform.GetComponent<AudioSource>())
                transform.GetComponent<AudioSource>().Play();

        }
    }
    private void disable()
    {
        if(state != "beaten")
        {
            state = "disabled";
            transform.GetComponent<Renderer>().material.SetColor("_Color", Color.gray);
        }
    }
}
