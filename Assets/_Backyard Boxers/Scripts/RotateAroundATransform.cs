using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateAroundATransform : MonoBehaviour {

    [SerializeField] private Transform ringCenter;
    [SerializeField] private float degreesPerSec = 45;

    bool canRotate = false;

    private void OnEnable() {
        EventManager.Instance.StartListening(EventManager.Events.ShowRingGirl, OnShowRingGirl);
    }
    private void OnDisable() {
        EventManager.Instance.StopListening(EventManager.Events.ShowRingGirl, OnShowRingGirl);
    }

    // Update is called once per frame
    void Update() {
        // if(!canRotate) {
        //     return;
        // }

        transform.RotateAround(ringCenter.position, Vector3.up, degreesPerSec * Time.deltaTime);
    }

    void OnShowRingGirl() {
        canRotate = true;
    }
}