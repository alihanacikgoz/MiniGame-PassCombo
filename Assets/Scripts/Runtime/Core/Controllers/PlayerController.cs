using NaughtyAttributes;
using Runtime.Singletons;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Runtime.Core.Controllers
{
    public class PlayerController : MonoBehaviour
    {
        #region SerializedField Variables

        [Foldout("Player Attributes"),Range(0,10)]
        [SerializeField] private float passIntensity;

        [Foldout("Ball Attributes"), SerializeField]
        private GameObject ballPrefab;
        [Foldout("Ball Attributes"), SerializeField]
        private GameObject ballParent;
        
        [Foldout("Animation"), SerializeField] private Animator anim;

        #endregion

        private void OnEnable()
        {
            PlayerControlsSingleton.Instance.CharacterControls.Character.Pass.performed += PassAction;
            PlayerControlsSingleton.Instance.CharacterControls.Character.Move.performed += MoveAction;
            BallInstantiate();
        }

        private void BallInstantiate()
        {
            GameObject ball = Instantiate(ballPrefab, ballParent.transform);
        }

        private void MoveAction(InputAction.CallbackContext obj)
        {
           Vector2 move = obj.ReadValue<Vector2>();
           
           anim.SetBool("isWalking", move != Vector2.zero);
        }

        private void PassAction(InputAction.CallbackContext obj)
        {
            if (obj.ReadValueAsButton())
            {
                anim.SetTrigger("Kick");
            }
        }

        private void OnDisable()
        {
            PlayerControlsSingleton.Instance.CharacterControls.Character.Pass.performed -= PassAction;
            PlayerControlsSingleton.Instance.CharacterControls.Character.Move.performed -= MoveAction;
        }
    }
}