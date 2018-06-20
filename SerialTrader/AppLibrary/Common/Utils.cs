using System;
using System.Web;

namespace AppLibrary.Common
{
	/// <summary>
	/// General utility methods for UI.
	/// </summary>
	/// <remarks></remarks>
	public class Utils
	{

		/// <summary>
		/// Returns the value of parameter if it exist, if it doesn't throws and exception
		/// </summary>
		/// <param name="ParameterValue">The value of the parameter</param>
		/// <returns>The parameter as string</returns>
		public static string GetParameter(object ParameterValue)
		{
			if ((ParameterValue != null))
			{
				return ParameterValue.ToString();
			}
			else
			{
				throw new Exception("Missing parameter.");
			}
		}

		/// <summary>
		/// Returns an integer parameter if it exist, if it doesn't throws and exception
		/// </summary>
		/// <param name="ParameterValue">The value of the parameter</param>
		/// <returns>The parameter as integer</returns>
		public static int GetParameterInteger(object ParameterValue)
		{
			if ((ParameterValue != null))
			{
				return Convert.ToInt32(ParameterValue.ToString());
			}
			else
			{
				throw new Exception("Missing parameter.");
			}
		}

		/// <summary>
		/// Returns a formated integer
		/// </summary>
		/// -----------------------------------------------------------------------------
		public static string SetFormString(string theString)
		{
			if ((theString == null))
			{
				return "";
			}
			else
			{
				return theString;
			}
		}

		/// <summary>
		/// Returns a formated boolean
		/// </summary>
		/// -----------------------------------------------------------------------------
		public static Nullable<bool> SetFormBoolean(Nullable<bool> theBoolean)
		{
			Nullable<bool> bFalse = false;
			if (!(theBoolean.HasValue))
			{
				return bFalse;
			}
			else
			{
				return theBoolean;
			}
		}

		/// <summary>
		/// Returns a formated integer
		/// </summary>
		/// <param name="theInteger">the integer to format</param>
		/// <returns>The formated integer</returns>
		/// -----------------------------------------------------------------------------
		public static string SetFormInteger(Nullable<int> theInteger)
		{
			if (!(theInteger.HasValue))
			{
				return "";
			}
			else
			{
				return theInteger.ToString();
			}
		}

        /// <summary>
        /// Returns a formated integer with comma separator
        /// </summary>
        /// <param name="theInteger"></param>
        /// <returns></returns>
        public static string SetFormIntegerWithCommaSeparator(Nullable<double> theInteger)
        {
            if (!(theInteger.HasValue))
            {
                return "";
            }
            else
            {
                return String.Format("{0:##,##0}", theInteger);
            }
        }

		/// <summary>
		/// Returns a formated long
		/// </summary>
		/// <param name="theLong">the long to format</param>
		/// <returns>The formated long</returns>
		/// -----------------------------------------------------------------------------
		public static string SetFormLong(Nullable<long> theLong)
		{
			if (!(theLong.HasValue))
			{
				return "";
			}
			else
			{
				return theLong.ToString();
			}
		}

		/// <summary>
		/// Returns a formated double
		/// </summary>
		/// <param name="theDouble">the double to format</param>
		/// <returns>The formated double</returns>
		/// -----------------------------------------------------------------------------
		public static string SetFormDouble(Nullable<double> theDouble)
		{
			if (!(theDouble.HasValue))
			{
				return "";
			}
			else
			{
                return String.Format("{0:##,##0.00}", theDouble);
			}
		}

        /// <summary>
        /// Returns a formated double
        /// </summary>
        /// <param name="theDouble">the double to format</param>
        /// <returns>The formated double</returns>
        /// -----------------------------------------------------------------------------
        public static string SetFormDoubleWithFormat(Nullable<double> theDouble)
        {
            if (!(theDouble.HasValue))
            {
                return "0.00";
            }
            else
            {
                return String.Format("{0:##,##0.00}", theDouble);
            }
        }

