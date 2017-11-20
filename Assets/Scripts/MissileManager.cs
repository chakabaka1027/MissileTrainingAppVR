﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MissileManager : MonoBehaviour {

    string animationName;
    string stage = "Close";
    
    public Button closeButton, openButton, guts1, guts2, guts3, guts4;
    public GameObject infoPanel;

    public GameObject guts1Obj, guts2Obj, guts3Obj, guts4Obj;

    public Components[] components;

    int panelContentsIndex = 0;
    int currentPanelIndex;
	// Use this for initialization
	void Start () {
		if(stage == "Close"){
            closeButton.gameObject.transform.parent.gameObject.SetActive(false);
            openButton.gameObject.transform.parent.gameObject.SetActive(true);
            guts1.gameObject.transform.parent.gameObject.SetActive(false);
            guts2.gameObject.transform.parent.gameObject.SetActive(false);
            guts3.gameObject.transform.parent.gameObject.SetActive(false);
            guts4.gameObject.transform.parent.gameObject.SetActive(false);
            infoPanel.SetActive(false);
            
            guts1Obj.GetComponent<BoxCollider>().enabled = false;
            guts2Obj.GetComponent<BoxCollider>().enabled = false;
            guts3Obj.GetComponent<BoxCollider>().enabled = false;
            guts4Obj.GetComponent<BoxCollider>().enabled = false;
    

        }
	}
	
	// Update is called once per frame
	void Update () {

	}

    public void ReturnButton(){
        StartCoroutine("Return");
    }

    IEnumerator Return(){
        string animationName = "";

        if(stage == "Guts1"){
            animationName = "Guts1 Reverse";
        }
        if(stage == "Guts2"){
            animationName = "Guts2 Reverse";
        }
        if(stage == "Guts3"){
            animationName = "Guts3 Reverse";
        }
        if(stage == "Guts4"){
            animationName = "Guts4 Reverse";
        }

        GameObject.Find("Missile").GetComponent<Animator>().Play(animationName);
        stage = "Open";

        openButton.gameObject.transform.parent.gameObject.SetActive(false);
        infoPanel.SetActive(false);

        yield return new WaitForSeconds(1.1f);
        closeButton.gameObject.transform.parent.gameObject.SetActive(true);
        guts1.gameObject.transform.parent.gameObject.SetActive(true);
        guts2.gameObject.transform.parent.gameObject.SetActive(true);
        guts3.gameObject.transform.parent.gameObject.SetActive(true);
        guts4.gameObject.transform.parent.gameObject.SetActive(true);
    }

    public void PlayAnimation(string animationName){

        GameObject.Find("Missile").GetComponent<Animator>().Play(animationName);
        stage = animationName;
        StopCoroutine("AlterButtons");
        StartCoroutine("AlterButtons");
    }

    IEnumerator AlterButtons() {
		if(stage == "Close"){
            closeButton.gameObject.transform.parent.gameObject.SetActive(false);
            guts1.gameObject.transform.parent.gameObject.SetActive(false);
            guts2.gameObject.transform.parent.gameObject.SetActive(false);
            guts3.gameObject.transform.parent.gameObject.SetActive(false);
            guts4.gameObject.transform.parent.gameObject.SetActive(false);
            infoPanel.SetActive(false);
            guts1Obj.GetComponent<BoxCollider>().enabled = false;
            guts2Obj.GetComponent<BoxCollider>().enabled = false;
            guts3Obj.GetComponent<BoxCollider>().enabled = false;
            guts4Obj.GetComponent<BoxCollider>().enabled = false;

            yield return new WaitForSeconds(1.5f);
            openButton.gameObject.transform.parent.gameObject.SetActive(true);
        }
		if(stage == "Open"){
            openButton.gameObject.transform.parent.gameObject.SetActive(false);
            yield return new WaitForSeconds(1.5f);
            closeButton.gameObject.transform.parent.gameObject.SetActive(true);
            guts1.gameObject.transform.parent.gameObject.SetActive(true);
            guts2.gameObject.transform.parent.gameObject.SetActive(true);
            guts3.gameObject.transform.parent.gameObject.SetActive(true);
            guts4.gameObject.transform.parent.gameObject.SetActive(true);

            guts1Obj.GetComponent<BoxCollider>().enabled = true;
            guts2Obj.GetComponent<BoxCollider>().enabled = true;
            guts3Obj.GetComponent<BoxCollider>().enabled = true;
            guts4Obj.GetComponent<BoxCollider>().enabled = true;

            infoPanel.SetActive(false);
        }
        if(stage == "Guts1" || stage == "Guts2" || stage == "Guts3" || stage == "Guts4"){
            openButton.gameObject.transform.parent.gameObject.SetActive(false);
            closeButton.gameObject.transform.parent.gameObject.SetActive(false);
            guts1.gameObject.transform.parent.gameObject.SetActive(false);
            guts2.gameObject.transform.parent.gameObject.SetActive(false);
            guts3.gameObject.transform.parent.gameObject.SetActive(false);
            guts4.gameObject.transform.parent.gameObject.SetActive(false);
            infoPanel.SetActive(true);
            
            //parse stage string to get the componentKey that allows info panel to be updated
            string[] gutParse = stage.Split('s');
            UpdateInfoPanel(int.Parse(gutParse[1]) - 1);
        }
    }

    void UpdateInfoPanel(int componentKey){
        panelContentsIndex = 0;
        currentPanelIndex = componentKey;
        infoPanel.transform.Find("Title").GetComponent<Text>().text = components[componentKey].title;
        //draw the test
        infoPanel.transform.Find("Contents").GetComponent<Text>().text = components[componentKey].contents[panelContentsIndex];

        //StartCoroutine(infoPanel.transform.Find("Contents").GetComponent<Typing>().TypeIn(components[componentKey].contents[panelContentsIndex]));
    }

    public void ChangePanelPage(bool forward){
        if(forward){
            panelContentsIndex++;
            if(panelContentsIndex > components[currentPanelIndex].contents.Length - 1){
                panelContentsIndex = 0;
            }
            infoPanel.transform.Find("Contents").GetComponent<Text>().text = components[currentPanelIndex].contents[panelContentsIndex];
            //StartCoroutine(infoPanel.transform.Find("Contents").GetComponent<Typing>().TypeIn(components[currentPanelIndex].contents[panelContentsIndex]));
        } else {
            panelContentsIndex--;
            if(panelContentsIndex < 0){
                panelContentsIndex = components[currentPanelIndex].contents.Length - 1;
            }
            infoPanel.transform.Find("Contents").GetComponent<Text>().text = components[currentPanelIndex].contents[panelContentsIndex];
            //StartCoroutine(infoPanel.transform.Find("Contents").GetComponent<Typing>().TypeIn(components[currentPanelIndex].contents[panelContentsIndex]));
        }
    }

    [System.Serializable]
    public class Components{
        public string title;
        public string[] contents;
    }




}
