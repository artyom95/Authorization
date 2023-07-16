using Hero;
using TMPro;
using UnityEngine;

namespace Views
{
    public class LobbyView : MonoBehaviour
    {
        [SerializeField] 
        private TextMeshProUGUI _nameText;
        [SerializeField] 
        private TextMeshProUGUI _levelText;
        [SerializeField] 
        private TextMeshProUGUI _experienceText;

        public void Initialize(HeroesSettings heroesSettings)
        {
            _nameText.text = heroesSettings.Name;
            _levelText.text = heroesSettings.Level.ToString();
            _experienceText.text = heroesSettings.Experience.ToString();
        }
    }
}