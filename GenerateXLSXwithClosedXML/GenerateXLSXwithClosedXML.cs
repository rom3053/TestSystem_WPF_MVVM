using ClosedXML.Excel;
using DP.MVVM.TestManagement.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenerateXLSXwithClosedXML
{
    public class GenerateXLSXClosedXML
    {
        private List<TestResultStatistics> _listForExcel;
        private XLWorkbook wb;
        private IXLWorksheet ws;
        public GenerateXLSXClosedXML()
        {

        }
        public GenerateXLSXClosedXML(List<TestResultStatistics> listForExcel)
        {
            GenerateStats(listForExcel);
        }
        private void GenerateStats(List<TestResultStatistics> listForExcel)
        {
            if (listForExcel != null)
            {

                string groupName = String.Empty;
                string testName = String.Empty;
                string testCategory = String.Empty;

                wb = new XLWorkbook();//XML
                                      // Создаём экземпляр листа Excel
                ws = wb.Worksheets.Add("StatsTest");//XML

                _listForExcel = listForExcel;
                if (listForExcel[0]!=null)
                {
                     groupName = listForExcel[0].GroupName;
                     testName = listForExcel[0].StatsTestName;
                     testCategory = listForExcel[0].StatsTestCategory;
                }

                InsertTitleForXML(); //XML

                InsertDataIntoExcel();

                #region SetUpFontForSheet
                SetUpFontForSheet();
                #endregion

                #region BordersDraw
                BordersDraw();
                #endregion

                #region SettingColumnsAndRowsFormatting
                AutoFitColumnsRows();
                SettingResultCellsFormatting();
                SettingAnswerCellsFormatting();
                #endregion

                #region CreateSaveFileAndExit

                wb.SaveAs(@"" + Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + 
                            @"\ClosedXML\" + groupName + @"\" + testCategory +
                            @"\" + groupName +"_"+ testName + "_" + DateTime.Now.ToLocalTime().ToString().Replace(':','-') + ".xlsx");
                wb.Dispose();
                #endregion
            }
        }

        private void InsertDataIntoExcel()
        {
            for (int j = 2; j < _listForExcel.Count + 2; j++)//XML
            {

                ws.Cell(j, "A").Value = j - 1;
                ws.Cell(j, "B").Value = _listForExcel[j - 2].UserName;
                ws.Cell(j, "C").Value = _listForExcel[j - 2].GroupName;
                ws.Cell(j, "D").Value = _listForExcel[j - 2].StatsResultTest;
                ws.Cell(j, "E").Value = _listForExcel[j - 2].StatsTestName;
                ws.Cell(j, "F").Value = _listForExcel[j - 2].StatsTestCategory;
                ws.Cell(j, "G").Value = GetPullAnswers(j);

                SetColorCellXML(ws.Cell(j, "D"));
            }
        }

        private void SettingResultCellsFormatting()
        {
            var rangeBordersDraw1 = ws.Range("D2", "D100");
            rangeBordersDraw1.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
        }

        private void AutoFitColumnsRows()
        {
            ws.Columns().AdjustToContents();
            ws.Rows().AdjustToContents();
        }

        private void SettingAnswerCellsFormatting()
        {
            var colG = ws.Column("G");
            colG.Style.Font.FontSize = 6;
            ws.Cell("G1").Style.Font.FontSize = 14;
            colG.Style.Alignment.WrapText = true;
            colG.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left;
            colG.Style.Alignment.Vertical = XLAlignmentVerticalValues.Top;
            colG.Width = 34;
        }

        private void BordersDraw()
        {
            ws.RangeUsed().Style.Border.OutsideBorder = XLBorderStyleValues.Thick;
            ws.RangeUsed().Style.Border.InsideBorder = XLBorderStyleValues.Thick;
        }

        private void SetUpFontForSheet()
        {
            ws.Style.Font.FontName = "New Times Roman";
            ws.Style.Font.FontSize = 14;
        }

        private void InsertTitleForXML()
        {
            ws.Cell("A1").Value = "ID";
            ws.Cell("B1").Value = "UserName";
            ws.Cell("C1").Value = "GroupName";
            ws.Cell("D1").Value = "ResultTest";
            ws.Cell("E1").Value = "TestName";
            ws.Cell("F1").Value = "TestCategory";
            ws.Cell("G1").Value = "SelectedAnswers";
        }
        private void SetColorCellXML(IXLCell range1)
        {
            Double numXML = (double)range1.Value;

            if (numXML >= 90)
            {
                range1.Style.Font.FontColor = XLColor.BlueViolet;

            }
            else if (numXML >= 75)
            {
                range1.Style.Font.FontColor = XLColor.MediumSeaGreen;

            }
            else if (numXML >= 60)
            {
                range1.Style.Font.FontColor = XLColor.Peru;

            }
            else if (numXML < 60)
            {
                range1.Style.Font.FontColor = XLColor.Red;
            }
        }
        private string GetPullAnswers(int j)
        {
            string strAns = null;
            foreach (var item in _listForExcel[j - 2].StatsSelectedAnswers)
            {

                string strValue = null;
                IEnumerable<string> sequence = from n in item.Value select n.ToString();
                foreach (var str in sequence)
                {
                    strValue = String.Concat(strValue, str + ',');
                }
                strValue = strValue.TrimEnd(',');
                strAns = String.Concat(strAns, item.Key.ToString() + ")" + strValue + "; ");
            }

            return strAns;
        }
    }
}

