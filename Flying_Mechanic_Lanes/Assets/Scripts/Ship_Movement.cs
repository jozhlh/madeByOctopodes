using UnityEngine;
using System.Collections;

public class Ship_Movement : MonoBehaviour {

    private float transition = 0.0f;
    private float animationDuration = 2.0f;
    [SerializeField]
    private float setShipForwardSpeed = 40.0f;
    public float shipForwardSpeed = 1.0f;
    public static float gameSpeed;
    private bool restrictSwipeVertical = true;
    private bool restrictSwipeHorizontal = true;
    private bool restrictSwipeDiagonal = true;
    static public bool restrictBullet = true;
    [SerializeField]
    private float shipMovementSpeed = 20.0f;

    [SerializeField]
    private LevelManager levelManager = null;

    private Transform shipTransform;
    public static Vector3 shipPosition = new Vector3(0, 0, 0);
    public static LaneManager.LaneInfo currentLane = new LaneManager.LaneInfo();
    public static LaneManager.LaneInfo targetLane = new LaneManager.LaneInfo();

    private bool reset;

    void Awake ()
    {
        Application.targetFrameRate = 30;
    }

    // Use this for initialization
    void Start () {
        currentLane = LaneManager.laneData[4];
        targetLane = currentLane;
        GameInput.ResetSwipe();
        //GameInput.OnTap += HandleOnTap;
        GameInput.OnSwipe += HandleOnSwipe;
        shipTransform = gameObject.GetComponent<Transform>();
        shipPosition = shipTransform.position;
        gameSpeed = shipForwardSpeed;
        reset = false;

        if (StateManager.gameState == StateManager.States.tutorial)
        {
            if (TutorialManager.shoot.enabled)
            {
                TutorialManager.shoot.enabled = false;
            }
            if (TutorialManager.horizontal.enabled)
            {
                TutorialManager.horizontal.enabled = false;
            }
            if (TutorialManager.vertical.enabled)
            {
                TutorialManager.vertical.enabled = false;
            }
            if (TutorialManager.diagonal.enabled)
            {
                TutorialManager.diagonal.enabled = false;
            }
        }
        
	}

    // Update is called once per frame
    void Update ()
    {
        if (reset)
        {
            ResetShip();
            reset = false;
        }
        if (StateManager.gameState == StateManager.States.play)
        {
            if (transition < 1.0f)
            {
                transition += Time.deltaTime * 1 / animationDuration;
        
            }
            else
            {
                shipForwardSpeed = setShipForwardSpeed;
            }
            MoveToLane();
        }
        else if (StateManager.gameState == StateManager.States.tutorial)
        {
            if (transition < 1.0f)
            {
                transition += Time.deltaTime * 1 / animationDuration;
                TutorialManager.DisableUI();
            }
            else
            {
                shipForwardSpeed = setShipForwardSpeed;
            }

            if (shipPosition.z > 300)
            {
                restrictSwipeVertical = false;
                restrictSwipeHorizontal = false;
                restrictSwipeDiagonal = false;
                restrictBullet = false;
            }
            MoveToLane();
        }
        else if (StateManager.gameState == StateManager.States.tutorial)
        {
            shipPosition = shipTransform.position;
            shipPosition.z += (shipForwardSpeed * Time.deltaTime);
            transform.position = shipPosition;
        }

        gameSpeed = shipForwardSpeed;
    }

    void OnTriggerEnter(Collider other)
    {
        if ((other.tag == "Obstacle") | (other.tag == "Laser"))
        {
            //ResetShip();
            reset = true;
            levelManager.ResetLevel();
        }

        if (StateManager.gameState == StateManager.States.tutorial)
        {
            if (other.tag == "HorizontalTutorial")
            {
                Debug.Log("horizontal enabled");
                setShipForwardSpeed = 0;
                restrictSwipeHorizontal = false;
                TutorialManager.horizontal.enabled = true;
            }
            else if (other.tag == "VerticalTutorial")
            {
                setShipForwardSpeed = 0;
                restrictSwipeVertical = false;
                TutorialManager.vertical.enabled = true;
            }
            else if (other.tag == "DiagonalTutorial")
            {
                setShipForwardSpeed = 0;
                restrictSwipeDiagonal = false;
                TutorialManager.diagonal.enabled = true;
            }
            else if (other.tag == "ShootingTutorial")
            {
                setShipForwardSpeed = 0;
                restrictSwipeVertical = false;
                restrictSwipeHorizontal = false;
                restrictSwipeDiagonal = false;
                restrictBullet = false;
                TutorialManager.shoot.enabled = true;
                GameInput.OnTap += HandleOnTap;
            }
        } 
    }

    void ResetShip()
    {
        shipPosition = new Vector3 (0, 0, -1);
        transform.position = shipPosition;
        targetLane = LaneManager.laneData[4];
        currentLane = targetLane;
        PlayerScore.Reset();
    }

    // Get the position of any new tap
    private void HandleOnTap(Vector3 position)
    {
        setShipForwardSpeed = 40;
        TutorialManager.shoot.enabled = false;
        restrictSwipeHorizontal = false;
        restrictSwipeVertical = false;
        restrictSwipeDiagonal = false;
        // Shoot bullet down lane
        GameInput.OnTap -= HandleOnTap;
    }

    // Get the direction of any new swipe
    private void HandleOnSwipe(GameInput.Direction direction)
    {
        if (StateManager.gameState == StateManager.States.tutorial)
        {
            TutorialMovement(direction);
        }
        else if (StateManager.gameState == StateManager.States.play)
        {
            GameMovement(direction);
            currentLane = LaneManager.laneData[(int)targetLane.laneID];
        }
        
        
    }

