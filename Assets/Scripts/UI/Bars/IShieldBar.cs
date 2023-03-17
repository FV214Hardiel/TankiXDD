using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IShieldBar
{
    void StartBar();

    void UpdateBar(float newSP);

    void ChangeMaxSP(float maxSP);

    //void ConnectShield(Shield shield);

}
