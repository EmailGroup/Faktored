using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Faktored.Models
{
	public class PeriodGroup
	{
		public int NumberOfDaysInFinancialYear { get; set; }
		public DateTime EndOfLastFinancialYear { get; set; }
		public List<Period> Periods { get; set; }
	}

}
