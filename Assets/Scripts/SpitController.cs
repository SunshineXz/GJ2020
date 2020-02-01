using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpitController : MonoBehaviour
{
    public float speed = 1.0f;
    public GameObject shooter;
    public float shootingTime = 5.0f;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DestroyProjectile());
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, transform.position + transform.up * Time.deltaTime * speed, 0.5f);
    }

    void OnTriggerEnter(Collider collider)
    {
        // Check if it's not himself
        if(!collider.CompareTag("Player") && shooter != collider.gameObject)
            Destroy(gameObject);
    }

    IEnumerator DestroyProjectile()
    {
        yield return new WaitForSeconds(shootingTime);

        Destroy(gameObject);
    }
}    
