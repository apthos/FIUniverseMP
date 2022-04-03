using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GazeInteractionController : MonoBehaviour
{
    public string description;
    public float tweenSpeed;
    public void SendDescription(){
        Debug.Log("Hello! Staring to send data");
    }

    public void CompleteDescription(){
        Debug.Log("That's all my data!");
    }

    public void CancelingDescription(){
        Debug.Log("Don't want my data anymore?");
    }

    public void MoveToPosition(Vector3 pos, bool intoField)
    {
        //if()
        LeanTween.move(gameObject, pos,tweenSpeed);
    }
}
