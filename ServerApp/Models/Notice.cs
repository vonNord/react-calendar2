using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServerApp.Models
{
	public class Notice
	{
		public Notice( int error, string content )
		{
			Error = error;
			Content = content;
		}

		public int Error { get; set; }
		public string Content { get; set; }
	}
}
