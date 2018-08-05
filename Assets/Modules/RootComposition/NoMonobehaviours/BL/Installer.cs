using System.Reflection;
using UnityEngine;
using Zenject;
using System.Linq;
using Framework.DI;

namespace Modules.RootComposition.NoMono
{
    /*
    -> Similar to pokemon go scriptable obj solution, but supports non mono fields

    Advantages:
        . Takes advantage from native C# constructor to avoid spreading [Inject] attribute over bll (Check NoMonoBehaviour class). UI and specific behaviours can use [Inject] attribute
        . Factories 
        . Still simple?
    Disadvantages:
        . Slower?
     */
    public class Installer : MonoInstaller
    {
        [Install(typeof(IRootModule))]
        [SerializeField] ModuleSpecification rootModule;

        public override void InstallBindings()
        {
            RecursivelyBind(rootModule);
        }

        void RecursivelyBind(object root) {
            var allFields = root.GetType()
                .GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);

            //Install all dependencies
            foreach (FieldInfo f in allFields)
            {
                if (!f.FieldType.IsClass) { continue; }

                var installAttribute = GetInstallAttribute(f);

                if (installAttribute == null) { continue; }

                if (IsAssinableFrom<MonoBehaviour>(f))
                {
                    BindMonobehaviourToContainer(installAttribute, f.GetValue(root) as MonoBehaviour);
                }
                else
                {
                    BindTypeToContainer(installAttribute, f.FieldType);
                }
            }

            //Keep binding
            foreach (FieldInfo moduleSpec in GetFields<ModuleSpecification>(root))
            {
                RecursivelyBind(moduleSpec.GetValue(root) as ScriptableObject);
            }
        }

        private bool IsAssinableFrom<T>(FieldInfo f)
        {
            return typeof(T).IsAssignableFrom(f.FieldType);
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

        private void BindMonobehaviourToContainer(InstallAttribute attr, Object prefab)
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

        private void BindTypeToContainer(InstallAttribute attr, System.Type type)
        {
            var binding = Container.Bind(attr.InstallType);

            switch (attr.InstallMethod)
            {
                case InstallMethod.Single:
                    binding.To(type).FromNew().AsCached();
                    break;
                case InstallMethod.Transient:
                    binding.To(type).FromNew().AsTransient();
                    break;
                case InstallMethod.WithId:
                    binding.WithId(attr.Id).To(type).FromNew().AsCached();
                    break;
            }
        }
    }

}
