using UnityEngine;

namespace Modules.RootComposition.PokemonGoScriptableObjs
{
    [CreateAssetMenu(menuName = "Modules/RootModule")]
    public class RootModule : ModuleSpecification, IRootModule {
        [SerializeField] Module1 m1;
    }
}
