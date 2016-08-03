using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.IO;
using System.Data;
using e0571.web.core.TypeInherited;
using e0571.web.core.TypeExtension;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using Newtonsoft.Json;
using SmartLife.Share.Models.Sys;
using e0571.web.core.Model;
using System.Web;
using NPOI.HSSF.Util;
using NPOI.SS.Util;


namespace SmartLife.Share.Behaviors
{
    public class NPOIHeader
    {
        public string CName { get; set; }
        public string EName { get; set; }
        public int? Length { get; set; }
        public string DataType { get; set; }
        public string DataOption { get; set; }
        public bool? SkipWhenDuplicate { get; set; }
        public bool? SkipWhenOverLength { get; set; }
        public bool? SkipWhenKVError { get; set; }
        public bool? SkipWhenDIError { get; set; }
        public bool? SkipWhenDError { get; set; }
    }

    public class NPOI_KV
    {
        public string K { get; set; }
        public string V { get; set; }
    }

    public class NPOIManager
    {
        public static IList<StringObjectDictionary> GetSheetData(Stream s, int index,bool firstLineAsHead)
        {
            IList<StringObjectDictionary> ret = new List<StringObjectDictionary>();
            HSSFWorkbook hssfworkbook = new HSSFWorkbook(s);
            ISheet sheet = hssfworkbook.GetSheetAt(index); 
            IEnumerator rows = sheet.GetRowEnumerator();

            List<NPOIHeader> headers = new List<NPOIHeader>();
            while (rows.MoveNext())
            {
                HSSFRow row = (HSSFRow)rows.Current;
                if (firstLineAsHead)
                {
                    //第一行是表头
                    if (row.RowNum == 0)
                    {
                        for (int i = 0; i < row.LastCellNum; i++)
                        {
                            ICell cell = row.GetCell(i);
                            NPOIHeader header = JsonConvert.DeserializeObject<NPOIHeader>(cell.ToString().Trim());
                            headers.Add(header);
                        }

                        continue;
                    }
                }
                else
                {
                    for (int i = 0; i < row.LastCellNum; i++)
                    {
                        headers.Add(new NPOIHeader { CName = i.ToString(), EName = i.ToString() });
                    }
                }

                StringObjectDictionary dataItem = new StringObjectDictionary();
                for (int i = 0; i < row.LastCellNum; i++)
                {
                    ICell cell = row.GetCell(i);
                    NPOIHeader header = headers[i];
                    string key = header.EName;
                    string value = null;
                    if (cell == null)
                    {
                        dataItem.Add(key, null);
                    }
                    else
                    {
                        value = cell.ToString().Trim();
                        if (header.Length != null)
                        {
                            if (cell.ToString().Length > header.Length.Value)
                            {
                                if (header.SkipWhenOverLength != null && header.SkipWhenOverLength.Value)
                                {
                                    break;
                                }
                                else
                                {
                                    value = value.Substring(0, header.Length.Value);
                                }
                            }
                        }

                        if (header.DataType == "D")
                        {
                            DateTime d;
                            if (DateTime.TryParse(value, out d))
                            {
                                dataItem.Add(key, d.ToString(header.DataOption));
                            }
                            else
                            {
                                if (header.SkipWhenDError != null && header.SkipWhenDError.Value)
                                {
                                    break;
                                }
                            }
                        }

                        if (header.DataType == "KV")
                        {
                            IList<NPOI_KV> kvs = JsonConvert.DeserializeObject<IList<NPOI_KV>>(header.DataOption);
                            if (kvs.Any(item => item.K == value))
                            {
                                value = kvs.First(item => item.K == value).V;
                            }
                            else
                            {
                                if (header.SkipWhenKVError != null && header.SkipWhenKVError.Value)
                                {
                                    break;
                                }
                            }
                        }

                        if (header.DataType == "DI")
                        {
                            IList<DictionaryItem> dis = BuilderFactory.DefaultBulder(GlobalManager.getConnectString()).List<DictionaryItem>(new DictionaryItem { DictionaryId = header.DataOption });
                            if (dis.Any(item => item.ItemName == value))
                            {
                                value = dis.First(item => item.ItemName == value).ItemId;
                            }
                            else
                            {
                                if (header.SkipWhenDIError != null && header.SkipWhenDIError.Value)
                                {
                                    break;
                                }
                            }
                        }

                        dataItem.Add(key, value);
                    }

                }

                ret.Add(dataItem);
            }
            return ret;
        }


        /// <summary>
        /// npoi 文件 导出excel
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <param name="excelType">2003/2007?</param>
        /// <param name="fileName">excel文件名</param>
        /// <param name="tableName">工作表标题</param>
        /// <param name="colNames">工作表列名</param>
        /// <param name="colNames">工作表列键</param>
        /// <param name="bHeaderByFRow">第一行作为工作表列名</param>
        public static void SaveToFileByList<T>(IList<T> dataList, int excelType, string fileName, string tableName, IList<string> colNames, IList<string> colKeys, bool bHeaderByFRow) where T : BaseModel , new()
        {
            using (FileStream fs = new FileStream(fileName, FileMode.Create, FileAccess.Write))
            {
                IWorkbook workbook = CreateExcelWorkBook(dataList, excelType, tableName, colNames, colKeys, bHeaderByFRow); ;

                MemoryStream ms = new MemoryStream();
                //填入内存
                if (workbook != null)
                {
                    workbook.Write(ms);
                    ms.Flush();
                    ms.Position = 0;
                    workbook = null;
                }
                byte[] data = ms.ToArray();

                fs.Write(data, 0, data.Length);
                fs.Flush();

                data = null;
            }
        }

