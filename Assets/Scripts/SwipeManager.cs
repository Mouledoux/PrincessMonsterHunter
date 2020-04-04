using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeManager : MonoBehaviour
{
    public float deadZone = 50f;

    public SwipeCard activeCard;
    private Vector3 lastClickPos;
    private Vector3 targetPos = Vector3.zero;


    private Mouledoux.Components.SuperiorStateMachine<ESwipeState> StateMachine =
        new Mouledoux.Components.SuperiorStateMachine<ESwipeState>(ESwipeState.INIT, ESwipeState.ANY);




    private void Awake()
    {
        System.Func<bool>[] emptyPrereq = {};
        System.Action emptyAction = () => {};

        System.Func<bool>[] holdingPrereq =
        {
           IsHolding,
           () => { return !IsSwipingLeft(); },
           () => { return !IsSwipingRight(); }
        };

        System.Func<bool>[] leftSwipePrereq =
        {
            IsHolding,
            IsSwipingLeft,
        };
        System.Func<bool>[] leftReleasePrereq =
        {
            IsSwipingLeft,
            () => { return !IsHolding(); }
        };

        System.Func<bool>[] rightSwipePrereq =
        {
            IsHolding,
            IsSwipingRight,
        };
        System.Func<bool>[] rightReleasePrereq =
        {
            IsSwipingRight,
            () => { return !IsHolding(); }
        };


        System.Action onHold;
        onHold = () => targetPos = Vector3.zero;
        onHold += () => print("hold");


        System.Action onLeftSwipe;
        onLeftSwipe = () => activeCard.cardEvents.onLeftSwipe.Invoke();
        onLeftSwipe += () => targetPos = Vector3.right * -1f;

        System.Action onLeftRelease;
        onLeftRelease = () => activeCard.cardEvents.onLeftRelease.Invoke();
        onLeftRelease += () => targetPos = Vector3.right * -16f;
        onLeftRelease += () => Destroy(activeCard.gameObject, 1f);

        System.Action onRightSwipe;
        onRightSwipe = () => activeCard.cardEvents.onRightSwipe.Invoke();
        onRightSwipe += () => targetPos = Vector3.right * 1f;

        System.Action onRightRelease;
        onRightRelease = () => activeCard.cardEvents.onRightRelease.Invoke();
        onRightRelease += () => targetPos = Vector3.right * 16f;
        onRightRelease += () => Destroy(activeCard.gameObject, 1f);



        StateMachine.AddTransition(ESwipeState.INIT, ESwipeState.IDLE, emptyPrereq, () => print("idle"));
        StateMachine.AddTransition(ESwipeState.ANY, ESwipeState.HOLDING, holdingPrereq, onHold);

        StateMachine.AddTransition(ESwipeState.HOLDING, ESwipeState.LEFT_SWIPE, leftSwipePrereq, onLeftSwipe);
        StateMachine.AddTransition(ESwipeState.HOLDING, ESwipeState.RIGHT_SWIPE, rightSwipePrereq, onRightSwipe);

        StateMachine.AddTransition(ESwipeState.LEFT_SWIPE, ESwipeState.LEFT_RELEASE, leftReleasePrereq, onLeftRelease);
        StateMachine.AddTransition(ESwipeState.RIGHT_SWIPE, ESwipeState.RIGHT_RELEASE, rightReleasePrereq, onRightRelease);
    }


    void Update()
    {
        if(activeCard == null) return;

        lastClickPos = Input.GetMouseButtonDown(0) ? Input.mousePosition : lastClickPos;
        StateMachine.Update();
        UpdateActiveCardPosition();
    }

    private bool IsHolding()
    {
         return (Input.GetMouseButton(0));
    }

    private bool IsSwipingLeft()
    {
        return (lastClickPos.x - Input.mousePosition.x > deadZone);
    }    
    
    private bool IsSwipingRight()
    {
        return (Input.mousePosition.x - lastClickPos.x > deadZone);
    }

    private void UpdateActiveCardPosition()
    {
        activeCard.transform.position = Vector3.Lerp(activeCard.transform.position, targetPos, Time.deltaTime * 10f);
    }


    public enum ESwipeState
    {
        INIT,
        ANY,
        IDLE,
        HOLDING,
        LEFT_SWIPE,
        LEFT_RELEASE,
        RIGHT_SWIPE,
        RIGHT_RELEASE,
    }
}
