using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//      https://www.youtube.com/watch?v=NY_fzd8g5MU&list=LL&index=1&t=3s

public class PlayerNarrationSystem : MonoBehaviour, IObserver
{
    [SerializeField] private Subject _playerSubject;
    public void OnNotify(PlayerActions action)
    {
        if (action == PlayerActions.Run)
        {
            //TODO: Some running music particles etc
        }
    }

    private void OnEnable()
    {
        _playerSubject.AddObserber(this);
    }

    private void OnDisable()
    {
        _playerSubject.RemoveObserber(this);
    }
}

