using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour {
    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        foreach (ContactPoint2D c in collision.contacts)
        {
            //Debug.Log(c.collider.gameObject.tag);
            if (c.collider.gameObject.tag == "Weapon")
                return;
        }

        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("Weapon Collided : " + collision.gameObject.name);
            collision.gameObject.GetComponent<PlayerObject>().ChangeHp(1);
            //Debug.Log(collision.gameObject.name + " " + collision.gameObject.GetComponent<PlayerObject>().player.hp);
        }
    }
}
