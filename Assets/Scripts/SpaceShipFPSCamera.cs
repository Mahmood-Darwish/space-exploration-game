using UnityEngine;


/// <summary>
/// FPS veiw for the spaceship when you press H. Allow it to change to normal view when pressing H.
/// </summary>
public class SpaceShipFPSCamera : MonoBehaviour
{
    [SerializeField]
    GameObject SpaceCamera;
    private void Start()
    {
        gameObject.SetActive(false);
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            SpaceCamera.SetActive(true);
            gameObject.SetActive(false);
        }
    }
}
