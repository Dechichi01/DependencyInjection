using UnityEngine;
using Framework.DI;

namespace Modules.RootComposition.PokemonGoScriptableObjs
{
    [CreateAssetMenu(menuName = "Modules/DefaultModule1")]
    public class Module1 : ModuleSpecification
    {
        [SerializeField] DefaultModule2 defaultF2Prefab;
        [SerializeField] Module2 f2Prefab;

        [Install(typeof(M1Behaviour))]
        [SerializeField] M1Behaviour m1;
    }
}

