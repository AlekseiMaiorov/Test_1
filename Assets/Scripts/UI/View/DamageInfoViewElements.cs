using TMPro;
using UnityEngine;

namespace Scripts.UI.View
{
    public class DamageInfoViewElements : MonoBehaviour
    {
        public TextMeshProUGUI DamageValue => _damageValue;

        [SerializeField]
        private TextMeshProUGUI _damageValue;
    }
}