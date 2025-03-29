using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] float smoothSpeed = 2f;
    [SerializeField] Vector3 offset = new Vector3(0, 2, -5);

    private void LateUpdate()
    {
        Vector3 desiredPosition = transform.position + target.TransformDirection(offset);
        transform.position = Vector3.Lerp(transform.position, desiredPosition,smoothSpeed * Time.deltaTime);
        transform.LookAt(target);
    }
}
