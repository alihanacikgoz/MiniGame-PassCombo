using UnityEngine;

namespace Runtime.Core.Controllers
{
    public class MenuController : MonoBehaviour
    {
        #region Singleton

        public static MenuController Instance { get; private set; }

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
            BackToMainMenu();
        }

        #endregion

        #region Variables
    
        [SerializeField] private GameObject[] menuItems;
    
        #endregion
        #region Costom Methods

        public void ActivateOptionsMenu()
        {
            foreach (GameObject item in menuItems)
            {
                item.SetActive(false);
            }
            menuItems[1].SetActive(true);
        }

        public void BackToMainMenu()
        {
            foreach (GameObject item in menuItems)
            {
                item.SetActive(false);
            }
            menuItems[0].SetActive(true);
        }

        #endregion
    }
}