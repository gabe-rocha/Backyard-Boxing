using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CameraAnglesController : MonoBehaviour {
    private int curCamera = 1;
    private int numCameras;
    private Animator anim;
    private CinemachineStateDrivenCamera cam;

    private void OnEnable() {
        EventManager.Instance.StartListening(EventManager.Events.ShowFightRoster, OnShowingRoster);
        EventManager.Instance.StartListening(EventManager.Events.ShowRingGirl, OnShowRingGirl);
        EventManager.Instance.StartListening(EventManager.Events.StartFight, OnStartFight);
    }
    private void OnDisable() {
        EventManager.Instance.StopListening(EventManager.Events.ShowFightRoster, OnShowingRoster);
        EventManager.Instance.StopListening(EventManager.Events.ShowRingGirl, OnShowRingGirl);
        EventManager.Instance.StopListening(EventManager.Events.StartFight, OnStartFight);
    }

    private void Awake() {
        anim = GetComponent<Animator>();
        cam = GetComponent<CinemachineStateDrivenCamera>();
    }

    private void Start() {
        numCameras = cam.ChildCameras.Length;
    }
    // public void OnCameraButtonPressed() {
    //     curCamera = curCamera == numCameras ? 1 : curCamera + 1;
    //     anim.SetTrigger($"Camera {curCamera}");
    // }

    private void OnShowingRoster() {
        // anim.SetTrigger($"Camera Roster"); // no need
    }
    private void OnShowRingGirl() {
        anim.SetTrigger($"Camera Ring Girl");
    }
    private void OnStartFight() {
        anim.SetTrigger($"Camera Fight");
    }
}