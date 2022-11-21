using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShopScript : MonoBehaviour
{
    public TMP_Dropdown shopDropdown;
    public Transform buttonsGrid;
    public GameObject buttonTemplate;
    public Transform infoPanel;

    [SerializeField]
    TextMeshProUGUI infoName;
    [SerializeField]
    TextMeshProUGUI infoText;
   


    public IShopItem chosenItem;
    GameObject activeButton;

    List<GameObject> createdButtons;

    private void Start()
    {
        infoName = infoPanel.Find("Name").GetComponent<TextMeshProUGUI>();
        infoName.text = "Tank Name";

        infoText= infoPanel.Find("Info").GetComponent<TextMeshProUGUI>();
        infoText.text = "Info";

        createdButtons = new();
        dropdownChange(shopDropdown.value);


    }

    public void dropdownChange(int value)
    {
        if (createdButtons.Count != 0)
        {
            foreach (GameObject child in createdButtons)
            {
                Destroy(child);
            }
        }
        
        switch (value)
        {
            case 0:
                Debug.Log("Hulls");
                foreach (TankHull item in GameInfoSaver.instance.tanksList.allHulls)
                {
                    GameObject newButton = CreateButton(item.Name);
                    createdButtons.Add(newButton);
                    if (GameInfoSaver.instance.tanksList.unlockedHulls.Contains(item))
                    {
                        newButton.GetComponent<UnityEngine.UI.Button>().interactable = false;
                    }

                }
                
                break;
            case 1:
                Debug.Log("Turrets");
                foreach (TankTurret item in GameInfoSaver.instance.tanksList.allTurrets)
                {
                    GameObject newButton = CreateButton(item.Name);
                    createdButtons.Add(newButton);
                    if (GameInfoSaver.instance.tanksList.unlockedTurrets.Contains(item))
                    {
                        newButton.GetComponent<UnityEngine.UI.Button>().interactable = false;
                    }

                }
                
                break;
            case 2:
                Debug.Log("Abilities");
                break;
            case 3:
                Debug.Log("Skins"); 
                foreach (SkinCard item in GameInfoSaver.instance.skinsList.allSkins)
                {
                    GameObject newButton = CreateButton(item.Name);
                    createdButtons.Add(newButton);
                    if (GameInfoSaver.instance.skinsList.unlockedSkins.Contains(item))
                    {
                        newButton.GetComponent<UnityEngine.UI.Button>().interactable = false;
                    }

                }
                break;

        }

    }

   

    public void ButtonClicked(GameObject button)
    {
        switch (shopDropdown.value)
        {
            case 0: //Hulls

                TankHull foundHull = GameInfoSaver.instance.tanksList.allHulls.Find(x => x.Name == button.name);

                chosenItem = foundHull;

                infoName.text = foundHull.name;
                infoText.text = ("HP: " + foundHull.modifications[0].baseHP + 
                    "\nShield: " + foundHull.modifications[0].baseSP +
                    "\nPrice: " + foundHull.Price +
                    "\nSpeed: " + foundHull.modifications[0].hullSpeed +
                    "\nRotation: " + foundHull.modifications[0].hullRotation +
                    "\nUpgrade price: " + foundHull.upgradePrice);

                break;
            case 1: //Turrets

                TankTurret foundTurret = GameInfoSaver.instance.tanksList.allTurrets.Find(x => x.Name == button.name);

                chosenItem = foundTurret;

                infoName.text = foundTurret.Name;
                infoText.text = ("Damage: " + foundTurret.modifications[0].damage +
                    "\nDelay between shots: " + foundTurret.modifications[0].delayBetweenShots +
                    "\nPrice: " + foundTurret.Price +
                    "\nRange: " + foundTurret.modifications[0].attackRange +
                    "\nRotation: " + foundTurret.modifications[0].turretRotationSpeed +
                    "\nUpgrade price: " + foundTurret.upgradePrice);
               
                break;
            case 2:
                Debug.Log(name);
                break;
            case 3: //Skins
                Debug.Log(name);

                SkinCard foundSkin = GameInfoSaver.instance.skinsList.allSkins.Find(x => x.Name == button.name);

                chosenItem = foundSkin;

                infoName.text = foundSkin.name;
                infoText.text = foundSkin.name + "\nPrice: " + foundSkin.Price;

                break;
        }
        activeButton = button;

    }

    public void BuyButtonClick()
    {
        if (chosenItem == null) return;
        if (chosenItem.Price > GameInfoSaver.instance.Currency)
        {
            Debug.LogWarning("Insufficient funds");
        }
        else
        {
            chosenItem.BuyItem();
            GameInfoSaver.instance.SubtractCurrency(chosenItem.Price);
            if (activeButton == null)
            {
                print("error");
            }
            else
            {
                activeButton.GetComponent<UnityEngine.UI.Button>().interactable = false;
            }
            
        }
    }

    GameObject CreateButton(string name)
    {
        GameObject newButton = Instantiate(buttonTemplate, buttonsGrid);
        newButton.SetActive(true);
        newButton.name = name;
        newButton.GetComponentInChildren<TextMeshProUGUI>().text = name;
        return newButton;

    }

}

public interface IShopItem
{
    string Name { get; }
    ushort Price { get;}

    void BuyItem();
}