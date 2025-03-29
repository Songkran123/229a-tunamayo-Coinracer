using UnityEngine;

public class CarReset : MonoBehaviour
{
    public Vector3 resetPosition = new Vector3(0, 2, 0);     // ตำแหน่งรีเซ็ต
    public Vector3 resetRotation = new Vector3(0, 0, 0);     // หมุนกลับทิศเดิม

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            ResetCar();
        }
    }

    void ResetCar()
    {
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        transform.position = resetPosition;
        transform.rotation = Quaternion.Euler(resetRotation);
    }
}