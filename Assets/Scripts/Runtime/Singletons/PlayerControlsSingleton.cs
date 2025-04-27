using UnityEngine;

namespace Runtime.Singletons
{
    public class PlayerControlsSingleton : MonoBehaviour
    {
        #region Singleton

        public static PlayerControlsSingleton Instance { get; private set; }

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

        #region Variables

        private static PlayerInputAction _characterControls;

        #endregion
        
        public PlayerInputAction CharacterControls => _characterControls;

        private void OnEnable()
        {
            _characterControls = new PlayerInputAction();
            _characterControls.Enable();
        }

        private void OnDisable()
        {
            _characterControls.Disable();
        }
    }
}