using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Maybe<T>
{
    // example use
    // Optional<Face> GetNeighbour(Face face){
    //     return face.Neighbour[0] != null 
    //         ? new Just<Face>(face.Neighbour)
    //         : new Nothing();
    // }

    public abstract bool HasValue();

    public abstract T Value { get; }

    public sealed class Just : Maybe<T>
    {
        private T value;

        public Just(T value){
            this.value = value;
        }

        public override T Value => value;

        public override bool HasValue()
        {
            return true;
        }
    }

    public sealed class Nothing : Maybe<T>
    {
        public override T Value => throw new Exception("Trying to access Value of Nothing");

        public override bool HasValue()
        {
            return false;
        }
    }
}

