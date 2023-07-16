using TMPro;
using UnityEngine;

namespace Money
{
    public class CurrencyView : MonoBehaviour
    {
        [SerializeField] 
        private TextMeshProUGUI _moneyLabel;
        [SerializeField] 
        private TextMeshProUGUI _gemsLabel;

        public void UpdateMoneyView(int value)
        {
            _moneyLabel.text = value.ToString();
        }
        
        public void UpdateGemsView(int value)
        {
            _gemsLabel.text = value.ToString();
        }
    }
}