        public static void SaveToFileByListStringObjectDictionary(IList<StringObjectDictionary> dataList, int excelType, string fileName, string tableName, IList<string> colNames, IList<string> colKeys, bool bHeaderByFRow)
        {
            using (FileStream fs = new FileStream(fileName, FileMode.Create, FileAccess.Write))
            {
                IWorkbook workbook = CreateExcelWorkBook(dataList, excelType, tableName, colNames, colKeys, bHeaderByFRow); ;

                MemoryStream ms = new MemoryStream();
                //填入内存
                if (workbook != null)
                {
                    workbook.Write(ms);
                    ms.Flush();
                    ms.Position = 0;
                    workbook = null;
                }
                byte[] data = ms.ToArray();

                fs.Write(data, 0, data.Length);
                fs.Flush();

                data = null;
            }
        }

        public static void SaveToFileByDataSet(DataSet dataSet, int excelType, string fileName, string tableName, IList<string> colNames, IList<string> colKeys, bool bHeaderByFRow)
        {
            using (FileStream fs = new FileStream(fileName, FileMode.Create, FileAccess.Write))
            {
                IWorkbook workbook = CreateExcelWorkBook(dataSet, excelType, tableName, colNames, colKeys, bHeaderByFRow); ;

                MemoryStream ms = new MemoryStream();
                //填入内存
                if (workbook != null)
                {
                    workbook.Write(ms);
                    ms.Flush();
                    ms.Position = 0;
                    workbook = null;
                }
                byte[] data = ms.ToArray();

                fs.Write(data, 0, data.Length);
                fs.Flush();

                data = null;
            }
        }

        public static void SaveToFileByDataReader(IDataReader dataReader, int excelType, string fileName, string tableName, IList<string> colNames, IList<string> colKeys, bool bHeaderByFRow)
        {
            using (FileStream fs = new FileStream(fileName, FileMode.Create, FileAccess.Write))
            {
                IWorkbook workbook = CreateExcelWorkBook(dataReader, excelType, tableName, colNames, colKeys, bHeaderByFRow); ;

                MemoryStream ms = new MemoryStream();
                //填入内存
                if (workbook != null)
                {
                    workbook.Write(ms);
                    ms.Flush();
                    ms.Position = 0;
                    workbook = null;
                }

                byte[] data = ms.ToArray();

                fs.Write(data, 0, data.Length);
                fs.Flush();

                data = null;
            }
        }

        /// <summary>
        /// npoi web 导出excel
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <param name="excelType">2003/2007?</param>
        /// <param name="fileName">excel文件名</param>
        /// <param name="tableName">工作表标题</param>
        /// <param name="colNames">工作表列名</param>
        /// <param name="colNames">工作表列键</param>
        /// <param name="bHeaderByFRow">第一行作为工作表列名</param>
        public static void RenderToBrowserByList<T>(HttpContext curContext, IList<T> dataList, int excelType, string fileName, string tableName, IList<string> colNames, IList<string> colKeys, bool bHeaderByFRow) where T : BaseModel, new()
        {
            IWorkbook workbook = CreateExcelWorkBook(dataList, excelType, tableName, colNames, colKeys, bHeaderByFRow);

            MemoryStream ms = new MemoryStream();
            //填入内存
            if (workbook != null)
            {
                workbook.Write(ms);
                ms.Flush();
                ms.Position = 0;
                workbook = null;
            }

            //// 设置编码和附件格式
            curContext.Response.Clear();
            curContext.Response.ClearHeaders();
            curContext.Response.Buffer = false;
            curContext.Response.ContentType = "application/octet-stream";//"application/vnd.ms-excel"; //"application/vnd.ms-excel";
            curContext.Response.ContentEncoding = System.Text.Encoding.UTF8;
            curContext.Response.Charset = "";
            curContext.Response.AppendHeader("Content-Disposition", "attachment;filename=" + HttpUtility.UrlEncode(fileName, Encoding.UTF8));
            curContext.Response.AppendHeader("Content-Length", ms.Length.ToString());
            curContext.Response.BinaryWrite(ms.GetBuffer());
            curContext.Response.Flush();
            curContext.Response.End();
        }

        public static void RenderToBrowserByListStringObjectDictionary(HttpContext curContext, IList<StringObjectDictionary> dataList, int excelType, string fileName, string tableName, IList<string> colNames, IList<string> colKeys, bool bHeaderByFRow)
        {
            IWorkbook workbook = CreateExcelWorkBook(dataList, excelType, tableName, colNames, colKeys, bHeaderByFRow);

            MemoryStream ms = new MemoryStream();
            //填入内存
            if (workbook != null)
            {
                workbook.Write(ms);
                ms.Flush();
                ms.Position = 0;
                workbook = null;
            }

            //// 设置编码和附件格式
            curContext.Response.Clear();
            curContext.Response.ClearHeaders();
            curContext.Response.Buffer = false;
            curContext.Response.ContentType = "application/octet-stream";//"application/vnd.ms-excel"; //"application/vnd.ms-excel";
            curContext.Response.ContentEncoding = System.Text.Encoding.UTF8;
            curContext.Response.Charset = "";
            curContext.Response.AppendHeader("Content-Disposition", "attachment;filename=" + HttpUtility.UrlEncode(fileName, Encoding.UTF8));
            curContext.Response.AppendHeader("Content-Length", ms.Length.ToString());
            curContext.Response.BinaryWrite(ms.GetBuffer());
            curContext.Response.Flush();
            curContext.Response.End();
        }

