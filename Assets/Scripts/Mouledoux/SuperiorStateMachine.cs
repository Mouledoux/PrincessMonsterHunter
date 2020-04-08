namespace Mouledoux.Components
{
    public sealed class SuperiorStateMachine<T>
    {
        private bool initialized = false;
        
        private T _anyState;
        
        private T _currentState;
        public T GetCurrentState()
        {
            return _currentState;
        }
        private void SetCurrentState(T newState)
        {
            _currentState = newState;
            availableTransitions.Clear();

            foreach (Transition t in allTransitions.Keys)
            {
                if(t.GetBState().Equals(_currentState)) continue;

                else if (t.GetAState().Equals(_currentState) || t.GetAState().Equals(_anyState))
                {
                    availableTransitions.Add(t);
                }
            }
        }


        private System.Collections.Generic.Dictionary<Transition, System.Action> allTransitions =
            new System.Collections.Generic.Dictionary<Transition, System.Action>();

        private System.Collections.Generic.List<Transition> availableTransitions =
            new System.Collections.Generic.List<Transition>();

        
        
        
        public SuperiorStateMachine(T initState, T anyState)
        {
            // You can't start in the AnyState
            //if(initState.Equals(anyState))
              //  Throw
        
            _currentState = initState;
            _anyState = anyState;
        }
        
        public void AddTransition(T _aState, T _bState, System.Func<bool>[] _preReqs, System.Action _onTransition)
        {
            // You can't transition to AnyState, only from
            //if(_bState.Equals(anyState))
              //  Throw
        
            Transition Transition = new Transition(_aState, _bState, _preReqs);
            AddTransition(Transition, _onTransition);
        }
        
        public T Update()
        {
            if(!initialized) Initialize();
            
            Transition transition;
            if(CheckForAvailableTransition(out transition))
            {   
                MakeTransition(transition);
            }
            return _currentState;
        }
        
        
        
        
        private void Initialize()
        {
            SetCurrentState(_currentState);
            initialized = true;
        }

        private void AddTransition(Transition _newTransition, System.Action _onTransition)
        {
            allTransitions.Add(_newTransition, _onTransition);
        }

        private void MakeTransition(Transition transition)
        {
            SetCurrentState(transition.GetBState());
            allTransitions[transition].Invoke();
        }

        private bool CheckForAvailableTransition(out Transition validTransition)
        {
            foreach (Transition transition in availableTransitions)
            {
                if (transition.CheckPrerequisites())
                {
                    validTransition = transition;
                    return true;
                }
            }
            
            validTransition = null;
            return false;
        }





        //  ---------- ---------- ---------- ---------- ---------- ---------- ---------- ----------
        private sealed class Transition
        {
            private T aState;
            public T GetAState()
            {
                return aState;
            }

            private T bState;
            public T GetBState()
            {
                return bState;
            }

            private System.Func<bool>[] preReqs;

            public Transition(T _aState, T _bState, System.Func<bool>[] _preReqs)
            {
                aState = _aState;
                bState = _bState;

                preReqs = _preReqs;
            }

            public bool CheckPrerequisites()
            {
                bool passPreReqs = true;

                foreach (System.Func<bool> pr in preReqs)
                {
                    passPreReqs = passPreReqs & pr.Invoke();
                }
                return passPreReqs;
            }
        }
    }
}
