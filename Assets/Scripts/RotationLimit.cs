using UnityEngine;

public class RotationLimit : MonoBehaviour
{
    public float maxRotationY = 90f;

    private void Update()
    {
        if(transform.rotation.eulerAngles.y > maxRotationY)
        {
            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, maxRotationY, transform.rotation.eulerAngles.z);
            print("s");
        }
    }
}