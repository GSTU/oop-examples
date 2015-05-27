using UnityEngine;
using System;
using System.Collections;

public class MoveNpcScript : MonoBehaviour {
	private Animator anim;
	private bool isFacingRight = true;
	public CharacterControllerScript Hero;
	public float timeClickAttack=0;
	//public float MegaTime;
	bool isAttack=false;
	public float move;
	public float x;
	public float y;
	public GameObject npc;
	string name="null";
	public int npcHP=100;
	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		if (!isAttack) {
						if (Hero.transform.position.x > transform.position.x) {
								move = 1f;
								rigidbody2D.velocity = new Vector2 (2f, rigidbody2D.velocity.y);
						}
						if (Hero.transform.position.x < transform.position.x) {
								move = -1f;
								rigidbody2D.velocity = new Vector2 (-2f, rigidbody2D.velocity.y);
						}
				} else
						move = 0;
		if (Math.Round (timeClickAttack, 2) <= Math.Round (Time.time, 2)) {
			isAttack=false;
			anim.SetBool ("Attack", false);
		}
		anim.SetFloat("Speed", Mathf.Abs(move));
		if ((Mathf.Abs (Hero.transform.position.x-transform.position.x)) <= 2f && !isAttack) {
						anim.SetBool ("Attack", true);
						timeClickAttack=Time.time;
						timeClickAttack+=1f;
						Hero.characterHP-=1;
						isAttack=true;
				}
		if (npcHP <= 0) {
			rigidbody2D.transform.position=new Vector3(-18,1);
			npcHP=200;
			Hero.score+=100;
				}
		if(move < 0 && !isFacingRight)
			Flip();
		else if (move > 0 && isFacingRight)
			Flip();
		//MegaTime = Time.time;
		x = Hero.transform.position.x;
		y = Hero.transform.position.y;
	}
	private void Flip()
	{
		isFacingRight = !isFacingRight;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}
	void OnGUI(){
		GUI.Box (new Rect(110,10,100,50),"NPC hit point:\n"+npcHP.ToString());
	}
}
