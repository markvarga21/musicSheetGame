using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleMover : MonoBehaviour
{
    [SerializeField]
    private float obstacleMoveSpeed;

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.Translate(this.obstacleMoveSpeed * Time.deltaTime, 0, 0);
    }
}
