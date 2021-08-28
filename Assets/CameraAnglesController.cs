using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraAnglesController : MonoBehaviour
{
    private int curCamera = 1;
    private int numCameras;
    private Animator anim;
    private CinemachineStateDrivenCamera cam;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        cam = GetComponent<CinemachineStateDrivenCamera>();
    }

    private void Start()
    {
        numCameras = cam.ChildCameras.Length;
    }
    public void OnCameraButtonPressed(){
        curCamera = curCamera == numCameras ? 1 : curCamera + 1;
        anim.SetTrigger($"Camera {curCamera}");
    }
}
