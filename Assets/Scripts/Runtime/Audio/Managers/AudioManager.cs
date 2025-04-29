using System;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Serialization;

namespace Runtime.Audio.Managers
{
    public class AudioManager : MonoBehaviour
    {
        #region Singleton

        public static AudioManager Instance { get; private set; }

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        #endregion

        #region SerializedField Variables

        [FoldoutGroup("Mixers"), SerializeField] private AudioMixer audioMixer;

        #endregion

        #region Constants

        const string MusicVolume = "MusicVolume";
        const string SfxVolume = "SFXVolume";
        const string MainMenuVolume = "MainMenuVolume";
        const string MasterVolume = "MasterVolume";

        #endregion

        #region Unity Callbacks

        private void Start()
        {
            SetAudioMixerValues();
        }

        #endregion


        #region Custom Methods

        private void SetAudioMixerValues()
        {
            audioMixer.SetFloat(MusicVolume,PlayerPrefs.GetFloat(MusicVolume));
            audioMixer.SetFloat(SfxVolume, PlayerPrefs.GetFloat(SfxVolume));
            audioMixer.SetFloat(MainMenuVolume, PlayerPrefs.GetFloat(MainMenuVolume) );
            audioMixer.SetFloat(MasterVolume, PlayerPrefs.GetFloat(MasterVolume) );
        }

        #endregion
    }
}