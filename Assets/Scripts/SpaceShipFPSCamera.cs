using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// FPS veiw for the spaceship when you press H. Allow it to change to normal view when pressing H.
/// Also, it has code for minpulating the HUD. 
/// </summary>
public class SpaceShipFPSCamera : MonoBehaviour
{
    [SerializeField]
    GameObject SpaceCamera;
    [SerializeField]
    Text Distance;
    [SerializeField]
    Text planetName;
    [SerializeField]
    Text Speed;
    [SerializeField]
    Image Marker;
    [SerializeField]
    Camera cam;

    // Support for calculations.
    GameObject[] Planets;
    int currentDistance;
    string currentPlanet;
    Transform target;
    int index;

    private void Start()
    {
        gameObject.SetActive(false);
        Planets = GameObject.FindGameObjectsWithTag("Planet");
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            SpaceCamera.SetActive(true);
            gameObject.SetActive(false);
        }
        currentDistance = (int)(Planets[0].transform.position - transform.position).magnitude;
        currentPlanet = Planets[0].name;
        index = 0;
        for (int i = 1; i < Planets.Length; i++)
        {
            if((int)(Planets[i].transform.position - transform.position).magnitude < currentDistance)
            {
                currentDistance = (int)(Planets[i].transform.position - transform.position).magnitude;
                currentPlanet = Planets[i].name;
                index = i;
            }
        }
        Distance.text = "Distance to closest planet: " + currentDistance;
        planetName.text = "Closet Planet: " + currentPlanet;
        Speed.text = "Speed: " + transform.parent.GetComponent<Rigidbody>().velocity.magnitude;

        //  Let the point marker point to the closest planet.
        float minX = Marker.GetPixelAdjustedRect().width / 2;
        float minY = Marker.GetPixelAdjustedRect().height / 2;
        float maxX = Screen.width - minX;
        float maxY = Screen.height - minY;
        target = Planets[index].transform;
        Vector2 pos = cam.WorldToScreenPoint(target.position);
        if(Vector3.Dot((target.position - transform.position), transform.forward) < 0)
        {
            if (pos.x < Screen.width / 2)
            {
                pos.x = maxX;
            }
            else
            {
                pos.x = minX;
            }
        }
        pos.x = Mathf.Clamp(pos.x, minX, maxX);
        pos.y = Mathf.Clamp(pos.y, minY, maxY);
        Marker.transform.position = pos;
    }
}
