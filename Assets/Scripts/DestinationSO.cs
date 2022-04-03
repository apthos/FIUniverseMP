using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(fileName = "DestinationSO", menuName = "SpaceObservatory/DestinationSO", order = 0)]
public class DestinationSO : ScriptableObject {
    public string name;
    public string description;

    public Material transitionSkybox; //1 is its own, 0 is this
    //cannot be more than 3?
    public List<DestinationSO> destinations;

    public List<GameObject> InteractionPoints;

    public List<InteractionData> infoPoints;

    //https://docs.unity3d.com/ScriptReference/Material.SetTexture.html
    public List<Texture> transitionSkyboxTextures;
}

[Serializable]
public struct InteractionData{
    public Vector3 globalPos;
    public string information;

    public InteractionData(Vector3 pos, string info) {
        this.globalPos = pos;
        this.information = info;
    }
}