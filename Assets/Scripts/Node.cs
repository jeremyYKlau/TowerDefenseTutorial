using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Node : MonoBehaviour {

    BuildManager buildManager;

    public AudioClip buildSfx;
    public AudioClip upgradeSfx;
    public AudioClip destroySfx;


    public Color hoverColour;
    public Color cannotBuildColour;
    public Vector3 placementOffset;

    [HideInInspector]
    public GameObject turret;
    [HideInInspector]
    public TurretBlueprint turretBlueprint;
    [HideInInspector]
    public bool isUpgraded = false;

    private Color startColour;
    private Renderer rend; //cant call renderer its a key word

    void Start()
    {
        buildManager = BuildManager.instance;
        rend = GetComponent<Renderer>();
        startColour = rend.material.color;
    }

    public Vector3 getBuildPosition()
    {
        return transform.position + placementOffset;
    }

    void OnMouseDown()
    {
        //this is to avoid building a turret on a node right below the shop buttons
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }

        if(turret != null)
        {
            buildManager.selectNode(this);
            return;
        }

        if (!buildManager.canBuild)
        {
            return;
        }
        buildTurret(buildManager.getTurretToBuild());

    }

    void buildTurret(TurretBlueprint bluePrint)
    {
        if (PlayerStats.money < bluePrint.cost)
        {
            Debug.Log("not enough money");
            return;
        }

        AudioManager.instance.playSound(buildSfx);
        PlayerStats.money -= bluePrint.cost;
        GameObject turretAlt = (GameObject)Instantiate(bluePrint.prefab, getBuildPosition(), Quaternion.identity);
        turret = turretAlt;
        turretBlueprint = bluePrint;
        GameObject effect = (GameObject)Instantiate(buildManager.buildEffect, getBuildPosition(), Quaternion.identity);
        Destroy(effect, 5f);
    }

    //only 1 upgrade because of prefab update for now, but later if i continue make more then 1 and maybe just change colours
    public void upgradeTurret()
    {
        if (PlayerStats.money < turretBlueprint.upgradeCost)
        {
            Debug.Log("not enough money to Upgrade");
            return;
        }

        PlayerStats.money -= turretBlueprint.upgradeCost;
        //get rid of old turret
        Destroy(turret);

        AudioManager.instance.playSound(upgradeSfx);

        //building new upgraded turret in it's place
        GameObject turretAlt = (GameObject)Instantiate(turretBlueprint.upgradedPrefab, getBuildPosition(), Quaternion.identity);
        turret = turretAlt;
        GameObject effect = (GameObject)Instantiate(buildManager.buildEffect, getBuildPosition(), Quaternion.identity);
        Destroy(effect, 5f);

        isUpgraded = true;
        Debug.Log("Turret upgraded");
    }

    public void sellTurret()
    {
        PlayerStats.money += turretBlueprint.getSellPrice(); //later I'd rather use total including upgrade so I'd change getsellprice in turretblueprint
        AudioManager.instance.playSound(destroySfx);

        GameObject effect = (GameObject)Instantiate(buildManager.sellEffect, getBuildPosition(), Quaternion.identity);
        Destroy(effect, 5f);

        Destroy(turret);
        turretBlueprint = null;
    }

    void OnMouseEnter() //called when mouse enters the colider(in our case node)
    {
        //this is to avoid building a turret on a node right below the shop buttons
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }

        if (buildManager.canBuild == null)
        {
            return;
        }
        if (buildManager.hasMoney) //PlayerStats.money >= turretBlueprint.cost
        {
            rend.material.color = hoverColour;
        }
        else
        {
            rend.material.color = cannotBuildColour;
        }
    }

    void OnMouseExit()
    {
        rend.material.color = startColour;
    }

}
