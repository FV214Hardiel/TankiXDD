using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBarPlayer : MonoBehaviour
{
    float health;
    float maxHealth;
    public RectTransform Bar;
    float barBaseWidth;

    TMPro.TextMeshProUGUI TextText;

    //public Vector2 huh;

    private void OnEnable()
    {
        barBaseWidth = Bar.rect.width;

        Player.playerIsChanged += OnPlayerChanged; //здесь мы подписываемся

        float perc = barBaseWidth;
        //Debug.Log(perc);
        Bar.sizeDelta = new Vector2(perc, 40);
        TextText = GetComponentInChildren<TMPro.TextMeshProUGUI>();
        TextText.text = maxHealth.ToString();

    }

   
    void OnPlayerChanged()
    {
        if (Player.PlayerHull.GetComponent<HealthPlayer>() != null)
        {
            //Debug.Log(health + "HP");
            maxHealth = Player.PlayerHull.GetComponent<HealthPlayer>().maxHP;
            health = Player.PlayerHull.GetComponent<HealthPlayer>().HP;
            UpdateBar(health);
        }
       
        

    }
    private void OnDisable()
    {
        //тут мы отписываемся
        Player.playerIsChanged -= OnPlayerChanged;
    }
    void Start()
    {

        

        maxHealth = Player.PlayerHull.GetComponent<HealthPlayer>().baseHP;
        health = Player.PlayerHull.GetComponent<HealthPlayer>().HP;
        OnPlayerChanged();

    }

    
    public void UpdateBar(float newHP)
    {
        float perc = barBaseWidth * (newHP / maxHealth);
        Bar.sizeDelta = new Vector2(perc, 40);

        newHP = Mathf.Floor(newHP);
        TextText.text = newHP.ToString();
    }
}
