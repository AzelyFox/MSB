using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerObject : MonoBehaviour {
    public GameObject weaponPrefabSword,weaponPrefabDagger,weaponPrefabShoot;
    GameObject weaponPrefab = null;
    public GameObject bulletPrefab;
    public GameObject bulletPrefabB;
    public GameObject HpText;
    public long lastFireTime = 0;
    public Player player;

    //push test 7kQon
    public void AttachWeapon(GameManager.PLAYER_CLASS playerClass)
    {
        GameObject weaponInstance = null;
        switch (playerClass)
        {
            case GameManager.PLAYER_CLASS.CLASS_SWORD:
                weaponPrefab = weaponPrefabSword;
                weaponInstance = Instantiate(weaponPrefab, transform.position + new Vector3(GetComponent<SpriteRenderer>().bounds.size.x / 2 * transform.localScale.x,
                Mathf.Abs((weaponPrefab.GetComponent<SpriteRenderer>().bounds.size.y - GetComponent<SpriteRenderer>().bounds.size.y) / 2),
                0), Quaternion.identity) as GameObject;
                break;
            case GameManager.PLAYER_CLASS.CLASS_DAGGER:
                weaponPrefab = weaponPrefabDagger;
                weaponInstance = Instantiate(weaponPrefab, transform.position + new Vector3(GetComponent<SpriteRenderer>().bounds.size.x / 2 * transform.localScale.x,
                Mathf.Abs((weaponPrefab.GetComponent<SpriteRenderer>().bounds.size.y - GetComponent<SpriteRenderer>().bounds.size.y) / 2),
                0), Quaternion.identity) as GameObject;
                Physics2D.IgnoreCollision(weaponInstance.GetComponent<Collider2D>(), GetComponent<Collider2D>());
                weaponInstance.transform.parent = this.transform;
                weaponInstance = Instantiate(weaponPrefab, transform.position + new Vector3(GetComponent<SpriteRenderer>().bounds.size.x / 2 * -1*transform.localScale.x,
                Mathf.Abs((weaponPrefab.GetComponent<SpriteRenderer>().bounds.size.y - GetComponent<SpriteRenderer>().bounds.size.y) / 2),
                0), Quaternion.identity) as GameObject;
                break;
            case GameManager.PLAYER_CLASS.CLASS_SHOOT:
                weaponPrefab = weaponPrefabShoot;
                weaponInstance = Instantiate(weaponPrefab, transform.position + new Vector3(GetComponent<SpriteRenderer>().bounds.size.x / 2 * transform.localScale.x*-1,
                0,
                0), Quaternion.identity) as GameObject;
                break;
        }
        
        Physics2D.IgnoreCollision(weaponInstance.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        weaponInstance.transform.parent = this.transform;
    }

    public void ChangeHp(int damage)
    {
        Debug.Log("ChangeHp called");
        if (GameObject.Find("GameManager").GetComponent<GameManager>().isGameOver==true)
        {
            Debug.Log("Game Over");
            return;
        }
        else
            Debug.Log("Game On");

        player.hp -= damage;
        HpText.GetComponent<Text>().text = player.hp.ToString();
        if (player.hp <= 0)
        {
            GameObject.Find("GameManager").GetComponent<GameManager>().isGameOver = true;
            //Debug.Log(GameManager.getInstance().isGameOver.ToString());
            GameObject.Find("GameOverText").GetComponent<Text>().text = "Player "+(player.playerIndex + 1).ToString() + " Defeated";
            Vector3 pos = GameObject.Find("RE").transform.position;
            //Debug.Log("RE pos:" + pos);
            pos.x = 0f;
            GameObject.Find("RE").transform.position = pos;
        }
    }

    public void Decelerate()
    {
        if (player.playerClass == GameManager.PLAYER_CLASS.CLASS_SHOOT)
        {
            Fire(0);
            return;
        }

        player.spinSpeed-=2;
        if (player.spinSpeed < player.baseSpinSpeed * 0)
        {
            player.spinSpeed+=2;
        }
    }

    public void Accelerate()
    {
        if (player.playerClass == GameManager.PLAYER_CLASS.CLASS_SHOOT)
        {
            Fire(1);
            return;
        }

        player.spinSpeed+=2;
        if (player.spinSpeed > player.baseSpinSpeed * 2.0f)
        {
            player.spinSpeed-=2;
        }
    }

    public void Fire(int mode)
    {
        GameObject bulletInstance;

        if (player.playerClass != GameManager.PLAYER_CLASS.CLASS_SHOOT)
            return;

        if((System.DateTime.Now.Ticks / 10000 ) - lastFireTime < 1000)
        {
            return;
        }

        if (mode == 0)
        {
            bulletInstance = Instantiate(bulletPrefab, weaponPrefab.transform.position + this.transform.position, Quaternion.identity) as GameObject;
            bulletInstance.GetComponent<Bullet>().speedMultiplier = 1.2f;
            bulletInstance.GetComponent<Bullet>().damage = 1.0f;
            
            Invoke("SecondFire", 0.05f);
        }
        else
        {
            bulletInstance = Instantiate(bulletPrefabB, weaponPrefab.transform.position + this.transform.position, Quaternion.identity) as GameObject;
            bulletInstance.GetComponent<Bullet>().speedMultiplier = 1.0f;
            bulletInstance.GetComponent<Bullet>().damage = 2.0f;
        }

        Physics2D.IgnoreCollision(bulletInstance.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        bulletInstance.GetComponent<Bullet>().z = transform.rotation.eulerAngles.z;
        bulletInstance.GetComponent<Bullet>().scaleX = (int)transform.localScale.x;
        lastFireTime = System.DateTime.Now.Ticks / 10000;
    }

    public void SecondFire()
    {
        GameObject bulletInstance = Instantiate(bulletPrefab, weaponPrefab.transform.position + this.transform.position, Quaternion.identity) as GameObject;
        Physics2D.IgnoreCollision(bulletInstance.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        bulletInstance.GetComponent<Bullet>().z = transform.rotation.eulerAngles.z;
        bulletInstance.GetComponent<Bullet>().speedMultiplier = 1.0f;
        bulletInstance.GetComponent<Bullet>().damage = 1.0f;
        bulletInstance.GetComponent<Bullet>().scaleX = (int)transform.localScale.x;  
    }

    void Start ()
    {
	    
	}

	void Update ()
    {
        transform.Rotate(0, 0, transform.localScale.x * -1 * player.spinSpeed * Time.deltaTime);
        
        player.onUpdate();
	}
}
