using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using TMPro;

public class maincontrol : MonoBehaviour
{
    public TMP_Text questionText;
    public TMP_Text option1Text;
    public TMP_Text option2Text;
    public GameObject[] objects;
    public float objectDisplayDuration = 3f;
    int[] array = new int[10]; // arrayの宣言と、生成した配列と代入を同時に!
    int[] anserarray = new int[10];
    private int currentQuestion;
    private int correctAnswer;
    private bool isDisplayingObject;
    private bool isAnswerSelected;
    private int correctfigure;
    private int correcselect;
    private int correcselectnumber = 0;
    

      private void Start()
    {
        currentQuestion = 0;
        correctfigure = 0;
        isAnswerSelected = false;
        isDisplayingObject = false;
         for (int i = 0; i < objects.Length; i++)
        {
            objects[i].SetActive(false);
        }
        GenerateQuestion();
    }

    
    

      private void GenerateQuestion()
    {
        // 3つの数値による四則演算の問題を生成
        int number1 = Random.Range(13, 17);
        int number2 = Random.Range(5, 9);
        int number3 = Random.Range(10, 16);
        int result = number1 * number2 + number3;
        correctAnswer = result;

        // 問題の表示
        questionText.text = "Question: " + number1 + " × " + number2 + " + " + number3;
        option1Text.text = ""; // 回答の候補を一旦非表示にする
        option2Text.text = ""; // 回答の候補を一旦非表示にする

        Invoke("ShowAnswerOptions", 5f); // 5秒後に回答の選択肢を表示
    }

    private void ShowAnswerOptions()
    {
        // 答えの候補を生成
    int option1;
    int option2;

    if (Random.value < 0.5f) // 50%の確率で正解を候補1にする
    {
        option1 = correctAnswer;
        option2 = correctAnswer +10;
        correcselect = 1;
       
    }
    else
    {
        option1 = correctAnswer-10;
        option2 = correctAnswer;
        correcselect = 2;
    }

    // 答えの選択肢を表示
    questionText.text = "";
    option1Text.text = "Option 1: " + option1;
    option2Text.text = "Option 2: " + option2;

    isAnswerSelected = false;
    }

     private void Update()
    {
        if (!isAnswerSelected)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                CheckAnswer(1);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                CheckAnswer(2);
            }
        }
    }

    private void CheckAnswer(int selectedOption)
    {
        isAnswerSelected = true;

        if (selectedOption == 1)
        {
            if(correcselect == 1){
                correcselectnumber ++;
            }
            else{
                
            }

            Debug.Log("Selected Option: 1, Correct Answer: " + correctAnswer);
            Invoke("DisplayRandomObject", 0.5f); // 3秒後にランダムなオブジェクトを表示
            option1Text.text = ""; // 回答の候補を一旦非表示にする
            option2Text.text = ""; // 回答の候補を一旦非表示にする
        }
        else if (selectedOption == 2)
        {
           if(correcselect == 2){
              
                correcselectnumber ++;
            }
            else{
                
            }
            Debug.Log("Selected Option: 2, Correct Answer: " + correctAnswer);
            Invoke("DisplayRandomObject", 0.5f); // 3秒後にランダムなオブジェクトを表示
            option1Text.text = ""; // 回答の候補を一旦非表示にする
            option2Text.text = ""; // 回答の候補を一旦非表示にする
        }

        
    }

    private void DisplayRandomObject()
    {
        int randomIndex = Random.Range(0, objects.Length);
        array[currentQuestion] = randomIndex;//配列に表示順に記入
        

        for (int i = 0; i < objects.Length; i++)
        {
            if (i == randomIndex)
            {
                objects[i].SetActive(true); // ランダムなオブジェクトを表示
                objects[i].transform.position = new Vector3(49f, 1.13f, 0.44f); // 座標を指定
                Debug.Log(randomIndex );    
            }
            else
            {
                objects[i].SetActive(false); // 他のオブジェクトを非表示
            }
        }

        isDisplayingObject = true;
        Invoke("HideObject", objectDisplayDuration); // 指定の時間経過後にオブジェクトを非表示
        currentQuestion++;

        if (currentQuestion < objects.Length)
        {
            Invoke("GenerateQuestion", 3f); // 3秒後に次の問題を生成
        }
        else
        {
            Invoke("StartCoroutineForFinalQuestion", 3f); //最終課題を3秒後に再生
        }
    }

   private void StartCoroutineForFinalQuestion()
    {
        StartCoroutine(GenerateFinalQuestionCoroutine()); // コルーチンを開始
    }

    private IEnumerator GenerateFinalQuestionCoroutine()
    {
         for (int i = 0; i < objects.Length; i++) // 全てのオブジェクトを表示
    {
        objects[i].SetActive(true);
    }

     for (int i = 0; i < objects.Length; i++) // オブジェクトの順番を打ち込み、正答数をカウント
    {
        int fig = array[i]+1;
    

        bool inputReceived = false; // 入力が受け取られたかどうかのフラグ

        while (!inputReceived) // 入力が受け取られるまでループ
        {
            if (Input.anyKeyDown)
            {
                // 入力された値をint型に変換して変数「number」に代入
                int number;
                if (int.TryParse(Input.inputString, out number))
                {
                    // 入力された値と変数「fig」の値を比較
                    if (number == fig)
                    {
                        correctfigure++;
                        Debug.Log("正解");
                    }
                    else
                    {
                        Debug.Log("不正解");
                    }

                    inputReceived = true; // 入力が受け取られたのでフラグを立てる
                }
                else
                {
                    Debug.Log("数字を入力してください");
                }
            }

            yield return null; // 次のフレームまで待機
        }
    }


    HideObject();
    Debug.Log("Test completed."); // 試験終了
    questionText.text = "Test completed";
    Invoke("last", 3f); // 3秒後にランダムなオブジェクトを表示
    
}

    private void last()
    {
       questionText.text = "C Test  " + correcselectnumber + "/"+ objects.Length + "\n" +"O Test  " + correctfigure + "/" + objects.Length ;

    }


    private void HideObject()
    {
        for (int i = 0; i < objects.Length; i++)
        {
            objects[i].SetActive(false); // 全てのオブジェクトを非表示
        }

        isDisplayingObject = false;
    }}
