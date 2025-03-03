using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

namespace SpeedJam
{
    public class SoundSettings : MonoBehaviour
    {
        [SerializeField] private Slider _sfxSlider;
        [SerializeField] private Slider _musicSlider;
        [SerializeField] private AudioMixer _mixer;

        private const string k_SFXKey = "SFX";
        private const string k_MusicKey = "Music";
        
        private void Start()
        {
            _sfxSlider.onValueChanged.AddListener(HandleSfxSlider);
            _musicSlider.onValueChanged.AddListener(HandleMusicSlider);
        }

        private void OnDestroy()
        {
            _sfxSlider.onValueChanged.RemoveListener(HandleSfxSlider);
            _musicSlider.onValueChanged.RemoveListener(HandleMusicSlider);
        }

        private void HandleSfxSlider(float value)
        {
            _mixer.SetFloat(k_SFXKey, value > 0.0001f ? Mathf.Log10(value) * 20 : -80);
        }

        private void HandleMusicSlider(float value)
        {
            _mixer.SetFloat(k_MusicKey, value > 0.0001f ? Mathf.Log10(value) * 20 : -80);
        }
    }
}
