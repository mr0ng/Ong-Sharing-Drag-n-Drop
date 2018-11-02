using HoloToolkit.Unity.InputModule;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickSharer : MonoBehaviour, IInputHandler, IFocusable, IInputClickHandler {

    public int sharingID;
    public bool TO = false;
    public bool isOriginal; //By default, this is false. We'll set this to true if our main script created this, to detect of this is an original ClickSharer.
    public string objectName; //name of the object, to keep track of clones

    // Use this for initialization
   void Start () {

        

    }

    void OnEnable()
    {
        StartCoroutine(CheckIfOriginal());
    }

    IEnumerator CheckIfOriginal()
    {
        yield return new WaitForSeconds(5f); //Add a short delay before checking to see if this was a cloned object or originally created by the Sharing ID Generator

        if (gameObject.name != objectName)
        {
            print("Assuming object: " + gameObject.name + " is cloned - updating sharingID for this object.");
            sharingID = SeanSharingManager.Instance.gameObject.GetComponent<OngSharingIDgenerator>().sharingID++;
            objectName = gameObject.name;

        }

        //if (isOriginal == false)
        //{
        //    print("Assuming object: " + gameObject.name + " is cloned - updating sharingID for this object.");
        //    sharingID = SeanSharingManager.Instance.gameObject.GetComponent<OngSharingIDgenerator>().sharingID++;
        //}

        ////Set isOriginal to false, so that if this object is cloned in the future - it will be caught be the if statement above.
        //isOriginal = false;
    }
	

    public void time()
    {
        TO = true;
        StartCoroutine(timeout());
    }

    IEnumerator timeout()
    {
        yield return null;
        yield return null;
        yield return null;
        TO = false;
    }


    public void OnInputDown(InputEventData eventData)
    {
        if (SeanSharingManager.IED != null)
        {
            if (!TO)
            {
                SeanSharingManager.Instance.InputDown(sharingID);
                print("Sending OnInputDown from: " + gameObject.name);
            }
        }
        else
        {
            SeanSharingManager.IED = eventData;
            InputManager.Instance.RemoveGlobalListener(gameObject);
        }
    }

    public void OnInputClicked(InputClickedEventData eventData)
    {
        if (!TO)
            SeanSharingManager.Instance.InputClicked(sharingID);
    }

    public void OnInputUp(InputEventData eventData)
    {
        if (!TO)
            SeanSharingManager.Instance.InputUp(sharingID);
    }

    public void OnFocusEnter()
    {
        if (!TO)
            SeanSharingManager.Instance.FocusEnter(sharingID);
    }

    public void OnFocusExit()
    {
        if (!TO)
            SeanSharingManager.Instance.FocusExit(sharingID);
    }
}
