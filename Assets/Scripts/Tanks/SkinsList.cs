using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Skins", menuName = "Tanks/Skins")]
public class SkinsList : ScriptableObject
{
    public List<Texture2D> allSkins;
    public List<Texture2D> unlockedSkins;
}
