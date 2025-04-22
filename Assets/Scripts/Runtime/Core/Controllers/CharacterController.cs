using NaughtyAttributes;
using Runtime.Singletons;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Runtime.Core.Controllers
{
    public class CharacterController : MonoBehaviour
    {
        #region SerializedField Variables

        [Foldout("Player Attributes"),Range(0,10)]
        [SerializeField] private float passIntensity;

        #endregion

        private void OnEnable()
        {
            PlayerControlsSingleton.Instance.CharacterControls.Character.Pass.performed += PassAction;
            PlayerControlsSingleton.Instance.CharacterControls.Character.Move.performed += MoveAction;
        }

        private void MoveAction(InputAction.CallbackContext obj)
        {
           
        }

        private void PassAction(InputAction.CallbackContext obj)
        {
            
        }

        private void OnDisable()
        {
            PlayerControlsSingleton.Instance.CharacterControls.Character.Pass.performed -= PassAction;
            PlayerControlsSingleton.Instance.CharacterControls.Character.Move.performed -= MoveAction;
        }
    }
}