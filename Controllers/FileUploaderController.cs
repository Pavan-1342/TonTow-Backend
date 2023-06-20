using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReportServiceProject.Service;
using System.Net.Mail;
using System.Net;
using TonTow.API.Data;
using TonTow.API.Models;
using System.Security.Cryptography;

namespace TonTow.API.Controllers
{
     [ApiController, Authorize]
    [Route("api/FileUploader")]
    public class FileUploaderController : Controller
    {
        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly TonTowDbContext _tonTowDbContext;
        private readonly IConfiguration _configuration;
        private IReportService IReport;
        public FileUploaderController(IReportService ReportService, IWebHostEnvironment environment, TonTowDbContext tonTowDbContext, IConfiguration configuration)
        {
            IReport = ReportService;
            _hostingEnvironment = environment;
            _tonTowDbContext = tonTowDbContext;
            _configuration = configuration;
        }

        [HttpGet("GetUserFiles")]
        public async Task<IActionResult> GetUserFiles(int TonTowRptId)
        {
            var FileUpload = from tonTowFileUpload in _tonTowDbContext.TonTowFileUpload
                             join PR in _tonTowDbContext.PoliceReport
                             on tonTowFileUpload.TonTowRptId equals PR.TonTowRptId
                             where tonTowFileUpload.TonTowRptId == TonTowRptId //&& TonTowFileUpload.Status == true
                               select new
                               {
                                   tonTowFileUpload,
                                   PR.JobNum
                               };
            return Ok(FileUpload);
        }
        [HttpPost("UploadTonTowReports")]
        public async Task<ActionResult> UploadTonTowReports(IFormFile FileData, int TonTowReportId, string JobNum, string UserName,string FileType)
        {

            TonTowFileUpload tonTowFileUpload = new TonTowFileUpload();
            try
            {
                string path = "";
                if (FileData.Length > 0)
                {
                    path = Path.GetFullPath(Path.Combine(Environment.CurrentDirectory + "\\TonTowFiles", JobNum));
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }
                    using (var fileStream = new FileStream(Path.Combine(path, FileData.FileName), FileMode.Create))
                    {
                        await FileData.CopyToAsync(fileStream);
                    }
                }
                using (var stream = new MemoryStream())
                {
                    FileData.CopyTo(stream);
                }
                tonTowFileUpload.TonTowRptId = TonTowReportId;
                tonTowFileUpload.FileName = FileData.FileName;
                tonTowFileUpload.FileType = FileType;
                tonTowFileUpload.CreatedDate = DateTime.Now;
                tonTowFileUpload.CreatedBy = UserName;
                var result = _tonTowDbContext.TonTowFileUpload.AddAsync(tonTowFileUpload);
                await _tonTowDbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                //Log ex
                ViewBag.Message = "File Upload Failed";
            }
            return Ok(tonTowFileUpload);
        }

        //[HttpPost]
        //[Route("CreateFile")]
        //public async Task<ActionResult> CreateFile(IFormFile fileData, int tonTowReportId, string jobReportId, string userName)
        //{
        //    try
        //    {
        //        string folderName = jobReportId;
        //        string path = Path.Combine(_hostingEnvironment.ContentRootPath + "TonTowFiles", folderName);
        //        TonTowFileUpload tonTowFileUpload = new TonTowFileUpload();
        //        if (!Directory.Exists(path))
        //        {
        //            Directory.CreateDirectory(path);
        //        }

        //        if (Directory.Exists(path))
        //        {
        //            var byteRes = new byte[] { };
        //            byteRes = IReport.CreateReportFile(path);

