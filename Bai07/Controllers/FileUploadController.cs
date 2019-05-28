using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Bai07.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Bai07.Controllers
{
	public class FileUploadController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> UploadFile(IFormFile file)
		{
			if (file == null || file.Length == 0) return Content("file not selected");

			var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "UploadFiles", file.FileName);

			using (var stream = new FileStream(path, FileMode.Create)) { await file.CopyToAsync(stream); }

			return RedirectToAction("ListFiles");
		}

		[HttpPost]
		public async Task<IActionResult> UploadFiles(List<IFormFile> files)
		{
			if (files == null || files.Count == 0) return Content("files not selected");

			foreach (var file in files)
			{
				var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "UploadFiles", file.FileName);

				using (var stream = new FileStream(path, FileMode.Create)) { await file.CopyToAsync(stream); }
			}

			return RedirectToAction("ListFiles");
		}

		[HttpPost]
		public async Task<IActionResult> UploadFileViaModel(FileInputModel model)
		{
			if (model == null || model.FileToUpload == null || model.FileToUpload.Length == 0) return Content("file not selected");

			var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "UploadFiles", model.FileToUpload.FileName);

			using (var stream = new FileStream(path, FileMode.Create)) { await model.FileToUpload.CopyToAsync(stream); }

			return RedirectToAction("ListFiles");
		}

		public IActionResult ListFiles()
		{
			var model = new FilesViewModel();
			var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "UploadFiles");
			DirectoryInfo d = new DirectoryInfo(path);
			foreach (var item in d.GetFiles() )
			{ model.Files.Add(new FileDetails
				{
					Name = item.Name, Path = item.FullName
				});
			}
			return View(model);
		}

		public IActionResult Download(string filename)
		{

			return View("ListFiles");
		}
	}
}