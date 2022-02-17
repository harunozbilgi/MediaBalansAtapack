using MediaBalans.Domain.Common;

namespace MediaBalans.Domain.Entities
{
    public class AppConfig : BaseEntity
    {
		public string FileCode { get; set; }
		public string Facebook { get; set; }
		public string Instagram { get; set; }
		public string Twitter { get; set; }
		public string FacebookPixel { get; set; }
		public string GoogleAnalytics { get; set; }
		public string MetaTag { get; set; }
		public string PhoneNumber { get; set; }
		public string PhoneNumberOne { get; set; }
		public string Map { get; set; }	
		public string Email { get; set; }
		public string Address { get; set; }
	}
}
