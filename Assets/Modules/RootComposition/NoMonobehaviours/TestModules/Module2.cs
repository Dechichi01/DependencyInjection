using UnityEngine;
using Framework.DI;

namespace Modules.RootComposition.NoMono
{
    [CreateAssetMenu(menuName = "NoMonoModules/Module2")]
    public class Module2 : ModuleSpecification
    {
        [Install(typeof(IM2Behaviour), InstallMethod.WithId, "M23")]
        [SerializeField] M2Behaviour3 m23;

        [Install(typeof(NoMonoBehaviour))]
        NoMonoBehaviour noMono;
    }
}
