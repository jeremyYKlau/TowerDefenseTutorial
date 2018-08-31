using UnityEngine;

public class CameraController : MonoBehaviour {

    public float panSpeed = 30f;
    public float panBorderThickness = 10f;

    /*//unused panning values
    public float panMaxX = 35f;
    public float panMinX = -35f;
    public float panMaxZ = 40f;
    public float panMinZ = -80f;
    */

    private Vector3 defaultCameraPos;

    public float scrollSpeed = 5f;
    public float zoomMin = 10f;
    public float zoomMax = 80f;

    //made just so that you can reset camera position if you mess it up more for debugging
    void Start()
    {
        defaultCameraPos = transform.position;    
    }

    void Update () {

        if (GameManager.gameIsOver)
        {
            this.enabled = false;
            return;
        }
        
        if (Input.GetKey("w") || Input.mousePosition.y >= Screen.height - panBorderThickness)
        {
            transform.Translate(Vector3.forward * panSpeed * Time.deltaTime, Space.World); //vec3 forward = (0,0,1)
        }
        if (Input.GetKey("s") || Input.mousePosition.y <= panBorderThickness)
        {
            transform.Translate(Vector3.back * panSpeed * Time.deltaTime, Space.World);
        }
        if (Input.GetKey("a") || Input.mousePosition.x <= panBorderThickness)
        {
            transform.Translate(Vector3.left * panSpeed * Time.deltaTime, Space.World);
        }
        if (Input.GetKey("d") || Input.mousePosition.x >= Screen.width - panBorderThickness)
        {
            transform.Translate(Vector3.right * panSpeed * Time.deltaTime, Space.World);
        }

        if (Input.GetKey("r"))
        {
            transform.position = defaultCameraPos;
        }

        float scroll = Input.GetAxis("Mouse ScrollWheel");

        Vector3 pos = transform.position;
        pos.y -= scroll * 350 * scrollSpeed * Time.deltaTime;
        pos.y = Mathf.Clamp(pos.y, zoomMin, zoomMax);

        transform.position = pos;
    }
}
