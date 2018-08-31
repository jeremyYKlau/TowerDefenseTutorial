using UnityEngine;

public class Shop : MonoBehaviour {

    public TurretBlueprint standardTurret;
    public TurretBlueprint missileTurret;
    public TurretBlueprint laserTurret;

    BuildManager buildManager;

    private void Start()
    {
        buildManager = BuildManager.instance;
        buildManager.selectTurretToBuild(null);
    }
    public void selectStandardTurret()
    {
        Debug.Log("Standard Turret Selected");
        buildManager.selectTurretToBuild(standardTurret);
    }
    public void selectMissileLauncher()
    {
        Debug.Log("Missile Launcher Selected");
        buildManager.selectTurretToBuild(missileTurret);
    }
    public void selectLaserBeamer()
    {
        Debug.Log("Laser Beamer Selected");
        buildManager.selectTurretToBuild(laserTurret);
    }
}
