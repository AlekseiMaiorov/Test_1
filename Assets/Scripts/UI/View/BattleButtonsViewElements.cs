using UnityEngine;
using UnityEngine.UI;

namespace Scripts.UI.View
{
    public class BattleButtonsViewElements : MonoBehaviour
    {
        public Button Start => _start;
        public Button End => _end;
        public Button ResetHealth => _resetHealth;

        [SerializeField]
        private Button _start;
        [SerializeField]
        private Button _end;
        [SerializeField]
        private Button _resetHealth;
    }
}