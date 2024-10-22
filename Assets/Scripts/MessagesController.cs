using TMPro;
using UnityEngine;

public class MessagesController : MonoBehaviour
{
    /// <summary>
    /// ������� ������ ��� ���������
    /// </summary>
    [SerializeField] private GameObject message;

    void Start()
    {
        message.SetActive(false);
    }

    /// <summary>
    /// �������� ���������, ��� ����� �������
    /// </summary>
    public void PickObjectMessage()
    {
        if (message.activeSelf == false)
        {
            message.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "����� ����� ������� E";
            message.SetActive(true);
        }
    }

    /// <summary>
    /// �������� ���������, ��� �������� �������
    /// </summary>
    public void PutObjectMessage()
    {
        if (message.activeSelf == false)
        {
            message.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "����� �������� ������� E";
            message.SetActive(true);
        }
    }

    /// <summary>
    /// ������� ���������
    /// </summary>
    public void CloseMessage()
    {
        message.SetActive(false);
    }
}
