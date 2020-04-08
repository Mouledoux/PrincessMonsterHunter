using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    public Turn currentTurn;
}

public class Turn
{
    // The very first action of a turn
    public System.Action onTurnStart;

    // The very last action of a turn
    public System.Action onTurnEnd;
}