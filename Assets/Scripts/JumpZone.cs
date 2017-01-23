using UnityEngine;
using System.Collections;

public class JumpZone : MonoBehaviour {
    public Player player1,player2;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (collision.gameObject.name == "Player1")
            {
                player1.Jump();
            }
            if (collision.gameObject.name == "Player2")
            {
                player2.Jump();
            }
            //Debug.Log(collision.gameObject.name + " contacted");
        }
    }
}
