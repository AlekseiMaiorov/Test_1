using TMPro;
using UnityEngine;

namespace Scripts.UI.View
{
    public class CharacterBattleStatusViewElements : MonoBehaviour
    {
        public TextMeshProUGUI State => _state;
        public TextMeshProUGUI Timer => _timer;

        [SerializeField]
        private TextMeshProUGUI _state;
        [SerializeField]
        private TextMeshProUGUI _timer;
    }
}