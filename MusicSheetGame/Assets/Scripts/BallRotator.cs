using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallRotator : MonoBehaviour
{
    private float ballSpeed;

    // Start is called before the first frame update
    void Start()
    {
        this.ballSpeed = 150.0F;
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.Rotate(0, 0, this.ballSpeed * Time.deltaTime);
    }

    public void changeDiretion() {
        this.ballSpeed *= -1;
    }
}
