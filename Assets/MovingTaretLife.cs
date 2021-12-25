using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingTaretLife : MonoBehaviour
{
    public List<int> playerHits;
    private string state = "active";

    private movingTargets movingTargetsHere;

    private AudioSource hitSound;

  
    private void Start()
    {
        hitSound = transform.GetComponent<AudioSource>();
        movingTargetsHere = GameObject.Find("moving targets").GetComponent<movingTargets>();
    }
    private void OnEnable()
    {
        state = "active";
        playerHits = new List<int> { };
        transform.GetComponent<Renderer>().material.SetColor("_Color", Color.white);
        transform.Find("_text").GetComponent<TextMesh>().text = "";
    }

    public void setHit(int hitScore)
    {
        if (state == "active")
        {
            playerHits.Add(hitScore);
            if (playerHits.Count >= 5)
            {
                state = "beaten";
                transform.GetComponent<Renderer>().material.SetColor("_Color", Color.red);
                movingTargetsHere.calcMovingTargetsScore(playerHits);
            }
            transform.Find("_text").GetComponent<TextMesh>().text = hitScore.ToString();
            //transform.GetComponent<Renderer>().material.SetColor("_Color", Color.red);

            if (hitSound)hitSound.Play();

        }
    }

}
