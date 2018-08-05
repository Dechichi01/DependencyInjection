using System.Reflection;
using UnityEngine;
using Zenject;
using System.Linq;
using Framework.DI;

namespace Modules.RootComposition.PokemonGoScriptableObjs
{
    /*
    -> Just like PokemonGo, but using scriptable objects

    Advantages:
        . Still simple
        . Separates dependencies definitions from game logic
    Disadvantages:
        . Too dependent on scriptable objects
        . [Inject] attribute goes all over the code, adding dependency to Framework.DI
     */
    public class Installer : MonoInstaller
    {
        [Install(typeof(IRootModule))]
        [SerializeField] ModuleSpecification rootModule;

        public override void InstallBindings()
        {
            RecursivelyBind(rootModule);
        }

        void RecursivelyBind(ScriptableObject root) {

            //Install all dependencies
            foreach (FieldInfo fieldToInstall in GetFields<MonoBehaviour>(root))
            {
                var installAttribute = GetInstallAttribute(fieldToInstall);
                if (installAttribute != null)
                {
                    var value = fieldToInstall.GetValue(root) as MonoBehaviour;
                    BindPrefabToContainer(installAttribute, value);
                }
            }

            //Keep binding
            foreach (FieldInfo moduleSpec in GetFields<ModuleSpecification>(root))
            {
                RecursivelyBind(moduleSpec.GetValue(root) as ScriptableObject);
            }
        }

        private FieldInfo[] GetFields<T>(object obj)
        {
            if (obj == null) return new FieldInfo[0];
            var fields = obj.GetType().GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);

            return fields.Where(f => typeof(T).IsAssignableFrom(f.FieldType)).ToArray();
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
