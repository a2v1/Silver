using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SilverGold.Comman
{
    class Conversion
    {
        public static System.Int32 ConToInt(string strvalue)
        {
            try
            {
                return Convert.ToInt32(strvalue);
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public static System.Int32 ConToInt(object strvalue)
        {
            try
            {
                if (strvalue != null & strvalue.ToString().Trim() != "")
                    return Convert.ToInt32(strvalue);
                else
                    return 0;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }


        public static System.Int64 ConToInt64(string strvalue)
        {
            try
            {
                return Convert.ToInt64(strvalue.Trim());
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public static System.Int64 ConToInt64(object strvalue)
        {
            try
            {
                if (strvalue != null & strvalue.ToString() != "")
                    return Convert.ToInt64(strvalue);
                else
                    return 0;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public static System.Int16 ConToInt16(string strvalue)
        {
            try
            {
                return Convert.ToInt16(strvalue.Trim());
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public static System.Int16 ConToInt16(object strvalue)
        {
            try
            {
                if (strvalue != null & strvalue.ToString().Trim() != "")
                    return Convert.ToInt16(strvalue);
                else
                    return 0;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public static System.Decimal ConToDec(string strvalue)
        {
            try
            {
                System.Decimal decVal = 0;
                decVal = Convert.ToDecimal(strvalue.Trim());
                return Math.Round(decVal, 2);
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public static System.Decimal ConToDec5(string strvalue)
        {
            try
            {
                System.Decimal decVal = 0;
                decVal = Convert.ToDecimal(strvalue.Trim());
                return Math.Round(decVal, 5);
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
        public static System.Decimal ConToDec6(string strvalue)
        {
            try
            {
                System.Decimal decVal = 0;
                decVal = Convert.ToDecimal(strvalue.Trim());
                return Math.Round(decVal, 6);
            }
            catch (Exception ex)
            {
                return 0;
            }
        }


        public static Boolean ChkToDec(string strvalue)
        {
            try
            {
                decimal val = Math.Round(Convert.ToDecimal(strvalue.Trim()), 2);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public static decimal ConToDec(object strvalue)
        {
            try
            {
                System.Decimal decVal = 0;
                if (strvalue != null & strvalue.ToString().Trim() != "")
                {
                    decVal = Convert.ToDecimal(strvalue.ToString().Trim());
                }
                else
                    return decVal = 0;

                return Math.Round(decVal, 2);

            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public static decimal ConToDec5(object strvalue)
        {
            try
            {
                System.Decimal decVal = 0;
                if (strvalue != null & strvalue.ToString().Trim() != "")
                {
                    decVal = Convert.ToDecimal(strvalue.ToString().Trim());
                }
                else
                    return decVal = 0;

                return Math.Round(decVal, 5);

            }
            catch (Exception ex)
            {
                return 0;
            }
        }



        public static decimal ConToDec(object strvalue, int Len)
        {
            try
            {
                System.Decimal decVal = 0;
                if (strvalue != null & strvalue.ToString().Trim() != "")
                {
                    decVal = Convert.ToDecimal(strvalue.ToString().Trim());
                }
                else
                    return decVal = 0;

                return Math.Round(decVal, 2);
            }
            catch (Exception ex)
            {
                return 0;
            }
        }



        public static Boolean ChkToDec(object strvalue)
        {
            try
            {
                if (strvalue != null & strvalue.ToString() != "")
                {
                    decimal VAL = Math.Round(Convert.ToDecimal(strvalue), 2);
                    return true;
                }
                else
                    return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }


        public static double ConTodob(string strvalue)
        {
            try
            {
                double decVal = 0;
                decVal = Convert.ToDouble(strvalue.Trim());
                return Math.Round(decVal, 2);
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public static Boolean ChkTodob(string strvalue)
        {
            try
            {
                Double dbVal = Convert.ToDouble(strvalue.Trim());
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }


        public static double ConTodob(object strvalue)
        {
            try
            {
                double decVal = 0;
                if (strvalue != null & strvalue.ToString().Trim() != "")
                {
                    decVal = Convert.ToDouble(strvalue.ToString().Trim());
                }
                else
                    return decVal = 0;

                return Math.Round(decVal, 2);
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public static double ConTodob5(object strvalue)
        {
            try
            {
                double decVal = 0;
                if (strvalue != null & strvalue.ToString().Trim() != "")
                {
                    decVal = Convert.ToDouble(strvalue.ToString().Trim());
                }
                else
                    return decVal = 0;

                return Math.Round(decVal, 5);
            }
            catch (Exception ex)
            {
                return 0;
            }
        }


        public static double ConTodob(string strvalue, int NDecimal)
        {
            try
            {
                return Math.Round(Convert.ToDouble(strvalue), NDecimal);

            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public static Boolean chkTodob(object strvalue)
        {
            try
            {
                double dbval = (System.Double)(strvalue);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public static Boolean chkEmptyOrNull(object strvalue)
        {
            try
            {
                if (string.IsNullOrEmpty(strvalue as string))
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }


        public static Boolean ConTobool(string strvalue)
        {
            try
            {
                return Convert.ToBoolean(strvalue.Trim());
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public static Boolean ConTobool(object strvalue)
        {
            try
            {
                if (strvalue != null & strvalue.ToString().Trim() != "")
                    return Convert.ToBoolean(strvalue.ToString().Trim());
                else
                    return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }


        public static DateTime ConToDT(string strvalue)
        {
            try
            {
                return Convert.ToDateTime(strvalue.Trim());
            }
            catch (Exception ex)
            {
                return DateTime.Today.Date;
            }
        }

        public static DateTime ConToDT(object strvalue)
        {
            try
            {
                if (strvalue == null || strvalue.ToString() == "")
                    return DateTime.Today;
                else
                    return Convert.ToDateTime(strvalue.ToString().Trim());
            }
            catch (Exception ex)
            {
                return DateTime.Today.Date;
            }
        }

        public static string ConToStr(object strvalue)
        {
            try
            {
                if (strvalue != null)
                    return strvalue.ToString();
                else
                    return string.Empty;
            }
            catch (Exception ex)
            {
                return string.Empty;
            }
        }

        public static DateTime GetDateStr(string str)
        {
            DateTime strdate;
            string str1;
            strdate = Convert.ToDateTime("01/01/2001");
            str1 = str.Substring(3, 2) + "/" + str.Substring(0, 2) + "/" + str.Substring(6, 4);
            strdate = Convert.ToDateTime(str1);
            strdate = strdate.Date;


            return strdate;
        }
    }
}