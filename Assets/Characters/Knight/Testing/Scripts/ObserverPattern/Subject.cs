using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Subject : MonoBehaviour
{
        private List<IObserver> _observers = new List<IObserver>();

        public void AddObserber(IObserver observer)
        {
                _observers.Add(observer);
        }

        public void RemoveObserber(IObserver observer)
        {
                _observers.Remove(observer);
        }

        protected void NotifyObservers(PlayerActions action)
        {
                _observers.ForEach((_observer) =>
                        {
                                _observer.OnNotify(action);
                        }
                        );
        }
}
