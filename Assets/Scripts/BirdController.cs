using System.Collections;
using UnityEngine;
using Assets.Scripts;
using System.Collections.Generic;

public class BirdController : MonoBehaviour
{
    [SerializeField] LineRenderer TrajectoryLineRenderer;
    public SlingshotState slingshotState;

    [SerializeField] Rigidbody2D shootRb;
    [SerializeField] float throwSpeed;
    public List<Rigidbody2D> birdPrefabs = new List<Rigidbody2D>();
    [SerializeField] Transform birdSpawnPos;
    [SerializeField] Transform cannon;
    [SerializeField] Transform cannonForce;
    [SerializeField] ParticleSystem boom;

    void InitializeBird()
    {
        birdPrefabs[0].transform.position = birdSpawnPos.position;
        slingshotState = SlingshotState.Idle;
        StartCoroutine(HideBird());
    }

    IEnumerator HideBird()
    {
        birdPrefabs[0].transform.position = new Vector3(0, 0, -15);
        yield return new WaitForSeconds(0.01f);
        birdPrefabs[0].gameObject.SetActive(false);
        StopCoroutine(HideBird());
    }

    void ThrowBird(float distance)
    {
        Vector3 velocity = birdSpawnPos.position - cannonForce.position;
        birdPrefabs[0].GetComponent<Bird>().OnThrow();
        birdPrefabs[0].GetComponent<Rigidbody2D>().velocity = new Vector2(velocity.x, velocity.y) * throwSpeed * distance;
    }

    void SetTrajectoryLineRenderesActive(bool active)
    {
        TrajectoryLineRenderer.enabled = active;
    }

    void DisplayTrajectoryLineRenderer(float distance)
    {
        SetTrajectoryLineRenderesActive(true);
        Vector3 v2 = birdSpawnPos.position - cannonForce.position;
        int segmentCount = 10;
        float segmentScale = 2;
        Vector2[] segments = new Vector2[segmentCount];

        segments[0] = cannonForce.position;

        Vector2 segVelocity = new Vector2(v2.x, v2.y) * throwSpeed * distance;

        float angle = Vector2.Angle(segVelocity, new Vector2(1, 0));
        float time = segmentScale / segVelocity.magnitude;
        for (int i = 1; i < segmentCount; i++)
        {
            float time2 = i * Time.fixedDeltaTime * 5;
            segments[i] = segments[0] + segVelocity * time2 + 0.5f * Physics2D.gravity * Mathf.Pow(time2, 2);
        }

        TrajectoryLineRenderer.positionCount = segmentCount;
        for (int i = 0; i < segmentCount; i++)
            TrajectoryLineRenderer.SetPosition(i, segments[i]);
    }

    void Update()
    {
        if (PickTheFirstBirds.isPicked)
        {
            switch (slingshotState)
            {
                case SlingshotState.Idle:
                    if (birdPrefabs.Count > 0)
                    {
                        InitializeBird();
                        if (Input.GetMouseButtonDown(0))
                        {
                            Vector3 location = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                            if (cannon.GetComponent<CircleCollider2D>() == Physics2D.OverlapPoint(location))
                            {
                                slingshotState = SlingshotState.UserPulling;
                            }
                        }
                    }
                    break;
                case SlingshotState.UserPulling:
                    birdPrefabs[0].isKinematic = false;
                    if (Input.GetMouseButton(0))
                    {
                        birdPrefabs[0].isKinematic = true;
                        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

                        float inputVertical = (4 + mousePos.y) * 8;
                        inputVertical = Mathf.Clamp(inputVertical, -6.4f, 70);
                        cannon.rotation = Quaternion.Euler(0, 0, inputVertical);
                        float inputHorizontal = mousePos.x;
                        inputHorizontal = Mathf.Clamp(inputHorizontal, 0, 10);
                        cannonForce.localPosition = new Vector2(inputHorizontal, 0);

                        float distance = Vector3.Distance(birdSpawnPos.position, cannonForce.position);
                        DisplayTrajectoryLineRenderer(distance);
                    }
                    else
                    {
                        SetTrajectoryLineRenderesActive(false);
                        float distance = Vector3.Distance(birdSpawnPos.position, cannonForce.position);
                        if (distance > 1)
                        {
                            birdPrefabs[0].gameObject.SetActive(true);
                            birdPrefabs[0].transform.position = birdSpawnPos.position;
                            Instantiate(boom, birdSpawnPos.position, Quaternion.identity);
                            ThrowBird(distance);
                            StartCoroutine(LetsGo());
                            slingshotState = SlingshotState.BirdFlying;
                        }
                        else
                        {
                            birdPrefabs[0].transform.position = birdSpawnPos.position;
                            birdPrefabs[0].isKinematic = true;
                        }
                    }
                    break;
                case SlingshotState.BirdFlying:
                    if (birdPrefabs.Count > 0 && birdPrefabs[0].transform.position.x >= 0 && Camera.main.transform.position.x <= 4.75f)
                        Camera.main.transform.position = new Vector3(birdPrefabs[0].transform.position.x, 0, -10);
                    else if (birdPrefabs.Count <= 0)
                        slingshotState = SlingshotState.Idle;
                    break;
                default:
                    break;
            }
        }
    }

    IEnumerator LetsGo()
    {
        yield return new WaitForSeconds(2);
        birdPrefabs.RemoveAt(0);
        slingshotState = SlingshotState.Idle;
    }
}
