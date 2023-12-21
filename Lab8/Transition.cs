using System;

namespace Lab8
{
    public class Transition<S, E> where S : Enum where E : Enum
    {
        public S FromState { get; set; }
        public S ToState { get; set; }
        public E Event { get; set; }
    }
}