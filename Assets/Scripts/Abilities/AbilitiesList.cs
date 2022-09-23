using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Abilities", menuName = "Tanks/AbilitiesList")]
public class AbilitiesList : ScriptableObject
{
    public List<AbilityCard> allAbilities;
    public List<AbilityCard> unlockedAbilities;
}
