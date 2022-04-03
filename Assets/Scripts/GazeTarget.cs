using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
public class GazeTarget : MonoBehaviour
{
    [SerializeField] UnityEvent gazeEnterEvent;
    [SerializeField] UnityEvent gazeExitEvent;
    [SerializeField] UnityEvent gazeCompletionEvent;
    // Start is called before the first frame update

    public void GazeEnter()
    {
        gazeEnterEvent.Invoke();
    }

    public void GazeExit()
    {
        gazeExitEvent.Invoke();
    }

    public void GazeCompletion(GazeManager manager)
    {
        //lmao this is so stupid
        var wrist = manager.GetComponent<DestinationController>().wrist;
        wrist.SetText(GetComponent<GazeInteractionController>().description);
        wrist.SetWristState(true);

        gazeCompletionEvent.Invoke();
    }
 
}
