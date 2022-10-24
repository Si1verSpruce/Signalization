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
        _alarm.Play();

        RestartCoroutine(_maxVolume);
    }

    private void Deactivate()
    {
        RestartCoroutine(_minVolume);
    }

    private void RestartCoroutine(float targetVolume)
    {
        if (_activeCoroutine != null)
        {
            StopCoroutine(_activeCoroutine);
        }

        _activeCoroutine = StartCoroutine(ChangeVolume(targetVolume));
    }

    private IEnumerator ChangeVolume(float targetVolume)
    {
        while (_alarm.volume != targetVolume)
        {
            _alarm.volume = Mathf.MoveTowards(_alarm.volume, targetVolume, _volumeChangeStep * Time.deltaTime);

            if (_alarm.volume == _minVolume)
            {
                _alarm.Stop();
            }

            yield return null;
        }
    }
}
