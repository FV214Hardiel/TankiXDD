using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ApplyButton : MonoBehaviour
{
    public TankHull chosenHull;
    public TankTurret chosenTurret;

    public TMP_Dropdown hullsDropdown;
    public TMP_Dropdown gunsDropdown;
    public TMP_Dropdown abilitiesDropdown;
    public TMP_Dropdown skinsDropdown;

    public Image flash;

    public AudioSource voiceAudio;

    public GameObject levelSelection;
    public GameObject tankSelection;



    private void Start()
    {
       
    }

    public void ApplyClicked()
    {
        //Debug.Log(GameInfoSaver.instance.chosenHull);
        //Debug.Log(GameInfoSaver.instance.tanksList.allHulls[hullsDropdown.value]);
        GameInfoSaver.instance.chosenHull = GameInfoSaver.instance.tanksList.allHulls[hullsDropdown.value];
        Debug.Log("ApplyClickedHull + " + GameInfoSaver.instance.chosenHull);
        GameInfoSaver.instance.chosenTurret = GameInfoSaver.instance.tanksList.allTurrets[gunsDropdown.value];
        Debug.Log("ApplyClickedTurret + " + GameInfoSaver.instance.chosenTurret);
        GameInfoSaver.instance.chosenSkin = GameInfoSaver.instance.skins.unlockedSkins[skinsDropdown.value];

        StartCoroutine(FlashEffect(0.2f, 0.1f));
    }

    IEnumerator FlashEffect(float flashDur, float maxAlpha)
    {
        Color currColor;
        GetComponent<Button>().interactable = false;
        voiceAudio.clip = RandomAudioClip(GameInfoSaver.instance.chosenHull.voiceover);
        voiceAudio.Play();
        //float halfDur = flashDur / 2;
        //for (float f = 0; f <= halfDur; f += Time.deltaTime)
        //{
        //    currColor = flash.color;
        //    currColor.a = Mathf.Lerp(0, maxAlpha, f / halfDur);
        //    flash.color = currColor;
        //    yield return null;
        //}
        currColor = flash.color;
        currColor.a = maxAlpha;
        flash.color = currColor;

        for (float f = 0; f <= flashDur; f += Time.deltaTime)
        {
            currColor = flash.color;
            currColor.a = Mathf.Lerp(maxAlpha, 0, f / flashDur);
            flash.color = currColor;
            yield return null;
        }

        currColor = flash.color;
        currColor.a = 0;
        flash.color = currColor;

        yield return new WaitForSeconds(0.4f);
        Debug.Log("Flash");
        GetComponent<Button>().interactable = true;

        levelSelection.SetActive(true);
        tankSelection.SetActive(false);


    }

    AudioClip RandomAudioClip(List<AudioClip> clipArray)
    {
        return clipArray[Random.Range(0, clipArray.Count)];
    }
}
