using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollower : MonoBehaviour {
    public Transform Target;

    private Vector3 offset;

    void Start() {
        offset = new Vector3(0, 0, -10);
    }

    void Update() {
        transform.position = Target.position + offset;
    }
}
