using UnityEngine;
using System.Collections;

public class GameInput : MonoBehaviour
{
    // The accepted directions of a swipe input
    public enum Direction { NW, N, NE, W, E, SW, S, SE };

    // Delegate functions for input
    public delegate void OnTapCallback(Vector3 position);
    public delegate void OnSwipeCallback(Direction direction);

    // Callback events for received input
    public static event OnTapCallback OnTap;
    public static event OnSwipeCallback OnSwipe;

    // Available mousebuttons and the total thereof
    private enum MouseButtons { Left, Right, Middle, NumMouseButtons };

    // How many mousebuttons are available
    private const int kNumMouseButtons = (int)MouseButtons.NumMouseButtons;
    // Used for determining if a swipe was straight or diagonal
    private const float DiagonalThresholdLo = 0.462f;
    private const float DiagonalThresholdHi = 0.887f;

    // Start position of an input
    private Vector3 mStartPosition;
    // Start time of an input
    private float mStartTime;
    // If the mousebuttons are down
    private bool[] mMouseButton;
    // If the mousebuttons wer down last frame
    private bool[] mMouseButtonLast;
    // The data received this frame about user input
    private InputData thisInput;

    // Use this for initialization
    void Start()
    {
        // Create arrays for available mouse buttons and initialise to false
        mMouseButton = new bool[kNumMouseButtons];
        mMouseButtonLast = new bool[kNumMouseButtons];
        for (int count = 0; count < kNumMouseButtons; count++)
        {
            mMouseButton[count] = false;
            mMouseButtonLast[count] = false;
        }
        // Interpret touch inpust as mouse input for calculations
        Input.simulateMouseWithTouches = true;
    }

    // Update is called once per frame
    void Update()
    {
        // Cache the last frame mouse status and read in the current mouse status 
        for (int i = 0; i < kNumMouseButtons; i++)
        {
            mMouseButtonLast[i] = mMouseButton[i];
            mMouseButton[i] = Input.GetMouseButton(i);
        }

        // Initialise tap, swipe and direction status
        bool tap = false;
        bool swipe = false;
        Direction direction = Direction.W;

        // If the screen was just touched, create a new input container and store starting information
        if (MouseButtonJustPressed(MouseButtons.Left))
        {
            mStartTime = Time.time;

            thisInput = new InputData();
            thisInput.duration = 0.0f;
            thisInput.initialPosition = Input.mousePosition;
            thisInput.endPosition = Input.mousePosition;
        }
        // If the screen is still being touched, record it's latest position and the duration it has been held for
        else if (MouseButtonHeld(MouseButtons.Left))
        {
            thisInput.duration = Time.time - mStartTime;
            thisInput.endPosition = Input.mousePosition;
        }
        // If the screen is no longer being touched, calculate wether it was a tap or a swipe
        else if (MouseButtonJustReleased(MouseButtons.Left))
        {
            thisInput.endPosition = Input.mousePosition;
            thisInput.CalculateInput();
            // If the displacement was under the sensitivity threshold, it is a tap
            if (thisInput.speed < (GameSettings.sensitivity - thisInput.displacement))
            {
                tap = true;
            }
            // If it was not a tap, calculate the direction of swipe based on vector between start and end point
            else
            {
                swipe = true;
                Vector3 difference = thisInput.initialPosition - thisInput.endPosition;
                direction = CalculateDirection(-difference);
            }
        }

        

        // If a tap or a swipe was completed this frame, notify Callback Event
        if (tap || swipe)
        {
            // If it was a tap, send the position of the tap
            if ((tap && OnTap != null) && (Input.mousePosition.y > 300))
            {
                OnTap(Input.mousePosition);
            }
            // If it was a swipe send the direction of the swipe
            else if (swipe && OnSwipe != null) 
            {
                OnSwipe(direction);
            }


        }
    }

    // Reset event delegates
    public static void ResetSwipe()
    {
        OnSwipe = null;
    }

    public static void ResetTap()
    {
        if (OnTap != null)
        {
            OnTap = null;
        }
    }

    // Check if tap already has a delegate assigned
    public static bool CanAddToTap()
    {
        if (OnTap == null)
        {
            return true;
        }
        return false;
    }
    
    // Calculate the direction of the swipe according to a vector
    private Direction CalculateDirection(Vector3 input)
    {
        Direction swipeDirection = Direction.W;
        input.Normalize();
        if ((input.x > -DiagonalThresholdLo) & (input.x < DiagonalThresholdLo))
        {
            if (input.y > 0)
            {
                // N
                swipeDirection = Direction.N;
            }
            else
            {
                // S
                swipeDirection = Direction.S;
            }
        }
        else if ((input.x > DiagonalThresholdLo) & (input.x < DiagonalThresholdHi))
        {
            if (input.y > 0)
            {
                // NE
                swipeDirection = Direction.NE;
            }
            else
            {
                // SE
                swipeDirection = Direction.SE;
            }
        }
        else if ((input.x > -DiagonalThresholdHi) & (input.x < -DiagonalThresholdLo))
        {
            if (input.y > 0)
            {
                // NW
                swipeDirection = Direction.NW;
            }
            else
            {
                // SW
                swipeDirection = Direction.SW;
            }
        }
        else if (input.x > DiagonalThresholdHi)
        {
            // E
            swipeDirection = Direction.E;
        }
        else if (input.x < -DiagonalThresholdHi)
        {
            // W
            swipeDirection = Direction.W;
        }

        return swipeDirection;
    }

    // Check the status of the mousebuttons against their cached data from last frame
    private bool MouseButtonJustPressed(MouseButtons button)
    {
        return mMouseButton[(int)button] && !mMouseButtonLast[(int)button];
    }

    private bool MouseButtonJustReleased(MouseButtons button)
    {
        return !mMouseButton[(int)button] && mMouseButtonLast[(int)button];
    }

    private bool MouseButtonHeld(MouseButtons button)
    {
        return mMouseButton[(int)button] && mMouseButtonLast[(int)button];
    }
}