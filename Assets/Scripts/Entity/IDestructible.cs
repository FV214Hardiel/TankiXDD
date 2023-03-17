using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDestructible //Determination that is ICorruptible
{
    
    void Die(IEntity killer);

}