		/// <summary>
		/// Returns a formated single
		/// </summary>
		/// <param name="theSingle">the single to format</param>
		/// <returns>The formated single</returns>
		/// -----------------------------------------------------------------------------
		public static string SetFormSingle(Nullable<float> theSingle)
		{
			if (!(theSingle.HasValue))
			{
				return "";
			}
			else
			{
                return String.Format("{0:##,##0.00}", theSingle);
			}
		}

		/// <summary>
		/// Returns a formated single
		/// </summary>
		/// <param name="theSingle">the single to format</param>
		/// <returns>The formated single</returns>
		/// -----------------------------------------------------------------------------
		public static string SetFormSingleDecimals(Nullable<float> theSingle)
		{
			if (!(theSingle.HasValue))
			{
				return "";
			}
			else
			{
				return String.Format("{0:##,##0.00000}", theSingle);
			}
		}

		/// <summary>
		/// Returns a formated decimal
		/// </summary>
		/// <param name="theDecimal">the decimal to format</param>
		/// <returns>The formated decimal</returns>
		/// -----------------------------------------------------------------------------
		public static string SetFormDecimal(Nullable<decimal> theDecimal)
		{
			if (!(theDecimal.HasValue))
			{
				return "";
			}
			else
			{
                return String.Format("{0:##,##0.00}", theDecimal);
			}
		}

		/// <summary>
		/// Returns a decimal number checking for null
		/// </summary>
		public static Nullable<decimal> GetValueDecimal(Nullable<decimal> theDecimal)
		{
			if (!(theDecimal.HasValue))
			{
				return 0.0M;
			}
			else
			{
				return theDecimal;
			}

		}

		/// <summary>
		/// Returns a formated date
		/// </summary>
		/// <param name="theDate">the date</param>
		/// <returns>The formated date</returns>
		/// -----------------------------------------------------------------------------
		public static string SetFormDate(Nullable<DateTime> theDate)
		{
			if (!(theDate.HasValue))
			{
				return "";
			}
			else
			{
				return ((DateTime)theDate).ToShortDateString();
			}
		}

		/// <summary>
		/// Returns the month
		/// </summary>
		/// <param name="theDate">the date</param>
		/// <returns>The formated date</returns>
		/// -----------------------------------------------------------------------------
		public static string SetFormMonth(Nullable<DateTime> theDate)
		{
			if (!(theDate.HasValue))
			{
				return "";
			}
			else
			{
                return String.Format("{0:MMM}", theDate);
			}
		}

		/// <summary>
		/// Returns the year
		/// </summary>
		/// <param name="theDate">the date</param>
		/// <returns>The formated date</returns>
		/// -----------------------------------------------------------------------------
		public static string SetFormYear(Nullable<DateTime> theDate)
		{
			if (!(theDate.HasValue))
			{
				return "";
			}
			else
			{
                return String.Format("{0:yyyy}", theDate);
			}
		}

		/// <summary>
		/// Returns a formated time
		/// </summary>
		/// <param name="theDate">the date</param>
		/// <returns>The formated time</returns>
		/// -----------------------------------------------------------------------------
		public static string SetFormTime(Nullable<DateTime> theDate)
		{
			if (!(theDate.HasValue))
			{
				return "";
			}
			else
			{
				return ((DateTime)theDate).ToShortTimeString();
			}
		}

		/// <summary>
		/// Returns a date period
		/// </summary>
		public static string SetFormDatePeriod(Nullable<DateTime> theDate, string periodName)
		{

			if (!(theDate.HasValue))
			{
				return "No Date";
			}
			else
			{
				switch (periodName) 
                {
					case "Month":
                        return String.Format("{0:MMM-yy}", theDate);
					case "Quarter":
						switch (((DateTime)theDate).Month) 
                        {
							case 1:
							case 2:
							case 3:
                                return "Q1-" + String.Format("{0:yy}", theDate);
							case 4:
							case 5:
							case 6:
                                return "Q2-" + String.Format("{0:yy}", theDate);
							case 7:
							case 8:
							case 9:
                                return "Q3-" + String.Format("{0:yy}", theDate);
							case 10:
							case 11:
							case 12:
                                return "Q4-" + String.Format("{0:yy}", theDate);
							default:
								return "??-??";
						}
						//break;
					case "Year":
                        return String.Format("{0:yyyy}", theDate);
					default:
						return "????";
				}
			}

		}

