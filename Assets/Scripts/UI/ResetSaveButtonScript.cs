using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetSaveButtonScript : MonoBehaviour
{
    public void ResetSave()
    {
        PlayerPrefs.DeleteAll();
        GameInfoSaver.instance.Load();
    }
}
