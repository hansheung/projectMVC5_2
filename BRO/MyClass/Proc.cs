using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace BRO.MyClass
{
    public class Proc
    {
        public int pPassConv(string sPass)
        {

            int iL = 3;

            int iTotal = 0;

            for (int iLoop = 1; iLoop <= sPass.Length ; iLoop = iLoop + 1)
            {
                //=== legacy VB string functions are 1-based, not 0-based
                string sTemp = sPass.Substring(iLoop - 1, 1);

                int iTemp = Convert.ToInt32(sPass[iLoop - 1]);

                iTotal = iTotal + iTemp * (iL + iLoop - 1);
            }

            //=== THE VB CODE==========================
            //=== For iLoop = 1 To sPass.Length
            //===   iTotal = iTotal + Asc(Mid(sPass, iLoop, 1)) * (iL + iLoop - 1)
            //=== Next

            //== pPassConv = iTotal

            return iTotal;

        }
    }
}