using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using System;
using System.IO;

namespace OpenXmlTest.Lib
{
    public class DocumentBuilder
    {
        public void CreateExcelFile(Stream stream)
        {
            // write document to stream
            using (var document = SpreadsheetDocument.Create(stream, SpreadsheetDocumentType.Workbook))
            {
                // initialize workbook
                var relationshipId = "rId1";
                var workbook = new Workbook(new Sheets(new Sheet() { Name = "First Sheet", SheetId = 1, Id = relationshipId }));
                var workbookPart = document.AddWorkbookPart();
                workbookPart.Workbook = workbook;

                // create worksheet with data
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

                // apply worksheet to document
                workSheet.Append(sheetData);
                workSheetPart.Worksheet = workSheet;

                // add document properties
                document.PackageProperties.Creator = "Marco Tröster";
                document.PackageProperties.Created = DateTime.UtcNow;

                // write changes to stream
                document.Save();
            }
        }
    }
}
