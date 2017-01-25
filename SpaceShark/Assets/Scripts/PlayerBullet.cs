using UnityEngine;
using System.Collections;

public class PlayerBullet : MonoBehaviour {

    [SerializeField]
    private float bulletSpeed = 40.0f;
    [SerializeField]
    private float bulletCulling = 100.0f;

    private Vector3 bulletPosition;

    public bool destroyThis = false;

    // Use this for initialization
    void Start () {
        bulletPosition = new Vector3();
        
	}

    void OnTriggerEnter(Collider other)
    {
        // Destroy bullet if it hits a wall
        if ((other.tag == "Obstacle") | (other.tag == "Enemy"))
        {
            destroyThis = true;
            // Debug.Log("Bullet hit" + other.tag);
            if (other.tag == "Enemy")
            {
                if (other.GetComponent<EnemyHitBox>())
                {
                    other.GetComponent<EnemyHitBox>().destroyThis = true;
                }
                else
                {
                    destroyThis = false;
                }  
            } 
        }
    }

    // Update is called once per frame
    void Update () {

        // gameObject.transform.Translate(0.0f, 0.0f, Time.deltaTime * bulletSpeed);
        bulletPosition = gameObject.transform.position;
        bulletPosition.z += Time.deltaTime * bulletSpeed;

        if (bulletPosition.z > (Ship_Movement.shipPosition.z + bulletCulling))
        {
            destroyThis = true;
        }

        gameObject.transform.position = bulletPosition;



    }

   
}
