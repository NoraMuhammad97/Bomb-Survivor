using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJoystickController : MonoBehaviour
{
    public float speed;
    public FixedJoystick fixedJoystick;
    public Rigidbody rb;

    public void FixedUpdate()
    {
        float angle = Mathf.Atan2(fixedJoystick.Horizontal, fixedJoystick.Vertical);
        angle = Mathf.Rad2Deg * angle;

        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, angle, 0), 5 * Time.deltaTime);

        Vector3 direction = Vector3.forward * fixedJoystick.Vertical + Vector3.right * fixedJoystick.Horizontal;
        rb.AddForce(direction * speed * Time.fixedDeltaTime, ForceMode.VelocityChange);
    }
}
