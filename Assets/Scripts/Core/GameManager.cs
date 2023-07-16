using System.Linq;
using Hero;
using UnityEngine;
using UnityEngine.UI;
using User;
using Views;

namespace Core
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] 
        private Button _startButton;
        [SerializeField] 
        private StartScreenView _startScreenView;
        [SerializeField] 
        private CurrencyManager _currencyManager;
        [SerializeField] 
        private HeroManager _heroManager;
        [SerializeField] 
        private LobbyView _lobbyView;

        private void Start()
        {
            _startButton.onClick.AddListener(ShowStartScreen);
            _startScreenView.Authorized += OnAuthorized;
        }

        private void ShowStartScreen()
        {
            _startScreenView.OnGameStarted();
        }

        private void OnAuthorized(UserProfile userProfile)
        {
            var selectedHeroSettings = userProfile.HeroesSettings.FirstOrDefault(hero => hero.IsSelected);
            
            _heroManager.Initialize(selectedHeroSettings);
            _currencyManager.Initialize(userProfile.Money, userProfile.Gems);
            _lobbyView.Initialize(selectedHeroSettings);
        }
    }
}