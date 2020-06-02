using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using System;

namespace OpenWordTest
{
    public class Program
    {
        public static void Main(string[] args)
        {
            string filename = "test.xlsx";

            using (var document = SpreadsheetDocument.Create(filename, SpreadsheetDocumentType.Workbook))
            {
                // initialize workbook
                var relationshipId = "rId1";
                var workbook = new Workbook(new Sheets(new Sheet() { Name = "First Sheet", SheetId = 1, Id = relationshipId }));
                var workbookPart = document.AddWorkbookPart();
                workbookPart.Workbook = workbook;

                //build Worksheet Part
                var workSheetPart = workbookPart.AddNewPart<WorksheetPart>(relationshipId);
                var workSheet = new Worksheet();
                var sheetData = new SheetData();

                for (int i = 0; i < 10; i++)
                {
                    var row = new Row();

                    for (int j = 0; j < 10; j++)
                    {
                        int value = i * j % 10;
                        var cell = new Cell() { CellValue = new CellValue(value.ToString()), DataType = new EnumValue<CellValues>(CellValues.Number) };
                        row.Append(cell);
                    }

                    sheetData.Append(row);
                }
                
                workSheet.Append(sheetData);
                workSheetPart.Worksheet = workSheet;

                //add document properties
                document.PackageProperties.Creator = "Marco Tröster";
                document.PackageProperties.Created = DateTime.UtcNow;
            }
        }
    }
}
