using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabBehaviors : MonoBehaviour {

    public GameObject toolTips;
    public GameObject otherHand;

    public void Grabbing(){
        DeactivatePointer();
        DeactivateTooltips();
    }

    public void NotGrabbing(){
        ActivatePointer();
        ActivateTooltips();
        //EnableObjColliders();
    }

    void DeactivateTooltips(){
        toolTips.SetActive(false);
    }

    void ActivateTooltips(){
        toolTips.SetActive(true);
    }

    void DeactivatePointer(){
        GetComponent<VRTK.VRTK_UIPointer>().enabled = false;
        GetComponent<VRTK.VRTK_StraightPointerRenderer>().enabled = false;
        otherHand.GetComponent<VRTK.VRTK_UIPointer>().enabled = false;
        otherHand.GetComponent<VRTK.VRTK_StraightPointerRenderer>().enabled = false;
    }

    void ActivatePointer(){
        GetComponent<VRTK.VRTK_UIPointer>().enabled = true;
        GetComponent<VRTK.VRTK_StraightPointerRenderer>().enabled = true;
        otherHand.GetComponent<VRTK.VRTK_UIPointer>().enabled = true;
        otherHand.GetComponent<VRTK.VRTK_StraightPointerRenderer>().enabled = true;

    }

}
