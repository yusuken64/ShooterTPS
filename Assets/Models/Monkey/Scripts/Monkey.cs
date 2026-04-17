using UnityEngine;
using System.Collections;

public class Monkey : MonoBehaviour {
    private IEnumerator coroutine;
    Animator monkey;

	// Use this for initialization
	void Start ()
	{
        monkey = GetComponent<Animator>();	
	}


}
