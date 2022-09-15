using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIAbilityHandler : MonoBehaviour
{
    public List<GameObject> abilitySlots;
    public string test = "Sample Text";
    public List<RectTransform> remCDSlots;
    // Start is called before the first frame update
    void OnEnable()
    {
       foreach (AbilitySlot _slot in GetComponentsInChildren<AbilitySlot>())
        {
            abilitySlots.Add(_slot.gameObject);
           
        }
        
    }

   
}
