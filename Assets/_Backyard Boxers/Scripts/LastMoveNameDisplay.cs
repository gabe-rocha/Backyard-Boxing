using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LastMoveNameDisplay : MonoBehaviour {

#region Public Fields

#endregion

#region Private Serializable Fields
    [SerializeField] private TextMeshProUGUI textLastMovelName;
#endregion

#region Private Fields

#endregion

    private void OnEnable() {
        EventManager.Instance.StartListeningWithStringParam(EventManager.Events.LastMove, OnLastMove);
    }
    private void OnDisable() {
        EventManager.Instance.StopListeningWithStringParam(EventManager.Events.LastMove, OnLastMove);
    }

#region MonoBehaviour CallBacks
    void Awake() {
        //component = GetComponent<Component>();
        //if(component == null) {
        //Debug.LogError($"{name} is missing a component");
        //}
        textLastMovelName.text = "";
    }

    void Start() {

    }

    void Update() {

    }
#endregion

#region Private Methods
    void OnLastMove(string moveName) {
        textLastMovelName.gameObject.transform.localScale = Vector3.zero;
        textLastMovelName.text = moveName;
        LeanTween.scale(textLastMovelName.gameObject, Vector3.one, 0.25f).setEase(LeanTweenType.easeInElastic);
    }
#endregion

#region Public Methods

#endregion
}