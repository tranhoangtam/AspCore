using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Bai07.Models
{
	public class FileInputModel
	{
		public IFormFile FileToUpload { get; set; }
	}
}
