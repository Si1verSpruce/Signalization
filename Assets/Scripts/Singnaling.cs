using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]

public class Singnaling : MonoBehaviour
{
    [SerializeField] private float _alarmMinVolume;
    [SerializeField] private float _alarmMaxVolume;
    [SerializeField] private float _alarmVolumeChangeStep;

    private AudioSource _alarm;
    private bool _isScammerEntered;

    private void Awake()
    {
        _alarm = GetComponent<AudioSource>();
        _alarm.volume = _alarmMinVolume;
    }

    private void Update()
    {
        if (_isScammerEntered)
        {
            if (_alarm.volume == _alarmMinVolume)
            {
                _alarm.Play();
            }

            if (_alarm.volume < _alarmMaxVolume)
            {
                _alarm.volume = Mathf.MoveTowards(_alarm.volume, _alarmMaxVolume, _alarmVolumeChangeStep * Time.deltaTime);
            }
        }
        else
        {
            if (_alarm.volume  > _alarmMinVolume)
            {
                _alarm.volume = Mathf.MoveTowards(_alarm.volume, _alarmMinVolume, _alarmVolumeChangeStep * Time.deltaTime);
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
