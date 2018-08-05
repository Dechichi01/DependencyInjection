using UnityEngine;
using Framework.DI;

public class State2 : MonoBehaviour {

    [Install(typeof(State3))]
    [SerializeField] State3 f3Prefab;

    public void Start()
    {
        Debug.Log("Feature 2 STARTING " + gameObject.GetInstanceID());
        f3Prefab.Log();
    }

    public void Log()
    {
        Debug.Log("I'm feature 2 " + gameObject.GetInstanceID());
    }
    
}
