using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityHandler : MonoBehaviour
{
    public List<AbilityCard> abilitiesList;
    UIAbilityHandler abilityUI;
    void Start()
    {
        
        abilityUI = FindObjectOfType<UIAbilityHandler>();

        int slotCount = 0;
        //Debug.Log(abilitiesList[0]);
        foreach (AbilityCard _ability in GameInfoSaver.instance.chosenAbilities)
        {
            if (_ability != null)
            {
               
                Component addedAbility = gameObject.AddComponent(System.Type.GetType(_ability.script));
                addedAbility.GetType().GetField("card").SetValue(addedAbility, _ability);
                addedAbility.GetType().GetField("abilitySlot").SetValue(addedAbility, slotCount);
                
                addedAbility.GetType().GetField("abilityDisplay").SetValue(addedAbility, abilityUI.remCDSlots[slotCount]);
                
                GetComponent<EntityHandler>().abilities.Add(addedAbility);

                slotCount++;
            }
            
        }
    }

    
    void Update()
    {
        
    }
}
