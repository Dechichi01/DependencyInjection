using UnityEngine;
using Framework.DI;

public class State1 : RootState {

    [Install(typeof(State2))]
    [SerializeField] State2 f2Prefab;

    public void Start()
    {
        Debug.Log("Feature 1 STARTING " + gameObject.GetInstanceID());
        f2Prefab.Log();
    }

    public void Log()
    {
        Debug.Log("I'm feature 1 " + gameObject.GetInstanceID());
    }
}
