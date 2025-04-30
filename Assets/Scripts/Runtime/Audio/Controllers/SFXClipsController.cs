using System;
using System.Collections.Generic;
using Runtime.Signals;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Runtime.Audio.Controllers
{
    public class SfxClipsController : MonoBehaviour
    {
        
        #region Singleton
        
        public static SfxClipsController Instance { get; private set; }

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

        [FoldoutGroup("Audio Clips"), SerializeField] private List<AudioClip> sfxClips;
        
        [FoldoutGroup("Audio Source"), SerializeField] private AudioSource audioSource;

        #endregion
        
        #region Unity Callbacks

        private void OnEnable()
        {
            CoreUISignals.Instance.OnButtonClickAction += OnButtonClick;
        }

        private void OnDisable()
        {
            CoreUISignals.Instance.OnButtonClickAction -= OnButtonClick;       
        }

        private void OnButtonClick()
        {
            audioSource.PlayOneShot(sfxClips[0]);
        }

        #endregion
        
    }
}