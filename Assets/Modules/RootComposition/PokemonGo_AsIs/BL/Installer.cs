using System.Reflection;
using UnityEngine;
using Zenject;
using System.Linq;
using Framework.DI;

namespace Modules.RootComposition.PokemonGo
{
    /*
    Disadvantages:
        . Too dependent on monobehaviours
        . [Inject] attribute goes all over the code, adding dependency to Framework.DI
    Advantages:
        . Simple
     */
    public class Installer : MonoInstaller
    {
        [Install(typeof(IRootState))]
        [SerializeField] RootState rootStatePrefab;

        public override void InstallBindings()
        {
            RecursivelyBind(this);
        }

        void RecursivelyBind(MonoBehaviour root) {
            foreach (FieldInfo field in GetMonobehaviourFields(root))
            {
                var installAttribute = GetInstallAttribute(field);
                if (installAttribute != null)
                {
                    var value = field.GetValue(root) as MonoBehaviour;
                    BindPrefabToContainer(installAttribute, value);
                }

                RecursivelyBind(field.GetValue(root) as MonoBehaviour);
            }
        }

        private FieldInfo[] GetMonobehaviourFields(MonoBehaviour mono)
        {
            if (mono == null) return new FieldInfo[0];
            var fields = mono.GetType().GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);

            return fields.Where(f => typeof(MonoBehaviour).IsAssignableFrom(f.FieldType)).ToArray();
        }

        private InstallAttribute GetInstallAttribute(FieldInfo field)
        {
            var attribute = field.GetCustomAttributes(true).FirstOrDefault(a => a is InstallAttribute);

            return attribute as InstallAttribute;
        }

        private void BindPrefabToContainer(InstallAttribute attr, Object prefab)
        {
            var binding = Container.Bind(attr.InstallType);

            switch (attr.InstallMethod)
            {
                case InstallMethod.Single:
                    binding.FromComponentInNewPrefab(prefab).AsCached();
                    break;
                case InstallMethod.Transient:
                    binding.FromComponentInNewPrefab(prefab).AsTransient();
                    break;
                case InstallMethod.WithId:
                    binding.WithId(attr.Id).FromComponentInNewPrefab(prefab).AsCached();
                    break;
            }
        }
    }

}
