using System;
using System.Collections.Generic;
using UnityEngine;

public class Unsubscriber : IDisposable
    {
        private Observer observer;
        private IList<Observer> observers;

        public Unsubscriber(IList<Observer> observers, Observer observer)
        {
            this.observers = observers;
            this.observer = observer;
        }

        public void Dispose()
        {
            Debug.Log("Disposing");

            if (observer != null && observers.Contains(observer))
            {
                observers.Remove(observer);
            }
        }
    }