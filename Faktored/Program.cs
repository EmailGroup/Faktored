using Faktored.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Faktored
{
	public class Program
	{
		/// <summary>
		/// Main program to display the results of the calls to create the period data.
		/// </summary>
		/// <param name="args"></param>
		public static void Main(string[] args)
		{
			// Setup the input values for the application
			DateTime seedDate = new DateTime(2019, 7, 15);
			int numberOrPeriods = 24;

			// This is only so you can see upto the number or periods you have set in the console application window.
			Console.BufferHeight = 32000; // The max value for this is 32767.

			var periodGroups = CreatePeriodData(seedDate, numberOrPeriods);

			// Display the period groups.
			Console.WriteLine("\n Period Groups");
			Console.WriteLine(JsonConvert.SerializeObject(periodGroups, Formatting.Indented));

		}

		/// <summary>
		/// Returns a grouped list of period data based on test rules
		/// </summary>
		/// <param name="seedDate"></param>
		/// <param name="numberOrPeriods"></param>
		/// <returns></returns>
		public static List<PeriodGroup> CreatePeriodData(DateTime seedDate, int numberOrPeriods)
		{

			// Setup the console display for the test
			Console.WriteLine("Start Date: " + seedDate.ToString("yyyy/MM/dd") + "\n");
			Console.WriteLine("Start Period    End Period    Period Number");

			List<PeriodGroup> periodGroups = new List<PeriodGroup>();
			PeriodGroup periodGroup = new PeriodGroup();
			DateTime currentEndOfLastFinancialYear = new DateTime(1900, 1, 1);

			try
			{
				// Call the infinte period generator with the seed date and number of periods required.
				foreach (Period period in GetNextPeriod(seedDate, numberOrPeriods))
				{
					// Display the infinate periods used in this test.
					Console.WriteLine(period.PeriodStart.ToString("yyyy/MM/dd") + "      " + period.PeriodStart.ToString("yyyy/MM/dd") + "    " + period.PeriodNumber.ToString());

					// Create a PeriodGroup object and populate it with the returned periods until a financial year is completed.
					var eofyDate = GetEndOfLastFinancialYearDate(period.PeriodStart);

					if (currentEndOfLastFinancialYear != eofyDate)
					{
						if (currentEndOfLastFinancialYear != new DateTime(1900, 1, 1))
						{
							periodGroups.Add(periodGroup);
						}

						currentEndOfLastFinancialYear = eofyDate;
						periodGroup = new PeriodGroup();
						periodGroup.Periods = new List<Period>();
						periodGroup.EndOfLastFinancialYear = eofyDate;
						periodGroup.NumberOfDaysInFinancialYear = DateTime.IsLeapYear(period.PeriodStart.Year) ? 366 : 365;
						periodGroup.Periods.Add(period);
					}
					else
					{
						periodGroup.Periods.Add(period);
					}
				}

				// Add the last period group to the list of period groups
				if (periodGroup?.Periods?.Count > 0)
				{
					periodGroups.Add(periodGroup);
				}

			}
			catch (Exception)
			{
				// Do nothing and fall through to return and empty period group.
			}
			
			return periodGroups;
		}

		/// <summary>
		/// This function will generate an infinite list of periods for the 
		/// number or periods to generate based on the seed date passed in.
		/// </summary>
		/// <param name="seedDate"></param>
		/// <param name="numberOrPeriods"></param>
		/// <returns></returns>
		public static IEnumerable<Period> GetNextPeriod(DateTime seedDate, int numberOrPeriods)
		{

			int periodCounter = 0;

			// Get a start date from the seed date that was passed in
			var startDate = new DateTime(seedDate.Year, seedDate.Month, 1);

			// This will return a Period object created using the test algorithm rules
			var period = SetPeriodFromStartDate(startDate);

			for (;;)
			{
				// Once we have completed the number of periods to generate we exit.
				if (periodCounter == numberOrPeriods) break;

				// if this is the first time in then we just yield the period already created.
				// else we will create a new period based of off the current period for the next month.
				if (periodCounter > 0)
				{
					period = SetPeriodFromStartDate(period.PeriodStart.AddMonths(1));
				}

				yield return period;

				periodCounter++;
			}
		}

		/// <summary>
		/// This will return a Period object created using the test algorithm rules
		/// </summary>
		/// <param name="startDate"></param>
		/// <returns></returns>
		public static Period SetPeriodFromStartDate(DateTime startDate)
		{

			Period period = new Period
			{
				PeriodStart = startDate,
				PeriodEnd = startDate.AddMonths(1).AddDays(-1),
				PeriodNumber = startDate.Month > 6 ? startDate.Month - 6 : startDate.Month + 6
			};

			return period;
		}

		/// <summary>
		/// Return the end of financial year date for the previous year.
		/// </summary>
		/// <param name="date"></param>
		/// <returns></returns>
		public static DateTime GetEndOfLastFinancialYearDate(DateTime date)
		{
			// End of a financial year is 30th June in Australia
			DateTime eofyDate;

			if (date.Month < 7)
			{
				eofyDate = new DateTime(date.Year - 1, 6, 30);
			}
			else
			{
				eofyDate = new DateTime(date.Year, 6, 30);
			}

			return eofyDate;
		}

	}
}
