using TMPro;
using UnityEngine;

public class MessagesController : MonoBehaviour
{
    /// <summary>
    /// Игровой объект для сообщения
    /// </summary>
    [SerializeField] private GameObject message;

    void Start()
    {
        message.SetActive(false);
    }

    /// <summary>
    /// Показать сообщение, как взять предмет
    /// </summary>
    public void PickObjectMessage()
    {
        if (message.activeSelf == false)
        {
            message.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Чтобы взять нажмите E";
            message.SetActive(true);
        }
    }

    /// <summary>
    /// Показать сообщение, как положить предмет
    /// </summary>
    public void PutObjectMessage()
    {
        if (message.activeSelf == false)
        {
            message.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Чтобы положить нажмите E";
            message.SetActive(true);
        }
    }

    /// <summary>
    /// Закрыть сообщение
    /// </summary>
    public void CloseMessage()
    {
        message.SetActive(false);
    }
}
