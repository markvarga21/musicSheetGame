using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleMover : MonoBehaviour
{
    [SerializeField]
    private float obstacleMoveSpeed;

    private void Start() {
        this.obstacleMoveSpeed = 4f;
    }

    void Update()
    {
        this.transform.Translate(this.obstacleMoveSpeed * Time.deltaTime, 0, 0);
    }

    public void setObstacleMoveSpeed(float speed) {
        this.obstacleMoveSpeed = speed;
        Debug.Log("Obstacle speed: " + this.obstacleMoveSpeed.ToString());
    }
}
