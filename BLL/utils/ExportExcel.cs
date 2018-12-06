using Model.Dto;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace BLL.utils
{
    public class ExportExcel
    {
        public void exportExcel(List<CallRecordDto> list,string file)
        {
            HSSFWorkbook workbook2003 = new HSSFWorkbook(); //新建工作簿
            workbook2003.CreateSheet("Sheet1");  //新建1个Sheet工作表            
            HSSFSheet SheetOne = (HSSFSheet)workbook2003.GetSheet("Sheet1"); //获取名称为Sheet1的工作表

            //先创建首行
            SheetOne.CreateRow(1);
            //对每一行创建5个单元格
            HSSFRow SheetRow = (HSSFRow)SheetOne.GetRow(0);  //获取Sheet1工作表的首行
            HSSFCell[] SheetCell = new HSSFCell[5];
            SheetCell[0].SetCellValue("频率");
            SheetCell[1].SetCellValue("日期");
            SheetCell[2].SetCellValue("主持人");
            SheetCell[3].SetCellValue("呼入电话");
            SheetCell[4].SetCellValue("时长");

           


            //对工作表先添加行，下标从0开始,并进行赋值
            for (int i = 1; i <= list.Count; i++)
            {
                SheetOne.CreateRow(i);   //创建i行
                HSSFCell[] sheetCell = new HSSFCell[5];
                sheetCell[0].SetCellValue(list[i].databaseAnotherName.ToString());
                sheetCell[1].SetCellValue(list[i].startTime.ToString());
                sheetCell[2].SetCellValue(list[i].hostname.ToString());
                sheetCell[3].SetCellValue(list[i].called.ToString());
                sheetCell[4].SetCellValue(list[i].callTime.ToString());

            }



            //for (int i = 0; i < dt.Rows.Count; i++)
            //{
            //    SheetCell[i] = (HSSFCell)SheetRow.CreateCell(i);  //为第一行创建10个单元格
            //}
            ////创建之后就可以赋值了
            //SheetCell[0].SetCellValue(true); //赋值为bool型         
            //SheetCell[1].SetCellValue(0.000001); //赋值为浮点型
            //SheetCell[2].SetCellValue("Excel2003"); //赋值为字符串
            //SheetCell[3].SetCellValue("123456789987654321");//赋值为长字符串
            //for (int i = 4; i < 10; i++)
            //{
            //    SheetCell[i].SetCellValue(i);    //循环赋值为整形
            //}

            //FileStream file2003 = new FileStream(@"E:\通话记录.xls", FileMode.Create);
            //workbook2003.Write(file2003);
            //file2003.Close();
            //workbook2003.Close();

            //转为字节数组  
            MemoryStream stream = new MemoryStream();
            workbook2003.Write(stream);
            var buf = stream.ToArray();

            //保存为Excel文件  
            using (FileStream fs = new FileStream(file, FileMode.Create, FileAccess.Write))
            {
                fs.Write(buf, 0, buf.Length);
                fs.Flush();
            }

        }




        public  void ListToExcel(List<CallRecordDto> list, string file)
        {
            IWorkbook workbook;
            string fileExt = Path.GetExtension(file).ToLower();
            if (fileExt == ".xlsx") { workbook = new XSSFWorkbook(); } else if (fileExt == ".xls") { workbook = new HSSFWorkbook(); } else { workbook = null; }
            if (workbook == null) { return; }
            ISheet sheet = workbook.CreateSheet("sheet1");

            //表头  
            IRow row = sheet.CreateRow(0);
            for (int i = 0; i < 5; i++)
            {
                ICell cell = row.CreateCell(i);
                if (i == 0) {
                    cell.SetCellValue("频道");
                }
                else if (i==1)
                {
                    cell.SetCellValue("日期");
                }
                else if (i==2)
                {
                    cell.SetCellValue("主持人");
                }
                else if(i==3)
                {
                    cell.SetCellValue("呼入电话");
                }
                else
                {
                    cell.SetCellValue("时长");
                }
                
            }

            //数据  
            for (int i = 0; i < list.Count; i++)
            {
                IRow row1 = sheet.CreateRow(i + 1);
                for (int j = 0; j < 5; j++)
                {
                    ICell cell = row1.CreateCell(j);
                    //cell.SetCellValue(i+j);
                    if (j == 0)
                    {
                        cell.SetCellValue(list[i].databaseAnotherName);
                    }
                    else if (j == 1)
                    {
                        cell.SetCellValue(list[i].startTime);
                    }
                    else if (j == 2)
                    {
                        cell.SetCellValue(list[i].hostname);
                    }
                    else if (j == 3)
                    {
                        cell.SetCellValue(list[i].called);
                    }
                    else
                    {
                        cell.SetCellValue(list[i].callTime);
                    }
                }
            }

            //转为字节数组  
            MemoryStream stream = new MemoryStream();
            workbook.Write(stream);
            var buf = stream.ToArray();

            //保存为Excel文件  
            using (FileStream fs = new FileStream(file, FileMode.Create, FileAccess.Write))
            {
                fs.Write(buf, 0, buf.Length);
                fs.Flush();

            }

           
           

            
        }
    }
}
