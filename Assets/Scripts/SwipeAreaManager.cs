using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeAreaManager : MonoBehaviour
{
    public SwipeCard activeCard;

    private Mouledoux.Components.Mediator.Subscriptions subscriptions =
        new Mouledoux.Components.Mediator.Subscriptions();


    private void Awake()
    {
        string newCardMessage = $"{GetInstanceID()}:NewCard";
        subscriptions.Subscribe(newCardMessage, SetActiveCard);
    }


    void Update()
    {
        if(activeCard == null)
        {
            string message = "needCard";
            object[] args = { GetInstanceID() };
            Mouledoux.Components.Mediator.NotifySubscribers(message, args);
            return;
        }
    }

    public void SetActiveCard(object[] args)
    {
        foreach(object arg in args)
        {
            if(arg is SwipeCard newCard)
            {
                SetActiveCard(newCard);
                return;
            }
        }
    }

    public void SetActiveCard(SwipeCard newCard)
    {
        activeCard = newCard;
    }

}
