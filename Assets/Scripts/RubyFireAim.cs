using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class RubyFireAim : MonoBehaviour
{
    // Start is called before the first frame update
    private Transform aimTransform;
    private GameObject player;
    private readonly float distance = .24f;
    public GameObject shotPrefab;
    private GameObject fire;
    public int shotCounter;

    private bool shootingEnabled;

    private void Awake()
    {
        //Debug.Log(shotPrefab.name);
        //shotPrefab = Resources.Load("Prefabs/shot") as GameObject;
        //Debug.Log(shotPrefab.name);
        //aimTransform = transform.Find("Fire");
        if (MainScript.UseShooter)
        {
            player = GameObject.Find("Ruby");
            fire = FindObject(player, "Fire");
            fire.SetActive(true);
            aimTransform = fire.transform;
            shotCounter = 0;
            shootingEnabled = true;
        }
    }

    //created because GameObject.Find only find active GameObjects, but we want to find "Fire" even when it's inactive
    public static GameObject FindObject(GameObject parent, string name)
    {
        Transform[] trs = parent.GetComponentsInChildren<Transform>(true);
        foreach (Transform t in trs)
        {
            if (t.name == name)
            {
                return t.gameObject;
            }
        }
        return null;
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
        if (MainScript.UseShooter)
        {
            Vector3 mousePosition = GetMouseWorldPosition();
            Vector3 playerPosition = player.transform.position;
            mousePosition -= playerPosition;
            float angle = Mathf.Atan2(mousePosition.y, mousePosition.x) * Mathf.Rad2Deg;
            if (angle < 0.0f) angle += 360.0f;
            float xPos = Mathf.Cos(Mathf.Deg2Rad * angle) * distance;
            float yPos = Mathf.Sin(Mathf.Deg2Rad * angle) * distance;
            aimTransform.position = new Vector3(playerPosition.x + xPos, playerPosition.y + yPos - 0.2f, 0);

            if (Input.GetButtonDown("Fire1") && shootingEnabled && CountdownController.GameStarted && !EndBattleGameMenu.PlayerFinished && !GameMenu.gameMenuIsActivated)
            {
                Shot.direction = GetMouseWorldPosition() - playerPosition;
                Shot.direction.Normalize();
                Shoot();
                shotCounter++;
            }
            if (shotCounter > 2)
            {
                DisableShooting();
            }
        }
    }

    void Shoot()
    {
        Instantiate(shotPrefab, player.transform.position, player.transform.rotation);
        //EditorUtility.SetDirty(shotPrefab);
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

    public void EnableShooting()
    {
        shotCounter -= 3;
        fire.SetActive(true);
        shootingEnabled = true;
    }

    public void DisableShooting()
    {
        fire.SetActive(false);
        shootingEnabled = false;
    }
}
