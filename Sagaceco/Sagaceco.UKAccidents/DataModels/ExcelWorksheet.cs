using NPOI.SS.UserModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sagaceco.UKAccidents.DataModels
{
    public class ExcelWorksheet
    {
        public static IWorkbook XlsxCreateWorkbook()
        {
            return new NPOI.XSSF.UserModel.XSSFWorkbook();
        }

        public static IWorkbook XlsCreateWorkbook()
        {
            NPOI.HSSF.UserModel.HSSFWorkbook workbook = NPOI.HSSF.UserModel.HSSFWorkbook.Create( NPOI.HSSF.Model.InternalWorkbook.CreateWorkbook() );

            return workbook;
        }
    }
}
