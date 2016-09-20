using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sagaceco.UKAccidents.Services
{
    public class ExcelService
    {
        public static NPOI.SS.UserModel.IWorkbook XlsxCreateWorkbook()
        {
            return new NPOI.XSSF.UserModel.XSSFWorkbook();
        }

        public static NPOI.SS.UserModel.IWorkbook XlsCreateWorkbook()
        {
            NPOI.HSSF.UserModel.HSSFWorkbook workbook = NPOI.HSSF.UserModel.HSSFWorkbook.Create( NPOI.HSSF.Model.InternalWorkbook.CreateWorkbook() );

            return workbook;
        }
    }
}
