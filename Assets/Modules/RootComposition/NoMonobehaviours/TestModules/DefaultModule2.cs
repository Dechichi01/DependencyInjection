using UnityEngine;
using Framework.DI;

namespace Modules.RootComposition.NoMono
{
    [CreateAssetMenu(menuName = "NoMonoModules/DefaultModule2")]
    public class DefaultModule2 : ModuleSpecification
    {
        [Install(typeof(IM2Behaviour))]
        [SerializeField] M2UniqueBehaviour m2;

        [Install(typeof(IM2Behaviour), InstallMethod.WithId, "M22")]
        [SerializeField] M2Behaviour2 m22;
    }

}