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
    private Sensor _sensor;
    private Coroutine _activeCoroutine;

    private void Awake()
    {
        _alarm = GetComponent<AudioSource>();
        _alarm.volume = _minVolume;
        _sensor = GetComponentInChildren<Sensor>();
    }

    private void OnEnable()
    {
        _sensor.Entered += Activate;
        _sensor.Released += Deactivate;
    }

    private void OnDisable()
    {
        _sensor.Entered -= Activate;
        _sensor.Released -= Deactivate;
    }

    private void Activate()
    {
        StopNonNullCoroutine(_activeCoroutine);

        _alarm.Play();

        _activeCoroutine = StartCoroutine(RaiseVolume());
    }

    private void Deactivate()
    {
        StopNonNullCoroutine(_activeCoroutine);

        _activeCoroutine = StartCoroutine(ReduceVolume());
    }

    private void StopNonNullCoroutine(Coroutine coroutine)
    {

        if (_activeCoroutine != null)
        {
            StopCoroutine(coroutine);
        }
    }

    private IEnumerator RaiseVolume()
    {
        while (_alarm.volume < _maxVolume)
        {
            _alarm.volume = Mathf.MoveTowards(_alarm.volume, _maxVolume, _volumeChangeStep * Time.deltaTime);

            yield return null;
        }
    }

    private IEnumerator ReduceVolume()
    {
        while (_alarm.volume > _minVolume)
        {
            _alarm.volume = Mathf.MoveTowards(_alarm.volume, _minVolume, _volumeChangeStep * Time.deltaTime);

            if (_alarm.volume == _minVolume)
            {
                _alarm.Stop();
            }

            yield return null;
        }
    }
}
