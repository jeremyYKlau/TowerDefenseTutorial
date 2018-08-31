using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NodeUI : MonoBehaviour {

    public GameObject uI;
    private Node target;
    public Text upgradeCost;
    public Text sellAmount;

    public Button upgradeButton;

    public void setTarget(Node targetTemp)
    {
        target = targetTemp;

        transform.position = target.getBuildPosition();

        uI.SetActive(true);
        if (!target.isUpgraded)
        {
            upgradeCost.text = "$" + target.turretBlueprint.upgradeCost;
            upgradeButton.interactable = true;
        }
        else
        {
            upgradeCost.text = "Complete";
            upgradeButton.interactable = false;
        }
        sellAmount.text = "$" + target.turretBlueprint.getSellPrice();
    }

    public void hide()
    {
        uI.SetActive(false);
    }

    public void upgrade()
    {
        target.upgradeTurret();
        BuildManager.instance.deselectNode();//don't use hide because the ui will still be selected even though not on screen anymore
    }

    public void sell()
    {
        target.sellTurret();
        BuildManager.instance.deselectNode();
    }
}
