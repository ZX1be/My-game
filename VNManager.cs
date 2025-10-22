
using System.Collections.Generic;
using System.IO.Enumeration;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class VNManager : MonoBehaviour
{

    public TextMeshProUGUI speakName;
    public TextMeshProUGUI speakingContent;
    public TypewriterEffect TypewriterEffect;
    public Image avatarImage;
    public Image BackgroundImage;
    public Image characterImage1;
    public Image characterImage2;

    public GameObject choicePanel;
    public Button choiceButton1;
   public Button choiceButton2;






    private string storyPath = Constants.STORY_PATH;
    private string defaultStoryName = Constants.DEFAULT_STORY_FILE_NAME;
    private string excelFileExtension = Constants.EXCEL_FILE_EXTENSION;
    private List<ExcelReader.ExcelData> storyData;
    private int currentLine = 0;

    void Start()
    {
        InitializeAndLoadStory(defaultStoryName);

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            DisplayNextLine();
        }
    }
    void InitializeAndLoadStory(string fileName)
    {
        Initialize();
        LoadStoryFromFile(fileName);
        DisplayNextLine();
    }
    void Initialize()
    {
        currentLine = Constants.DEFAULT_START_LINE;

        choicePanel.SetActive(false);
    }
    void LoadStoryFromFile(string filename)
    {
        var path =storyPath+filename+excelFileExtension;
        storyData = ExcelReader.ReadExcel(path);
        if (storyData == null || storyData.Count == 0)
        {
            Debug.LogError(Constants.NO_DATA_FOUND);
        }
    }

    void DisplayNextLine()
    {
        // 检查当前行是否已超出故事数据范围
        if (storyData == null || currentLine >= storyData.Count)
        {
            Debug.LogWarning("已无更多剧情内容");
            return;
        }

       
        var currentData = storyData[currentLine];
        // 处理结束事件
        if (currentData.EventType == Constants.END_OF_STORY)
        {
            Debug.Log(Constants.END_OF_STORY);
            return;
        }
        // 处理选择事件
        if (currentData.EventType == Constants.CHOICE)
        {
            ShowChoice();
            return;
        }
        if (TypewriterEffect.IsTyping())
        {
            TypewriterEffect.CompleteLine();
        }
        else
        {
            DisplayThisLine();
        }
    }

    void ShowChoice()
    {
        var data = storyData[currentLine];
        choiceButton1.onClick.RemoveAllListeners();
        choiceButton2.onClick.RemoveAllListeners();
        choicePanel.SetActive(true);
        choiceButton1.GetComponentInChildren<TextMeshProUGUI>().text = data.Choice1content;
        choiceButton1.onClick.AddListener(() => InitializeAndLoadStory(data.Choice1fileName));
        choiceButton2.GetComponentInChildren<TextMeshProUGUI>().text = data.Choice2content;
        choiceButton2.onClick.AddListener(() => InitializeAndLoadStory(data.Choice2fileName));
    }
    void DisplayThisLine()
    {
        var data = storyData[currentLine];
        speakName.text = data.speaker;
        speakingContent.text = data.content;
        TypewriterEffect.StartTyping(speakingContent.text);
        if (NotNullNorEmpty(data.characterimage))
        {
            UpdataCharacterimage(data.characterimage);
        }
        else
        {
            avatarImage.gameObject.SetActive(false);
        }
    
            currentLine++;
    }

    bool NotNullNorEmpty(string str)
    {
        return !string.IsNullOrEmpty(str);
    }

    void UpdataCharacterimage(string imageFileName)
    {
        string imagePath=Constants.AVATAR_PATH + imageFileName;
        Sprite sprite = Resources.Load<Sprite>(imagePath);
        if(sprite != null)
        {
            avatarImage.sprite= sprite;
            avatarImage.gameObject.SetActive(true);
        }
        else
        {
            Debug.LogError("No Find Image");
        }
    }

}