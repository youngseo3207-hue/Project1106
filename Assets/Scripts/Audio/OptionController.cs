using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class OptionController : MonoBehaviour
{
    [Header("볼륨 슬라이더")]
    [SerializeField] private Slider _bgmSlider;
    [SerializeField] private Slider _sfxSlider;

    [Header("Audio Mixer")]
    [SerializeField] private AudioMixer _audioMixer;

    private void Start()
    {
        if (_bgmSlider != null)
        {
            _bgmSlider.onValueChanged.AddListener(SetBgmVolume);
        }

        if (_sfxSlider != null)
        {
            _sfxSlider.onValueChanged.AddListener(SetSfxVolume);
        }
    }

    private void SetBgmVolume(float value)
    {
        if (_audioMixer == null) return;
        _audioMixer.SetFloat("BGMVolume", Mathf.Log10(Mathf.Max(value, 0.0001f)) * 20f);    
    }

    private void SetSfxVolume(float value)
    {
        if(_audioMixer == null) return;
        _audioMixer.SetFloat("SFXVolume", Mathf.Log10(Mathf.Max(value, 0.0001f)) * 20f);
    }
}