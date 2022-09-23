using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[ExecuteInEditMode]
public class CreateMovePoint : MonoBehaviour
{
    [SerializeField] Transform MovePoint;
    [SerializeField] public List<Vector3> MoveAroundPoint;
    public static CreateMovePoint Instance;
    private void Awake()
    {
        Instance = this;
        if (MoveAroundPoint == null) MoveAroundPoint = new List<Vector3> ();
    }
    public void AddMovePoint()
    {
        MoveAroundPoint.Add(MovePoint.position);
    }
    public Vector3 GetPoint()
    {
        return MoveAroundPoint[Random.Range(0,MoveAroundPoint.Count)];
    }
}
