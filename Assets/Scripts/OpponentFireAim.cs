using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpponentFireAim : MonoBehaviour
{

    private Transform aimTransform;
    private GameObject opponentObject;
    private readonly float distance = .24f;
    public GameObject shotPrefab;
    private GameObject fire2;
    public int shotCounter;
    private bool shootingEnabled;
    public static Queue<int> WhenToShoot { get; set; }

    private void Awake()
    {
        opponentObject = GameObject.Find("Opponent");
        fire2 = RubyFireAim.FindObject(opponentObject, "OpFire");
        Debug.Log("Fire2 name: " + fire2.name);
        fire2.SetActive(true);
        aimTransform = fire2.transform;
        shotCounter = 0;
        shootingEnabled = true;
    }

    // Update is called once per frame
    private void Update()
    {
        Vector3 mousePosition = RubyFireAim.GetMouseWorldPosition();
        Vector3 opponentPosition = opponentObject.transform.position;
        mousePosition -= opponentPosition;
        float angle = Mathf.Atan2(mousePosition.y, mousePosition.x) * Mathf.Rad2Deg;
        if (angle < 0.0f) angle += 360.0f;
        float xPos = Mathf.Cos(Mathf.Deg2Rad * angle) * distance;
        float yPos = Mathf.Sin(Mathf.Deg2Rad * angle) * distance;
        aimTransform.position = new Vector3(opponentPosition.x + xPos, opponentPosition.y + yPos - 0.2f, 0);

        if (Input.GetButtonDown("Fire1") && shootingEnabled && CountdownController.GameStarted && !EndBattleGameMenu.PlayerFinished)
        {
            Shot2.direction = RubyFireAim.GetMouseWorldPosition() - opponentPosition;
            Shot2.direction.Normalize();
            Shoot();
            shotCounter++;
        }
        if (shotCounter > 2)
        {
            DisableShooting();
        }
    }

    void Shoot()
    {
        Instantiate(shotPrefab, opponentObject.transform.position, opponentObject.transform.rotation);
        //EditorUtility.SetDirty(shotPrefab);
    }

    public static void GenerateShootTimings(int amount)
    {
        int Timing = 0;
        for(int i = 0; i<amount; i++)
        {
            Timing = 0;
            WhenToShoot.Enqueue(Timing);
        }
    }

    public void EnableShooting()
    {
        shotCounter -= 3;
        fire2.SetActive(true);
        shootingEnabled = true;
    }

    public void DisableShooting()
    {
        fire2.SetActive(false);
        shootingEnabled = false;
    }
}
