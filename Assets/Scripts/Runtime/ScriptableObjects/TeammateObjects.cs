using UnityEngine;

namespace Runtime.ScriptableObjects
{
    [CreateAssetMenu(fileName = "TeammateObjects", menuName = "NFC/TeammateObjects")]
    public class TeammateObjects : ScriptableObject
    {
        public string id;
        public GameObject teammatePrefab;
        
    }
}