        public static void RenderToBrowserByDataSet(HttpContext curContext, DataSet dataSet, int excelType, string fileName, string tableName, IList<string> colNames, IList<string> colKeys, bool bHeaderByFRow)
        {
            IWorkbook workbook = CreateExcelWorkBook(dataSet, excelType, tableName, colNames, colKeys, bHeaderByFRow);

            MemoryStream ms = new MemoryStream();
            //填入内存
            if (workbook != null)
            {
                workbook.Write(ms);
                ms.Flush();
                ms.Position = 0;
                workbook = null;
            }

            //// 设置编码和附件格式
            curContext.Response.Clear();
            curContext.Response.ClearHeaders();
            curContext.Response.Buffer = false;
            curContext.Response.ContentType = "application/octet-stream";//"application/vnd.ms-excel"; //"application/vnd.ms-excel";
            curContext.Response.ContentEncoding = System.Text.Encoding.UTF8;
            curContext.Response.Charset = "";
            curContext.Response.AppendHeader("Content-Disposition", "attachment;filename=" + HttpUtility.UrlEncode(fileName, Encoding.UTF8));
            curContext.Response.AppendHeader("Content-Length", ms.Length.ToString());
            curContext.Response.BinaryWrite(ms.GetBuffer());
            curContext.Response.Flush();
            curContext.Response.End();
        }

        public static void RenderToBrowserByDataReader(HttpContext curContext, IDataReader dataReader, int excelType, string fileName, string tableName, IList<string> colNames, IList<string> colKeys, bool bHeaderByFRow)
        {
            IWorkbook workbook = CreateExcelWorkBook(dataReader, excelType, tableName, colNames, colKeys, bHeaderByFRow);

            MemoryStream ms = new MemoryStream();
            //填入内存
            if (workbook != null)
            {
                workbook.Write(ms);
                ms.Flush();
                ms.Position = 0;
                workbook = null;
            }

            //// 设置编码和附件格式
            curContext.Response.Clear();
            curContext.Response.ClearHeaders();
            curContext.Response.Buffer = false;
            curContext.Response.ContentType = "application/octet-stream";//"application/vnd.ms-excel"; //"application/vnd.ms-excel";
            curContext.Response.ContentEncoding = System.Text.Encoding.UTF8;
            curContext.Response.Charset = "";
            curContext.Response.AppendHeader("Content-Disposition", "attachment;filename=" + HttpUtility.UrlEncode(fileName, Encoding.UTF8));
            curContext.Response.AppendHeader("Content-Length", ms.Length.ToString());
            curContext.Response.BinaryWrite(ms.GetBuffer());
            curContext.Response.Flush();
            curContext.Response.End();
        }


        


        /// <summary>
        /// IList<T> 生成WorkBook
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dataList"></param>
        /// <param name="excelType">2003/2007?</param>
        /// <param name="tableName">工作表标题</param>
        /// <param name="colNames">工作表列名</param>
        /// <param name="colNames">工作表列键</param>
        /// <param name="bHeaderByFRow">第一行作为工作表列名</param>
        /// <returns></returns>
        private static IWorkbook CreateExcelWorkBook<T>(IList<T> dataList, int excelType, string tableName, IList<string> colNames, IList<string> colKeys, bool bHeaderByFRow)
        {
            IWorkbook workbook = new HSSFWorkbook();
            ISheet sheet;
            IRow dataRow;
            ICell newCell;
            ICellStyle cellStyle, tbHeaderStyle, colHeaderStyle;

            if (dataList != null && dataList.Count > 0) {
                //反射属性
                System.ComponentModel.PropertyDescriptorCollection properties = System.ComponentModel.TypeDescriptor.GetProperties(typeof(T));
                //列出全部列
                bool bKeyName = false;
                //获取列数
                int iColNum = 0;
                if (!bHeaderByFRow && colNames != null)
                {
                    iColNum = colNames.Count;
                }
                if (colKeys != null && colKeys.Count > 0)
                {
                    iColNum = colKeys.Count;
                    bKeyName = true;
                }
                if (iColNum == 0)
                {
                    iColNum = properties.Count;
                }

                //工作表数
                int rowIndex = 0;

                #region 单元格样式
                // 设置行宽度
                //sheet.SetColumnWidth(2, 10 * 256);

                cellStyle = CreateCellStyle(workbook, "");      //普通单元格样式
                tbHeaderStyle = CreateCellStyle(workbook, "tbHeadName");  //表头样式
                colHeaderStyle = CreateCellStyle(workbook, "colHeadName");//列头名称样式
                #endregion

                sheet = workbook.CreateSheet();
                //表头及样式
                if (!string.IsNullOrEmpty(tableName))
                {
                    dataRow = sheet.CreateRow(rowIndex);
                    dataRow.HeightInPoints = 25;
                    newCell = dataRow.CreateCell(0);
                    newCell.SetCellValue(tableName);
                    newCell.CellStyle = tbHeaderStyle;
                    //合并单元格
                    sheet.AddMergedRegion(new CellRangeAddress(0, 0, 0, iColNum - 1));
                    rowIndex++;
                }

                //列头名及列样式
                if (!bHeaderByFRow && colNames != null && colNames.Count > 0)
                {
                    dataRow = sheet.CreateRow(rowIndex);
                    for (int j = 0; j < colNames.Count; j++)
                    {
                        newCell = dataRow.CreateCell(j);
                        newCell.SetCellValue(colNames[j]);
                        newCell.CellStyle = colHeaderStyle;
                    }
                    rowIndex++;
                }

                string drValue = "";
                foreach (var item in dataList) {
                    if (dataList.Count % 65535 == 0)
                    {
                        sheet = workbook.CreateSheet();
                    }

                    dataRow = sheet.CreateRow(rowIndex);
                    for (int i = 0; i < iColNum; i++) {
                        if (bKeyName)
                        {
                            drValue = properties[colKeys[i]].GetValue(item) == null ? "" : properties[colKeys[i]].GetValue(item).ToString();
                        }
                        else {
                            drValue = properties[i].GetValue(item) == null ? "" : properties[i].GetValue(item).ToString();
                        }

                        newCell = dataRow.CreateCell(i);
                        newCell.SetCellValue(drValue);
                        newCell.CellStyle = ((!bHeaderByFRow || dataList.IndexOf(item)  > 0) ? cellStyle : colHeaderStyle);
                    }
                    rowIndex++;
                }
                
            }

            return workbook;
        }


