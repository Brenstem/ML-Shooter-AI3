using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float lifetime;

    private Timer timer;

    private void Start()
    {
        timer = new Timer(lifetime);
        GetComponent<Rigidbody>().AddForce(transform.right * speed);
    }

    private void Update()
    {
        timer.DecrementTimer(Time.deltaTime);

        if (timer.Done)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Destroy(this.gameObject);
    }
}
