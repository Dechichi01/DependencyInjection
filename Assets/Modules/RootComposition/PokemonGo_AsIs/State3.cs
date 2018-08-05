using UnityEngine;
using Framework.DI;

namespace Modules.RootComposition.PokemonGo
{
    public class State3 : MonoBehaviour
    {

        [Inject] IRootState f1;
        [Inject] IState2 defaultF2;
        [Inject(Id = "NotDefaultState2")] IState2 f2;

        private void Start()
        {
            Debug.Log("Feature 3 STARTING " + gameObject.GetInstanceID());
            ((State1)f1).Log();
            defaultF2.Log();
            f2.Log();
        }

        public void Log()
        {
            Debug.Log("I'm feature 3 " + gameObject.GetInstanceID());
        }
    }

}