        /// <summary>
        /// ListStringObjectDictionary 生成WorkBook
        /// </summary>
        /// <param name="dataList"></param>
        /// <param name="excelType">2003/2007?</param>
        /// <param name="tableName">工作表标题</param>
        /// <param name="colNames">工作表列名</param>
        /// <param name="colNames">工作表列键</param>
        /// <param name="bHeaderByFRow">第一行作为工作表列名</param>
        /// <returns></returns>
        private static IWorkbook CreateExcelWorkBook(IList<StringObjectDictionary> dataList, int excelType, string tableName, IList<string> colNames, IList<string> colKeys, bool bHeaderByFRow)
        {
            IWorkbook workbook = new HSSFWorkbook();
            ISheet sheet;
            IRow dataRow;
            ICell newCell;
            ICellStyle cellStyle, tbHeaderStyle, colHeaderStyle;

            if (dataList != null && dataList.Count > 0)
            {
                //列出全部列
                bool bKeyName = false;
                //获取列数
                int iColNum = 0;
                if (!bHeaderByFRow && colNames != null)
                {
                    iColNum = colNames.Count;
                }
                if (colKeys != null && colKeys.Count > 0)
                {
                    iColNum = colKeys.Count;
                    bKeyName = true;
                }
                if (iColNum == 0)
                {
                    iColNum = dataList.FirstOrDefault().Count();
                }

                //工作表数
                int rowIndex = 0;

                #region 单元格样式
                // 设置行宽度
                //sheet.SetColumnWidth(2, 10 * 256);

                cellStyle = CreateCellStyle(workbook, "");      //普通单元格样式
                tbHeaderStyle = CreateCellStyle(workbook, "tbHeadName");  //表头样式
                colHeaderStyle = CreateCellStyle(workbook, "colHeadName");//列头名称样式
                #endregion

                sheet = workbook.CreateSheet();
                //表头及样式
                if (!string.IsNullOrEmpty(tableName))
                {
                    dataRow = sheet.CreateRow(rowIndex);
                    dataRow.HeightInPoints = 25;
                    newCell = dataRow.CreateCell(0);
                    newCell.SetCellValue(tableName);
                    newCell.CellStyle = tbHeaderStyle;
                    //合并单元格
                    sheet.AddMergedRegion(new CellRangeAddress(0, 0, 0, iColNum - 1));
                    rowIndex++;
                }

                //列头名及列样式
                if (!bHeaderByFRow && colNames != null && colNames.Count > 0)
                {
                    dataRow = sheet.CreateRow(rowIndex);
                    for (int j = 0; j < colNames.Count; j++)
                    {
                        newCell = dataRow.CreateCell(j);
                        newCell.SetCellValue(colNames[j]);
                        newCell.CellStyle = colHeaderStyle;
                    }
                    rowIndex++;
                }

                string drValue = "";
                foreach (var item in dataList)
                {
                    if (dataList.Count % 65535 == 0)
                    {
                        sheet = workbook.CreateSheet();
                    }

                    dataRow = sheet.CreateRow(rowIndex);
                    for (int i = 0; i < iColNum; i++)
                    {
                        if (bKeyName)
                        {
                            drValue = item[colKeys[i]] == null ? "" : item[colKeys[i]].ToString();
                        }
                        else
                        {
                            drValue = item.Values.ElementAt(i) == null ? "" : item.Values.ElementAt(i).ToString();
                        }

                        newCell = dataRow.CreateCell(i);
                        newCell.SetCellValue(drValue);
                        newCell.CellStyle = ((!bHeaderByFRow || dataList.IndexOf(item) > 0) ? cellStyle : colHeaderStyle);
                    }
                    rowIndex++;
                }

            }

            return workbook;
        }


