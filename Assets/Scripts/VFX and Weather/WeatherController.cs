using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.HighDefinition;
using UnityEngine.VFX;

public class WeatherController : MonoBehaviour
{
    [SerializeField] private WeatherTypes _currentWeather;
    [SerializeField] private WindZone _windController;
    [SerializeField] private LocalVolumetricFog _fog;
    [SerializeField] private VisualEffect _rainEffect;
    [SerializeField] private ParticleSystem _rainHitFX;
    [SerializeField] private ParticleSystem _leaves, _wind;
    [SerializeField] private AudioSource _rainAudio, _windAudio;


    private void Start()
    {
        ReadWeather(_currentWeather);
    }
    
    public void ChangeWeatherType(WeatherTypes weatherType)
    {
        _currentWeather = weatherType;
        ReadWeather(weatherType);
    }

    private void ReadWeather(WeatherTypes weatherType)
    {
        ToggleRainDrops(weatherType.HasFlag(WeatherTypes.Rain));
        ToggleWind(weatherType.HasFlag(WeatherTypes.Windy));
        ToggleFog(weatherType.HasFlag(WeatherTypes.Foggy));
    }

    private void ToggleRainDrops(bool value)
    {
        if (value)
        {
            _rainEffect.SetFloat("Spawn Rate", 1000);
            _rainHitFX.Play();
            _rainAudio.Play();
        }
        else
        {
            _rainEffect.SetFloat("Spawn Rate", 0);
            _rainHitFX.Stop();
            _rainAudio.Stop();
        }
    }

    private void ToggleWind(bool value)
    {
        if(value)
        {
            _windController.windMain = 10;
            _wind.Play();
            _leaves.Play();
            _windAudio.Play();
        }
        else
        {
            _windController.windMain = 0;
            _wind.Stop();
            _leaves.Stop();
            _windAudio.Stop();
        }
    }

    private void ToggleFog(bool value)
    {
       _fog.gameObject.SetActive(value);
    }
}
