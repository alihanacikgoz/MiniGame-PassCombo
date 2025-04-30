using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Runtime.Audio.Controllers
{
    public class BackgroundMusicController : MonoBehaviour
    {
        #region Singleton
    
        public static BackgroundMusicController Instance { get; private set; }

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }
            Instance = this;
        }

        #endregion
    
        #region SerializedField Variables
    
        [FoldoutGroup("Music Clips"), SerializeField] private List<AudioClip> musicClips;
    
        [FoldoutGroup("Audio Source"), SerializeField] private AudioSource audioSource;
    
        #endregion
    
        #region Unity Callbacks

        private void Start()
        {
            audioSource.resource = musicClips[Random.Range(0, musicClips.Count)];
            audioSource.Play();
        }

        #endregion
    }
}