        /// DataSet 生成WorkBook
        /// </summary>
        /// <param name="dataSet"></param>
        /// <param name="excelType">2003/2007?</param>
        /// <param name="tableName">工作表标题</param>
        /// <param name="colNames">工作表列名</param>
        /// <param name="colNames">工作表列键</param>
        /// <param name="bHeaderByFRow">第一行作为工作表列名</param>
        /// <returns></returns>
        private static IWorkbook CreateExcelWorkBook(DataSet dataSet, int excelType, string tableName, IList<string> colNames, IList<string> colKeys, bool bHeaderByFRow)
        {
            IWorkbook workbook = new HSSFWorkbook();
            ISheet sheet;
            IRow dataRow;
            ICell newCell;
            ICellStyle cellStyle, tbHeaderStyle, colHeaderStyle;

            using (dataSet) {
                DataTable dtSource;
                int iSheetCount = 0;    //工作表个数
                bool bKeyName = false;  //列出全部列

                int cellCount = 0;  //列数
                if (!bHeaderByFRow && colNames != null) {
                    cellCount = colNames.Count;
                }
                if (colKeys != null && colKeys.Count > 0)
                {
                    cellCount = colKeys.Count;
                    bKeyName = true;
                }

                string colHeadName = ""; //列标题名
                for (int i = 0; i < dataSet.Tables.Count; i++)
                {
                    int rowIndex = 0;
                    dtSource = dataSet.Tables[i];
                    if (cellCount == 0) {
                        cellCount = dtSource.Columns.Count;
                    }

                    //循环创建工作表
                    iSheetCount++;
                    sheet = workbook.CreateSheet();

                    #region 取得列宽
                    //int[] arrColWidth = new int[dtSource.Columns.Count];
                    //foreach (DataColumn item in dtSource.Columns)
                    //{
                    //    arrColWidth[item.Ordinal] = Encoding.UTF8.GetBytes(item.ColumnName.ToString()).Length;
                    //}
                    //for (int i = 0; i < dtSource.Rows.Count; i++)
                    //{
                    //    for (int j = 0; j < dtSource.Columns.Count; j++)
                    //    {
                    //        int intTemp = Encoding.UTF8.GetBytes(dtSource.Rows[i][j].ToString()).Length;
                    //        if (intTemp > arrColWidth[j])
                    //        {
                    //            arrColWidth[j] = intTemp;
                    //        }
                    //    }
                    //}
                    #endregion

                    #region 单元格样式
                    // 设置行宽度
                    //sheet.SetColumnWidth(2, 10 * 256);
                    // 获取单元格 并设置样式
                    cellStyle = CreateCellStyle(workbook, "");      //普通单元格样式
                    tbHeaderStyle = CreateCellStyle(workbook, "tbHeadName");  //表头样式
                    colHeaderStyle = CreateCellStyle(workbook, "colHeadName");//列头名称样式
                    #endregion

                    //表头及样式
                    if (!string.IsNullOrEmpty(tableName))
                    {
                        dataRow = sheet.CreateRow(0);
                        dataRow.HeightInPoints = 25;
                        newCell = dataRow.CreateCell(0);
                        newCell.SetCellValue(tableName);
                        newCell.CellStyle = tbHeaderStyle;
                        //合并单元格
                        sheet.AddMergedRegion(new CellRangeAddress(0, 0, 0, cellCount - 1));
                        rowIndex++;
                    }

                    //列头及样式(非第一行作为列头名)
                    if (!bHeaderByFRow)
                    {
                        dataRow = sheet.CreateRow(rowIndex);
                        for (int j = 0; j < cellCount; j++)
                        {
                            if (colNames != null && colNames.Count > 0)
                            {
                                colHeadName = colNames[j];
                            }
                            else
                            {
                                colHeadName = dtSource.Columns[j].ColumnName;
                                //传入的列键不包含当前列键 则跳出
                                if (bKeyName && !colKeys.Contains(colHeadName)) { continue; }
                            }

                            newCell = dataRow.CreateCell(j);
                            newCell.SetCellValue(colHeadName);
                            newCell.CellStyle = colHeaderStyle;
                        }
                        rowIndex++;
                    }

                    // 填充内容
                    foreach (DataRow row in dtSource.Rows)
                    {
                        //超出65535行 则新建一个工作表
                        if (rowIndex > 0 && rowIndex % 65535 == 0)
                        {
                            iSheetCount++;
                            sheet = workbook.CreateSheet();
                            rowIndex = 0;   //如果超出 新建一个工作表 不用创建表头和标题头
                        }

                        dataRow = sheet.CreateRow(rowIndex);
                        string drValue = "";
                        for (int k = 0; k < cellCount; k++) {
                            newCell = dataRow.CreateCell(k);
                            //直接转换成字符串
                            if (bKeyName)
                            {
                                drValue = row[colKeys[k]] == null ? "" : row[colKeys[k]].ToString();
                            }
                            else {
                                drValue = row[k] == null ? "" : row[k].ToString();
                            }

                            newCell.SetCellValue(drValue);
                            newCell.CellStyle = ((!bHeaderByFRow || dtSource.Rows.IndexOf(row) > 0) ? cellStyle : colHeaderStyle);
                        }
                        rowIndex++;
                    }
                }
            }

            return workbook;
        }


