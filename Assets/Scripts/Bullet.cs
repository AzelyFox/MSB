using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour
{
    public float z;
    public float damage;
    public float speedMultiplier;
    public int scaleX;

	void Start ()
    {
	}

	void Update ()
    {
        transform.Translate(new Vector3(Mathf.Cos(z*Mathf.Deg2Rad),Mathf.Sin(z*Mathf.Deg2Rad),0) * Values.BULLET_SPEED *-1*scaleX* speedMultiplier*Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log("Bullet Collided"+" "+collision.gameObject.name);
        if (collision.gameObject.tag != "Weapon")
        {
            Destroy(this.gameObject);
        }

        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerObject>().ChangeHp((int)damage);
            Debug.Log(collision.gameObject.name + " " + collision.gameObject.GetComponent<PlayerObject>().player.hp);
        }

        //Destroy(this.gameObject);
    }
}
