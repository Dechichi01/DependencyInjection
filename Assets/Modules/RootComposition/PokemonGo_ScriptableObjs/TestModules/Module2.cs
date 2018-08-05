using UnityEngine;
using Framework.DI;

namespace Modules.RootComposition.PokemonGoScriptableObjs
{
    [CreateAssetMenu(menuName = "Modules/Module2")]
    public class Module2 : ModuleSpecification
    {
        [Install(typeof(IM2Behaviour), InstallMethod.WithId, "M23")]
        [SerializeField] M2Behaviour3 m23;
    }
}