        /// <summary>
        /// IDataReader 生成WorkBook
        /// </summary>
        /// <param name="dataReader"></param>
        /// <param name="excelType">2003/2007?</param>
        /// <param name="tableName">工作表标题</param>
        /// <param name="colNames">工作表列名</param>
        /// <param name="colNames">工作表列键</param>
        /// <param name="bHeaderByFRow">第一行作为工作表列名</param>
        /// <returns></returns>
        private static IWorkbook CreateExcelWorkBook(IDataReader dataReader, int excelType, string tableName, IList<string> colNames, IList<string> colKeys, bool bHeaderByFRow)
        {
            IWorkbook workbook = new HSSFWorkbook();
            ISheet sheet;
            IRow dataRow;
            ICell newCell;
            ICellStyle cellStyle, tbHeaderStyle, colHeaderStyle;

            using (dataReader) {
                sheet = workbook.CreateSheet();

                int rowIndex = 0;
                #region 单元格样式
                // 设置行宽度
                //sheet.SetColumnWidth(2, 10 * 256);
                // 获取单元格 并设置样式
                cellStyle = CreateCellStyle(workbook, "");      //普通单元格样式
                tbHeaderStyle = CreateCellStyle(workbook, "tbHeadName");  //表头样式
                colHeaderStyle = CreateCellStyle(workbook, "colHeadName");//列头名称样式
                #endregion

                //列出全部列
                bool bKeyName = false;
                //获取列数,首取传入列名，再取传入列键，后取所有列键
                int cellCount = 0;
                if (!bHeaderByFRow && colNames != null)
                {
                    cellCount = colNames.Count;
                }
                if (colKeys != null && colKeys.Count > 0) {
                    cellCount = colKeys.Count;
                    bKeyName = true;
                }
                if (cellCount == 0) {
                    cellCount = dataReader.FieldCount;
                }

                //表头及样式
                if (!string.IsNullOrEmpty(tableName))
                {
                    dataRow = sheet.CreateRow(0);
                    dataRow.HeightInPoints = 25;
                    newCell = dataRow.CreateCell(0);
                    newCell.SetCellValue(tableName);
                    newCell.CellStyle = tbHeaderStyle;
                    //合并单元格
                    sheet.AddMergedRegion(new CellRangeAddress(0, 0, 0, cellCount - 1));
                    rowIndex++;
                }

                //列头及样式(非第一行作为列头名)
                if (!bHeaderByFRow)
                {
                    dataRow = sheet.CreateRow(rowIndex);
                    string colHeadName = "";
                    for (int i = 0; i < cellCount; i++)
                    {
                        if (colNames != null && colNames.Count > 0)
                        {
                            colHeadName = colNames[i];
                        }
                        else {
                            colHeadName = dataReader.GetName(i);
                            //传入的列键不包含当前列键 则跳出
                            if (bKeyName && !colKeys.Contains(colHeadName)) { continue; }
                        }

                        newCell = dataRow.CreateCell(i);
                        newCell.SetCellValue(colHeadName);
                        newCell.CellStyle = colHeaderStyle;
                    }
                    rowIndex++;
                }

                string drValue = "";
                int iflag = 0;
                while (dataReader.Read())
                {
                    if ((rowIndex / cellCount) % 65535 == 0)
                    {
                        sheet = workbook.CreateSheet();
                        rowIndex = 0;
                    }

                    dataRow = sheet.CreateRow(rowIndex);
                    for (int i = 0; i < cellCount; i++)
                    {
                        //当前列是否包含在出入列键中，不是的话 略过当前值
                        if (bKeyName)
                        {
                            drValue = dataReader[colKeys[i]] == null ? "" : dataReader[colKeys[i]].ToString();
                        }
                        else
                        {
                            drValue = dataReader[i] == null ? "" : dataReader[i].ToString();
                        }
                        newCell = dataRow.CreateCell(i);
                        newCell.SetCellValue(drValue);
                        newCell.CellStyle = ((!bHeaderByFRow || iflag > 0) ? cellStyle : colHeaderStyle);
                    }
                    rowIndex++;
                    iflag++;
                }
            }

            return workbook;
        }


        /// <summary>
        /// 单元格样式
        /// </summary>
        /// <param name="workbook">工作区</param>
        /// <param name="cellType">单元格类型</param>
        /// <returns></returns>
        private static ICellStyle CreateCellStyle(IWorkbook workbook, string cellType) {
            ICellStyle cellStyle = workbook.CreateCellStyle();    //单元格 并设置样式
            IFont font = workbook.CreateFont(); //字体

            //居中
            cellStyle.Alignment = HorizontalAlignment.LEFT;
            //垂直居中
            cellStyle.VerticalAlignment = VerticalAlignment.CENTER;
            //设置字体
            font.Color = HSSFColor.OLIVE_GREEN.BLACK.index;
            cellStyle.SetFont(font);

            switch (cellType)
            {
                case "tbHeadName":
                    cellStyle.Alignment = HorizontalAlignment.CENTER;
                    cellStyle.GetFont(workbook).FontHeightInPoints = 20;
                    cellStyle.GetFont(workbook).Boldweight = 700;
                    break;
                case "colHeadName":
                    cellStyle.Alignment = HorizontalAlignment.CENTER;
                    cellStyle.GetFont(workbook).FontHeightInPoints = 10;
                    cellStyle.GetFont(workbook).Boldweight = 300;
                    break;
                default:
                    break;
            }

            return cellStyle;
        }



