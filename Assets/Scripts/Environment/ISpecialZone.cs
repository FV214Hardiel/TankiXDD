using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISpecialZone
{
    void TriggerEntered(IEntity other);

    void TriggerExited(IEntity other);
}
