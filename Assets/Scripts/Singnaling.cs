using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]

public class Singnaling : MonoBehaviour
{
    [SerializeField] private float _minVolume;
    [SerializeField] private float _maxVolume;
    [SerializeField] private float _volumeChangeStep;

    private AudioSource _alarm;
    private bool _isScammerEntered;

    private void Awake()
    {
        _alarm = GetComponent<AudioSource>();
        _alarm.volume = _minVolume;
    }

    private void Update()
    {
        if (_isScammerEntered)
        {
            if (_alarm.volume == _minVolume)
            {
                _alarm.Play();
            }

            if (_alarm.volume < _maxVolume)
            {
                _alarm.volume = Mathf.MoveTowards(_alarm.volume, _maxVolume, _volumeChangeStep * Time.deltaTime);
            }
        }
        else
        {
            if (_alarm.volume  > _minVolume)
            {
                _alarm.volume = Mathf.MoveTowards(_alarm.volume, _minVolume, _volumeChangeStep * Time.deltaTime);
            }
            else
            {
                _alarm.Stop();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Scammer>(out Scammer scammer))
        {
            _isScammerEntered = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Scammer>(out Scammer scammer))
        {
            _isScammerEntered = false;
        }
    }
}
