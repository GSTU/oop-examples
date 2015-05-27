using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Schema;
using System;

public class CharacterControllerScript : MonoBehaviour
{
	public float maxSpeed = 10f; 
	private bool isFacingRight = true;
	private Animator anim;
	private bool isGrounded = false;
	public bool isAttack=false;
	public Transform groundCheck;
	public Transform attackCheck;
	public LayerMask attackLayer;
	private float groundRadius = 0.2f;
	public LayerMask whatIsGround;
	public int i=0,score=0;
	public float timeClickAttack=0;
	public int characterHP=100;
	public trigger trig;
	public MoveNpcScript Npc;
	public string text_debag;
	public XmlDocument doc;
	bool test=true;

	private void Start()
	{
		anim = GetComponent<Animator>();
		text_debag="Xml file not found";
		try
		{

			XmlReaderSettings Settings = new XmlReaderSettings(); 
			Settings.Schemas.Add(null,Application.dataPath+"/test.xsd");
			Settings.ValidationType = ValidationType.Schema; 
			Settings.ValidationEventHandler += new ValidationEventHandler(SettingsValidationEventHandler);
			XmlReader XmlR = XmlReader.Create(Application.dataPath + "/options.xml", Settings);
			while (XmlR.Read()) { } 
			XmlDocument xDoc = new XmlDocument();
			xDoc.Load (Application.dataPath + "/options.xml");
			if(test)
			{
				transform.position=new Vector3(float.Parse(xDoc.DocumentElement.SelectSingleNode("position").ChildNodes[0].InnerText),float.Parse(xDoc.DocumentElement.SelectSingleNode("position").ChildNodes[1].InnerText));
				characterHP=int.Parse(xDoc.DocumentElement.SelectSingleNode("userhp").InnerText);
				Npc.npcHP=int.Parse(xDoc.DocumentElement.SelectSingleNode("npchp").InnerText);
				text_debag="Xml load successful";
			}
			else text_debag="Xml value error";
		}
		catch(NullReferenceException e)//instead FileNotFoundException
		{
			text_debag+="\n"+Application.dataPath;
			return;
		}
	}
	public void SettingsValidationEventHandler(object sender, ValidationEventArgs e) 
	{ 
		if (e.Severity == XmlSeverityType.Warning) 
		{ 
			text_debag="Wariang"; 
		} 
		else if (e.Severity == XmlSeverityType.Error) 
		{ 
			text_debag=e.Message;
			test=false;
		} 
	}

	private void FixedUpdate()
	{
		isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsGround); 
		anim.SetBool ("Ground", isGrounded);
		anim.SetFloat ("vSpeed", rigidbody2D.velocity.y);
		float move = Input.GetAxisRaw("Horizontal");
		anim.SetFloat("Speed", Mathf.Abs(move));
		rigidbody2D.velocity = new Vector2(move * maxSpeed, rigidbody2D.velocity.y);
		if(move < 0 && !isFacingRight)
			Flip();
		else if (move > 0 && isFacingRight)
			Flip();
		if (Math.Round (timeClickAttack, 2) <= Math.Round (Time.time, 2)) {
						anim.SetBool ("Attack", false);
						isAttack=false;
				}
		anim.SetFloat("time",Time.time);
	}

	private void Update()
	{
		if (isGrounded && Input.GetKeyDown (KeyCode.Space)) 
		{
			anim.SetBool("Ground", false);
			rigidbody2D.AddForce(new Vector2(0, 600));
		}
		if (Input.GetKeyDown (KeyCode.Mouse0)&& anim.GetFloat("Speed")<0.01)
		{
			anim.SetBool("Attack",true);
			timeClickAttack=Time.time;
			timeClickAttack+=0.5f;
			anim.SetFloat("timeClick",timeClickAttack);
			isAttack=true;
			if ((Mathf.Abs (Npc.transform.position.x-transform.position.x)) <= 2f)Npc.npcHP-=20;
			trig.isAttack=false;
			//if(Physics2D.OverlapCircle(attackCheck.position, groundRadius, attackLayer))score+=10;
		}
		if (Input.GetKeyDown (KeyCode.Escape))
						Application.Quit ();
	}
	private void Flip()
	{
		isFacingRight = !isFacingRight;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}
	void OnGUI(){
		GUI.Box (new Rect(300,10,700,75),"Score: "+score+"\nHP: "+characterHP+"\n"+text_debag);
		if(characterHP<=0)GUI.Box (new Rect(10,10,100,50),"You is dead");
		}
}