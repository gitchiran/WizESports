using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Wiz_eSports_Management.Common
{
    public static class WizFileHandling
    {
        public static string UploadAttachments(IList<IFormFile> fileAttachments, string filePath, bool changeFileName)
        {
            string Attachments = "";
            long fileSize = fileAttachments.Sum(f => f.Length);

            if (fileAttachments != null && fileSize > 0)
            {
                foreach (var file in fileAttachments)
                {
                    var fileName = file.FileName;

                    if (changeFileName)
                    {
                        string fileExtension = Path.GetExtension(file.FileName);
                        fileName = Guid.NewGuid().ToString() + fileExtension;
                    }


                    if (!Directory.Exists(filePath))
                    {
                        Directory.CreateDirectory(filePath);
                    }

                    fileSize += file.Length;

                    using (FileStream fs = System.IO.File.Create(Path.Combine(filePath, fileName)))
                    {
                        file.CopyTo(fs);
                        fs.Flush();
                    }

                    Attachments += fileName + ",";
                };

                Attachments = Attachments.TrimEnd(',');

            }

            return Attachments;
        }
    }
}
