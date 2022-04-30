using System.Collections;
using UnityEngine;

public class PermanentAction : CharacterAction
{

    protected virtual void Start()
    {
        TryFindActionScheduler();
        _actionsScheduler.AddAction(this);
    }

}
