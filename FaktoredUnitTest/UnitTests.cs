using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Faktored;

namespace FaktoredUnitTest
{
	[TestClass]
	public class UnitTests
	{

		/// <summary>
		/// Call CreatePeriodData with 24 periods, should return 2 period groups
		/// </summary>
		[TestMethod]
		public void CreatePeriodDataFor24Periods()
		{
			var periodGroups = Program.CreatePeriodData(new DateTime(2019, 7, 15), 24);

			// Should return 2 period groups
			Assert.AreEqual(periodGroups.Count, 2);
		}

		/// <summary>
		/// Call CreatePeriodData with 60 periods, should return 5 period groups
		/// </summary>
		[TestMethod]
		public void CreatePeriodDataFor60Periods()
		{
			var periodGroups = Program.CreatePeriodData(new DateTime(2019, 7, 15), 60);

			// Should return 5 period groups
			Assert.AreEqual(periodGroups.Count, 5);
		}

		/// <summary>
		/// Call CreatePeriodData with 10000 periods, should return 834 period groups
		/// </summary>
		[TestMethod]
		public void CreatePeriodDataFor10000Periods()
		{
			var periodGroups = Program.CreatePeriodData(new DateTime(2019, 7, 15), 10000);

			// Should return 834 period groups
			Assert.AreEqual(periodGroups.Count, 834);
		}

		/// <summary>
		/// Call CreatePeriodData with 0 periods, should return 0 period groups
		/// </summary>
		[TestMethod]
		public void CreatePeriodDataFor0Periods()
		{
			var periodGroups = Program.CreatePeriodData(new DateTime(2019, 7, 15), 0);

			// Should return 0 period groups
			Assert.AreEqual(periodGroups.Count, 0);
		}

		/// <summary>
		/// Call CreatePeriodData with 12 periods and an invalid date, should return 0 period groups
		/// </summary>
		[TestMethod]
		public void CreatePeriodDataFor0PeriodsAndInvalidDate()
		{
			var periodGroups = Program.CreatePeriodData(new DateTime(9999, 12, 31), 12);

			// Should return 0 period groups
			Assert.AreEqual(periodGroups.Count, 0);
		}


		/// <summary>
		/// Call GetEndOfLastFinancialYearDate with 2019/07/15, should return 2019/06/30
		/// </summary>
		[TestMethod]
		public void GetEndOfLastFinancialYearDateFor2019_07_15()
		{
			var lastEOFYDate = Program.GetEndOfLastFinancialYearDate(new DateTime(2019, 7, 15));

			// Should return 2019/06/30
			Assert.AreEqual(lastEOFYDate, new DateTime(2019, 6, 30));
		}

		/// <summary>
		/// Call GetEndOfLastFinancialYearDate with 9999/12/31, should return 9999/06/30
		/// </summary>
		[TestMethod]
		public void GetEndOfLastFinancialYearDateFor9999_12_31()
		{
			var lastEOFYDate = Program.GetEndOfLastFinancialYearDate(new DateTime(9999, 12, 31));

			// Should return 2019/06/30
			Assert.AreEqual(lastEOFYDate, new DateTime(9999, 6, 30));
		}


	}
}
