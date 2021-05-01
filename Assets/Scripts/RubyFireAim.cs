using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RubyFireAim : MonoBehaviour
{
    // Start is called before the first frame update
    private Transform aimTransform;
    private GameObject player;
    private readonly float distance = .24f;
    public GameObject shotPrefab;
    public int shotCounter;

    private void Awake()
    {
        aimTransform = transform.Find("Fire");
        player = GameObject.Find("Ruby");
        shotCounter = 0;
        GameObject.Find("Fire").SetActive(true);
    }

  
    // Update is called once per frame
    private void Update()
    {
        /*
        Vector3 mousePosition = Input.mousePosition;
        Vector3 aimDirection = (mousePosition - transform.position).normalized;
        float angle = Mathf.Atan2(aimDirection.x, aimDirection.y) * Mathf.Rad2Deg;
        aimTransform.eulerAngles = new Vector3(0, 0, angle);
        Debug.Log(angle);
        */
        Vector3 mousePosition = GetMouseWorldPosition();
        Vector3 playerPosition = player.transform.position;
        mousePosition -= playerPosition;
        float angle = Mathf.Atan2(mousePosition.y, mousePosition.x) * Mathf.Rad2Deg;
        if (angle < 0.0f) angle += 360.0f;
        float xPos = Mathf.Cos(Mathf.Deg2Rad * angle) * distance;
        float yPos = Mathf.Sin(Mathf.Deg2Rad * angle) * distance;
        aimTransform.position = new Vector3(playerPosition.x + xPos, playerPosition.y + yPos - 0.2f, 0);

        if (Input.GetButtonDown("Fire1"))
        {
            Shot.direction = GetMouseWorldPosition() - playerPosition;
            Shot.direction.Normalize();
            Shoot();
            //shotCounter++;
        }
        if (shotCounter > 2)
        {
            GameObject.Find("Fire").SetActive(false);
            this.enabled = false;
        }
    }

    void Shoot()
    {
        Instantiate(shotPrefab, player.transform.position, player.transform.rotation);
    }

    // Get Mouse Position in World with Z = 0f
    public static Vector3 GetMouseWorldPosition()
    {
        Vector3 vec = GetMouseWorldPositionWithZ(Input.mousePosition, Camera.main);
        vec.z = 0f;
        return vec;
    }

    public static Vector3 GetMouseWorldPositionWithZ(Vector3 screenPosition, Camera worldCamera)
    {
        Vector3 worldPosition = worldCamera.ScreenToWorldPoint(screenPosition);
        return worldPosition;
    }
}
