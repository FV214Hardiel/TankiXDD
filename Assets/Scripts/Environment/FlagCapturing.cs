using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlagCapturing : MonoBehaviour
{
    [SerializeField] float capturingProgress;
    bool capturing;
    [SerializeField] float captureRange;
    [SerializeField] float totalCaptureTime;
    float captureSpeed;
    [SerializeField] bool resetIfPlayerOutOfRange;

    Slider progressBar;
    GameObject progressBarCanvas;

    AudioSource capturingSound;
    AudioSource captureCompleteSound;

    public bool isCaptured;

    ObjectiveHandler objectiveHandlerInstance;

    
    void Start()
    {
        objectiveHandlerInstance = FindObjectOfType<ObjectiveHandler>();

        captureSpeed = 100 / totalCaptureTime;
        transform.localScale = new Vector3(2 * captureRange, 2*captureRange, 0.5f * captureRange);

        progressBarCanvas = GameObject.Find("progressBarCanvas"); 
        progressBarCanvas.SetActive(false);

        progressBar = GetComponentInChildren<Slider>(true);

        capturingSound = transform.Find("CapturingProgressSound").GetComponent<AudioSource>();
        captureCompleteSound = transform.Find("CaptureCompleteSound").GetComponent<AudioSource>();
    }

    
    void Update()
    {
        if (capturing)
        {
            capturingSound.pitch = 0.5f + capturingProgress / 100;
            progressBar.value = capturingProgress;
            capturingProgress += Time.deltaTime * captureSpeed;
            if (capturingProgress >= 100)
            {
                capturingProgress = Mathf.Clamp(capturingProgress, 0, 100);
                progressBarCanvas.SetActive(false);
                
                captureCompleteSound.Play();
                isCaptured = true;                
                enabled = false;
                
                objectiveHandlerInstance.OnFlagCapture();
                
            }
            
            
            //Debug.Log(capturingProgress);
        }
        

    }

    

    private void OnTriggerStay(Collider other)
    {
        //print(other.name);
        if (other == Player.PlayerHullColl)
        {
           
            if (Vector3.Distance(transform.position, other.transform.position) < captureRange && !isCaptured)
            {
                progressBarCanvas.SetActive(true);
                capturingSound.enabled = true;
                capturing = true;
                
            }
            else
            {
                progressBarCanvas.SetActive(false);
                capturingSound.enabled = false;
                capturing = false;
                if (resetIfPlayerOutOfRange) { capturingProgress = 0; }

                
            }
                
        }
    }
}
