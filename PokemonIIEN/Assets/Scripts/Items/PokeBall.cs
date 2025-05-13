using UnityEngine;

namespace Items
{
    [CreateAssetMenu(fileName = "PokeBall", menuName = "Game/ItemOnEnemy/Pokeball")]
    public class PokeBall : ItemOnEnemy
    {
        public int capturePower;

        public override bool UseOnEnemy(Pokemon enemyPokemon)
        {
            Debug.Log(enemyPokemon);
            Debug.Log(enemyPokemon.transform.parent);
            if (enemyPokemon.transform.parent.TryGetComponent(out WildPokemon pokemon))
            {
                CombatManager currentCombat = CombatSingleton.Instance.currentCombat;
                if (currentCombat != null)
                {
                    (int, int) prob = pokemon.CaptureRate(capturePower);
                    if (Random.Range(0, prob.Item2) < prob.Item1)
                    {
                        Player player = currentCombat.Player;
                        Pokemon newPartyMember = Instantiate(pokemon.GetNonKoPokemon(), player.transform);
                        currentCombat.Player.AddNewPokemon(newPartyMember);
                        newPartyMember.HealToMax();
                        currentCombat.EndCombat();
                    }
                    return true;
                }
            
            
                Debug.Log("Can't Use Outside Combat !!");
                return false;
            }
        
            Debug.Log("Can't Catch Trainer Pokemon !!");
            return false;
        }
    }
}
