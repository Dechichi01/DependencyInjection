using System.Collections;
using UnityEngine;
using Zenject;

public class WaitAndSpawnPrefab : MonoBehaviour {

    [SerializeField] GameObject prefab;

	void Start () {
        StartCoroutine(WaitAndInstantiate());
	}

    IEnumerator WaitAndInstantiate()
    {
        yield return new WaitForSeconds(5);
        Instantiate(prefab); //This throws an exception since the prefab wasn't inject
    }
}
