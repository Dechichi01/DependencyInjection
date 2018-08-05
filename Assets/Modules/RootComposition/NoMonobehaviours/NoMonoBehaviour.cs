using UnityEngine;

namespace Modules.RootComposition.NoMono
{
    public class NoMonoBehaviour
    {
        readonly M1Behaviour m1;
        readonly IM2Behaviour m2;

        public NoMonoBehaviour(M1Behaviour m1, IM2Behaviour m2)
        {
            this.m1 = m1;

            Debug.Log("NoMonoBehaviour was created!");
            m1.Log();
            m2.Log();
            Debug.Log("--------");
        }

        public void Log()
        {
            Debug.Log("I'm a NOMONOBehaviour!");
        }
    }
}
