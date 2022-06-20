using AlfaBank.DataAccess.Models;
using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AlfaBank.ConsoleApp
{
    public static class ExcelParser
    {
        /// <summary>
        /// Save users to excel file
        /// </summary>
        /// <param name="users">User list to save to excel</param>
        /// <param name="path">The full path to the file including its name and type</param>
        public static void SaveUsersToExcel(List<User> users,string path)
        {
            IXLWorkbook workbook = new XLWorkbook();
            IXLWorksheet worksheet = workbook.Worksheets.Add("Users");

            worksheet.Cell(1, 1).Value = "Id";
            worksheet.Cell(1, 2).Value = "FullName";
            worksheet.Cell(1, 3).Value = "Login";
            worksheet.Cell(1, 4).Value = "RegistrationDate";
            worksheet.Cell(1, 5).Value = "IsDeleted";

            for(int i = 0; i < users.Count; i++)
            {
                worksheet.Cell(i + 2, 1).Value = users[i].Id;
                worksheet.Cell(i + 2, 2).Value = users[i].FullName;
                worksheet.Cell(i + 2, 3).Value = users[i].Login;
                worksheet.Cell(i + 2, 4).Value = users[i].RegistrationDate;
                worksheet.Cell(i + 2, 5).Value = users[i].IsDeleted;
            }

            workbook.SaveAs(path);
        }
    }
}
