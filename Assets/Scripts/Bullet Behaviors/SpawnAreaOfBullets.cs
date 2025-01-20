using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnAreaOfBullets : MonoBehaviour
{
    public GameObject[] bullets; // the bullets to randomly place in the box;
    public Vector2 halfBox = new Vector2(3, 3);
    public bool useRandomDirection = false;
    public int bulletsToPlace = 10;
    // Start is called before the first frame update
    void Start()
    {
        UnityEngine.Random.InitState(this.gameObject.scene.GetHashCode() * (int)Time.timeSinceLevelLoad);

        for(int i = 0; i < bulletsToPlace; i++ ){
            int randomBulletIndex = (int)UnityEngine.Random.Range(0, bullets.Length);
            Quaternion rot;
            if(useRandomDirection){
                rot = UnityEngine.Random.rotation;
            }else{
                rot = transform.rotation;
            }
            SpawnBullet(bullets[randomBulletIndex],new Vector2(
                UnityEngine.Random.Range(-halfBox.x, halfBox.x),
                UnityEngine.Random.Range(-halfBox.y, halfBox.y)
            ), Quaternion.Euler(0, 0, rot.eulerAngles.z));
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    GameObject SpawnBullet(GameObject bullPat, Vector3 position, Quaternion rotation){
        GameObject bullet = Instantiate(
            bullPat, 
            position, 
            rotation
        );

        return bullet;
    }
}