        #region 扩展 
        #region 下载告知书
        public static void CreateEvaluateNoticeExcel<T>(HttpContext curContext, IList<T> dataList, string fileName, string tableName, string connectId) where T : BaseModel, new()
        {
            IWorkbook workbook = new HSSFWorkbook();
            ISheet sheet;
            IRow dataRow;
            ICell newCell;
            IFont cellFont, tbHeaderFont;
            ICellStyle lCellStyle, rCellStyle, cCellStyle, tbHeaderStyle;

            #region 单元格样式
            //标题字体设置
            tbHeaderFont = workbook.CreateFont();
            tbHeaderFont.FontName = "宋体";
            tbHeaderFont.FontHeightInPoints = 18;
            tbHeaderFont.Boldweight = 600;  //加粗
            tbHeaderFont.Color = HSSFColor.OLIVE_GREEN.BLACK.index; //黑色
            //单元格字体设置
            cellFont = workbook.CreateFont();
            cellFont.FontName = "宋体";
            cellFont.FontHeightInPoints = 12;
            //cellFont.Boldweight = 400;  //加粗
            tbHeaderFont.Color = HSSFColor.OLIVE_GREEN.BLACK.index; //黑色

            //单元格 标题名
            tbHeaderStyle = workbook.CreateCellStyle();
            tbHeaderStyle.Alignment = HorizontalAlignment.CENTER;   //居中
            tbHeaderStyle.VerticalAlignment = VerticalAlignment.CENTER; //垂直居中
            tbHeaderStyle.SetFont(tbHeaderFont);    //设置字体

            //靠左单元格
            lCellStyle = workbook.CreateCellStyle();
            lCellStyle.Alignment = HorizontalAlignment.LEFT;   //靠左
            lCellStyle.VerticalAlignment = VerticalAlignment.CENTER; //垂直居中
            lCellStyle.SetFont(cellFont);    //设置字体
            //靠右单元格
            rCellStyle = workbook.CreateCellStyle();
            rCellStyle.Alignment = HorizontalAlignment.RIGHT;   //靠右
            rCellStyle.VerticalAlignment = VerticalAlignment.CENTER; //垂直居中
            rCellStyle.SetFont(cellFont);    //设置字体
            //居中单元格
            cCellStyle = workbook.CreateCellStyle();
            cCellStyle.Alignment = HorizontalAlignment.CENTER;   //居中
            cCellStyle.VerticalAlignment = VerticalAlignment.CENTER; //垂直居中
            cCellStyle.SetFont(cellFont);    //设置字体
            #endregion

            IList<DictionaryItem> dict = BuilderFactory.DefaultBulder(connectId).List<DictionaryItem>(new { DictionaryId = "12002", Status = 1 }.ToStringObjectDictionary());

            if (dataList != null && dataList.Count > 0)
            {
                //反射属性
                System.ComponentModel.PropertyDescriptorCollection properties = System.ComponentModel.TypeDescriptor.GetProperties(typeof(T));

                // 设置行宽度
                //sheet.SetColumnWidth(2, 10 * 256);

                sheet = workbook.CreateSheet();
                //sheet.DefaultRowHeight = 20 * 20; //无效
                //sheet.DefaultRowHeightInPoints = 20;
                //sheet.DefaultColumnWidth = 9;
                //A4内容
                string drValue = "";
                foreach (var item in dataList)
                {
                    //sheet
                    if (dataList.Count % 65535 == 0)
                    {
                        sheet = workbook.CreateSheet();
                        //sheet.DefaultRowHeight = 20 * 20;
                        //sheet.DefaultRowHeightInPoints = 20;
                        //sheet.DefaultColumnWidth = 9;
                    }

                    int ifirstrow = dataList.IndexOf(item) * 56 - (dataList.IndexOf(item) > 2 ? 1 : 0);
                    //表头及样式
                    dataRow = sheet.CreateRow(ifirstrow);
                    newCell = dataRow.CreateCell(0);
                    newCell.SetCellValue(tableName);
                    newCell.CellStyle = tbHeaderStyle;
                    //合并单元格
                    sheet.AddMergedRegion(new CellRangeAddress(ifirstrow, ifirstrow + 4, 0, 8));

                    //表内容及样式
                    //第一个内容
                    dataRow = sheet.CreateRow(ifirstrow + 5);
                    newCell = dataRow.CreateCell(0);
                    drValue = "编号:" + (string)properties["ReportCode"].GetValue(item);
                    newCell.SetCellValue(drValue);
                    newCell.CellStyle = rCellStyle;
                    sheet.AddMergedRegion(new CellRangeAddress(ifirstrow + 5, ifirstrow + 6, 0, 8));

                    //第二个内容
                    dataRow = sheet.CreateRow(ifirstrow + 7);
                    newCell = dataRow.CreateCell(0);
                    drValue = "  " + (string)properties["EvaluatedName"].GetValue(item) + "同志：";
                    newCell.SetCellValue(drValue);
                    newCell.CellStyle = lCellStyle;
                    sheet.AddMergedRegion(new CellRangeAddress(ifirstrow + 7, ifirstrow + 8, 0, 8));

                    //第三个内容
                    dataRow = sheet.CreateRow(ifirstrow + 9);
                    newCell = dataRow.CreateCell(0);
                    drValue = "您的养老服务补贴申请已收悉，经审核，您的服务补贴申请被批准。经养老服务需求评估认定：";
                    newCell.SetCellValue(drValue);
                    newCell.CellStyle = lCellStyle;
                    newCell.CellStyle.WrapText = true;
                    sheet.AddMergedRegion(new CellRangeAddress(ifirstrow + 9, ifirstrow + 12, 0, 8));

                    //第4个内容
                    dataRow = sheet.CreateRow(ifirstrow + 13);
                    newCell = dataRow.CreateCell(0);
                    drValue = "您的服务形式：" + string.Join("  ", dict.Select(s => (s.ItemId == (string)properties["ServeItemType"].GetValue(item) ? " √ " + s.ItemName : s.ItemName)).ToArray());
                    newCell.SetCellValue(drValue);
                    newCell.CellStyle = lCellStyle;
                    sheet.AddMergedRegion(new CellRangeAddress(ifirstrow + 13, ifirstrow + 14, 0, 8));

                    //第4个内容
                    dataRow = sheet.CreateRow(ifirstrow + 15);
                    newCell = dataRow.CreateCell(0);
                    drValue = "您的服务标准：";
                    newCell.SetCellValue(drValue);
                    newCell.CellStyle = lCellStyle;
                    sheet.AddMergedRegion(new CellRangeAddress(ifirstrow + 15, ifirstrow + 16, 0, 8));

                    //第4个内容
                    dataRow = sheet.CreateRow(ifirstrow + 17);
                    newCell = dataRow.CreateCell(0);
                    drValue = "1、居家养老服务，每月补助：" + (string.IsNullOrEmpty((string)properties["OcaResult"].GetValue(item)) ? "0" : (string)properties["OcaResult"].GetValue(item)) + "元";
                    newCell.SetCellValue(drValue);
                    newCell.CellStyle = lCellStyle;
                    sheet.AddMergedRegion(new CellRangeAddress(ifirstrow + 17, ifirstrow + 18, 0, 8));

                    //第4个内容
                    dataRow = sheet.CreateRow(ifirstrow + 19);
                    newCell = dataRow.CreateCell(0);
                    drValue = "2、机构养老服务，每月补助：" + (string.IsNullOrEmpty((string)properties["PamResult"].GetValue(item)) ? "0" : (string)properties["PamResult"].GetValue(item)) + "元";
                    newCell.SetCellValue(drValue);
                    newCell.CellStyle = lCellStyle;
                    sheet.AddMergedRegion(new CellRangeAddress(ifirstrow + 19, ifirstrow + 20, 0, 8));

                    //第4个内容
                    dataRow = sheet.CreateRow(ifirstrow + 21);
                    newCell = dataRow.CreateCell(0);
                    drValue = "3、日照中心服务，每月补助：" + (string.IsNullOrEmpty((string)properties["DccResult"].GetValue(item)) ? "0" : (string)properties["DccResult"].GetValue(item)) + "元";
                    newCell.SetCellValue(drValue);
                    newCell.CellStyle = lCellStyle;
                    sheet.AddMergedRegion(new CellRangeAddress(ifirstrow + 21, ifirstrow + 22, 0, 8));

                    //第4个内容
                    dataRow = sheet.CreateRow(ifirstrow + 25);
                    newCell = dataRow.CreateCell(0);
                    drValue = "服务开始日期：";
                    if (properties["ServiceBeginTime"].GetValue(item) is DateTime)
                    {
                        drValue += ((DateTime)properties["ServiceBeginTime"].GetValue(item)).ToString("yyyy年MM月dd日");
                    }
                    newCell.SetCellValue(drValue);
                    newCell.CellStyle = lCellStyle;
                    sheet.AddMergedRegion(new CellRangeAddress(ifirstrow + 25, ifirstrow + 26, 0, 8));

                    //第4个内容
                    dataRow = sheet.CreateRow(ifirstrow + 27);
                    newCell = dataRow.CreateCell(0);
                    drValue = "特此告知";
                    newCell.SetCellValue(drValue);
                    newCell.CellStyle = lCellStyle;
                    sheet.AddMergedRegion(new CellRangeAddress(ifirstrow + 27, ifirstrow + 28, 0, 8));

                    //第4个内容
                    dataRow = sheet.CreateRow(ifirstrow + 35);
                    newCell = dataRow.CreateCell(0);
                    drValue = "市民政局（盖章）            ";
                    newCell.SetCellValue(drValue);
                    newCell.CellStyle = rCellStyle;
                    sheet.AddMergedRegion(new CellRangeAddress(ifirstrow + 35, ifirstrow + 42, 0, 8));

                    //第4个内容
                    dataRow = sheet.CreateRow(ifirstrow + 43);
                    newCell = dataRow.CreateCell(0);
                    drValue = DateTime.Now.ToString("yyyy年MM月dd日") + "      ";
                    newCell.SetCellValue(drValue);
                    newCell.CellStyle = rCellStyle;
                    sheet.AddMergedRegion(new CellRangeAddress(ifirstrow + 43, ifirstrow + 45, 0, 8));
                }

            }

            MemoryStream ms = new MemoryStream();
            //填入内存
            if (workbook != null)
            {
                workbook.Write(ms);
                ms.Flush();
                ms.Position = 0;
                workbook = null;
            }

            //// 设置编码和附件格式
            curContext.Response.Clear();
            curContext.Response.ClearHeaders();
            curContext.Response.Buffer = false;
            curContext.Response.ContentType = "application/octet-stream";//"application/vnd.ms-excel"; //"application/vnd.ms-excel";
            curContext.Response.ContentEncoding = System.Text.Encoding.UTF8;
            curContext.Response.Charset = "";
            curContext.Response.AppendHeader("Content-Disposition", "attachment;filename=" + HttpUtility.UrlEncode(fileName, Encoding.UTF8));
            curContext.Response.AppendHeader("Content-Length", ms.Length.ToString());
            curContext.Response.BinaryWrite(ms.GetBuffer());
            curContext.Response.Flush();
            curContext.Response.End();
            //curContext.ApplicationInstance.CompleteRequest();
            
        }
        #endregion
        #endregion
    }
}
