using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeBody : MonoBehaviour
{
  public bool isRewinding = false;
  private List<PointInTime> pointsInTime;
  private Rigidbody2D rigidBody;

  void Start()
  {
    pointsInTime = new List<PointInTime>();
    rigidBody = GetComponent<Rigidbody2D>();
  }

  // Update is called once per frame
  void Update()
  {
    if (Input.GetMouseButtonDown("Fire1"))
    {
      StartRewind();
    }
    else
    {
      StopRewind();
    }
  }

  private void FixedUpdate()
  {
    if (isRewinding)
    {
      Rewind();
    }
    else
    {
      Record();
    }
  }

  public void StartRewind()
  {
    isRewinding = true;
  }

  public void StopRewind()
  {
    isRewinding = false;
  }

  public void Rewind()
  {
    if (pointsInTime.Count > 0)
    {
      PointInTime point = pointsInTime[0];
      SetPointInTime(point);
      pointsInTime.RemoveAt(0);
    }
    else
    {
      StopRewind();

    }
  }

  public void Record()
  {

  }


  void SetPointInTime(PointInTime point)
  {
    transform.position = point.position;
    rigidBody.velocity = point.velocity;
  }
}
