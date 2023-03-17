using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHealthBar
{
    void StartBar();

    void UpdateBar(float newHP);

    void ChangeMaxHP(float maxHP);

    //void ConnectHealth(Health health);
}
