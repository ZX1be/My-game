using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
public class CSVManager : MonoBehaviour
{
    
    public string FileName;
    public List<Book> books = new List<Book>(); 
    void Start()
    {
        //�����ļ�·��
        string textFolderPath = Application.streamingAssetsPath + "/text/";
        string filePath = textFolderPath + FileName + ".csv";
        //�����Ƿ�����ļ���
        if (!Directory.Exists(Application.streamingAssetsPath))
        {
            Directory.CreateDirectory(Application.streamingAssetsPath);
        }
            if (!Directory.Exists(textFolderPath))
            {
                Directory.CreateDirectory(textFolderPath);
            }
            StreamWriter sw= new StreamWriter(filePath);
        sw.WriteLine("ID,Name,Autour");
        //�洢����
        for (int i = 0; i < books.Count; i++)
        {
            //д������
            sw.Write($"{books[i].ID},{books[i].Name},{books[i].author}");
        }
    //�������ļ�
    sw.Flush();
    sw.Close();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
[System.Serializable]
public class Book
{
    public int ID;
    public string Name;
    public string author;
};