using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class OptionController : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private GameObject _optionPanel;
    [SerializeField] private Button _optionIconButton;
    [SerializeField] private Button _closeButton;
    [SerializeField] private Slider _bgmSlider;
    [SerializeField] private Slider _sfxSlider;

    [Header("Audio Mixer")]
    [SerializeField] private AudioMixer _audioMixer;

    private void Start()
    {
        _optionPanel.SetActive(false);

        _optionIconButton.onClick.AddListener(TogglePanel);
        _closeButton.onClick.AddListener(ClosePanel);

        _bgmSlider.onValueChanged.AddListener(SetBgmVolume);
        _sfxSlider.onValueChanged.AddListener(SetSfxVolume);
    }

    public void TogglePanel()
    {
        bool isOpen = !_optionPanel.activeSelf;
        _optionPanel.SetActive(isOpen);
        Time.timeScale = isOpen ? 0f : 1f;
    }

    public void ClosePanel()
    {
        _optionPanel.SetActive(false);
        Time.timeScale = 1f;
    }

    private void SetBgmVolume(float value)
    {
        _audioMixer.SetFloat("BGMVolume", Mathf.Log10(Mathf.Max(value, 0.0001f)) * 20f);
    }

    private void SetSfxVolume(float value)
    {
        _audioMixer.SetFloat("SFXVolume", Mathf.Log10(Mathf.Max(value, 0.0001f)) * 20f);
    }
}