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
            mStartPosition = Input.mousePosition;
            mStartTime = Time.time;
            mIsPotentiallyTapping = true;
            mIsPotentiallySwiping = false;
        }
        else if (MouseButtonHeld(MouseButtons.Left))
        {
            float duration = Time.time - mStartTime;
            mIsPotentiallyTapping = mStartPosition == Input.mousePosition && duration <= TapDuration;
            mIsPotentiallySwiping = mStartPosition != Input.mousePosition && duration <= SwipeDuration;
        }
        else if (MouseButtonJustReleased(MouseButtons.Left))
        {
            if (mIsPotentiallyTapping)
            {
                tap = true;
            }
            else if (mIsPotentiallySwiping)
            {
                swipe = true;
                Vector3 difference = mStartPosition - Input.mousePosition;
                // normalize direction vector
                // check against compass point thresholds
                // set direction

                direction = CalculateDirection(-difference);
               
                /*
                if (Mathf.Abs(difference.x) > Mathf.Abs(difference.y))
                {
                    // Horizontal Movement
                    if (difference.x < 0)
                    {
                        // Right
                        direction = Direction.Right;
                    }
                    else
                    {
                        // Left
                        direction = Direction.Left;
                    }
                }
                else
                {
                    // Vertical Movement
                    if (difference.y < 0)
                    {
                        // Up
                        direction = Direction.Up;
                    }
                    else
                    {
                        // Down
                        direction = Direction.Down;
                    }
                }
                */
            }
        }
        else
        {
            mStartPosition = Vector3.zero;
            mStartTime = 0.0f;
            mIsPotentiallyTapping = false;
            mIsPotentiallySwiping = false;
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