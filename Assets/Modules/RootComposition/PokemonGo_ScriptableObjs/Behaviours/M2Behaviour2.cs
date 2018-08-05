using UnityEngine;

namespace Modules.RootComposition.PokemonGoScriptableObjs
{
    public class M2Behaviour2 : MonoBehaviour, IM2Behaviour
    {
        public void Log()
        {
            Debug.Log("I'm M2Behaviour 2222");
        }
    }

    public interface IM2Behaviour
    {
        void Log();
    }

}