        /// <summary>
        /// Retrun string format of date
        /// </summary>
        /// <param name="periodName"></param>
        /// <param name="theDate"></param>
        /// <returns></returns>
        public static string SetFormDatePeriod(string periodName, Nullable<DateTime> theDate)
        {

            if (!(theDate.HasValue))
            {
                return "No Date";
            }
            else
            {
                switch (periodName)
                {
                    case "Month":
                        return String.Format("{0:yyyy-MMM}", theDate);
                    case "Quarter":
                        switch (((DateTime)theDate).Month)
                        {
                            case 1:
                            case 2:
                            case 3:
                                return String.Format("{0:yy}", theDate) + "-Q1";
                            case 4:
                            case 5:
                            case 6:
                                return String.Format("{0:yy}", theDate) + "-Q2";
                            case 7:
                            case 8:
                            case 9:
                                return String.Format("{0:yy}", theDate) + "-Q3";
                            case 10:
                            case 11:
                            case 12:
                                return String.Format("{0:yy}", theDate) + "-Q4";
                            default:
                                return "??-??";
                        }
                    //break;
                    case "Year":
                        return String.Format("{0:yyyy}", theDate);
                    default:
                        return "????";
                }
            }

        }

		/// <summary>
		/// Returns a formated double with one decimals
		/// </summary>
		/// <param name="theDouble">the float to format</param>
		/// <returns>The formated float</returns>
		/// -----------------------------------------------------------------------------
		public static string FormatDoubleOneDecimal(Nullable<double> theDouble)
		{
			if (!(theDouble.HasValue))
			{
				return "";
			}
			else
			{
                return String.Format("{0:##,##0.0}", theDouble);
			}
		}

		/// <summary>
		/// Returns a formated double with two decimals
		/// </summary>
		/// <param name="theDouble">the float to format</param>
		/// <returns>The formated float</returns>
		/// -----------------------------------------------------------------------------
		public static string FormatDoubleTwoDecimals(Nullable<double> theDouble)
		{
			if (!(theDouble.HasValue))
			{
				return "";
			}
			else
			{
                return String.Format("{0:##,##0.00}", theDouble);
			}
		}

		/// <summary>
		/// Converts an string read on a web form to an string object
		/// </summary>
		/// -----------------------------------------------------------------------------
		public static string GetFormString(string str)
		{
            if (str == null || str == string.Empty)
            {
                return null;
            }
            
            if (str.Trim() == "")
			{
				return null;
			}
			else
			{
                return str.Trim();
			}
		}

		/// <summary>
		/// Converts an string read on a web form to a DateTime object
		/// </summary>
		/// <param name="strDate">Then String with the date</param>
		/// <returns>The DateTime object</returns>
		/// -----------------------------------------------------------------------------
        public static Nullable<DateTime> GetFormDate(string strDate)
        {
            DateTime result= new DateTime();
            if (DateTime.TryParse(strDate, out result))
            {
                return result;
            }
            return null;
        }

		/// <summary>
		/// Converts two strings read on a web form to a DateTime object.
		/// </summary>
		/// <param name="strDate">Then String with the date</param>
        /// <param name="strTime">Then String with the time</param>
        /// <returns>The DateTime object</returns>
		/// -----------------------------------------------------------------------------
		public static Nullable<DateTime> GetFormDate(string strDate, string strTime)
		{
            if (strDate == null || strDate == string.Empty)
            {
                return null;
            }
            if (strDate.Trim() == "")
			{
				return null;
			}
			else
			{
                if (strTime == null || strTime == string.Empty)
                {
                    return Convert.ToDateTime(strDate);
                }

                if (strTime.Trim() == "")
				{
					return Convert.ToDateTime(strDate);
				}
				else
				{
					return Convert.ToDateTime(strDate + " " + strTime);
				}
			}
		}

