using UnityEngine;

namespace Hero
{
    public class HeroManager : MonoBehaviour
    {
        [SerializeField]
        private HeroController[] _heroPrefabs;
        [SerializeField] 
        private Transform _heroHolder;

        public void Initialize(HeroesSettings heroesSettings)
        {
            foreach (var heroPrefab in _heroPrefabs)
            {
                if (heroPrefab.HeroesSettings.PrefabId == heroesSettings.PrefabId)
                {
                    var hero = Instantiate(heroPrefab, _heroHolder);
                    hero.HeroesSettings = heroesSettings;
                }
            }
        }
    }
}