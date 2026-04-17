using UnityEngine;
using System.Collections;

public class Frog : MonoBehaviour {
    public GameObject cam;
    Animator frog;
	// Use this for initialization
	void Start () {
        frog = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        if ((Input.GetKey(KeyCode.S))||(Input.GetKey("down")))
        {
            frog.SetBool("idle", true);
            frog.SetBool("walk", false);
            frog.SetBool("turnleft", false);
            frog.SetBool("turnright", false);
            frog.SetBool("run", false);
            frog.SetBool("runleft", false);
            frog.SetBool("runright", false);
            frog.SetBool("ladderclimb", false);
        }
        if (Input.GetKey(KeyCode.W))
        {
            frog.SetBool("walk", true);
            frog.SetBool("idle", false);
            frog.SetBool("turnleft", false);
            frog.SetBool("turnright", false);
            frog.SetBool("run", false);
            frog.SetBool("runleft", false);
            frog.SetBool("runright", false);
        }
        if (Input.GetKey(KeyCode.A))
        {
            frog.SetBool("turnleft", true);
            frog.SetBool("turnright", false);
            frog.SetBool("walk", false);
            frog.SetBool("idle", false);
            frog.SetBool("run", false);
            frog.SetBool("runleft", false);
            frog.SetBool("runright", false);
        }
        if (Input.GetKey(KeyCode.D))
        {
            frog.SetBool("turnright", true);
            frog.SetBool("turnleft", false);
            frog.SetBool("walk", false);
            frog.SetBool("idle", false);
            frog.SetBool("run", false);
            frog.SetBool("runleft", false);
            frog.SetBool("runright", false);
        }
        if (Input.GetKey(KeyCode.Space))
        {
            frog.SetBool("jump", true);
            frog.SetBool("turnleft", false);
            frog.SetBool("turnright", false);
            frog.SetBool("walk", false);
            frog.SetBool("idle", false);
            frog.SetBool("run", false);
            frog.SetBool("runleft", false);
            frog.SetBool("runright", false);
            StartCoroutine("idle");
        }
        if (Input.GetKey("up"))
        {
            frog.SetBool("run", true);
            frog.SetBool("runleft", false);
            frog.SetBool("runright", false);
            frog.SetBool("walk", false);
            frog.SetBool("turnleft", false);
            frog.SetBool("turnright", false);
            frog.SetBool("idle", false);
            frog.SetBool("jump", false);
        }
        if (Input.GetKey("left"))
        {
            frog.SetBool("runleft", true);
            frog.SetBool("run", false);
            frog.SetBool("runright", false);
            frog.SetBool("turnleft", false);
            frog.SetBool("turnright", false);
            frog.SetBool("walk", false);
            frog.SetBool("idle", false);
        }
        if (Input.GetKey("right"))
        {
            frog.SetBool("runright", true);
            frog.SetBool("run", false);
            frog.SetBool("runleft", false);
            frog.SetBool("turnright", false);
            frog.SetBool("turnleft", false);
            frog.SetBool("walk", false);
            frog.SetBool("idle", false);
        }
        if (Input.GetKey(KeyCode.E))
        {
            frog.SetBool("somersault",true);
            frog.SetBool("jump", false);
            frog.SetBool("turnleft", false);
            frog.SetBool("turnright", false);
            frog.SetBool("walk", false);
            frog.SetBool("idle", false);
            frog.SetBool("run", false);
            frog.SetBool("runleft", false);
            frog.SetBool("runright", false);
            cam.GetComponent<CameraFollow>().enabled = false;
            StartCoroutine("idle");
        }
        if (Input.GetKey(KeyCode.R))
        {
            frog.SetBool("ladderclimb", true);
            frog.SetBool("walk", false);
            frog.SetBool("turnleft", false);
            frog.SetBool("turnright", false);
            frog.SetBool("run", false);
            frog.SetBool("runleft", false);
            frog.SetBool("runright", false);
            frog.SetBool("idle", false);
        }
        if (Input.GetKey(KeyCode.F))
        {
            frog.SetBool("thumbup", true);
            frog.SetBool("walk", false);
            frog.SetBool("turnleft", false);
            frog.SetBool("turnright", false);
            frog.SetBool("run", false);
            frog.SetBool("runleft", false);
            frog.SetBool("runright", false);
            frog.SetBool("idle", false);
            frog.SetBool("ladderclimb", false);
            StartCoroutine("idle");
        }
        if (Input.GetKey(KeyCode.Keypad0))
        {
            frog.SetBool("die", true);
            frog.SetBool("walk", false);
            frog.SetBool("turnleft", false);
            frog.SetBool("turnright", false);
            frog.SetBool("run", false);
            frog.SetBool("runleft", false);
            frog.SetBool("runright", false);
            frog.SetBool("idle", false);
            frog.SetBool("thumbup", false);
            frog.SetBool("ladderclimb", false);
        }
    }
    IEnumerator idle()
    {
        yield return new WaitForSeconds(0.2f);
        frog.SetBool("idle", true);
        frog.SetBool("jump", false);
        frog.SetBool("somersault", false);
        frog.SetBool("thumbup", false);
        yield return new WaitForSeconds(0.8f);
        cam.GetComponent<CameraFollow>().enabled = true;
    }
}
