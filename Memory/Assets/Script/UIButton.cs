using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIButton : MonoBehaviour
{
    [SerializeField]
    private GameObject targetObject; // ссылка на целовой объект для информирования о щелчках
    [SerializeField]
    private string targetMessage;
    public Color highlightColor = Color.cyan;

    public void OnMouseOver()
    {
        SpriteRenderer sprite = GetComponent<SpriteRenderer>();
        if (sprite != null)
        {
            sprite.color = highlightColor; // Меняет цвет кнопки при навелении на нее указателем мыши
        }
    }

    public void OnMouseExit()
    {
        SpriteRenderer sprite = GetComponent<SpriteRenderer>();
        if(sprite != null)
        {
            sprite.color = Color.white;
        }
    }

    public void OnMouseDown()
    {
        transform.localScale = new Vector3(1.1f, 1.1f, 1.1f); // в момент нажатия кнопка увеличивается
    }

    public void OnMouseUp()
    {
        transform.localScale = Vector3.one;
        if(targetObject != null)
        {
            targetObject.SendMessage(targetMessage); // Отправка сообщеия целевому объекту в момент щелчка на кнопке
        }
        
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
