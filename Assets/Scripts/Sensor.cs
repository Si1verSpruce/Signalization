using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Sensor : MonoBehaviour
{
    [SerializeField] private UnityEvent _entered = new UnityEvent();
    [SerializeField] private UnityEvent _released = new UnityEvent();

    public event UnityAction Entered
    {
        add => _entered.AddListener(value);
        remove => _entered.RemoveListener(value);
    }

    public event UnityAction Released
    {
        add => _released.AddListener(value);
        remove => _released.RemoveListener(value);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Scammer>(out Scammer scammer))
        {
            _entered.Invoke();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Scammer>(out Scammer scammer))
        {
            _released.Invoke();
        }
    }
}
