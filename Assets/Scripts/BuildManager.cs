using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour {

    //below is basic singleton creation of BuildManager, singleton means only instantiate 1 of it
    public static BuildManager instance;//stores a reference to itself so all others can reference it

    void Awake()
    {
        if (instance!= null)
        {
            Debug.LogError("More than one BuildManager in scene!");
            return;
        }
        instance = this; //only do this if you know you only need one of the object
    }

    private TurretBlueprint turretToBuild;
    private Node selectedNode;
    public NodeUI nodeUI;

    public bool canBuild { get { return turretToBuild != null;  } }//property, only allow to get something from variable canBuild. Cannot be set
    public bool hasMoney { get { return PlayerStats.money >= turretToBuild.cost;} }

    public GameObject buildEffect;
    public GameObject sellEffect;

    public void selectNode(Node node)
    {
        if(selectedNode == node)
        {
            deselectNode();
            return;
        }

        selectedNode = node;
        turretToBuild = null;

        nodeUI.setTarget(node);
    }

    public void deselectNode()
    {
        selectedNode = null;
        nodeUI.hide();
    }

    public void selectTurretToBuild(TurretBlueprint turret)
    {
        turretToBuild = turret;
        deselectNode();
    }

    public TurretBlueprint getTurretToBuild()
    {
        return turretToBuild;
    }
}
