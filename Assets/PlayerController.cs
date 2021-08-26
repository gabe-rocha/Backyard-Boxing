using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Player player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 tilt = Input.acceleration;
        player.HandleTiltInput(tilt);
        #if UNITY_EDITOR
        Debug.DrawRay(transform.position + Vector3.up, tilt, Color.blue);
        Debug.Log($"{tilt}");
        #endif
    }
}
