using UnityEngine;
using UnityEngine.UI;

namespace Scripts.UI.View
{
    public class BackpackViewElements : MonoBehaviour
    {
        public Button ItemOne => _itemOne;
        public Button ItemTwo => _itemTwo;

        [SerializeField]
        private Button _itemOne;
        [SerializeField]
        private Button _itemTwo;
    }
}