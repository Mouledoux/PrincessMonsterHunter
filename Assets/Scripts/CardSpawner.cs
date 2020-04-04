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

    public SwipeObject[] deck;

    private Mouledoux.Components.Mediator.Subscriptions subscriptions =
        new Mouledoux.Components.Mediator.Subscriptions();

    void Start()
    {
        subscriptions.Subscribe("needCard", SendNewCard);
    }

    public void SendNewCard(object[] args)
    {
        if(args[0] is int instanceID)
        {
            Mouledoux.Components.Mediator.NotifySubscribers($"{instanceID}:NewCard", new object[]{ SpawnCard(index) });
        }
    }

    public SwipeCard SpawnCard(int index)
    {
        GameObject newCard = Instantiate(cardPrefab);
        SwipeCard newSwipe = newCard.GetComponent<SwipeCard>();

        newCard.transform.position = cardSpawnPos.position;
        newSwipe.cardData = deck[index];
        newSwipe.Initialize();

        return newSwipe;
    }

}
