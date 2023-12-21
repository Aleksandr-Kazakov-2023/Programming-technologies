using System;

namespace Lab8
{
    public class State<S> where S : Enum
    {
        public S Name { get; set; }
        public Action OnEnter;
        public Action OnExit;
        public Action OnUpdate;
    }
}