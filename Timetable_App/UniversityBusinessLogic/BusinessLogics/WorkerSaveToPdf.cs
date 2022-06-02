﻿using System.Collections.Generic;
using MigraDoc.DocumentObjectModel;
using MigraDoc.DocumentObjectModel.Tables;
using MigraDoc.Rendering;
using TimetableBusinessLogic.HelperModels;

namespace TimetableBusinessLogic.BusinessLogics
{
    public class WorkerSaveToPdf
    {
        public static void CreateDoc(WorkerPdfInfo info)
        {
            Document document = new Document();
            DefineStyles(document);
            Section section = document.AddSection();
            Paragraph paragraph = section.AddParagraph(info.Title);
            paragraph.Format.SpaceAfter = "1cm";
            paragraph.Format.Alignment = ParagraphAlignment.Center;
            paragraph.Style = "NormalTitle";
            paragraph = section.AddParagraph($"с {info.DateFrom.ToShortDateString()} по { info.DateTo.ToShortDateString()}");
            paragraph.Format.SpaceAfter = "1cm";
            paragraph.Format.Alignment = ParagraphAlignment.Center;
            paragraph.Style = "Normal";

            var table = document.LastSection.AddTable();
            List<string> columns = new List<string> { "4cm", "2cm", "2cm", "4cm", "3cm" };

            foreach (var elem in columns)
            {
                table.AddColumn(elem);
            }
            CreateRow(new PdfRowParameters
            {
                Table = table,
                Texts = new List<string> { "План", "Дата начала", "Дата окончания", "Студент", "Дисциплина" },
                Style = "NormalTitle",
                ParagraphAlignment = ParagraphAlignment.Center
            });
            foreach (var epss in info.EducationPlansStudentsSubjects)
            {
                CreateRow(new PdfRowParameters
                {
                    Table = table,

                    Texts = new List<string> {
                                epss.EducationPlanName,
                                epss.DateStart.ToShortDateString(),
                                epss.DateEnd.ToShortDateString(),
                                epss.StudentName,
                                epss.SubjectName
                            },
                    Style = "Normal",
                    ParagraphAlignment = ParagraphAlignment.Left
                });
            }
            PdfDocumentRenderer renderer = new PdfDocumentRenderer(true,
                PdfSharp.Pdf.PdfFontEmbedding.Always)
            {
                Document = document
            };
            renderer.RenderDocument();
            renderer.PdfDocument.Save(info.FileName);
        }

        /// Создание стилей для документа
        private static void DefineStyles(Document document)
        {
            Style style = document.Styles["Normal"];
            style.Font.Name = "Times New Roman";
            style.Font.Size = 11;
            style = document.Styles.AddStyle("NormalTitle", "Normal");
            style.Font.Bold = true;
        }

        /// Создание и заполнение строки
        private static void CreateRow(PdfRowParameters rowParameters)
        {
            Row row = rowParameters.Table.AddRow();
            for (int i = 0; i < rowParameters.Texts.Count; ++i)
            {
                FillCell(new PdfCellParameters
                {
                    Cell = row.Cells[i],
                    Text = rowParameters.Texts[i],
                    Style = rowParameters.Style,
                    BorderWidth = 0.5,
                    ParagraphAlignment = rowParameters.ParagraphAlignment
                });
            }
        }

        /// Заполнение ячейки
        private static void FillCell(PdfCellParameters cellParameters)
        {
            cellParameters.Cell.AddParagraph(cellParameters.Text);
            if (!string.IsNullOrEmpty(cellParameters.Style))
            {
                cellParameters.Cell.Style = cellParameters.Style;
            }
            cellParameters.Cell.Borders.Left.Width = cellParameters.BorderWidth;
            cellParameters.Cell.Borders.Right.Width = cellParameters.BorderWidth;
            cellParameters.Cell.Borders.Top.Width = cellParameters.BorderWidth;
            cellParameters.Cell.Borders.Bottom.Width = cellParameters.BorderWidth;
            cellParameters.Cell.Format.Alignment = cellParameters.ParagraphAlignment;
            cellParameters.Cell.VerticalAlignment = VerticalAlignment.Center;
        }
    }
}
