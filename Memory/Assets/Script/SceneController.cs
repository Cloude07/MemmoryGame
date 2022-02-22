using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneController : MonoBehaviour
{
    public const int gridRows = 2; //значения указывающие количество ячеек сетки и их растояние друг от друга
    public const int gridCols = 4;
    public const float offsetX = 2f;
    public const float offsetY = 2.5f;

    private MemoryCard _firstRevealed;
    private MemoryCard _secondRevealed;
    private int _score = 0;
    public bool canReveal 
    {
        get { return _secondRevealed == null; } //Функция чтения котораявозвращает значение false если вторая карта открыта
    }

    [SerializeField]
    private MemoryCard originalCard; // Ссылка для карты в сцене
    [SerializeField]
    private Sprite[] image; //массив для ссылок на ресурсы спрайты
    [SerializeField]
    private TextMesh scoreLabel;
    void Start()
    {
        Vector3 startPos = originalCard.transform.position; //положениие первой карты и положение остальные карты отсчитывается от этой точки
        int[] numbers = { 0, 0, 1, 1, 2, 2, 3, 3 }; //обьявляем целосленный массив с парами идентификаторов для всех четырех спрайтов с иобоажениями карт
        numbers = ShuffleArray(numbers); //вызов функции, перемешивающего элементы массива
        
        for (int i = 0; i < gridCols; i++)
        {
            for (int j = 0; j < gridRows; j++) //циклы задающие как столбцы так и строки нашей сетки
            {
                MemoryCard card; // ссылка на контейнер для исходной карты или ее копий
                if (i == 0 && j ==0)
                {
                    card = originalCard;
                }
                else
                {
                    card = Instantiate(originalCard) as MemoryCard; 
                }
                int index = j * gridCols + i;
                //int id = Random.Range(0, image.Length); //рандомный id номер от 0 до макс количество карт 
                int id = numbers[index]; //Получаем индефикаторы из перемешенного списка, а не из случайных цифр
                card.SetCard(id, image[id]);

                float posX = (offsetX * i) + startPos.x;
                float posY = -(offsetY * j) + startPos.y;
                card.transform.position = new Vector3(posX, posY, startPos.z); // в двухмерной графике нам нужно тольео смещение по осям X & Y значение Z не меняется
            }
        }
       
        //originalCard.SetCard(id, image[id]); // вызов откртого метода, который мы добавили в сценарий MemoryCard
    }
    private int[] ShuffleArray(int[] numbers)
    {
        int[] newArray = numbers.Clone() as int[];
        for (int i = 0; i < newArray.Length; i++)
        {
            int tmp = newArray[i];
            int r = Random.Range(i, newArray.Length);
            newArray[i] = newArray[r];
            newArray[r] = tmp;
        }
        return newArray;
    }

    public void CardRevealed(MemoryCard card)
    {
        if (_firstRevealed == null)
        {
            _firstRevealed = card; //сохраняет одну из двух переменных в зависимости от того какая из них свободна
        }
        else
        {
            _secondRevealed = card;
            //Debug.Log("Match? " + (_firstRevealed.id == _secondRevealed.id)); //Сравнение идентификатора двух открытых карт
            StartCoroutine(CheckMatch());
        }
    }
    
    private IEnumerator CheckMatch()
    {
        if (_firstRevealed.id== _secondRevealed.id)
        {
            _score++;
            scoreLabel.text = "Score: " + _score;
        }
        else
        {
            yield return new WaitForSeconds(.5f);

            _firstRevealed.Unreveal(); // Закрывает несовпадающих карты
            _secondRevealed.Unreveal();
        }
        _firstRevealed = null; // отчистка переменной не зависимо от того было совпадение или нет
        _secondRevealed = null;
    }

    [System.Obsolete]
    public void Restart()
    {
       
        Application.LoadLevel("Scene"); //это команда загружает ресурс scene
    }
    void Update()
    {
        
    }
}
