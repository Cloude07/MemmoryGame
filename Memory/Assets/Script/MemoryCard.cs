using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MemoryCard : MonoBehaviour
{
    [SerializeField]
    private GameObject cardBack; //Переменная которая появится в unity
    //[SerializeField]
    //private Sprite image; //ссылка на загрузка ресурс Sprite
    [SerializeField]
    private SceneController controller;

    private int _id;
    public int id
    {
        get { return _id; } //функция чтения
    }

    public void SetCard(int id, Sprite image)
    {
        _id = id;
        GetComponent<SpriteRenderer>().sprite = image;
    }
    public void OnMouseDown() //функция срабатывает поле нажатия мыши
    {
        if (cardBack.activeSelf && controller.canReveal) //если объект активен\видимый не более двух объектов
        {
            cardBack.SetActive(false); //меняем видимсоть на фолс
            controller.CardRevealed(this); // уведомляет контроллео при открытия этой карты
        }
       
    }

    public void Unreveal() //скрывает карту 
    {
        cardBack.SetActive(true);
    }
    void Start()
    {
      //  GetComponent<SpriteRenderer>().sprite = image; //Сопоставляет этот спрайт компоненту SpriteRenderer
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
