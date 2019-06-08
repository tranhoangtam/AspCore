using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Bai08.Models
{
	public class BirthDateCheckAttribute : ValidationAttribute
	{
		public override bool IsValid(object value)
		{
			DateTime data = (DateTime)value;
			return data < DateTime.Now;
		}

		protected override ValidationResult IsValid(object value, ValidationContext validationContext)
		{
			DateTime data = (DateTime)value;
			if (data < DateTime.Now)
			{
				return ValidationResult.Success;
			}
			return new ValidationResult("Không thể sinh trong tương lai");
		}
	}
}
