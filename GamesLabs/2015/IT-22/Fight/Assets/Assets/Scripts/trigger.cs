using UnityEngine;
using System.Collections;

public class trigger : MonoBehaviour {

	// Use this for initialization
	public bool isAttack=false;
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void OnTriggerStay2D(Collider2D a)
	{
		if (a.gameObject.layer == 9) {
						isAttack = true;
				}
	}
	void OnTriggerExit2D(Collider2D a)
	{
		if (a.gameObject.layer == 9) {
			isAttack = false;
		}
	}
}
