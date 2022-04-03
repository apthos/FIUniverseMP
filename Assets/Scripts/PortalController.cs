using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalController : MonoBehaviour
{
    public DestinationSO currentDestination;
    public DestinationSO futureDestination;

    
    GazeInteractionController gazeInteractionController;
    GazeTarget gazeTarget;

    private void Awake() {
        gazeInteractionController = GetComponent<GazeInteractionController>();
        gazeTarget = GetComponent<GazeTarget>();
    }

    public void Initialize(DestinationSO current, DestinationSO future)
    {
        currentDestination = current;
        futureDestination = future;
        gameObject.SetActive(true);
        gazeInteractionController.description = "To " + future.name;
    }
    private void OnTriggerEnter(Collider other) {
        
        if (other.TryGetComponent(out DestinationController dest))
        {
            dest.ChangeDestination(futureDestination);
        }
    }
}
