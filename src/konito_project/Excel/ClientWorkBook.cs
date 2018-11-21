using ClosedXML.Excel;
using konito_project.Exceptions;
using konito_project.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace konito_project.Excel
{
    public static class ClientWorkBook {
        public const string WORKBOOK_NAME = "./db/거래처_정보.xlsx";

        public static readonly string[] COLUMNS = {
            "거래처 코드",
            "거래처명",
            "대표자명",
            "사업자번호",
            "계좌은행",
            "계좌번호",
            "예금주명",
            "전화번호",
            "팩스번호",
            "업태",
            "종목",
            "주소",
            "상세 주소",
            "등록일",
            "수정일"
        };
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="client"></param>
        /// <returns>Return the inserted row number.</returns>
        public static int AddClientRecord(Client client) {
            using (var workBook = new XLWorkbook(WORKBOOK_NAME)) {

                var sheet = workBook.Worksheet("data");

                if (sheet == null)
                    throw new WrongExcelFormatException();

                if (client == null)
                    throw new NullReferenceException();

                int rows = sheet.Cell("A1").CurrentRegion.RowCount() + 1;
                DateTime now = DateTime.Now;

                sheet.Cell(rows, 1).Value = client.Id;
                sheet.Cell(rows, 2).Value = client.CompanyName;
                sheet.Cell(rows, 3).Value = client.OwnerName;
                sheet.Cell(rows, 4).Value = client.CompanyCode;
                sheet.Cell(rows, 5).Value = client.BankName;
                sheet.Cell(rows, 6).Value = client.BankAccountCode;
                sheet.Cell(rows, 7).Value = client.BankAccountOwnerName;
                sheet.Cell(rows, 8).Value = client.ContactNumber;
                sheet.Cell(rows, 9).Value = client.FaxNumber;
                sheet.Cell(rows, 10).Value = client.Business;
                sheet.Cell(rows, 11).Value = client.BusinessClassification;
                sheet.Cell(rows, 12).Value = client.Address1;
                sheet.Cell(rows, 13).Value = client.Address2;
                sheet.Cell(rows, 14).Value = now;
                sheet.Cell(rows, 15).Value = now;

                workBook.Save();

                return rows;
            }
        }

        public static Client GetClientByIdOrNull(int id) {

            using (var workBook = new XLWorkbook(WORKBOOK_NAME)) {

                var sheet = workBook.Worksheet("data");

                if (sheet == null)
                    throw new WrongExcelFormatException();

                int lastRow = sheet.Cell("A1").CurrentRegion.RowCount();

                if(lastRow == 1)
                    return null;

                IXLRangeRow findRow = sheet.Range(2, 1, lastRow, 1).FindRow(r => r.FirstCell().GetValue<int>() == id);

                return RowToClient(findRow);
            }
        }

        private static Client RowToClient(IXLRangeRow row) {

            if (row == null)
                return null;

            return new Client() {
                Id = row.Cell(1).GetValue<int>(),
                
            };
        }

    }
}
