using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIAbilityHandler : MonoBehaviour
{
    public List<GameObject> abilitySlots;
    public string test = "Sample Text";
    public List<RectTransform> remCDSlots;

    public int slots;
    public GameObject slotPrefab;
    // Start is called before the first frame update
    void OnEnable()
    {
        //foreach (AbilitySlot _slot in GetComponentsInChildren<AbilitySlot>())
        // {
        //     abilitySlots.Add(_slot.gameObject);

        // }
        slots = GameInfoSaver.instance.chosenAbilities.Count;
        for (int i = 1; i <= slots; i++)
            {
            GameObject newSlot = Instantiate(slotPrefab, transform);
            abilitySlots.Add(newSlot);
            remCDSlots.Add(newSlot.transform.Find("RemCD").GetComponent<RectTransform>());

        }
        
    }

   
}
