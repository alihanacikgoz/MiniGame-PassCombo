using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

namespace Runtime.Audio.Controllers
{
    public class VolumeController : MonoBehaviour
    {
        #region SerializedField Variables

        [FoldoutGroup("Volume Sliders"), SerializeField] private Slider musicVolumeSlider;
        [FoldoutGroup("Volume Sliders"), SerializeField] private Slider soundEffectsVolumeSlider;
        [FoldoutGroup("Volume Sliders"), SerializeField] private Slider mainMenuVolumeSlider;
        [FoldoutGroup("Volume Sliders"), SerializeField] private Slider masterVolumeSlider;

        [FoldoutGroup("Mixer"), SerializeField] private AudioMixer audioMixer;

        #endregion
        
        #region Constants
        
        const string MusicVolume = "MusicVolume";
        const string SfxVolume = "SFXVolume";
        const string MainMenuVolume = "MainMenuVolume";
        const string MasterVolume = "MasterVolume";

        #endregion
        
        #region Unity Callbacks

        private void Awake()
        {
            musicVolumeSlider.onValueChanged.AddListener(SetMusicVolume);
            soundEffectsVolumeSlider.onValueChanged.AddListener(SetSFXVolume);
            mainMenuVolumeSlider.onValueChanged.AddListener(SetMainMenuVolume);
            masterVolumeSlider.onValueChanged.AddListener(SetMasterVolume);
            
            audioMixer.GetFloat(MusicVolume, out float musicVolume);
            float lineerMusicVolume = Mathf.Pow(10, musicVolume / 20);
            musicVolumeSlider.value = lineerMusicVolume;
            
            audioMixer.GetFloat(SfxVolume, out float sfxVolume);
            float lineerSfxVolume = Mathf.Pow(10, sfxVolume / 20);
            soundEffectsVolumeSlider.value = lineerSfxVolume;
            
            audioMixer.GetFloat(MainMenuVolume, out float mainMenuVolume);
            float lineerMainMenuVolume = Mathf.Pow(10, mainMenuVolume / 20);
            mainMenuVolumeSlider.value = lineerMainMenuVolume;
            
            audioMixer.GetFloat(MasterVolume, out float masterVolume);
            float lineerMasterVolume = Mathf.Pow(10, masterVolume / 20);
            masterVolumeSlider.value = lineerMasterVolume;
        }

        #endregion

        #region Custom Methods

        private void SetMusicVolume(float value)
        {
            audioMixer.SetFloat(MusicVolume,Mathf.Log10(value) * 20);
            PlayerPrefs.SetFloat(MusicVolume, Mathf.Log10(value) * 20);
        }
        
        private void SetSFXVolume(float value)
        {
            audioMixer.SetFloat(SfxVolume,Mathf.Log10(value) * 20);
            PlayerPrefs.SetFloat(SfxVolume, Mathf.Log10(value) * 20);
            
        }
        
        private void SetMainMenuVolume(float value)
        {
            audioMixer.SetFloat(MainMenuVolume,Mathf.Log10(value) * 20);
            PlayerPrefs.SetFloat(MainMenuVolume, Mathf.Log10(value) * 20);
        }
        
        private void SetMasterVolume(float value)
        {
            audioMixer.SetFloat(MasterVolume,Mathf.Log10(value) * 20);
            PlayerPrefs.SetFloat(MasterVolume, Mathf.Log10(value) * 20);
        }

        #endregion
        
        
    }
}