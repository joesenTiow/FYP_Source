using UnityEngine;

public class Orbit : MonoBehaviour
{
    public float speed;
    public GameObject center;

    // Update is called once per frame
    void Update()
    {
        transform.RotateAround(center.transform.position, Vector3.up, speed * Time.deltaTime);
    }
}

