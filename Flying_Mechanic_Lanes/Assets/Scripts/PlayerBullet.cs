using UnityEngine;
using System.Collections;

public class PlayerBullet : MonoBehaviour {

    [SerializeField]
    private float bulletSpeed = 40.0f;

    private Vector3 bulletPosition;

    private float bulletCulling;

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
            Debug.Log("Bullet hit" + other.tag);
            if (other.tag == "Enemy")
            {
                other.GetComponent<EnemyHitBox>().destroyThis = true;
            }
            destroyThis = true;
        }
    }

    // Update is called once per frame
    void Update () {
        bulletCulling = LaneManager.lengthOfLevel;

        // gameObject.transform.Translate(0.0f, 0.0f, Time.deltaTime * bulletSpeed);
        bulletPosition = gameObject.transform.position;
        bulletPosition.z += Time.deltaTime * bulletSpeed;

        if (bulletPosition.z > (bulletCulling * 2))
        {
            destroyThis = true;
        }

        gameObject.transform.position = bulletPosition;



    }

   
}
