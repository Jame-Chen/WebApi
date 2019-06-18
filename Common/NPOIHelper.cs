﻿using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;
using System.Web;
using NPOI;
using NPOI.HPSF;
using NPOI.HSSF;
using NPOI.HSSF.UserModel;
using NPOI.HSSF.Util;
using NPOI.POIFS;
using NPOI.Util;
using NPOI.SS.UserModel;
using NPOI.SS.Util;
using NPOI.XWPF.UserModel;
using Model;
using NPOI.OpenXmlFormats.Wordprocessing;

namespace Common
{
    /// <summary>
    /// NPOI操作帮助类
    /// </summary>
    public class NPOIHelper
    {
        /// <summary>  
        /// DataTable导出到Excel文件  
        /// </summary>  
        /// <param name="dtSource">源DataTable</param>  
        /// <param name="strHeaderText">表头文本</param>  
        /// <param name="strFileName">保存位置</param>  
        public static void Export(DataTable dtSource, string strHeaderText, string strFileName)
        {
            using (MemoryStream ms = Export(dtSource, strHeaderText))
            {
                using (FileStream fs = new FileStream(strFileName, FileMode.Create, FileAccess.Write))
                {
                    byte[] data = ms.ToArray();
                    fs.Write(data, 0, data.Length);
                    fs.Flush();
                }
            }
        }



        /// <summary>  
        /// DataTable导出到Excel的MemoryStream  
        /// </summary>  
        /// <param name="dtSource">源DataTable</param>  
        /// <param name="strHeaderText">表头文本</param>  
        public static MemoryStream Export(DataTable dtSource, string strHeaderText)
        {
            HSSFWorkbook workbook = new HSSFWorkbook();
            ISheet sheet = workbook.CreateSheet();

            ICellStyle dateStyle = workbook.CreateCellStyle();
            IDataFormat format = workbook.CreateDataFormat();
            dateStyle.DataFormat = format.GetFormat("yyyy-MM-dd");

            #region 取得每列的列宽（最大宽度）
            int[] arrColWidth = new int[dtSource.Columns.Count];
            foreach (DataColumn item in dtSource.Columns)
            {
                //GBK对应的code page是CP936
                arrColWidth[item.Ordinal] = Encoding.GetEncoding(936).GetBytes(item.ColumnName.ToString()).Length;
            }
            for (int i = 0; i < dtSource.Rows.Count; i++)
            {
                for (int j = 0; j < dtSource.Columns.Count; j++)
                {
                    int intTemp = Encoding.GetEncoding(936).GetBytes(dtSource.Rows[i][j].ToString()).Length;
                    if (intTemp > arrColWidth[j])
                    {
                        arrColWidth[j] = intTemp;
                    }
                }
            }
            #endregion

            int rowIndex = 0;

            foreach (DataRow row in dtSource.Rows)
            {
                #region 新建表，填充表头，填充列头，样式
                if (rowIndex == 65535 || rowIndex == 0)
                {
                    if (rowIndex != 0)
                    {
                        sheet = workbook.CreateSheet();
                    }

                    #region 表头及样式
                    {
                        IRow headerRow = sheet.CreateRow(0);
                        headerRow.HeightInPoints = 25;
                        headerRow.CreateCell(0).SetCellValue(strHeaderText);

                        ICellStyle headStyle = workbook.CreateCellStyle();
                        headStyle.Alignment = HorizontalAlignment.Center;
                        IFont font = workbook.CreateFont();
                        font.FontHeightInPoints = 20;
                        font.Boldweight = 700;
                        headStyle.SetFont(font);

                        headerRow.GetCell(0).CellStyle = headStyle;

                        sheet.AddMergedRegion(new CellRangeAddress(0, 0, 0, dtSource.Columns.Count - 1));
                    }
                    #endregion


                    #region 列头及样式
                    {
                        IRow headerRow = sheet.CreateRow(1);
                        ICellStyle headStyle = workbook.CreateCellStyle();
                        headStyle.Alignment = HorizontalAlignment.Center;
                        IFont font = workbook.CreateFont();
                        font.FontHeightInPoints = 10;
                        font.Boldweight = 700;
                        headStyle.SetFont(font);


                        foreach (DataColumn column in dtSource.Columns)
                        {
                            headerRow.CreateCell(column.Ordinal).SetCellValue(column.ColumnName);
                            headerRow.GetCell(column.Ordinal).CellStyle = headStyle;

                            //设置列宽  
                            if (arrColWidth[column.Ordinal] > 255)
                            {
                                arrColWidth[column.Ordinal] = 254;
                            }
                            else
                            {
                                sheet.SetColumnWidth(column.Ordinal, (arrColWidth[column.Ordinal] + 1) * 256);
                            }
                        }
                    }
                    #endregion

                    rowIndex = 2;
                }
                #endregion


                #region 填充内容
                ICellStyle contentStyle = workbook.CreateCellStyle();
                contentStyle.Alignment = HorizontalAlignment.Left;
                IRow dataRow = sheet.CreateRow(rowIndex);
                foreach (DataColumn column in dtSource.Columns)
                {
                    NPOI.SS.UserModel.ICell newCell = dataRow.CreateCell(column.Ordinal);
                    newCell.CellStyle = contentStyle;

                    string drValue = row[column].ToString();

                    switch (column.DataType.ToString())
                    {
                        case "System.String"://字符串类型  
                            newCell.SetCellValue(drValue);
                            break;
                        case "System.DateTime"://日期类型  
                            DateTime dateV;
                            DateTime.TryParse(drValue, out dateV);
                            newCell.SetCellValue(dateV);

                            newCell.CellStyle = dateStyle;//格式化显示  
                            break;
                        case "System.Boolean"://布尔型  
                            bool boolV = false;
                            bool.TryParse(drValue, out boolV);
                            newCell.SetCellValue(boolV);
                            break;
                        case "System.Int16"://整型  
                        case "System.Int32":
                        case "System.Int64":
                        case "System.Byte":
                            int intV = 0;
                            int.TryParse(drValue, out intV);
                            newCell.SetCellValue(intV);
                            break;
                        case "System.Decimal"://浮点型  
                        case "System.Double":
                            double doubV = 0;
                            double.TryParse(drValue, out doubV);
                            newCell.SetCellValue(doubV);
                            break;
                        case "System.DBNull"://空值处理  
                            newCell.SetCellValue("");
                            break;
                        default:
                            newCell.SetCellValue("");
                            break;
                    }

                }
                #endregion

                rowIndex++;
            }


            using (MemoryStream ms = new MemoryStream())
            {
                workbook.Write(ms);
                ms.Flush();
                ms.Position = 0;

                //sheet.Dispose();
                //workbook.Dispose();//一般只用写这一个就OK了，他会遍历并释放所有资源，但当前版本有问题所以只释放sheet  
                return ms;
            }

        }


