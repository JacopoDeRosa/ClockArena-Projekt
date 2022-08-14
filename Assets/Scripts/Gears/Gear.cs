using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gear : MonoBehaviour
{
    private enum Direction { Left, Right};

    [SerializeField] private float _size = 1;
    [SerializeField] private Direction _rotationDirection;


    public void Rotate(float speed)
    {
        // Left = Positive  Right = Negavite

        if(_rotationDirection == Direction.Left)
        {
            transform.Rotate(new Vector3(0, 0, speed / _size));
        }
        else if(_rotationDirection == Direction.Right)
        {
            transform.Rotate(new Vector3(0, 0, -(speed / _size)));
        }
    }
}
