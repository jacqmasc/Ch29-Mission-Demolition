using UnityEngine;
using System.Collections;

public class Goal : MonoBehaviour {

	static public bool goalMet = false;

	void OnTriggerEnter(Collider other)
	{
		//check if a projectile hit the goal
		if (other.gameObject.tag == "Projectile") 
		{
			Goal.goalMet = true;
			//set alpha color higher
			Color c = renderer.material.color;
			c.a = 1;
			renderer.material.color = c;
		}
	}
}