    private void TutorialMovement(GameInput.Direction direction)
    {
        if (transition > 1.0f)
        {
            if (!restrictSwipeDiagonal & (!restrictSwipeHorizontal & !restrictSwipeVertical))
            {
                GameMovement(direction);
                currentLane = LaneManager.laneData[(int)targetLane.laneID];
                setShipForwardSpeed = 40;
            }
            else if (!restrictSwipeHorizontal)
            {
                if ((direction == GameInput.Direction.E) | (direction == GameInput.Direction.W))
                {
                    GameMovement(direction);
                    currentLane = LaneManager.laneData[(int)targetLane.laneID];
                    setShipForwardSpeed = 40;
                    restrictSwipeHorizontal = true;
                    TutorialManager.horizontal.enabled = false;
                }
            }
            else if (!restrictSwipeVertical)
            {
                if ((direction == GameInput.Direction.N) | (direction == GameInput.Direction.S))
                {
                    GameMovement(direction);
                    currentLane = LaneManager.laneData[(int)targetLane.laneID];
                    setShipForwardSpeed = 40;
                    restrictSwipeVertical = true;
                    TutorialManager.vertical.enabled = false;
                }
            }
            else if (!restrictSwipeDiagonal)
            {
                if (((direction == GameInput.Direction.NE) | (direction == GameInput.Direction.NW)) | ((direction == GameInput.Direction.SE) | (direction == GameInput.Direction.SW)))
                {
                    GameMovement(direction);
                    currentLane = LaneManager.laneData[(int)targetLane.laneID];
                    setShipForwardSpeed = 40;
                    restrictSwipeDiagonal = true;
                    TutorialManager.diagonal.enabled = false;
                }
            }
            
            /*
                if (shipPosition.z < 300)
                {
                    restrictSwipeHorizontal = true;
                    if (TutorialManager.shoot.enabled)
                    {
                        TutorialManager.shoot.enabled = false;
                    }
                    else if (TutorialManager.horizontal.enabled)
                    {
                        TutorialManager.horizontal.enabled = false;
                    }
                    else if (TutorialManager.vertical.enabled)
                    {
                        TutorialManager.vertical.enabled = false;
                    }
                    else if (TutorialManager.diagonal.enabled)
                    {
                        TutorialManager.diagonal.enabled = false;
                    }
                }
            */
        }
    }

    private void GameMovement(GameInput.Direction direction)
    {
        if (transition > 1.0f)
        {
            switch (direction)
            {
                case GameInput.Direction.W:
                    //Move player left
                    if (((int)currentLane.laneID != 0) & (((int)currentLane.laneID != 3) & ((int)currentLane.laneID != 6)))
                    {
                        targetLane = LaneManager.laneData[((int)currentLane.laneID - 1)];
                    }
                    break;
                case GameInput.Direction.E:
                    //Move player Right
                    if (((int)currentLane.laneID != 2) & (((int)currentLane.laneID != 5) & ((int)currentLane.laneID != 8)))
                    {
                        targetLane = LaneManager.laneData[((int)currentLane.laneID + 1)];
                    }
                    break;
                case GameInput.Direction.N:
                    //Move player Up
                    if ((int)currentLane.laneID > 2)
                    {
                        targetLane = LaneManager.laneData[((int)currentLane.laneID - 3)];
                    }
                    break;
                case GameInput.Direction.S:
                    //Move player Down
                    if ((int)currentLane.laneID < 6)
                    {
                        targetLane = LaneManager.laneData[((int)currentLane.laneID + 3)];
                    }
                    break;
                case GameInput.Direction.NE:
                    //Move player up and right
                    if (((int)currentLane.laneID < 8) & (((int)currentLane.laneID > 2) & ((int)currentLane.laneID != 5)))
                    {
                        targetLane = LaneManager.laneData[((int)currentLane.laneID - 2)];
                    }
                    break;
                case GameInput.Direction.SE:
                    //Move player down and right
                    if (((int)currentLane.laneID < 5) & ((int)currentLane.laneID != 2))
                    {
                        targetLane = LaneManager.laneData[((int)currentLane.laneID + 4)];
                    }
                    break;
                case GameInput.Direction.SW:
                    //Move player down and left
                    if (((int)currentLane.laneID < 6) & (((int)currentLane.laneID > 0) & ((int)currentLane.laneID != 3)))
                    {
                        targetLane = LaneManager.laneData[((int)currentLane.laneID + 2)];
                    }
                    break;
                case GameInput.Direction.NW:
                    //Move player up and left
                    if (((int)currentLane.laneID > 3) & ((int)currentLane.laneID != 6))
                    {
                        targetLane = LaneManager.laneData[((int)currentLane.laneID - 4)];
                    }
                    break;
            }
        }
    }

    private void MoveToLane()
    {
      
        shipPosition = shipTransform.position;
        shipPosition.z += (shipForwardSpeed * Time.deltaTime);
        if ((shipPosition.x != targetLane.laneX) | (shipPosition.y != targetLane.laneY))
        {
            // Move toward the target position
            shipPosition.x = Mathf.SmoothStep(shipPosition.x, targetLane.laneX, Time.deltaTime * shipMovementSpeed);
            shipPosition.y = Mathf.SmoothStep(shipPosition.y, targetLane.laneY, Time.deltaTime * shipMovementSpeed);
        }
        // Set the updated position
        transform.position = shipPosition;
    } 

}
