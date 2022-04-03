using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class DestinationController : MonoBehaviour
{
    public DestinationSO currentDestination;
    public GameObject portalPrefab;
    public GameObject warpParticles;


    public List<Transform> portalPositions;

    
    private List<GameObject> currentPointsOfinterests;

    private List<PortalController> portals;

    [SerializeField]
    private List<GazeInteractionController> InfoRocks;

    private Action callbackEnterDestination;

    [SerializeField]
    public WristMenuController wrist;

    
    // Start is called before the first frame update
    void Start()
    {
        portals = new List<PortalController>();
        currentPointsOfinterests = new List<GameObject>();

        for(var i = 0; i< 3; i++)
        {
            portals.Add(Instantiate(portalPrefab, portalPositions[i].transform.position, portalPositions[i].transform.rotation).GetComponent<PortalController>());
            portals[i].gameObject.SetActive(false);
        }

        callbackEnterDestination = EnterCurrentDestination;

        EnterCurrentDestination();

        GetComponent<SkyBoxFadeController>().InitialTransition(currentDestination);
    }


    [ContextMenu("EnterCurrentDestination")]
    public void EnterCurrentDestination(){
        //Spawn Points of Interest at their positions.
        for(var i = 0; i< currentDestination.InteractionPoints.Count; i++)
        {
            var temp = Instantiate(currentDestination.InteractionPoints[i], currentDestination.InteractionPoints[i].transform.position, currentDestination.InteractionPoints[i].transform.rotation);
            currentPointsOfinterests.Add(temp);
            //maybe need to be reseted?

        }

        //in the way i set it up...nomore than 3 allowed lol
        //Spawn Destination points
        for(var i = 0; i< currentDestination.destinations.Count; i++)
        {
            if(currentDestination.destinations[i].name != "EMPTY")
            {
                portals[i].Initialize(currentDestination, currentDestination.destinations[i]);
                portals[i].gameObject.SetActive(true);
            }//LEAVE NON EMPTies innactive!
            
        }


        //Spawn information points...
        for(var i = 0; i< currentDestination.infoPoints.Count; i++)
        {
            InfoRocks[i].description = currentDestination.infoPoints[i].information;
            InfoRocks[i].MoveToPosition(currentDestination.infoPoints[i].globalPos, true);
        }
    }

    public void ChangeDestination(DestinationSO toDestination){

        //get rid of interest points
        for (int i = currentPointsOfinterests.Count-1; i >= 0; i--)
        {
            if(currentPointsOfinterests[i].TryGetComponent(out PlanetController cont)){
                cont.Leave();
            }
            //Destroy(currentPointsOfinterests[i]);
            currentPointsOfinterests.RemoveAt(i);
        }
        Debug.Log("removed all points of interest " + currentPointsOfinterests.Count);
        //deactivate destination points
        for(var i = 0; i< portalPositions.Count; i++)
        {
            portals[i].gameObject.SetActive(false);
            
            
        }

        //move all infopoints back to station.
        for(var i = 0; i< currentDestination.infoPoints.Count; i++)
        {
            InfoRocks[i].MoveToPosition(new Vector3(0,-5f,0), false);
        }

        //tell skybox changer
        GetComponent<SkyBoxFadeController>().BeginTransition(currentDestination, toDestination, callbackEnterDestination);
        
        currentDestination = toDestination;
        var warp = Instantiate(warpParticles,warpParticles.transform.position, warpParticles.transform.rotation);
        Destroy(warp,8f);
    }
}

