using UnityEngine;
using System.Collections;

public class GameInput : MonoBehaviour
{
    public enum Direction { NW, N, NE, W, E, SW, S, SE };

    public delegate void OnTapCallback(Vector3 position);
    public delegate void OnSwipeCallback(Direction direction);

    public static event OnTapCallback OnTap;
    public static event OnSwipeCallback OnSwipe;

    private enum MouseButtons
    {
        Left,
        Right,
        Middle,

        NumMouseButtons
    };

    private const int kNumMouseButtons = (int)MouseButtons.NumMouseButtons;

    private Vector3 mStartPosition;
    private float mStartTime;
    private bool[] mMouseButton;
    private bool[] mMouseButtonLast;
    private bool mIsPotentiallyTapping;
    private bool mIsPotentiallySwiping;

    private const float TapMoveThreshold = 50.0f;
    private const float TapDuration = 0.5f;
    private const float SwipeDuration = 1.0f;

    private const float DiagonalThresholdLo = 0.462f;
    private const float DiagonalThresholdHi = 0.887f;

    InputData thisInput;
    [SerializeField]
    private float sensitivity = 200.0f;

    void Start()
    {
        mMouseButton = new bool[kNumMouseButtons];
        mMouseButtonLast = new bool[kNumMouseButtons];
        for (int count = 0; count < kNumMouseButtons; count++)
        {
            mMouseButton[count] = false;
            mMouseButtonLast[count] = false;
        }

        Input.simulateMouseWithTouches = true;
    }

    void Update()
    {
        // Cache the last frame mouse status and read in the current mouse status 
        for (int count = 0; count < kNumMouseButtons; count++)
        {
            mMouseButtonLast[count] = mMouseButton[count];
            mMouseButton[count] = Input.GetMouseButton(count);
        }

        bool tap = false;
        bool swipe = false;

        Direction direction = Direction.W;

        // Detect different input types: Tap and Swipe
        if (MouseButtonJustPressed(MouseButtons.Left))
        {
            mStartTime = Time.time;

            thisInput = new InputData();
            thisInput.duration = 0.0f;
            thisInput.initialPosition = Input.mousePosition;
            thisInput.endPosition = Input.mousePosition;
        }
        else if (MouseButtonHeld(MouseButtons.Left))
        {
            thisInput.duration = Time.time - mStartTime;
            thisInput.endPosition = Input.mousePosition;
        }
        else if (MouseButtonJustReleased(MouseButtons.Left))
        {
            thisInput.endPosition = Input.mousePosition;
            thisInput.CalculateInput();
            float result = thisInput.speed + thisInput.displacement;
            if (thisInput.speed < (sensitivity - thisInput.displacement))
            {
                tap = true;
            }
            else
            {
                swipe = true;
                Vector3 difference = thisInput.initialPosition - thisInput.endPosition;
                direction = CalculateDirection(-difference);
            }
        }

        if (tap || swipe)
        {
            if (tap && OnTap != null)
            {
                OnTap(Input.mousePosition);
            }
            else if (swipe && OnSwipe != null)
            {
                OnSwipe(direction);
            }
        }
    }

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