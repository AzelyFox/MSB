using UnityEngine;
using System.Collections;

public class Player
{
    public float hp;
    public float moveSpeed;
    public float baseSpinSpeed;
    public float spinSpeed;
    private Bullet b;

    public GameObject playerObject;
    public GameManager.PLAYER_CLASS playerClass;
    private Rigidbody2D rb;
    private BoxCollider2D bc;

    public int playerIndex;

    public Player(int myIndex, GameManager.PLAYER_CLASS myClass)
    {
        this.hp = 10;
        this.playerIndex = myIndex;
        this.playerClass = myClass;
        switch (myClass)
        {
            case GameManager.PLAYER_CLASS.CLASS_SWORD:
                moveSpeed = 0.5f;
                baseSpinSpeed = 180;
                spinSpeed = 180;
                break;
            case GameManager.PLAYER_CLASS.CLASS_DAGGER:
                moveSpeed = 0.7f;
                baseSpinSpeed = 270;
                spinSpeed = 270;
                break;
            case GameManager.PLAYER_CLASS.CLASS_SHOOT:
                moveSpeed = 1.0f;
                baseSpinSpeed = 360;
                spinSpeed = 360f;
                break;

        }
    }

    public void setGameObject(GameObject _playerObject)
    {
        playerObject = _playerObject;
        rb = playerObject.GetComponent<Rigidbody2D>();
        bc = playerObject.GetComponent<BoxCollider2D>();
        playerObject.GetComponent<PlayerObject>().player = this;
        playerObject.GetComponent<PlayerObject>().AttachWeapon(playerClass);
    }

    public void setMine()
    {
        
    }

    public void onUpdate()
    {
        float targetAxis = 0;
        if (playerIndex == 0)
        {
            if (Input.GetKey(KeyCode.A))
            {
                targetAxis -= 1;
            }
            if (Input.GetKey(KeyCode.D))
            {
                targetAxis += 1;
            }

            if (Input.GetKey(KeyCode.F))
                playerObject.GetComponent<PlayerObject>().Decelerate();

            if (Input.GetKeyUp(KeyCode.F))
                spinSpeed = baseSpinSpeed ;

            if (Input.GetKey(KeyCode.G))
                playerObject.GetComponent<PlayerObject>().Accelerate();

            if (Input.GetKeyUp(KeyCode.G))
                spinSpeed = baseSpinSpeed;
        } else
        {
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                targetAxis -= 1;
            }
            if (Input.GetKey(KeyCode.RightArrow))
            {
                targetAxis += 1;
            }

            if (Input.GetKey(KeyCode.Keypad1))
                playerObject.GetComponent<PlayerObject>().Accelerate();

            if (Input.GetKeyUp(KeyCode.Keypad1))
                spinSpeed = baseSpinSpeed;

            if (Input.GetKey(KeyCode.Keypad2))
                playerObject.GetComponent<PlayerObject>().Decelerate();

            if (Input.GetKeyUp(KeyCode.Keypad2))
                spinSpeed = baseSpinSpeed;
        }
        rb.AddForce(new Vector2(targetAxis * moveSpeed * Values.MOVE_SPEED * Time.fixedDeltaTime, 0));
    }

    public void Jump()
    {
        rb.velocity = Vector2.zero;
        rb.AddForce(Vector2.up * Values.JUMP_SPEED);
    }

    /*public Vector2 Aim()
    {
        Vector2 dir;

        return dir;
    }*/
    
    /*public void Fire()
    {
        
    }*/

    /*public void Hit()
    {

    }*/

    /*private void FixedUpdate()
    {
        this.Move();
    }*/
}
