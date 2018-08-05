using UnityEngine;
using Framework.DI;

namespace Modules.RootComposition.PokemonGoScriptableObjs
{
    public class DefaultState2 : MonoBehaviour, IState2
    {
        [Install(typeof(State3))]
        [SerializeField] State3 f3Prefab;

        public void Start()
        {
            Debug.Log("DEFAULT Feature 2 STARTING " + gameObject.GetInstanceID());
            f3Prefab.Log();
        }

        public void Log()
        {
            Debug.Log("I'm DEFAULT feature 2 " + gameObject.GetInstanceID());
        }
    }

}