using UnityEngine;
using System.Collections;

public class ControllerState2D  {

    public bool IsCollindingRight { get; set; }
    public bool IsCollindingLeft { get; set; }
    public bool IsCollindingAbove { get; set; }
    public bool IsCollindingBelow { get; set; }
    public bool IsMovingDownSlope { get; set; }
    public bool IsMovingUpSlope { get; set; }
    public bool IsGrounded { get { return IsCollindingBelow; } }
    public float SlopeAngle { get; set; }
    public bool HasCollisions { get { return IsCollindingAbove || IsCollindingBelow || IsCollindingLeft || IsCollindingRight; } }

    public void Reset()
    {
        IsMovingDownSlope = IsMovingUpSlope = IsCollindingRight = IsCollindingLeft = IsCollindingBelow = IsCollindingAbove = false;
        SlopeAngle = 0;
    }

    public override string ToString()
    {
        return string.Format("(controller r:{0} l:{1} a:{2} b:{3} down-s:{4} up-s{5} angle:{6})",IsCollindingLeft,IsCollindingRight,IsCollindingAbove,IsCollindingBelow,IsMovingDownSlope,IsMovingUpSlope,SlopeAngle);
    }
}
