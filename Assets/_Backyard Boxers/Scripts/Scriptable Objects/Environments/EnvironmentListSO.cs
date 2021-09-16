using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Environment List")]
public class EnvironmentListSO : ScriptableObject {
    public List<EnvironmentSO> environments;
}