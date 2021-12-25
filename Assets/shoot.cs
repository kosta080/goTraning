using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class shoot : MonoBehaviour
{
    private GameObject textObject;
    private bool loaded = false;
    private AudioSource gunshotSound;
    public LayerMask targetsLayerMask;
    public LayerMask buttonsLayerMask;
    public bool shootingEnabled = false;
    public Animator gunAnimator;
    private void Start()
    {
        textObject = GameObject.Find("refText");
        gunshotSound = GetComponent<AudioSource>();
        if (gunshotSound == null)
            Debug.LogError("Audio Source is NULL");

        EventManager.StartTraining += EnableShooting;
        EventManager.PouseGame += DisableShooting;
    }
    private void EnableShooting()
    {
        shootingEnabled = true;
    }
    private void DisableShooting()
    {
        shootingEnabled = false;
    }
    private void Update()
    {
        if (!shootingEnabled) return;
        if (Input.GetMouseButtonDown(0) && loaded)
        {
            loaded = false;
            rayCalcShot();
            //gunshotSound.Play();
        }
            
        if (Input.GetMouseButtonUp(0))
        {
            loaded = true;
        }

    }

    private void rayCalcShot()
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, targetsLayerMask))
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
            gunshotSound.Play();
            gunAnimator.SetTrigger("play");

            float dist = Vector3.Distance(hit.point, hit.transform.position);
            float targetRadious = hit.transform.GetComponent<Collider>().bounds.size.y / 2;
            int hitScoreRounded = (int)Mathf.Ceil(Mathf.Abs((dist / targetRadious) - 1) * 10);

            textObject.GetComponent<Text>().text = "Its a Hit " + hitScoreRounded;


            //writeScoreOnTarget
            //hit.transform.Find("_text").GetComponent<TextMesh>().text = hitScoreRounded.ToString();
            if (hit.transform.GetComponent<targetLife>())
            {
                Debug.Log("_1_");
                hit.transform.GetComponent<targetLife>().setBeaten(hitScoreRounded);
            }

            else if (hit.transform.GetComponent<MovingTaretLife>())
            {
                Debug.Log("_2_");
                hit.transform.GetComponent<MovingTaretLife>().setHit(hitScoreRounded);
            }
        }
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, buttonsLayerMask))
        {
            Debug.Log("button");
            hit.transform.GetComponent<ButtonAction>().buttonAction();
        }
        else
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 1000, Color.white);
            textObject.GetComponent<Text>().text = "you missed...";
            gunshotSound.Play();
            gunAnimator.SetTrigger("play");
        }
    }
   
   
}