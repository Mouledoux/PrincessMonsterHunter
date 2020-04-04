using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewSwipeObjecr", menuName = "ScriptableObjects/SwipeObject", order = 1)]
public class SwipeObject : ScriptableObject
{
    public UnityEngine.Events.UnityEvent onLeftSwipe;
    public UnityEngine.Events.UnityEvent onLeftRelease;

    public UnityEngine.Events.UnityEvent onRightSwipe;
    public UnityEngine.Events.UnityEvent onRightRelease;

    public void Print(string s)
    {
        Debug.Log(s);
    }
}
