using System.Collections.Generic;
using System.IO;
using ExcelDataReader;
using System.Text;



public class ExcelReader
{
    public struct ExcelData
    {
        public string speaker;
        public string content;
        public string characterimage;
        public string EventType;
        public string Choice1content;
        public string Choice2content;
        public string Choice1fileName;
        public string Choice2fileName;
    }

    public static List<ExcelData> ReadExcel(string filePath)
    {
        List<ExcelData> excelDatas = new List<ExcelData>();
        Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
        using (var stream = File.Open(filePath,FileMode.Open,FileAccess.Read))
        {
            using(var reader = ExcelReaderFactory.CreateReader(stream))
            {
                do
                {
                    while (reader.Read())
                    {
                        ExcelData data = new ExcelData();
                        data.speaker = reader.IsDBNull(0) ? string.Empty : reader.GetValue(0)?.ToString() ?? string.Empty;
                        data.content = reader.IsDBNull(1) ? string.Empty : reader.GetValue(1)?.ToString() ?? string.Empty;
                        data.characterimage = reader.IsDBNull(2) ? string.Empty : reader.GetValue(2)?.ToString() ?? string.Empty;
                        data.EventType = reader.IsDBNull(3) ? string.Empty : reader.GetValue(3)?.ToString() ?? string.Empty;
                        data.Choice1content = reader.IsDBNull(4) ? string.Empty : reader.GetValue(4)?.ToString() ?? string.Empty;
                        data.Choice1fileName = reader.IsDBNull(5) ? string.Empty : reader.GetValue(5)?.ToString() ?? string.Empty;
                        data.Choice2content = reader.IsDBNull(6) ? string.Empty : reader.GetValue(6)?.ToString() ?? string.Empty;
                        data.Choice2fileName = reader.IsDBNull(7) ? string.Empty : reader.GetValue(7)?.ToString() ?? string.Empty;
                        excelDatas.Add(data);
                    }
                }while(reader.NextResult());
            }
        }
        return excelDatas; 
    }



}