        /// <summary>  
        /// 用于Web导出  
        /// </summary>  
        /// <param name="dtSource">源DataTable</param>  
        /// <param name="strHeaderText">表头文本</param>  
        /// <param name="strFileName">文件名</param>  
        //public static void ExportByWeb(DataTable dtSource, string strHeaderText, string strFileName)
        //{

        //    HttpContext curContext = HttpContext.Current;

        //    // 设置编码和附件格式  
        //    curContext.Response.ContentType = "application/vnd.ms-excel";
        //    curContext.Response.ContentEncoding = Encoding.UTF8;
        //    curContext.Response.Charset = "";
        //    curContext.Response.AppendHeader("Content-Disposition",
        //        "attachment;filename=" + HttpUtility.UrlEncode(strFileName + ".xls", Encoding.UTF8));

        //    curContext.Response.BinaryWrite(Export(dtSource, strHeaderText).GetBuffer());
        //    curContext.Response.End();

        //}


        /// <summary>读取excel  
        /// 默认第一行为标头  
        /// </summary>  
        /// <param name="strFileName">excel文档路径</param>  
        /// <returns></returns>  
        public static DataTable Import(string strFileName)
        {
            DataTable dt = new DataTable();

            HSSFWorkbook hssfworkbook;
            using (FileStream file = new FileStream(strFileName, FileMode.Open, FileAccess.Read))
            {
                hssfworkbook = new HSSFWorkbook(file);
            }
            ISheet sheet = hssfworkbook.GetSheetAt(0);
            System.Collections.IEnumerator rows = sheet.GetRowEnumerator();

            IRow headerRow = sheet.GetRow(0);
            int cellCount = headerRow.LastCellNum;

            for (int j = 0; j < cellCount; j++)
            {

                NPOI.SS.UserModel.ICell cell = headerRow.GetCell(j);

                dt.Columns.Add(cell.ToString());
            }

            for (int i = (sheet.FirstRowNum + 1); i <= sheet.LastRowNum; i++)
            {
                IRow row = sheet.GetRow(i);
                DataRow dataRow = dt.NewRow();

                for (int j = row.FirstCellNum; j < cellCount; j++)
                {
                    if (row.GetCell(j) != null)
                        dataRow[j] = row.GetCell(j).ToString();
                }

                dt.Rows.Add(dataRow);
            }
            return dt;
        }