        //            System.IO.File.WriteAllBytes(path + "\\" + fileData.FileName + ".pdf", byteRes);
        //        }
        //        tonTowFileUpload.TonTowRptId = tonTowReportId;
        //        tonTowFileUpload.FileName = fileData.FileName;
        //        tonTowFileUpload.FileType = "PoliceRpt";
        //        tonTowFileUpload.CreatedDate = DateTime.Now;
        //        tonTowFileUpload.CreatedBy = userName;
        //        var result = _tonTowDbContext.TonTowFileUpload.AddAsync(tonTowFileUpload);
        //        await _tonTowDbContext.SaveChangesAsync();
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }            
        //    return Ok();
        //}

        //[HttpGet("DonwloadFile")]
        //public IActionResult DownloadFile1(string FileName)
        //{
        //    try
        //    {
        //        // check if the file name is valid
        //        if (string.IsNullOrEmpty(FileName))
        //        {
        //            return BadRequest("File name is not specified.");
        //        }

        //        // check if the file exists
        //        if (!System.IO.File.Exists("Report/"+FileName))
        //        {
        //            return BadRequest("File does not exist.");
        //        }

        //        // create a file stream object
        //        using FileStream fileStream = new FileStream(FileName, FileMode.Open);

        //        // create a byte array to hold the file content
        //        byte[] fileBytes = new byte[fileStream.Length];

        //        // read the file content into the byte array
        //        fileStream.Read(fileBytes, 0, (int)fileStream.Length);

        //        // return the file content as a file download
        //        return File(fileBytes, "application/octet-stream", FileName);
        //    }
        //    catch (Exception ex)
        //    {
        //        // handle the exception and return an error response
        //        return StatusCode(500, ex.Message);
        //    }
        //}

        //[HttpGet("DownloadFile")]
        //public IActionResult DownloadFile(string jobNo, string fileName)
        //{
        //    try
        //    {
        //        // check if the file name is valid
        //        if (string.IsNullOrEmpty(fileName))
        //        {
        //            return BadRequest("File name is not specified.");
        //        }

        //        // check if the file exists
        //        if (!System.IO.File.Exists("TonTowFiles/" + jobNo + "/" + fileName))
        //        {
        //            return BadRequest("File does not exist.");
        //        }

        //        // create a file stream object
        //        using FileStream fileStream = new FileStream("TonTowFiles/" + jobNo + "/" + fileName, FileMode.Open);

        //        // create a byte array to hold the file content
        //        byte[] fileBytes = new byte[fileStream.Length];

        //        // read the file content into the byte array
        //        fileStream.Read(fileBytes, 0, (int)fileStream.Length);

        //        // return the file content as a file download
        //        return File(fileBytes, "application/octet-stream", fileName);
        //    }
        //    catch (Exception ex)
        //    {
        //        // handle the exception and return an error response
        //        return StatusCode(500, ex.Message);
        //    }
        //}
        [HttpGet("DownloadFile")]
        public IActionResult DownloadFile(string jobNo, string fileName)
        {
            try
            {
                // check if the file name is valid
                if (string.IsNullOrEmpty(fileName))
                {
                    return BadRequest("File name is not specified.");
                }

                // check if the file exists
                if (!System.IO.File.Exists("TonTowFiles/" + jobNo + "/" + fileName))
                {
                    return BadRequest("File does not exist.");
                }

                // create a file stream object
                using FileStream fileStream = new FileStream("TonTowFiles/" + jobNo + "/" + fileName, FileMode.Open);

                // create a byte array to hold the file content
                byte[] fileBytes = new byte[fileStream.Length];

                // read the file content into the byte array
                fileStream.Read(fileBytes, 0, (int)fileStream.Length);

                // return the file content as a file download
                return File(fileBytes, "application/octet-stream", fileName);
            }
            catch (Exception ex)
            {
                // handle the exception and return an error response
                return StatusCode(500, ex.Message);
            }
        }
        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        [HttpGet("ForgetPassword")]
        public async Task<IActionResult> ForgetPassword(string userName)
        {
            var UserFound = _tonTowDbContext.TonTowUser.SingleOrDefault(
                  p => p.Username == userName
                  );
            if (UserFound == null)
            {
                return StatusCode(StatusCodes.Status404NotFound, "User Not Found");
            }

            if (UserFound.TonTowRptId != 0)
            {

                var policeReportOperator = _tonTowDbContext.PoliceReportOperatorDtls.SingleOrDefault(
                            p => p.TonTowRptId == UserFound.TonTowRptId
                        );

                var OprDob = policeReportOperator?.DOB;

                if (OprDob == null)
                {
                    return StatusCode(StatusCodes.Status404NotFound, "Dob Not Found");
                }

                if (OprDob != null)
                {
                    var sPass = UserFound.Username + OprDob.ToString().Replace("/", "");
                    CreatePasswordHash(sPass, out byte[] passwordHash, out byte[] passwordSalt);
                    UserFound.PasswordHash = passwordHash;
                    UserFound.PasswordSalt = passwordSalt;
                    _tonTowDbContext.TonTowUser.UpdateRange(UserFound);
                    int res = await _tonTowDbContext.SaveChangesAsync();
                    if (res > 0)
                    {
                        try
                        {

                            var host = _configuration.GetSection("MailSettings:Host").Value;
                            var mailId = _configuration.GetSection("MailSettings:MailId").Value;
                            var pwd = _configuration.GetSection("MailSettings:Pwd").Value;
                            int port = Convert.ToInt16(_configuration.GetSection("MailSettings:Port").Value);

                            var smtpClient = new SmtpClient(host, port)
                            {
                                UseDefaultCredentials = false,
                                EnableSsl = true,
                                Credentials = new NetworkCredential(mailId, pwd)
                            };

                            string msgBody = "Dear User" + Environment.NewLine
                                + "Your Password has beem reset to - " + sPass
                                + Environment.NewLine + "Kindly change the password after login"
                                + Environment.NewLine + Environment.NewLine
                                + "Admin";
                            var mailMessage = new MailMessage
                            {
                                From = new MailAddress(mailId),
                                Subject = "Password Reset",
                                Body = msgBody
                            };
                            mailMessage.To.Add(UserFound.EmailId);

                            await smtpClient.SendMailAsync(mailMessage);

                            Console.WriteLine("Email sent successfully.");
                        }
                        catch (Exception ex)
                        {
                            return StatusCode(StatusCodes.Status400BadRequest, ex.Message);

                        }

                    }
                }

            }
            return Ok();
        }

    }
}
