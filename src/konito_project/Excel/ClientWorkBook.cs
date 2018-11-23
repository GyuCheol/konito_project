using ClosedXML.Excel;
using konito_project.Exceptions;
using konito_project.Model;
using konito_project.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace konito_project.Excel
{
    public static class ClientWorkBook {
        public const string WORKBOOK_NAME = "./db/거래처_정보.xlsx";
        public const string SHEET_NAME = "data";

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
        /// Insert a client row in the excel file.
        /// </summary>
        /// <param name="client"></param>
        public static void AddClientRecord(Client client) {
            WorkBookHelperMethods.InsertRow(WORKBOOK_NAME, SHEET_NAME, InsertRow, client);
        }

        public static int GetRecordCount() => WorkBookHelperMethods.GetWorksheetCount(WORKBOOK_NAME, SHEET_NAME);
        public static IEnumerable<Client> GetAllRecords() => WorkBookHelperMethods.GetConvertedWorkSheetRow(WORKBOOK_NAME, SHEET_NAME, RecordToData);
        public static void RemoveRecordById(int id) => WorkBookHelperMethods.RemoveSheetRow(WORKBOOK_NAME, SHEET_NAME, r => r.FirstCell().GetValue<int>() == id);

        public static Client GetClientByIdOrNull(int id) {
            return WorkBookHelperMethods.FindConvertedWorkSheetRowOrDefault(
                WORKBOOK_NAME,
                SHEET_NAME,
                r => r.FirstCell().GetValue<int>() == id,
                RecordToData
            );
        }

        private static void InsertRow(IXLRow row, Client client) {
            if (client == null)
                throw new NullReferenceException();

            var now = DateTime.Now;

            row.Cell(1).Value = client.Id;
            row.Cell(2).Value = client.CompanyName;
            row.Cell(3).Value = client.OwnerName;
            row.Cell(4).Value = client.CompanyCode;
            row.Cell(5).Value = client.BankName;
            row.Cell(6).Value = client.BankAccountCode;
            row.Cell(7).Value = client.BankAccountOwnerName;
            row.Cell(8).Value = client.ContactNumber;
            row.Cell(9).Value = client.FaxNumber;
            row.Cell(10).Value = client.Business;
            row.Cell(11).Value = client.BusinessClassification;
            row.Cell(12).Value = client.Address1;
            row.Cell(13).Value = client.Address2;
            row.Cell(14).Value = now;
            row.Cell(15).Value = now;
        }

        private static Client RecordToData(IXLRow row) {

            if (row == null)
                return null;

            return new Client() {
                Id = row.Cell(1).GetValue<int>(),
                CompanyName = row.Cell(2).GetValue<string>(),
                OwnerName = row.Cell(3).GetValue<string>(),
                CompanyCode = row.Cell(4).GetValue<string>(),
                BankName = row.Cell(5).GetValue<string>(),
                BankAccountCode = row.Cell(6).GetValue<string>(),
                BankAccountOwnerName = row.Cell(7).GetValue<string>(),
                ContactNumber = row.Cell(8).GetValue<string>(),
                FaxNumber = row.Cell(9).GetValue<string>(),
                Business = row.Cell(10).GetValue<string>(),
                BusinessClassification = row.Cell(11).GetValue<string>(),
                Address1 = row.Cell(12).GetValue<string>(),
                Address2 = row.Cell(13).GetValue<string>(),
                CreatedTime = row.Cell(14).GetValue<DateTime>(),
                LastestUpdatedTime = row.Cell(15).GetValue<DateTime>()
            };
        }

    }
}
