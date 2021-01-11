using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeBody : MonoBehaviour
{
  public float recordTime = 5f;
  private List<PointInTime> pointsInTime;
  private bool isRewinding = false;
  private Rigidbody2D rigidBody;

  void Start()
  {
    pointsInTime = new List<PointInTime>();
    rigidBody = GetComponent<Rigidbody2D>();
  }

  // Update is called once per frame
  void Update()
  {
    if (Input.GetButton("Fire2"))
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
    Debug.Log("voltou");
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

    Debug.Log("gravou");
    if (pointsInTime.Count > Mathf.Round(recordTime / Time.fixedDeltaTime))
    {
      pointsInTime.RemoveAt(pointsInTime.Count - 1);
    }

    pointsInTime.Insert(0, new PointInTime(transform.position, rigidBody.velocity, transform.localScale));
  }



  void SetPointInTime(PointInTime point)
  {
    transform.position = point.position;
    rigidBody.velocity = point.velocity;
  }
}