		/// <summary>
		/// Converts a string read on a web form to an integer
		/// </summary>
		/// <param name="strInteger">the string with the integer value</param>
		/// <returns>The integer value</returns>
		/// -----------------------------------------------------------------------------
		public static Nullable<int> GetFormInteger(string strInteger)
		{
            if (strInteger == null || strInteger == string.Empty)
            {
                return null;
            }

            if (strInteger.Trim() == "")
			{
				return null;
			}
			else
			{
				return Convert.ToInt32(strInteger);
			}
		}

        /// <summary>
        /// Get Integer Value from Comma Seperated text
        /// </summary>
        /// <param name="strInteger"></param>
        /// <returns></returns>
        public static Nullable<int> GetFormCommaSeperatedInteger(string strInteger)
        {
            if (strInteger == null || strInteger == string.Empty)
            {
                return null;
            }

            if (strInteger.Trim() == "")
            {
                return null;
            }
            else
            {
                string[] values = strInteger.Split(',');
                if (values.Length != 0)
                {
                    string intValue = "";
                    foreach (string value in values)
                    {
                        intValue += value;
                    }
                    return Convert.ToInt32(intValue);
                }
                else
                {
                    return null;
                }
                
            }
        }

		/// <summary>
		/// Converts a string read on a web form to an long
		/// </summary>
		/// <param name="strLong">the string with the long value</param>
		/// <returns>The long value</returns>
		/// -----------------------------------------------------------------------------
		public static Nullable<long> GetFormLong(string strLong)
		{
            if (strLong == null || strLong == string.Empty)
            {
                return null;
            }

            if (strLong.Trim() == "")
			{
				return null;
			}
			else
			{
				return Convert.ToInt64(strLong);
			}
		}

		/// <summary>
		/// Returns a fixed list of IDs. i.e.  ,9,6,7 -> 9,6,7
		/// </summary>
		/// <param name="IDs">The list of IDs</param>
		/// <returns>A string with the formated list of IDs</returns>
		public static string GetFormIDsList(string IDs)
		{
            if (IDs == null || IDs == string.Empty)
            {
                return null;
            }
            
            if (IDs == "")
			{
				return null;
			}
			else
			{
                return IDs.Substring(1, IDs.Length - 1); //IDs.Length 
			}
		}

		/// <summary>
		/// Converts a string read on a web form to a double
		/// </summary>
		/// <param name="strDouble">the string with the double value</param>
		/// <returns>The double value</returns>
		/// -----------------------------------------------------------------------------
		public static Nullable<double> GetFormDouble(string strDouble)
		{
            if (strDouble == null || strDouble == string.Empty)
            {
                return null;
            }

            if (strDouble.Trim() == "")
			{
				return null;
			}
			else
			{
				return Convert.ToDouble(strDouble);
			}
		}

		/// <summary>
		/// Converts a string read on a web form to a Single
		/// </summary>
		/// <param name="strSingle">the string with the Single value</param>
		/// <returns>The Single value</returns>
		/// -----------------------------------------------------------------------------
		public static Nullable<float> GetFormSingle(string strSingle)
		{
            if (strSingle == null || strSingle == string.Empty)
            {
                return null;
            }

            if (strSingle.Trim() == "")
			{
				return null;
			}
			else
			{
				return Convert.ToSingle(strSingle);
			}
		}

		/// <summary>
		/// Converts a string read on a web form to a decimal
		/// </summary>
		/// <param name="strDecimal">the string with the decimal value</param>
		/// <returns>The decimal value</returns>
		/// -----------------------------------------------------------------------------
		public static Nullable<decimal> GetFormDecimal(string strDecimal)
		{
            if (strDecimal == null || strDecimal == string.Empty)
            {
                return null;
            }

            if (strDecimal.Trim() == "")
			{
				return null;
			}
			else
			{
				return Convert.ToDecimal(strDecimal);
			}
		}