        public static string ExpManagementDetail(ExpManagementDetail mange)
        {
            XWPFDocument doc = new XWPFDocument();
            //标题
            XWPFParagraph p1 = doc.CreateParagraph();
            XWPFRun r1 = p1.CreateRun();
            r1.IsBold = true;
            r1.FontSize = 18;
            r1.SetFontFamily("华文楷体", FontCharRange.None);
            //r1.FontFamily = "华文楷体";//设置雅黑字体
            r1.SetText("排水设施巡查问题清单");

            XWPFParagraph p2 = doc.CreateParagraph();
            p2.Alignment = ParagraphAlignment.LEFT;
            XWPFRun r2 = p2.CreateRun();
            r2.FontSize = 12;
            r2.SetFontFamily("华文楷体", FontCharRange.None);
            // r2.FontFamily = "华文楷体";//设置雅黑字体
           // r2.SetText("制表人:张燕剑");

            CT_P doc_p1 = doc.Document.body.GetPArray(0);//标题居中
            doc_p1.AddNewPPr().AddNewJc().val = ST_Jc.center;
            //创建表格
            XWPFTable table = doc.CreateTable(12, 3);//行，列

            XWPFTableRow row_0 = table.GetRow(0);//第一行

            XWPFTableCell cell_0_0 = row_0.GetCell(0);

            //cell_0_0.SetParagraph(SetCellText(doc, table, "制表人:自动生成"));

            cell_0_0.GetCTTc().AddNewTcPr().AddNewVMerge().val = ST_Merge.restart; //合并行开始
            //cell_0_0.GetCTTc().AddNewTcPr().AddNewVAlign().val = ST_VerticalJc.center;
            cell_0_0.SetParagraph(SetCellText(doc, table, "", mange.UrlList[0], (int)(120 * 9525), (int)(180 * 9525)));
            // cell_0_0.GetCTTc().AddNewTcPr().AddNewVAlign().val = ST_VerticalJc.center;//垂直
            //row_0.MergeCells(0, 2);

            XWPFTableCell cell_0_1 = row_0.GetCell(1);
            cell_0_1.SetParagraph(SetCellText(doc, table, "任务号"));

            XWPFTableCell cell_0_2 = row_0.GetCell(2);
            cell_0_2.SetParagraph(SetCellText(doc, table, mange.W_Taskno));


            XWPFTableRow row_1 = table.GetRow(1);//第二行

            XWPFTableCell cell_1_0 = row_1.GetCell(0);


            cell_1_0.GetCTTc().AddNewTcPr().AddNewVMerge().val = ST_Merge.@continue; //合并行

            XWPFTableCell cell_1_1 = row_1.GetCell(1);
            cell_1_1.SetParagraph(SetCellText(doc, table, "接报时间"));

            XWPFTableCell cell_1_2 = row_1.GetCell(2);
            cell_1_2.SetParagraph(SetCellText(doc, table, mange.Time));



            XWPFTableRow row_2 = table.GetRow(2);//第三行

            XWPFTableCell cell_2_0 = row_2.GetCell(0);
            cell_2_0.GetCTTc().AddNewTcPr().AddNewVMerge().val = ST_Merge.@continue; //合并行

            XWPFTableCell cell_2_1 = row_2.GetCell(1);
            cell_2_1.SetParagraph(SetCellText(doc, table, "问题大类"));

            XWPFTableCell cell_2_2 = row_2.GetCell(2);
            cell_2_2.SetParagraph(SetCellText(doc, table, mange.Category));


            XWPFTableRow row_3 = table.GetRow(3);//第四行

            XWPFTableCell cell_3_0 = row_3.GetCell(0);
            cell_3_0.GetCTTc().AddNewTcPr().AddNewVMerge().val = ST_Merge.@continue; //合并行

            XWPFTableCell cell_3_1 = row_3.GetCell(1);
            cell_3_1.SetParagraph(SetCellText(doc, table, "问题小类"));

            XWPFTableCell cell_3_2 = row_3.GetCell(2);
            cell_3_2.SetParagraph(SetCellText(doc, table, mange.Type));


            XWPFTableRow row_4 = table.GetRow(4);//第五行

            XWPFTableCell cell_4_0 = row_4.GetCell(0);
            cell_4_0.GetCTTc().AddNewTcPr().AddNewVMerge().val = ST_Merge.@continue; //合并行


            XWPFTableCell cell_4_1 = row_4.GetCell(1);
            cell_4_1.SetParagraph(SetCellText(doc, table, "街道"));

            XWPFTableCell cell_4_2 = row_4.GetCell(2);
            cell_4_2.SetParagraph(SetCellText(doc, table, mange.S_TOWNID));

            XWPFTableRow row_5 = table.GetRow(5);//第六行

            XWPFTableCell cell_5_0 = row_5.GetCell(0);
            cell_5_0.GetCTTc().AddNewTcPr().AddNewVMerge().val = ST_Merge.@continue; //合并行


            XWPFTableCell cell_5_1 = row_5.GetCell(1);
            cell_5_1.SetParagraph(SetCellText(doc, table, "巡视单位"));

            XWPFTableCell cell_5_2 = row_5.GetCell(2);
            cell_5_2.SetParagraph(SetCellText(doc, table, mange.MangeCompany));

            XWPFTableRow row_6 = table.GetRow(6);//第七行

            XWPFTableCell cell_6_0 = row_6.GetCell(0);
            cell_6_0.GetCTTc().AddNewTcPr().AddNewVMerge().val = ST_Merge.@continue; //合并行

            XWPFTableCell cell_6_1 = row_6.GetCell(1);
            cell_6_1.SetParagraph(SetCellText(doc, table, "处置人"));

            XWPFTableCell cell_6_2 = row_6.GetCell(2);
            cell_6_2.SetParagraph(SetCellText(doc, table, mange.MangeMan));

            XWPFTableRow row_7 = table.GetRow(7);//第八行

            XWPFTableCell cell_7_0 = row_7.GetCell(0);
            cell_7_0.SetParagraph(SetCellText(doc, table, "发生地址：" + mange.Location));
            row_7.MergeCells(0, 2);/* 合并列 */

            XWPFTableRow row_8 = table.GetRow(8);//第九行

            XWPFTableCell cell_8_0 = row_8.GetCell(0);
            cell_8_0.SetParagraph(SetCellText(doc, table, "案件描述：" + mange.Desc, 80));
            row_8.MergeCells(0, 2);/* 合并列 */

            XWPFTableRow row_9 = table.GetRow(9);//第十行

            XWPFTableCell cell_9_0 = row_9.GetCell(0);
            cell_9_0.SetParagraph(SetCellText(doc, table, "处理要求：请尽快进行处理，并在处理完成后及时进行反馈。"));
            row_9.MergeCells(0, 2);/* 合并列 */

            XWPFTableRow row_10 = table.GetRow(10);//第十一行

            XWPFTableCell cell_10_0 = row_10.GetCell(0);
            cell_10_0.SetParagraph(SetCellText(doc, table, "处理时限：" + mange.CLtime + "天"));
            row_10.MergeCells(0, 2);/* 合并列 */

            XWPFTableRow row_11 = table.GetRow(11);//第十二行

            XWPFTableCell cell_11_0 = row_11.GetCell(0);
            cell_11_0.SetParagraph(SetCellText(doc, table, "", mange.MapUrl, (int)(550 * 11525), (int)(300 * 11525)));
            row_11.MergeCells(0, 2);/* 合并列 */

            XWPFParagraph p3 = doc.CreateParagraph();
            p3.Alignment = ParagraphAlignment.LEFT;
            XWPFRun r3 = p3.CreateRun();
            r3.FontSize = 12;
            r3.SetFontFamily("华文楷体", FontCharRange.None);
            // r3.FontFamily = "华文楷体";//设置雅黑字体
            r3.SetText("上报核实图片");

            XWPFParagraph p4 = doc.CreateParagraph();
            p4.Alignment = ParagraphAlignment.LEFT;
            XWPFRun r4 = p4.CreateRun();
            for (int i = 1; i < mange.UrlList.Count; i++)
            {
                using (FileStream picData = new FileStream(mange.UrlList[i], FileMode.Open, FileAccess.Read))
                {
                    r4.AddPicture(picData, (int)NPOI.XWPF.UserModel.PictureType.PNG, "1.png", (int)(250 * 9525), (int)(250 * 9525));
                    if (i % 2 != 0)
                    {
                        r4.AppendText("          ");
                    }

                }
            }

            XWPFParagraph p5 = doc.CreateParagraph();
            p5.Alignment = ParagraphAlignment.LEFT;
            XWPFRun r5 = p5.CreateRun();
            r5.FontSize = 12;
            r5.SetFontFamily("华文楷体", FontCharRange.None);
            // r3.FontFamily = "华文楷体";//设置雅黑字体
            r5.SetText("处置核实图片");

            XWPFParagraph p6 = doc.CreateParagraph();
            p6.Alignment = ParagraphAlignment.LEFT;
            XWPFRun r6 = p6.CreateRun();
            for (int i = 1; i < mange.UrlListCZ.Count; i++)
            {
                using (FileStream picData = new FileStream(mange.UrlListCZ[i], FileMode.Open, FileAccess.Read))
                {
                    r6.AddPicture(picData, (int)NPOI.XWPF.UserModel.PictureType.PNG, "1.png", (int)(250 * 9525), (int)(250 * 9525));
                    if (i % 2 != 0)
                    {
                        r6.AppendText("          ");
                    }

                }
            }


            string strFileName = "";
            string filePath = HttpContext.Current.Server.MapPath("~/File/");
            if (!Directory.Exists(filePath))
            {
                Directory.CreateDirectory(filePath);
            }
            bool flag = true;
            int iname = 1;
            strFileName = filePath + "工单详情.docx";
            while (flag)
            {
                if (File.Exists(strFileName))
                {
                    strFileName = filePath + "工单详情(" + iname + ").docx";
                    iname++;
                }
                else {
                    flag = false;
                }
            }

            using (MemoryStream ms = new MemoryStream())
            {
                doc.Write(ms);
                ms.Flush();
                using (FileStream fs = new FileStream(strFileName, FileMode.Create, FileAccess.Write))
                {
                    byte[] data = ms.ToArray();
                    fs.Write(data, 0, data.Length);
                    fs.Flush();
                    foreach (string item in mange.UrlList)
                    {
                        File.Delete(item);
                    }
                    foreach (string item in mange.UrlListCZ)
                    {
                        File.Delete(item);
                    }
                    if (!string.IsNullOrEmpty(mange.MapUrl))
                    {
                        File.Delete(mange.MapUrl);
                    }
                }
            }
            return strFileName;
        }








