using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateAroundATransform : MonoBehaviour {

    [SerializeField] private Transform ringCenter;
    [SerializeField] private float anglesPerSec = 45;

    // Update is called once per frame
    void Update() {
        transform.RotateAround(ringCenter.position, Vector3.up, anglesPerSec * Time.deltaTime);
    }
}