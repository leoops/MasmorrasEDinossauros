using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointInTime
{
  public Vector3 position;
  public Vector3 velocity;
  public Vector3 localScale;

  public PointInTime(Vector3 _position, Vector3 _velocity, Vector3 _localScale)
  {
    position = _position;
    velocity = _velocity;
    localScale = _localScale;
  }
}

