using UnityEngine;

public class CameraBehavior : MonoBehaviour
{
    /// <summary>
    /// ���������������� ������
    /// </summary>
    private float sensitivity = 2f;

    /// <summary>
    /// ������������ ����, �� ������� ����� ����������� ������, �� ��� y
    /// </summary>
    private float maxYAngle = 90f;

    /// <summary>
    /// ����������� ��������� �� ��������
    /// </summary>
    private float distance = 5f;

    /// <summary>
    /// ������� ������ �� ��� x
    /// </summary>
    private float rotationX;

    /// <summary>
    /// ���������� ��� �������� ����������� ��������
    /// </summary>
    private Transform pointObject;

    /// <summary>
    /// ���������� ��� �������� ����������� ��������
    /// </summary>
    private Transform outlinedObject;

    /// <summary>
    /// ���������� ��� �������� ������������ ��������
    /// </summary>
    private Transform pickedObject;

    /// <summary>
    /// �������� ��� ���� ���������
    /// </summary>
    [SerializeField] private Material objectMaterial;

    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        pointObject = null;
        outlinedObject = null;
        pickedObject = null;
    }

    void Update()
    {
        RotateCamera();
        Ray();
    }

    /// <summary>
    /// ������� ������
    /// </summary>
    private void RotateCamera()
    {
        float mouseX = Input.GetAxis("Mouse X") * sensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * sensitivity;

        transform.parent.Rotate(Vector3.up * mouseX * sensitivity);

        rotationX -= mouseY * sensitivity;
        rotationX = Mathf.Clamp(rotationX, -maxYAngle, maxYAngle);
        transform.localRotation = Quaternion.Euler(rotationX, 0f, 0f);
    }

    /// <summary>
    /// ����� �������� � ������� ����
    /// </summary>
    private void Ray()
    {
        Ray ray = Camera.main.ScreenPointToRay(new Vector2(Screen.width / 2, Screen.height / 2));
        RaycastHit hit;

        Debug.DrawRay(transform.position, transform.forward * distance, Color.blue);

        if (Physics.Raycast(ray, out hit, distance))
        {
            if(hit.transform.tag == "ClearItem" || hit.transform.tag == "Item")
            {
                if (hit.transform.tag == "ClearItem")
                {
                    PointObject(hit.transform);
                }

                else if (hit.transform.tag == "Item")
                {
                    OutlineObject(hit.transform);
                }
            }
            else
            {
                if (pointObject != null)
                {
                    ClearPointObject();
                }

                if (outlinedObject != null)
                {
                    ClearOutlineObject();
                }
            }
        }
    }

    /// <summary>
    /// �������� ��������� �������
    /// </summary>
    /// <param name="hittedObject"></param>
    private void PointObject(Transform hittedObject)
    {
        MeshRenderer currentObject = hittedObject.gameObject.GetComponent<MeshRenderer>();
        if (currentObject != pointObject && pointObject != null)
        {
            pointObject.gameObject.GetComponent<MeshRenderer>().enabled = false;
            pointObject = null;
        }

        currentObject.enabled = true;
        pointObject = hittedObject;

        MessagesController messagesController = FindObjectOfType<MessagesController>();
        messagesController.PutObjectMessage();

        if (Input.GetKeyDown(KeyCode.E) && pickedObject != null && pickedObject.name == pointObject.name)
        {
            pickedObject = null;
            pointObject = null;
            hittedObject.gameObject.GetComponent<Renderer>().material = objectMaterial;
            hittedObject.tag = "Untagged";
            hittedObject.gameObject.GetComponent<MeshRenderer>().enabled = true;
            messagesController.CloseMessage();
        }
    }

    /// <summary>
    /// �������� ���������� �������
    /// </summary>
    private void ClearPointObject()
    {
        MessagesController messagesController = FindObjectOfType<MessagesController>();
        messagesController.CloseMessage();
        pointObject.gameObject.GetComponent<MeshRenderer>().enabled = false;
        pointObject = null;
    }

    /// <summary>
    /// ������� ��������� �������
    /// </summary>
    /// <param name="hittedObject"></param>
    private void OutlineObject(Transform hittedObject)
    {
        Outline currentObject = hittedObject.gameObject.GetComponent<Outline>();
        if (currentObject != outlinedObject && outlinedObject != null)
        {
            outlinedObject.gameObject.GetComponent<Outline>().enabled = false;
            outlinedObject = null;
        }

        currentObject.enabled = true;
        outlinedObject = hittedObject;
        MessagesController messagesController = FindObjectOfType<MessagesController>();
        messagesController.PickObjectMessage();

        if (Input.GetKeyDown(KeyCode.E) && pickedObject == null)
        {
            pickedObject = hittedObject;
            pickedObject.gameObject.SetActive(false);
            messagesController.CloseMessage();
        }
    }

    /// <summary>
    /// �������� ���������� �������
    /// </summary>
    private void ClearOutlineObject()
    {
        MessagesController messagesController = FindObjectOfType<MessagesController>();
        messagesController.CloseMessage();
        outlinedObject.gameObject.GetComponent<Outline>().enabled = false;
        outlinedObject = null;
    }
}
