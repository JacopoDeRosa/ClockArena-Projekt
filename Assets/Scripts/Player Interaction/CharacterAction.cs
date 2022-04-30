using System.Collections;
using UnityEngine;
using System;

public delegate void CharacterActionHandler(CharacterAction characterAction);

public class CharacterAction : MonoBehaviour
{
    [SerializeField] private Sprite _actionSprite;
    [SerializeField] protected ActionsScheduler _actionsScheduler;
    [SerializeField] private string _name;
    [SerializeField] private string _description;

    public CharacterActionHandler onBegin;
    public CharacterActionHandler onEnd;

    public virtual string GetName()
    {
        return _name;
    }
    public virtual string GetDescription()
    {
        return _description;
    }
    public virtual Sprite GetActionSprite()
    {
        return _actionSprite;
    }
    public virtual void Begin()
    {
        onBegin?.Invoke(this);
    }
    public virtual void End()
    {
        onEnd?.Invoke(this);
    }
    public virtual bool Cancel()
    {
        return true;
    }
    protected virtual void OnValidate()
    {
        TryFindActionScheduler();
    }
    protected void TryFindActionScheduler()
    {
        if (_actionsScheduler == null)
        {
            _actionsScheduler = FindObjectOfType<ActionsScheduler>();
        }
    }
}
