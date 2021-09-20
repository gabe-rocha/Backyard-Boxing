using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventManager : MonoBehaviour {

    public enum Events {
        OnGameManagerReady,
        OnGameStarted,
        OnGamePaused,
        OnGameUnpaused,
        OnGameOver,
        ButtonBackPressed,
        ButtonPrevPressed,
        ButtonNextPressed,
        ButtonPlayPressed,
        ButtonPausePressed,
        ButtonOptionsPressed,
        ButtonQuitPressed,
        ButtonCharacterCustomizationPressed,
        ShowFightRoster,
        ShowRingGirl,
        StartFight,
        FightStarted
    }

    private Dictionary<Events, UnityEvent> simpleEventDictionary = new Dictionary<Events, UnityEvent>();
    private Dictionary<Events, UnityEvent<int>> paramIntEventDictionary = new Dictionary<Events, UnityEvent<int>>();
    private Dictionary<Events, UnityEvent<float>> paramFloatEventDictionary = new Dictionary<Events, UnityEvent<float>>();
    private Dictionary<Events, UnityEvent<string>> paramStringEventDictionary = new Dictionary<Events, UnityEvent<string>>();
    private Dictionary<Events, UnityEvent<GameObject>> paramGOEventDictionary = new Dictionary<Events, UnityEvent<GameObject>>();
    private Dictionary<Events, UnityEvent<Vector3>> paramVec3EventDictionary = new Dictionary<Events, UnityEvent<Vector3>>();

    public static EventManager Instance { get; private set; }

    private void Awake() {
        if(Instance == null) {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        } else {
            Destroy(gameObject);
            return;
        }
    }

    //========================
    public void StartListening(Events eventName, UnityAction listener) {
        UnityEvent thisEvent = null;

        if(simpleEventDictionary.TryGetValue(eventName, out thisEvent)) {
            thisEvent.AddListener(listener);
        } else {
            thisEvent = new UnityEvent();
            thisEvent.AddListener(listener);
            simpleEventDictionary.Add(eventName, thisEvent);
        }
    }

    public void StartListeningWithGOParam(Events eventName, UnityAction<GameObject> listener) {
        UnityEvent<GameObject> thisParamEvent = null;
        if(paramGOEventDictionary.TryGetValue(eventName, out thisParamEvent)) {
            thisParamEvent.AddListener(listener);
        } else {
            thisParamEvent = new UnityEvent<GameObject>();
            thisParamEvent.AddListener(listener);
            paramGOEventDictionary.Add(eventName, thisParamEvent);
        }
    }

    public void StartListeningWithIntParam(Events eventName, UnityAction<int> listener) {
        UnityEvent<int> thisParamEvent = null;
        if(paramIntEventDictionary.TryGetValue(eventName, out thisParamEvent)) {
            thisParamEvent.AddListener(listener);
        } else {
            thisParamEvent = new UnityEvent<int>();
            thisParamEvent.AddListener(listener);
            paramIntEventDictionary.Add(eventName, thisParamEvent);
        }
    }
    public void StartListeningWithFloatParam(Events eventName, UnityAction<float> listener) {
        UnityEvent<float> thisParamEvent = null;
        if(paramFloatEventDictionary.TryGetValue(eventName, out thisParamEvent)) {
            thisParamEvent.AddListener(listener);
        } else {
            thisParamEvent = new UnityEvent<float>();
            thisParamEvent.AddListener(listener);
            paramFloatEventDictionary.Add(eventName, thisParamEvent);
        }
    }

    public void StartListeningWithStringParam(Events eventName, UnityAction<string> listener) {
        UnityEvent<string> thisParamEvent = null;
        if(paramStringEventDictionary.TryGetValue(eventName, out thisParamEvent)) {
            thisParamEvent.AddListener(listener);
        } else {
            thisParamEvent = new UnityEvent<string>();
            thisParamEvent.AddListener(listener);
            paramStringEventDictionary.Add(eventName, thisParamEvent);
        }
    }

    public void StartListeningWithVec3Param(Events eventName, UnityAction<Vector3> listener) {
        UnityEvent<Vector3> thisParamEvent = null;
        if(paramVec3EventDictionary.TryGetValue(eventName, out thisParamEvent)) {
            thisParamEvent.AddListener(listener);
        } else {
            thisParamEvent = new UnityEvent<Vector3>();
            thisParamEvent.AddListener(listener);
            paramVec3EventDictionary.Add(eventName, thisParamEvent);
        }
    }

    //========================
    public void StopListening(Events eventName, UnityAction listener) {
        if(Instance == null)return;
        UnityEvent thisEvent = null;
        if(simpleEventDictionary.TryGetValue(eventName, out thisEvent)) {
            thisEvent.RemoveListener(listener);
        }
    }

    public void StopListeningWithGOParam(Events eventName, UnityAction<GameObject> listener) {
        if(Instance == null)return;
        UnityEvent<GameObject> thisParamEvent = null;
        if(paramGOEventDictionary.TryGetValue(eventName, out thisParamEvent)) {
            thisParamEvent.RemoveListener(listener);
        }
    }
    public void StopListeningWithIntParam(Events eventName, UnityAction<int> listener) {
        if(Instance == null)return;
        UnityEvent<int> thisParamEvent = null;
        if(paramIntEventDictionary.TryGetValue(eventName, out thisParamEvent)) {
            thisParamEvent.RemoveListener(listener);
        }
    }
    public void StopListeningWithFloatParam(Events eventName, UnityAction<float> listener) {
        if(Instance == null)return;
        UnityEvent<float> thisParamEvent = null;
        if(paramFloatEventDictionary.TryGetValue(eventName, out thisParamEvent)) {
            thisParamEvent.RemoveListener(listener);
        }
    }
    public void StopListeningWithStringParam(Events eventName, UnityAction<string> listener) {
        if(Instance == null)return;
        UnityEvent<string> thisParamEvent = null;
        if(paramStringEventDictionary.TryGetValue(eventName, out thisParamEvent)) {
            thisParamEvent.RemoveListener(listener);
        }
    }

    public void StopListeningWithVec3Param(Events eventName, UnityAction<Vector3> listener) {
        if(Instance == null)return;
        UnityEvent<Vector3> thisParamEvent = null;
        if(paramVec3EventDictionary.TryGetValue(eventName, out thisParamEvent)) {
            thisParamEvent.RemoveListener(listener);
        }
    }

    //========================
    public void TriggerEvent(Events eventName) {
        UnityEvent thisEvent = null;
        if(simpleEventDictionary.TryGetValue(eventName, out thisEvent)) {
            thisEvent.Invoke();
        }
    }

    public void TriggerEventWithGOParam(Events eventName, GameObject go) {
        UnityEvent<GameObject> thisParamEvent = null;
        if(paramGOEventDictionary.TryGetValue(eventName, out thisParamEvent)) {
            thisParamEvent.Invoke(go);
        }
    }

    public void TriggerEventWithIntParam(Events eventName, int i) {
        UnityEvent<int> thisParamEvent = null;
        if(paramIntEventDictionary.TryGetValue(eventName, out thisParamEvent)) {
            thisParamEvent.Invoke(i);
        }
    }
    public void TriggerEventWithFloatParam(Events eventName, float value) {
        UnityEvent<float> thisParamEvent = null;
        if(paramFloatEventDictionary.TryGetValue(eventName, out thisParamEvent)) {
            thisParamEvent.Invoke(value);
        }
    }
    public void TriggerEventWithStringParam(Events eventName, string s) {
        UnityEvent<string> thisParamEvent = null;
        if(paramStringEventDictionary.TryGetValue(eventName, out thisParamEvent)) {
            thisParamEvent.Invoke(s);
        }
    }

    public void TriggerEventWithVec3Param(Events eventName, Vector3 vec3) {
        UnityEvent<Vector3> thisParamEvent = null;
        if(paramVec3EventDictionary.TryGetValue(eventName, out thisParamEvent)) {
            thisParamEvent.Invoke(vec3);
        }
    }
}