
using UnityEngine;

public class CameraFollow : MonoBehaviour {
    public Transform target;
    public Vector3 offset;
    [Range(1, 10)]
    public float smoothFactor;

    private void Awake()
    {
        if(target == null)
        {
            target = FindObjectOfType<PlayerMovements>().transform;
        }
    }

    private void FixedUpdate()
    {
        FollowCharacter();
    }

    private void FollowCharacter()
    {
        Vector3 playerPos = target.position + offset;
        Vector3 smoothPos = Vector3.Lerp(transform.position, target.position + offset, smoothFactor * Time.fixedDeltaTime);
        transform.position = smoothPos;
    }
}
