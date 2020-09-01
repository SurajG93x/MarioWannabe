using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToAndFro : MonoBehaviour
{ 
    [SerializeField] private float speed;
    [SerializeField] private bool canMove;
    [SerializeField] private Transform movingObjectTransform, moveTo;

    private Vector3 originalPos, moveToPos, nextPos;

    private void Start()
    {
        originalPos = movingObjectTransform.localPosition;
        moveToPos = moveTo.localPosition;
        nextPos = moveToPos;
    }

    private void Update()
    {
        if (canMove)
        {
            Move();
        }
    }

    private void Move()
    {
        movingObjectTransform.localPosition = Vector3.MoveTowards(movingObjectTransform.localPosition, nextPos, speed*Time.deltaTime);

        if (Vector3.Distance(movingObjectTransform.localPosition, nextPos) <= 0.1)
            ChangeDestination();
    }
    
    private void ChangeDestination()
    {
        nextPos = nextPos != originalPos ? originalPos : moveToPos;
    }
}
