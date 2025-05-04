using TMPro;
using UnityEngine;

namespace Runtime.Helpers
{
    public class VerticalText : MonoBehaviour
    {
        #region SerializedField Variables

        [SerializeField] private TextMeshProUGUI verticalText;
        [SerializeField] private string normalText;

        #endregion

        #region Unity Callbacks

        private void Start()
        {
            verticalText.text = ConvertToVertical(normalText);
        }

        private string ConvertToVertical(string text)
        {
            return string.Join("\n", text.ToCharArray());
        }

        #endregion
    }
}