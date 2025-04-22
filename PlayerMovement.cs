using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
    public float turnSpeed = 20f;
    public float normalSpeed = 0.01f;
    public float boostMultiplier = 70f;

    private float currentSpeed;
    Animator m_Animator;
    Rigidbody m_Rigidbody;
    Vector3 m_Movement;
    Quaternion m_Rotation = Quaternion.identity;

    private bool hasSpeedBoost = false;
    private float boostEndTime = 0f;

    void Start()
    {
        m_Animator = GetComponent<Animator>();
        m_Rigidbody = GetComponent<Rigidbody>();
        currentSpeed = normalSpeed;
        Debug.Log($"Starting Normal Speed: {normalSpeed}, Current Speed: {currentSpeed}");
    }

    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        m_Movement.Set(horizontal, 0f, vertical);
        m_Movement.Normalize();

        bool hasHorizontalInput = !Mathf.Approximately(horizontal, 0f);
        bool hasVerticalInput = !Mathf.Approximately(vertical, 0f);
        bool isWalking = hasHorizontalInput || hasVerticalInput;
        m_Animator.SetBool("IsWalking", isWalking);

        Vector3 desiredForward = Vector3.RotateTowards(transform.forward, m_Movement, turnSpeed * Time.deltaTime, 0f);
        m_Rotation = Quaternion.LookRotation(desiredForward);

        if (hasSpeedBoost)
        {
            float boostRemaining = boostEndTime - Time.time;

            if (boostRemaining > 0f)
            {
                currentSpeed = normalSpeed * boostMultiplier;
            }
            else
            {
                hasSpeedBoost = false;
                currentSpeed = normalSpeed;
            }
        }
        else
        {
            currentSpeed = normalSpeed;
        }
        Debug.Log($"Current Speed: {currentSpeed}");
    }
    void FixedUpdate()
    {
        m_Rigidbody.MovePosition(m_Rigidbody.position + m_Movement * currentSpeed * Time.deltaTime);
        m_Rigidbody.MoveRotation(m_Rotation);

        Debug.Log($"New Position: {m_Rigidbody.position}");
    }

    public void ApplySpeedBoost(float multiplier, float duration)
    {
        boostMultiplier = multiplier;
        boostEndTime = Time.time + duration;
        hasSpeedBoost = true;
        Debug.Log($"Boost Applied! Multiplier = {boostMultiplier}, Ends at: {boostEndTime}");
    }

}
