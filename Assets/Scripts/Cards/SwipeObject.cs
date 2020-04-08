using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewSwipeObjecr", menuName = "ScriptableObjects/SwipeObject", order = 1)]
public class SwipeObject : ScriptableObject
{
    public string cardName;
    public Sprite monsterImage;

    public UnityEngine.Events.UnityEvent onLeftSwipe;
    public UnityEngine.Events.UnityEvent onRightSwipe;

    public UnityEngine.Events.UnityEvent onLeftRelease;
    public UnityEngine.Events.UnityEvent onRightRelease;

    public void Print(string s)
    {
        Debug.Log(s);
    }

    public void RemoveCard()
    {
        Mouledoux.Components.Mediator.NotifySubscribers("removeCard", new object[] { this });
    }

    public void SelfDestruct(GameObject go)
    {
        Destroy(go, 0.2f);
    }
}
