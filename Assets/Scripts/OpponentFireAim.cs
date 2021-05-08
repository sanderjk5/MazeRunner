using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpponentFireAim : MonoBehaviour
{

    private Transform aimTransform;
    private GameObject opponentObject;
    private GameObject rubyObject;
    private readonly float distance = .24f;
    public GameObject shotPrefab;
    private GameObject fire2;
    public int shotCounter;
    private bool shootingEnabled;
    public static Queue<float> WhenToShoot { get; set; }

    private void Awake()
    {
        opponentObject = GameObject.Find("Opponent");
        rubyObject = GameObject.Find("Ruby");
        
        //set surrounding fire active
        fire2 = RubyFireAim.FindObject(opponentObject, "OpFire");
        fire2.SetActive(true);
        aimTransform = fire2.transform;

        //set up shooting enabling relevant variables
        shotCounter = 0;
        shootingEnabled = true;
        
        //set up shooting timings
        WhenToShoot = new Queue<float>();
        GenerateShootTimings(3);
    }

    // Update is called once per frame
    private void Update()
    {
        //calculate circular position of the fire around the opponent
        Vector3 opponentPosition = opponentObject.transform.position;
        Vector3 ShootingDirection = GetShootingDirection() * distance;
        aimTransform.position = new Vector3((opponentPosition.x + ShootingDirection.x), (opponentPosition.y + ShootingDirection.y), 0);

        //let the opponent shoot at given timings 'Shoot' is called only once for every timing, since the if statement checks if WhenToShoot is empty
        if (shootingEnabled && WhenToShoot.Count!=0 && CountdownController.GameStarted && !EndBattleGameMenu.PlayerFinished && !EndBattleGameMenu.OpponentFinished)
        {
            //Invoke is called in every update() call. It loads the shoot timings instantly and call the shoot method after each timing once
            Invoke("Shoot", WhenToShoot.Dequeue());
        }
        //Disables Shooting after 3 shots.
        if (shotCounter > 2)
        {
            DisableShooting();
        }
    }

    /**
     * It basically shoots. Creates a "bullet" sent to a calculated direction.
     */
    void Shoot()
    {
        Shot2.direction = GetShootingDirection();
        Shot2.direction.Normalize();
        Instantiate(shotPrefab, opponentObject.transform.position, opponentObject.transform.rotation);
        shotCounter++;
    }

    /**
     * Generate the timings that determine when the opponent shoots
     */
    public static void GenerateShootTimings(int amount)
    {
        float Timing = 0;
        for(int i = 0; i<amount; i++)
        {
            Timing = Random.Range(0.0f,20.0f);
            WhenToShoot.Enqueue(Timing);
        }
    }

    /**
     * calculates a random direction for shooting in a fixed angle range
     */
    private Vector3 GetShootingDirection()
    {
        //create line from opponent to ruby
        Vector3 direction = rubyObject.transform.position - opponentObject.transform.position;

        //get a random angle between -4 and +4 degrees
        int signPositive = Random.Range(0, 2);
        float rand = Random.Range(1, 400)/100;
        float angle = (signPositive == 0) ? (-1) * rand : rand;
        
        //calculate new coordinates by rotating the direction vectors coordinates 
        float cosValue = Mathf.Cos(Mathf.Deg2Rad * angle);
        float sinValue = Mathf.Sin(Mathf.Deg2Rad * angle);
        float xPos = direction.x * cosValue - direction.y * sinValue;
        float yPos = direction.x * sinValue + direction.y * cosValue;

        //set new direction values
        direction.x = xPos;
        direction.y = yPos;
        direction.Normalize();
        return direction;
    }

    public void EnableShooting()
    {
        shotCounter -= 3;
        fire2.SetActive(true);
        shootingEnabled = true;
        GenerateShootTimings(3);
    }

    public void DisableShooting()
    {
        fire2.SetActive(false);
        shootingEnabled = false;
    }
}
