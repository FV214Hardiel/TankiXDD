using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AbilityHandler : MonoBehaviour
{
    public List<AbilityCard> abilitiesList;
    UIAbilityHandler abilityUI;

    void OnEnable()
    {
        abilityUI = GameObject.Find("AbilitiesUI").GetComponent<UIAbilityHandler>();
        abilityUI.enabled = true;
        
        int slotCount = 0;

        int slots = GameInfoSaver.instance.chosenAbilities.Count;

        for (int i = 1; i <= slots; i++)
        {
            GameObject newSlot = Instantiate(abilityUI.slotPrefab, abilityUI.transform);
            abilityUI.abilitySlots.Add(newSlot);
            abilityUI.remCDSlots.Add(newSlot.transform.Find("RemCD").GetComponent<RectTransform>());

        }
        
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

    
    
}
