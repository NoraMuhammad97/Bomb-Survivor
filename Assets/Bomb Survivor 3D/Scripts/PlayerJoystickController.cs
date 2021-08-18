using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJoystickController : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] FixedJoystick fixedJoystick;
    [SerializeField] Rigidbody rb;
    [SerializeField] Animator anim;
    private void Start()
    {
        transform.forward = -Vector3.forward;
        //anim.Play("Ninja Idle");
    }

    public void FixedUpdate()
    {
        if (!LevelManager.Instance.GameIsPaused)
        {
            SetPlayerRotation();

            Vector3 direction = transform.forward * fixedJoystick.Vertical + transform.right * fixedJoystick.Horizontal;
            if (direction.sqrMagnitude != 0)
                rb.AddForce(direction * speed * Time.fixedDeltaTime, ForceMode.VelocityChange);
            else
                rb.velocity = Vector3.zero;

            SetPlayerAnimationState(direction);
        }
        else
        {
            rb.velocity = Vector3.zero;
        }
    }

    private void SetPlayerRotation()
    {
        float angle = Mathf.Atan2(fixedJoystick.Horizontal, fixedJoystick.Vertical);
        angle = Mathf.Rad2Deg * angle;

        if (angle != 0)
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, angle, 0), 5 * Time.deltaTime);
    }
    void SetPlayerAnimationState(Vector3 direction)
    {
        BombHolder holder = GetComponent<BombHolder>();
        if(holder)
        {
            if (direction.sqrMagnitude > 0.1f)
            {
                if (holder.isHoldingBomb)
                    GetComponent<BombHolder>().SetState(BombHolder.State.RunningwithBomb);
                else
                    GetComponent<BombHolder>().SetState(BombHolder.State.Running);
            }
            else
            {
                if (holder.isHoldingBomb)
                    GetComponent<BombHolder>().SetState(BombHolder.State.IdlewithBomb);
                else
                    GetComponent<BombHolder>().SetState(BombHolder.State.Idle);
            }
        }
    }
}