        /// <summary>
        /// 设置字体格式
        /// </summary>
        /// <param name="doc"></param>
        /// <param name="table"></param>
        /// <param name="setText"></param>
        /// <returns></returns>
        public static XWPFParagraph SetCellText(XWPFDocument doc, XWPFTable table, string setText, int sPos = 0)
        {
            //table中的文字格式设置
            CT_P para = new CT_P();
            XWPFParagraph pCell = new XWPFParagraph(para, table.Body);
            pCell.Alignment = ParagraphAlignment.LEFT;//字体居中
            pCell.VerticalAlignment = TextAlignment.CENTER;//字体居中

            XWPFRun r1c1 = pCell.CreateRun();
            r1c1.SetText(setText);
            r1c1.FontSize = 12;
            r1c1.SetFontFamily("华文楷体", FontCharRange.None);
            //r1c1.FontFamily = "华文楷体";//设置雅黑字体
            if (sPos != 0)
            {
                r1c1.SetTextPosition(sPos);//设置高度
            }


            return pCell;
        }

        public static XWPFParagraph SetCellText(XWPFDocument doc, XWPFTable table, string setText, string ImgUrl, int widthEmus, int heightEmus, int sPos = 0)
        {
            //table中的文字格式设置
            CT_P para = new CT_P();
            XWPFParagraph pCell = new XWPFParagraph(para, table.Body);
            if (!string.IsNullOrEmpty(setText))
            {
                pCell.Alignment = ParagraphAlignment.LEFT;//字体居中
            }
            else
            {
                pCell.Alignment = ParagraphAlignment.CENTER;//字体居中
            }
            pCell.VerticalAlignment = TextAlignment.CENTER;//字体居中
            XWPFRun r1c1 = pCell.CreateRun();
            if (!string.IsNullOrEmpty(setText))
            {
                r1c1.SetText(setText);
                r1c1.FontSize = 12;
                r1c1.SetFontFamily("华文楷体", FontCharRange.None);
            }
            //r1c1.FontFamily = "华文楷体";//设置雅黑字体
            if (sPos != 0)
            {
                r1c1.SetTextPosition(sPos);//设置高度
            }
            using (FileStream picData = new FileStream(ImgUrl, FileMode.Open, FileAccess.Read))
            {
                r1c1.AddPicture(picData, (int)NPOI.XWPF.UserModel.PictureType.PNG, "11.png", widthEmus, heightEmus);
            }
            return pCell;
        }


    }
}
