using UnityEngine;
using Framework.DI;

public class State3 : MonoBehaviour {

    [Inject] IRootState f1;
    [Inject] State2 f2;

    private void Start()
    {
        Debug.Log("Feature 3 STARTING " + gameObject.GetInstanceID());
        ((State1) f1).Log();
        f2.Log();
    }

    public void Log()
    {
        Debug.Log("I'm feature 3 " + gameObject.GetInstanceID());
    }
}
