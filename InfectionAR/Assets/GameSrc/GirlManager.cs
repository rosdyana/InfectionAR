using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GirlManager : MonoBehaviour {

    private Animation anim;
	// Use this for initialization
	void Start () {
        anim = GetComponent<Animation>();
        anim.Play("FreeVoxelGirl-jump");
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
