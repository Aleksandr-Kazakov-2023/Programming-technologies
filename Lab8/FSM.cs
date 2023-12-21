using System;
using System.Collections.Generic;
using System.Linq;

namespace Lab8
{
    public class FSM<S, E> where S : Enum where E : Enum
    {
        private State<S> currentState = null;
        private readonly Dictionary<S, State<S>> states = new();
        private List<Transition<S, E>> transitions;

        public S State
        {
            get => currentState.Name;
            set => SetState(value);
        }

        public FSM(S initialState)
        {
            foreach (S item in Enum.GetValues(typeof(S)))
            {
                State<S> state = new() { Name = item };
                states.Add(state.Name, state);
            }
            currentState = GetState(initialState);
        }

        private State<S> GetState(S stateName)
        {
            if (!states.ContainsKey(stateName)) return null;
            return states[stateName];
        }

        private bool SetState(S stateName)
        {
            if (!states.ContainsKey(stateName)) return false;
            currentState.OnExit?.Invoke();
            currentState = states[stateName];
            currentState.OnEnter?.Invoke();
            return true;
        }

        public void Tick()
        {
            currentState.OnUpdate?.Invoke();
        }

        public void OnEvent(E Event)
        {
            var result = transitions.Where(e =>
                (e.FromState == null || e.FromState.Equals(currentState.Name) && (e.Event.Equals(Event))));
            if (result.Any())
                SetState(result.First().ToState);
        }

        public void RegisterTransitions(Transition<S, E>[] transitions)
        {
            this.transitions = new(transitions);
        }

        public State<S> this[S stateName]
        {
            get => GetState(stateName);
        }
    }
}