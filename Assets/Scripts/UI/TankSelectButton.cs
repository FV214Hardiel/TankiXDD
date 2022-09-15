using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class TankSelectButton : MonoBehaviour
{

    public TankHull hullInfo;
    public GameObject infoPanel;
    public ApplyButton okbutton;
    TMPro.TextMeshProUGUI infoText;
    TMPro.TextMeshProUGUI nameText;
    AudioSource voiceover;
    public AudioClip[] voices;
    //Image infoImage;
    void Start()
    {
       
        voiceover = GetComponent<AudioSource>();
        
        nameText = infoPanel.transform.Find("ModuleNameText").GetComponent<TMPro.TextMeshProUGUI>();
        infoText = infoPanel.transform.Find("InfoPanelText").GetComponent<TMPro.TextMeshProUGUI>();

        

    }

    AudioClip RandomAudioClip(AudioClip[] clipArray)
    {
        return clipArray[Random.Range(0, clipArray.Length)];
    }

    public void ClickBlin()
    {
        voiceover.clip = RandomAudioClip(voices);
        voiceover.Play();
        nameText.text = hullInfo.hullName;
       
        infoText.text = "Base HP = " + hullInfo.baseHP.ToString() + "\n" + hullInfo.hullSpeed.ToString();
        okbutton.chosenHull = hullInfo;
        //okbutton.GetComponent<AudioSource>().clip = RandomAudioClip(voices);
    }

    
    void Update()
    {
        
    }
}
