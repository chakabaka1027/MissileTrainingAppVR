﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;


public class MissileManager : NetworkBehaviour {

    string animationName;
    string stage = "Close";
    
    public GameObject infoPanel;

    [Header("Info Panel Content")]
    public Components[] components;

    int panelContentsIndex = 0;
    int currentAssemblyIndex;
    int currentSubassemblyIndex;
    int reference;


    [Command]
    void CmdReturnButton(){
        RpcReturnButton();
    }

    [ClientRpc]
    public void RpcReturnButton(){
        Return();
    }

    public void ReturnButton(){
        if(!Network.isServer){
            CmdReturnButton();
        } else {
            RpcReturnButton();
        }
    }

    void Return(){
        string animationName = "";
        string[] gutParse = stage.Split('_');

        //if its an assembly
        if(gutParse.Length == 2){
            animationName = "Guts_" + gutParse[1].ToString() + "_Reverse";
        }
        //if its a subassembly
        if(gutParse.Length >= 3){
            animationName = "Guts_" + gutParse[1].ToString() + "_" + gutParse[2].ToString() + "_Reverse";
        }

        GameObject.Find("Missile").GetComponent<Animator>().Play(animationName);
        stage = "Open";
    }

    [Command]
    void CmdPlayAnimation(string animationName){
        RpcPlayAnimation(animationName);
    }

    [ClientRpc]
    public void RpcPlayAnimation(string animationName){
        GameObject.Find("Missile").GetComponent<Animator>().Play(animationName);
        stage = animationName;
        string[] gutParse = stage.Split('_');

        if(gutParse.Length == 2){
            RpcUpdateInfoPanel(int.Parse(gutParse[1]) - 1, 0);
        }

        if(gutParse.Length > 2 && int.TryParse(gutParse[2], out reference)){
            RpcUpdateInfoPanel(int.Parse(gutParse[1]) - 1, int.Parse(gutParse[2]) - 1);
        }

        if(stage == "Guts_1_Close"){
            stage = "Guts_1";
        }
    }

    public void PlayAnimation(string animationName){
        if(!Network.isServer){
            CmdPlayAnimation(animationName);
        } else {
            RpcPlayAnimation(animationName);
        }
    }

    [Command]
    void CmdUpdateInfoPanel(int componentKey, int subassemblyKey){
        RpcUpdateInfoPanel(componentKey, subassemblyKey);
    }

    [ClientRpc]
    public void RpcUpdateInfoPanel(int assemblyKey, int subassemblyKey){
        panelContentsIndex = 0;
        currentAssemblyIndex = assemblyKey;
        currentSubassemblyIndex = subassemblyKey;
        if(stage.Split('_').Length <= 2){
            infoPanel.transform.Find("Title").GetComponent<Text>().text = components[assemblyKey].title;
            infoPanel.transform.Find("Contents").GetComponent<Text>().text = components[assemblyKey].contents[panelContentsIndex];
        } else {
            infoPanel.transform.Find("Title").GetComponent<Text>().text = components[assemblyKey].subassemblies[subassemblyKey].title;
            infoPanel.transform.Find("Contents").GetComponent<Text>().text = components[assemblyKey].subassemblies[subassemblyKey].contents[panelContentsIndex];
        }
    }

    public void UpdateInfoPanel(int componentKey, int subassemblyKey){
        if(!Network.isServer){
            CmdUpdateInfoPanel(componentKey, subassemblyKey);
        } else {
            RpcUpdateInfoPanel(componentKey, subassemblyKey);
        }
    }

    [Command]
    void CmdChangePanelPage(bool forward){
        RpcChangePanelPage(forward);
    }

    [ClientRpc]
    public void RpcChangePanelPage(bool forward){
        if(forward){
            //only increase index if there is more than 1 element in contents array
            if(stage.Split('_').Length > 2 && components[currentAssemblyIndex].subassemblies[currentSubassemblyIndex].contents.Length > 1 || stage.Split('_').Length <= 2 && components[currentAssemblyIndex].contents.Length > 1){
                panelContentsIndex++;
            }
            //cycle back to first element in array when progressing passed the last element    
            if(panelContentsIndex > components[currentAssemblyIndex].contents.Length - 1){
                panelContentsIndex = 0;
            }
            //read the index and display contents
            if(stage.Split('_').Length > 2){
                infoPanel.transform.Find("Contents").GetComponent<Text>().text = components[currentAssemblyIndex].subassemblies[currentSubassemblyIndex].contents[panelContentsIndex];
            } else {
                infoPanel.transform.Find("Contents").GetComponent<Text>().text = components[currentAssemblyIndex].contents[panelContentsIndex];
            }
        } else {
            //only decrease index if there is more than 1 element in contents array
            if(stage.Split('_').Length > 2 && components[currentAssemblyIndex].subassemblies[currentSubassemblyIndex].contents.Length > 1 || stage.Split('_').Length <= 2 && components[currentAssemblyIndex].contents.Length > 1){
                panelContentsIndex--;
            }
            //cycle to last element in array when progressing passed the first element    
            if(panelContentsIndex < 0){
                panelContentsIndex = components[currentAssemblyIndex].contents.Length - 1;
            }
            //read the index and display contents
            if(stage.Split('_').Length > 2){
                infoPanel.transform.Find("Contents").GetComponent<Text>().text = components[currentAssemblyIndex].subassemblies[currentSubassemblyIndex].contents[panelContentsIndex];
            } else {
                infoPanel.transform.Find("Contents").GetComponent<Text>().text = components[currentAssemblyIndex].contents[panelContentsIndex];
            }
        }
    }

    public void ChangePanelPage(bool forward){
        if(!Network.isServer){
            CmdChangePanelPage(forward);
        } else {
            RpcChangePanelPage(forward);
        }
    }

    [System.Serializable]
    public class Components{
        public string title;
        public string[] contents;
        public Subassemblies[] subassemblies;

        [System.Serializable]
        public class Subassemblies{
            public string title;
            public string[] contents;
        }
    }
}