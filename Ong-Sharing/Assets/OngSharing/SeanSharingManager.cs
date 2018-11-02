using HoloToolkit.Unity;
using HoloToolkit.Unity.InputModule;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System;



public class SeanSharingManager : Singleton<SeanSharingManager> {

    public PhotonView photonView;
    //Standard input code:::::
    //Standard input code:::::

    public static InputEventData IED;

    public static List<GameObject> indexedObjs;

    public static bool FocusTimeout = false;

    public void InputDown(int Object)
    {
        print("SeanSharingManager: Input Down Being Sent from GameObject: " + getSharedObjectFromID(Object).name);
        //print(getSharedObjectFromID(Object) + ":::::::::::::");
        photonView.RPC("InputDownRPC", PhotonTargets.Others, Object);
        StartCoroutine(reindex());
    }

    [PunRPC]
    public void InputDownRPC(int Object)
    {

        print("SeanSharingManager: Input Down RPC Recieved for object number: " + Object + ". Which is of name: " +getSharedObjectFromID(Object));

        if (getSharedObjectFromID(Object).GetComponent<ClickSharer>() != null)
            getSharedObjectFromID(Object).GetComponent<ClickSharer>().time();
        else
            print("SeanSharingManager: WARNING: There's an object without a click sharer: " + getSharedObjectFromID(Object).name);


        getSharedObjectFromID(Object).SendMessage("OnInputDown", IED);

        StartCoroutine(reindex());

    }

    IEnumerator reindex()
    {
        yield return null;
        yield return null;
        yield return null;
        GetComponent<OngSharingIDgenerator>().index();
    }

    public void InputUp(int Object)
    {
      
        photonView.RPC("InputUpRPC", PhotonTargets.Others, Object);
    }

    [PunRPC]
    public void InputUpRPC(int Object)
    {
        getSharedObjectFromID(Object).GetComponent<ClickSharer>().time();
        getSharedObjectFromID(Object).SendMessage("OnInputUp", IED);
    }

    public void InputClicked(int Object)
    {
        print("SeanSharingManager: Input Click Being Sent");
        photonView.RPC("InputClickedRPC", PhotonTargets.Others, Object);
    }

    [PunRPC]
    public void InputClickedRPC(int Object)
    {
        getSharedObjectFromID(Object).GetComponent<ClickSharer>().time();
        getSharedObjectFromID(Object).SendMessage("OnInputClicked", IED);

        print("Input Clicked RPC Recieved for object number: " + Object + ". Which is of name: " + getSharedObjectFromID(Object));
    }

    public void FocusEnter(int Object)
    {
        print("SeanSharingManager: Focus Enter Being Sent");
        photonView.RPC("FocusEnterRPC", PhotonTargets.Others, Object);
    }

    [PunRPC]
    public void FocusEnterRPC(int Object)
    {
        print("SeanSharingManager: Focused entered RPC Recieved for object number: " + Object + ". Which is of name: " + getSharedObjectFromID(Object));

        getSharedObjectFromID(Object).GetComponent<ClickSharer>().time();
        getSharedObjectFromID(Object).SendMessage("OnFocusEnter");
    }

    public void FocusExit(int Object)
    {
        print("SeanSharingManager: Focus Exit Being Sent");
        photonView.RPC("FocusExitRPC", PhotonTargets.Others, Object);
    }

    [PunRPC]
    public void FocusExitRPC(int Object)
    {
        print("SeanSharingManager: Input Exit RPC Recieved for object number: " + Object + ". Which is of name: " + getSharedObjectFromID(Object));

        getSharedObjectFromID(Object).GetComponent<ClickSharer>().time();
        getSharedObjectFromID(Object).SendMessage("OnFocusExit");
    }

    GameObject getSharedObjectFromID(int ObjectID)
    {
        var objectToReturn = gameObject; //Just set the default object to be this object

        foreach (GameObject obj in indexedObjs)
        {
            if (obj.GetComponent<ClickSharer>().sharingID == ObjectID)
            {
                objectToReturn = obj;
                return obj;
            }
        }

        return objectToReturn;
    }


    //Standard input code:::::
    //Standard input code:::::
    // Use this for initialization
    void Start () {
        photonView = GetComponent<PhotonView>();

        IED = new InputClickedEventData(EventSystem.current);

    }



    void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting)
        {
            //We own this player: send the others our data 

        }
        else
        {
            //Network player, receive data 

        }
    }
}
