using NPOI.SS.UserModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sagaceco.UKAccidents.DataModels
{
    public class ExcelWorkbook
    {
        private IWorkbook workbook = null;
        private NPOI.XSSF.UserModel.XSSFWorkbook xssfWorkbook = null;
        private NPOI.HSSF.UserModel.HSSFWorkbook hssfWorkbook = null;

        public ExcelWorkbook( IWorkbook workbook )
        {
            this.workbook = workbook;

            xssfWorkbook = workbook as NPOI.XSSF.UserModel.XSSFWorkbook;
            hssfWorkbook = workbook as NPOI.HSSF.UserModel.HSSFWorkbook;
        }

        public void Write(System.IO.Stream stream )
        {
            workbook.Write( stream );
        }

        public ICellStyle CreateCellStyle()
        {
            if( xssfWorkbook != null )
                return xssfWorkbook.CreateCellStyle();
            else
                return hssfWorkbook.CreateCellStyle();
        }

        public IDataFormat CreateDataFormat()
        {
            if( xssfWorkbook != null )
                return xssfWorkbook.CreateDataFormat();
            else
                return hssfWorkbook.CreateDataFormat();
        }

        public ISheet CreateSheet( string sheetName )
        {
            return workbook.CreateSheet( sheetName );
        }

        public ISheet CreateSheet()
        {
            return workbook.CreateSheet();
        }

        public IFont CreateFont()
        {
            return workbook.CreateFont();
        }
    }
}