		/// <summary>
		/// Returns text for a boolean value
		/// </summary>
		/// <param name="theBoolean">the boolean to format</param>
		/// <returns>A string with boolean info</returns>
		/// -----------------------------------------------------------------------------
		public static string ViewBooleanText(bool theBoolean)
		{
			if (theBoolean)
			{
				return "Yes";
			}
			else
			{
				return "No";
			}
		}

		/// <summary>
		/// Gives format to an addess
		/// </summary>
		public static string FormatAddress(Nullable<int> StreetNoFrom, Nullable<int> StreetNoTo, string StreetName, string StreetDir, string StreetSuffix)
		{
			string StreetNoStr = null;
			string AddressStr = "";

			if ((StreetNoFrom.HasValue))
			{
				if ((StreetNoTo.HasValue))
				{
					if (((int)StreetNoFrom == (int)StreetNoTo))
					{
						StreetNoStr = StreetNoFrom.ToString();
					}
					else
					{
						StreetNoStr = StreetNoFrom.ToString() + " - " + StreetNoTo.ToString();
					}
				}
			}

            if ((StreetNoStr != null)) AddressStr = AddressStr + StreetNoStr + " ";
            if ((StreetDir != null)) AddressStr = AddressStr + StreetDir + " ";
            if ((StreetName != null)) AddressStr = AddressStr + StreetName + " ";
			if ((StreetSuffix != null)) AddressStr = AddressStr + StreetSuffix; 

			return AddressStr.Trim();
		}

        /// <summary>
        /// Gives format to an addess
        /// </summary>
        public static string FormatAddress(Nullable<int> StreetNoFrom, Nullable<int> StreetNoTo, string StreetNo, string StreetName, string StreetDir, string StreetSuffix)
        {
            string StreetNoStr = null;
            string AddressStr = "";

            if ((StreetNoFrom.HasValue))
            {
                if ((StreetNoTo.HasValue))
                {
                    if (((int)StreetNoFrom == (int)StreetNoTo))
                    {
                        StreetNoStr = StreetNoFrom.ToString();
                    }
                    else
                    {
                        StreetNoStr = StreetNoFrom.ToString() + " - " + StreetNoTo.ToString();
                    }
                }
                else
                {
                    StreetNoStr = StreetNoFrom.ToString();
                }
            }
            else
            {
                if ((StreetNoTo.HasValue))
                {
                    StreetNoStr = StreetNoTo.ToString();
                }
                else
                {
                    StreetNoStr = StreetNo != null ? StreetNo.ToString() : string.Empty;
                }
            }

            if ((StreetNoStr != null)) AddressStr = AddressStr + StreetNoStr + " ";
            if ((StreetDir != null)) AddressStr = AddressStr + StreetDir + " ";
            if ((StreetName != null)) AddressStr = AddressStr + StreetName + " ";
            if ((StreetSuffix != null)) AddressStr = AddressStr + StreetSuffix;

            return AddressStr.Trim();
        }

    

        /// <summary>
		/// Format for an address with no Street from an to, just street No.
		/// </summary>
		public static string FormatAddress(string StreetNo, string StreetName, string StreetDir, string StreetSuffix)
		{
			string AddressStr = "";

            if ((StreetNo != null)) AddressStr = AddressStr + StreetNo + " ";
            if ((StreetName != null)) AddressStr = AddressStr + StreetName + " ";
            if ((StreetDir != null)) AddressStr = AddressStr + StreetDir + " ";
            if ((StreetSuffix != null)) AddressStr = AddressStr + StreetSuffix;
            
			return AddressStr.Trim();
		}
        /// <summary>
        /// Converts an string read on a web form to a String object
        /// </summary>
        /// <param name="strStepType">Then String with the StepType</param>
        /// <returns>The String object</returns>
        /// -----------------------------------------------------------------------------
        public static String GetFormStepType(string strStepType)
        {
            if (strStepType == null || strStepType == string.Empty || strStepType == "''")
            {
                return null;
            }
            else
            {
                return strStepType;
            }
        }

        public static TimeSpan? GetFormTimeSpam(object time)
        {
            if (time == null)
            {
                return null;
            }
            else
            {
                return TimeSpan.Parse(time.ToString());
            }
        }
	}
}
