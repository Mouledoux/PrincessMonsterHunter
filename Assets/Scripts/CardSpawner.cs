using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardSpawner : MonoBehaviour
{
    public int index;

    [SerializeField]
    GameObject cardPrefab;

    [SerializeField]
    Transform cardSpawnPos;

    public List<SwipeObject> deck = new List<SwipeObject>();

    private Mouledoux.Components.Mediator.Subscriptions subscriptions =
        new Mouledoux.Components.Mediator.Subscriptions();

    void Start()
    {
        subscriptions.Subscribe("needCard", SendNewCard);
        subscriptions.Subscribe("removeCard", RemoveCard);
    }

    public void SendNewCard(object[] args)
    {
        if(args[0] is int instanceID)
        {
            Mouledoux.Components.Mediator.NotifySubscribers($"{instanceID}:NewCard", new object[] { SpawnCard(Random.Range(0, deck.Count)) });
        }
    }

    public SwipeCard SpawnCard(int index)
    {
        if (index >= deck.Count) return null;

        GameObject newCard = Instantiate(cardPrefab);
        SwipeCard newSwipe = newCard.GetComponent<SwipeCard>();

        newCard.transform.position = cardSpawnPos.position;
        newSwipe.cardData = deck[index];
        newSwipe.Initialize();

        return newSwipe;
    }

    public void RemoveCard(object[] args)
    {
        foreach(object arg in args)
        {
            if(arg is SwipeObject card)
            {
                RemoveCard(card);
                return;
            }
        }
    }

    public void RemoveCard(SwipeObject card)
    {
        deck.Remove(card);
    }

}
