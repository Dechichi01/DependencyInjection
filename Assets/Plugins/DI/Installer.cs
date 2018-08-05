using System.Reflection;
using UnityEngine;
using Zenject;
using System.Linq;

namespace Framework.DI
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
                    Container.Bind(installAttribute.InstallType).FromComponentInNewPrefab(value).AsSingle();
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
    }

}
