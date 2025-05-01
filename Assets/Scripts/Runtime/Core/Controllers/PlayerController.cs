using NaughtyAttributes;
using Runtime.Signals;
using Runtime.Singletons;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;


namespace Runtime.Core.Controllers
{
    public class PlayerController : MonoBehaviour
    {
        #region SerializedField Variables

        [Foldout("Player Attributes"), Range(0, 100), SerializeField]
        private float passIntensity;

        [Foldout("Ball Attributes"), SerializeField]
        private GameObject ballPrefab;

        [Foldout("Ball Attributes"), SerializeField]
        private GameObject ballParent;

        [Foldout("Ball Attributes"), SerializeField]
        private GameObject ballOrigin;

        [Foldout("Animation"), SerializeField] private Animator anim;

        #endregion

        #region Public Variables

        public bool passPerformer;

        #endregion

        #region Private Variables

        private Vector2 _teamMatePosition;
        private Rigidbody2D _ballRigidbody;
        private GameObject _ball;

        #endregion

        #region Unity Callbacks

        private void Start()
        {
            Assignments();
        }

        private void OnDisable()
        {
            UnAssignments();
        }

        private void Awake()
        {
            BallInstantiate();
        }

        private void Update()
        {
            if (passPerformer)
                PassToTeamMate();
        }

        #endregion

        #region Delegation Methods

        private void Assignments()
        {
            PlayerControlsSingleton.Instance.CharacterControls.Character.Pass.performed += PassAction;
            PlayerControlsSingleton.Instance.CharacterControls.Character.Move.performed += MoveAction;

            BallActionsSignals.Instance.OnCorrectPassAction += PassPerformedSuccessfully;
            BallActionsSignals.Instance.OnWrongPassAction += PassPerformedUnsuccessfully;

            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        private void OnSceneLoaded(Scene arg0, LoadSceneMode arg1)
        {
            ballPrefab = GameObject.FindGameObjectWithTag("Football");
            ballOrigin = GameObject.FindGameObjectWithTag("BallOrigin");
            ballParent = GameObject.FindGameObjectWithTag("BallParent");
        }

        private void UnAssignments()
        {
            PlayerControlsSingleton.Instance.CharacterControls.Character.Pass.performed -= PassAction;
            PlayerControlsSingleton.Instance.CharacterControls.Character.Move.performed -= MoveAction;

            BallActionsSignals.Instance.OnCorrectPassAction -= PassPerformedSuccessfully;
            BallActionsSignals.Instance.OnWrongPassAction -= PassPerformedUnsuccessfully;

            SceneManager.sceneLoaded -= OnSceneLoaded;
        }

        #endregion

        #region Custom Methods

        private void PassPerformedSuccessfully(int? points)
        {
            BallCorrection();
        }

        private void PassPerformedUnsuccessfully(int? points)
        {
            BallCorrection();
        }


        private void TryPassToTeamMate()
        {
#if UNITY_EDITOR || UNITY_STANDALONE
            Vector2 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
#elif UNITY_ANDROID || UNITY_IOS
            Vector2 worldPostion = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
#endif
            RaycastHit2D hit = Physics2D.Raycast(worldPosition, Vector2.zero);

            if (hit.collider != null && hit.collider.CompareTag("TeamMate"))
            {
                _teamMatePosition = hit.collider.transform.position;
                passPerformer = true;
            }
        }

        private void PassToTeamMate()
        {
            if (passPerformer)
            {
                Vector2 direction = (_teamMatePosition - (Vector2)ballOrigin.transform.position).normalized;

                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.Euler(0f, 0f, angle);

                _ballRigidbody.linearVelocity = direction * passIntensity;
                passPerformer = false;
            }
        }

        private void BallInstantiate()
        {
            _ball = Instantiate(ballPrefab, ballOrigin.transform.position, Quaternion.identity, ballParent.transform);
            _ballRigidbody = _ball.GetComponent<Rigidbody2D>();
        }

        private void MoveAction(InputAction.CallbackContext obj)
        {
            Vector2 move = obj.ReadValue<Vector2>();
            if (anim != null)
            {
                anim.SetBool("isWalking", move != Vector2.zero);
            }
        }

        private void BallCorrection()
        {
            _ball.transform.position = ballOrigin.transform.position;
            _ballRigidbody.linearVelocity = Vector2.zero;
        }

        private void PassAction(InputAction.CallbackContext obj)
        {
            if (obj.ReadValueAsButton())
            {
                if (anim != null)
                {
                    anim.SetTrigger("Kick");
                }

                PlayerActionsSignals.Instance.OnPlayerKickForPass?.Invoke();
                TryPassToTeamMate();
            }
        }

        #endregion
    }
}