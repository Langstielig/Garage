using UnityEngine;

public class MainCharacterBehavior : MonoBehaviour
{
    /// <summary>
    /// ����������� ���������
    /// </summary>
    private CharacterController characterController;

    /// <summary>
    /// �������� ���������
    /// </summary>
    private float speed = 5f;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    private void FixedUpdate()
    {
        MoveCharacter();
    }

    /// <summary>
    /// ����������� ���������
    /// </summary>
    private void MoveCharacter()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 moveDirection = transform.forward * verticalInput + transform.right * horizontalInput;
        moveDirection.y -= 9.81f * Time.fixedDeltaTime;

        characterController.Move(moveDirection * speed * Time.fixedDeltaTime);
    }
}
