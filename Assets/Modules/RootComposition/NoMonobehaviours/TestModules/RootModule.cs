using UnityEngine;

namespace Modules.RootComposition.NoMono
{
    [CreateAssetMenu(menuName = "NoMonoModules/RootModule")]
    public class RootModule : ModuleSpecification, IRootModule {
        [SerializeField] Module1 m1;
    }
}
