using ImportExport.Models;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using OfficeOpenXml.Table;
using System.Data;

namespace ImportExport.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DataController : Controller
    {
        private readonly ImportExportDbContext _context;

        public DataController(ImportExportDbContext context)
        {
            _context = context;
        }
        [HttpPost]
        public async Task<List<TblImport>> importData(IFormFile file)
        {
            var list = new List<TblImport>();
            using (var stream = new MemoryStream())
            {
                await file.CopyToAsync(stream);
                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                using (var package = new ExcelPackage(stream))
                {
                    ExcelWorksheet worksheet = package.Workbook.Worksheets[0];
                    var rowcount = worksheet.Dimension.Rows;
                    /*row=1 is header*/
                    for (int row = 2; row < rowcount; row++)
                    {
                        list.Add(new TblImport
                        {
                            UserId = worksheet.Cells[row, 1].Value.ToString().Trim(),
                            Name = worksheet.Cells[row, 2].Value.ToString().Trim(),
                            Surname = worksheet.Cells[row, 3].Value.ToString().Trim(),
                            Address = worksheet.Cells[row, 4].Value.ToString().Trim(),
                        });
                    }
                }
            }
            try
            {
                if (list != null)
                {
                    await _context.TblImports.AddRangeAsync(list);
                    await _context.SaveChangesAsync();
                    return list;
                }
                return null;
            }
            catch (Exception exception)
            {
                return null;
            }
        }

        [HttpGet]
        public IActionResult DownloadReport()
        {
            string reportname = $"Excel_{DateTime.Now}.xlsx";
            var list = _context.TblImports.ToList();
            if (list.Count > 0)
            {
                var exportbytes = ExporttoExcel<TblImport>(list, reportname);
                return File(exportbytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", reportname);
            }
            else
            {
                return null;
            }
        }

        private byte[] ExporttoExcel<T>(List<T> table, string filename)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            using ExcelPackage pack = new ExcelPackage();
            ExcelWorksheet ws = pack.Workbook.Worksheets.Add(filename);
            ws.Cells["A1"].LoadFromCollection(table, true, TableStyles.Light1);
            return pack.GetAsByteArray();
        }
    }
}

