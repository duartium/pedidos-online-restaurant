using iText.Kernel.Colors;
using iText.Kernel.Font;
using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas.Draw;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using ND.EDUC_CONTINUA.DOMAIN.Contracts.Services;
using Neutrinodevs.PedidosOnline.Domain.DTOs.Order;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ND.EDUC_CONTINUA.INFRAESTRUCTURE.Services
{
    public class PdfService : IPdfService
    {
        public PdfService()
        {
            
        }
        public Byte[] Generate(FinalOrderDto data)
        {
            try
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    PdfWriter writer = new PdfWriter(ms);
                    PdfDocument pdf = new PdfDocument(writer);
                    Document document = new Document(pdf);

                    Paragraph header = new Paragraph($"PEDIDO Nº{data.Number}")
                       .SetTextAlignment(TextAlignment.CENTER)
                       .SetFontSize(16).SetBold();
                    document.Add(header);

                    LineSeparator ls = new LineSeparator(new SolidLine());
                    document.Add(ls);
                    //document.Add(new Paragraph(Environment.NewLine));

                    var customer = new Paragraph("CLIENTE: " + data.CustomerName)
                        .SetFontSize(12)
                        .SetTextAlignment(TextAlignment.LEFT);
                    document.Add(customer);

                    var dealer = new Paragraph("REPARTIDOR: " + data.DealerName + "\n")
                      .SetTextAlignment(TextAlignment.LEFT)
                      .SetFontSize(12);
                    document.Add(dealer);

                    Table table = CreateTable(data);
                    document.Add(table);


                    //tabla de totales
                    Table tblTotales = new Table(2, false);
                    tblTotales.SetWidthPercent(60);

                    var lineBreak = new Paragraph(new Text("\n"));
                    document.Add(lineBreak);

                    Cell cellSUbtotalText = new Cell(1, 1)
                             .SetTextAlignment(TextAlignment.CENTER)
                             .Add(new Paragraph("SUBTOTAL"));
                    tblTotales.AddCell(cellSUbtotalText);

                    Cell cellSUbtotal = new Cell(1, 1)
                              .SetTextAlignment(TextAlignment.RIGHT)
                              .Add(new Paragraph(data.Subtotal.ToString()));
                    tblTotales.AddCell(cellSUbtotal);


                    Cell cellIvaText = new Cell(1, 1)
                             .SetTextAlignment(TextAlignment.CENTER)
                             .Add(new Paragraph("IVA"));
                    tblTotales.AddCell(cellIvaText);

                    Cell cellIva = new Cell(1, 1)
                         .SetTextAlignment(TextAlignment.RIGHT)
                         .Add(new Paragraph(data.Iva.ToString()));
                    tblTotales.AddCell(cellIva);

                    Cell cellTotalText = new Cell(1, 1)
                            .SetTextAlignment(TextAlignment.CENTER)
                            .Add(new Paragraph("TOTAL"));
                    tblTotales.AddCell(cellTotalText);

                    Cell cellTotal = new Cell(1, 1)
                         .SetTextAlignment(TextAlignment.RIGHT)
                         .Add(new Paragraph(data.Total.ToString()));
                    tblTotales.AddCell(cellTotal);
                    tblTotales.SetHorizontalAlignment(HorizontalAlignment.RIGHT);

                    document.Add(tblTotales);
                    document.Close();

                    return ms.ToArray();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        //public Byte[] Generate(List<string> data)
        //{
        //    try
        //    {
        //        using (MemoryStream ms = new MemoryStream())
        //        {
        //            PdfWriter writer = new PdfWriter(ms);
        //            PdfDocument pdf = new PdfDocument(writer);
        //            Document document = new Document(pdf, pageSize: PageSize.A4);

        //            Paragraph header = new Paragraph("ORDEN Nº ")
        //               .SetTextAlignment(TextAlignment.CENTER)
        //               .SetFontSize(20).SetBold();
        //            document.Add(header);
        //            LineSeparator ls = new LineSeparator(new SolidLine());
        //            document.Add(ls);
        //            document.Add(new Paragraph(Environment.NewLine));

        //            Table table = CreateTable(data);
        //            document.Add(table);

        //            document.Close();
        //            return ms.ToArray();
        //        }
        //    }
        //    catch (Exception)
        //    {
        //        return null;
        //    }
        //}

        //internal Table CreateTable(List<T> rows)
        //{
        //    int numColumns = typeof(T).GetProperties().Length;

        //    Table table = new Table(numColumns, false);
        //    table.SetWidth(new UnitValue(UnitValue.PERCENT, 100));

        //    foreach (var row in rows)
        //    {
        //        var rowTyped = (T)Convert.ChangeType(row, typeof(T));
        //        foreach (var prop in rowTyped.GetType().GetProperties())
        //        {
        //            Cell cell = new Cell(1, 1)
        //              .SetTextAlignment(TextAlignment.CENTER)
        //              .Add(new Paragraph( prop.GetValue(rowTyped, null).ToString() ));
        //            table.AddCell(cell);
        //        }
        //    }

        //    return table;
        //}

        //internal Table CreateTable(FullOrderDto orderDto)
        //{
        //    int numColumns = typeof(T).GetProperties().Length;

        //    Table table = new Table(numColumns, false);
        //    table.SetWidth(new UnitValue(UnitValue.PERCENT, 100));

        //    foreach (var row in orderDto.items)
        //    {
        //        Cell cell1 = new Cell(1, 1)
        //              .SetTextAlignment(TextAlignment.CENTER)
        //              .Add(new Paragraph(row.name));
        //        table.AddCell(cell1);

        //        Cell cell2 = new Cell(1, 1)
        //             .SetTextAlignment(TextAlignment.CENTER)
        //             .Add(new Paragraph(row.quantity.ToString()));
        //        table.AddCell(cell2);

        //        Cell cell3 = new Cell(1, 1)
        //             .SetTextAlignment(TextAlignment.CENTER)
        //             .Add(new Paragraph(row.price.ToString()));
        //        table.AddCell(cell3);
        //    }

        //    return table;
        //}

        internal Table CreateTable(FinalOrderDto orderDto)
        {
            Table table = new Table(3, false);
            table.SetWidthPercent(100);

            //estilo texto
            Style normal = new Style();
            PdfFont font = PdfFontFactory.CreateFont();
            normal.SetFont(font).SetFontSize(9);

            //cabecera de tabla
            table.AddCell(new Cell(1, 1)
            .SetBackgroundColor(Color.BLACK)
            .SetTextAlignment(TextAlignment.CENTER)
            .SetFontColor(Color.WHITE)
            .Add(new Paragraph("NOMBRE"))).AddStyle(normal);

            table.AddCell(new Cell(1, 1)
            .SetBackgroundColor(Color.BLACK)
            .SetTextAlignment(TextAlignment.CENTER)
            .SetFontColor(Color.WHITE)
            .Add(new Paragraph("CANTIDAD"))).AddStyle(normal);

            table.AddCell(new Cell(1, 1)
            .SetBackgroundColor(Color.BLACK)
            .SetTextAlignment(TextAlignment.CENTER)
            .SetFontColor(Color.WHITE)
            .Add(new Paragraph("PRECIO"))).AddStyle(normal);

            foreach (var row in orderDto.items)
            {
                Cell cell1 = new Cell(1, 1)
                      .SetTextAlignment(TextAlignment.CENTER)
                      .Add(new Paragraph(row.name));
                table.AddCell(cell1);

                Cell cell2 = new Cell(1, 1)
                     .SetTextAlignment(TextAlignment.CENTER)
                     .Add(new Paragraph(row.quantity.ToString()));
                table.AddCell(cell2);

                Cell cell3 = new Cell(1, 1)
                     .SetTextAlignment(TextAlignment.CENTER)
                     .Add(new Paragraph(row.price.ToString()));
                table.AddCell(cell3);
            }

            

            return table;
        }

    }
}
