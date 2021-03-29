using System.Diagnostics;
using System.IO;
using ClosedXML.Excel;
using NUnit.Framework;

namespace CSharpStudy.Tests.ClosedXml
{
    [TestFixture]
    public class ClosedXmlTests
    {
        [Test]
        public void Test()
        {
            var wb = new XLWorkbook();
            var ws = wb.Worksheets.Add("Sheet1");

            ws.Cell(1, 1).Value = "Hello World!";

            var tempFilePath = $"{Path.GetTempPath()}Book1.xlsx";

            wb.SaveAs(tempFilePath);

            var processStartInfo = new ProcessStartInfo(tempFilePath)
            {
                UseShellExecute = true
            };

            Process.Start(processStartInfo);
        }
    }
}