using System;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;
using System.Text.Unicode;
using System.Runtime.InteropServices;
using System.Text.Encodings.Web;
using MSScriptControl;



//using math;

public class DeckEncoder
{
    //("JavaScriptLibrary.JavaScript.JavaScrpit.js","application/x-javascript")]


   // private static void IncludeJavaScript
   //     (ClientScriptManager manager, string resourceName)
   // {
  //      var type = typeof(JavaScriptLibrary.JavaScriptHelper);
  //      manager.RegisterClientScriptResource(type, resourceName);
  //  }

    const int DECK_FIELD_LENGTH_BITS = 5;

    //WebBrowser Browser = new WebBrowser();

    public string encodedeck(DraftController drafter)
    {
        if (drafter.DraftPick == null) {
            return "";
        }

        string output = "";

        output = encodeLengthLeadingValue(2);
        output += encodeLengthLeadingValue(0);

        output += encodeLengthLeadingValue(drafter.DraftPick.id);

        int draftlength = drafter.LogiUnitsDrafted.Count() + drafter.infUnitsDrafted.Count() + drafter.artUnitsDrafted.Count() + drafter.TankUnitsDrafted.Count() + drafter.RecUnitsDrafted.Count() + drafter.AAUnitsDrafted.Count() + drafter.helUnitsDrafted.Count() + drafter.airUnitsDrafted.Count();

        output += encodeLengthLeadingValue(draftlength);

        List<int> BinXPLenL = new List<int>();
        List<int> BinIDLenL = new List<int>();
        foreach (DraftUnit unit in drafter.alldrafted)
        {
            BinXPLenL.Add(xpLength(unit.Level));
            BinIDLenL.Add(idLength(unit.BaseUnit.id));
        }
        int temp = BinXPLenL.Max();
        int BinXPLen = Convert.ToString(temp, 2).Length;

        temp = BinIDLenL.Max();
        int temp2 = Math.Max(temp, 10);
        int BinIDLen = Convert.ToString(temp2, 2).Length;

        output += encodeValue(BinXPLen);
        output += encodeValue(-1, Convert.ToString(BinIDLen, 2));

        foreach (DraftUnit unit in drafter.LogiUnitsDrafted)
        {
            output += encodeValue(unit.Level, "", BinXPLen) + encodeValue(unit.BaseUnit.id, "", BinIDLen);

            if (unit.transport != null)
            { output += encodeValue(unit.transport.id, "", BinIDLen); }
            else
            {
                output += encodeValue(0, "", BinIDLen);
            }

        }
        foreach (DraftUnit unit in drafter.infUnitsDrafted)
        {
            output += encodeValue(unit.Level, "", BinXPLen) + encodeValue(unit.BaseUnit.id, "", BinIDLen);

            if (unit.transport != null)
            { output += encodeValue(unit.transport.id, "", BinIDLen); }
            else
            {
                output += encodeValue(0, "", BinIDLen);
            }

        }
        foreach (DraftUnit unit in drafter.artUnitsDrafted)
        {
            output += encodeValue(unit.Level, "", BinXPLen) + encodeValue(unit.BaseUnit.id, "", BinIDLen);

            if (unit.transport != null)
            { output += encodeValue(unit.transport.id, "", BinIDLen); }
            else
            {
                output += encodeValue(0, "", BinIDLen);
            }

        }
        foreach (DraftUnit unit in drafter.TankUnitsDrafted)
        {
            output += encodeValue(unit.Level, "", BinXPLen) + encodeValue(unit.BaseUnit.id, "", BinIDLen);

            if (unit.transport != null)
            { output += encodeValue(unit.transport.id, "", BinIDLen); }
            else
            {
                output += encodeValue(0, "", BinIDLen);
            }

        }
        foreach (DraftUnit unit in drafter.RecUnitsDrafted)
        {
            output += encodeValue(unit.Level, "", BinXPLen) + encodeValue(unit.BaseUnit.id, "", BinIDLen);

            if (unit.transport != null)
            { output += encodeValue(unit.transport.id, "", BinIDLen); }
            else
            {
                output += encodeValue(0, "", BinIDLen);
            }

        }
        foreach (DraftUnit unit in drafter.AAUnitsDrafted)
        {
            output += encodeValue(unit.Level, "", BinXPLen) + encodeValue(unit.BaseUnit.id, "", BinIDLen);

            if (unit.transport != null)
            { output += encodeValue(unit.transport.id, "", BinIDLen); }
            else
            {
                output += encodeValue(0, "", BinIDLen);
            }

        }
        foreach (DraftUnit unit in drafter.helUnitsDrafted)
        {
            output += encodeValue(unit.Level, "", BinXPLen) + encodeValue(unit.BaseUnit.id, "", BinIDLen);

            if (unit.transport != null)
            { output += encodeValue(unit.transport.id, "", BinIDLen); }
            else
            {
                output += encodeValue(0, "", BinIDLen);
            }

        }
        foreach (DraftUnit unit in drafter.airUnitsDrafted)
        {
            output += encodeValue(unit.Level, "", BinXPLen) + encodeValue(unit.BaseUnit.id, "", BinIDLen);

            if (unit.transport != null)
            { output += encodeValue(unit.transport.id, "", BinIDLen); }
            else
            {
                output += encodeValue(0, "", BinIDLen);
            }

        }

        output += encodeValue(1);
        output = bitStringtoText(output);
        return output;
    }

    int xpLength(int xp)
    {
        string temp = Convert.ToString(xp, 2);
        return temp.Length;
    }
    int idLength(int id)
    {
        string temp = Convert.ToString(id, 2);
        return temp.Length;
    }

    string encodeValue(int Intvalue = -1, string Svalue = null, int length = DECK_FIELD_LENGTH_BITS)
    {
        string temp = "";
        if (Intvalue != -1)
        {
            temp = Convert.ToString(Intvalue, 2);

        }
        else
        {
            temp = Svalue;

        }
        string temp2 = temp.PadLeft(temp.Length + (length - temp.Length), '0');
        return temp2;
    }
    string encodeLengthLeadingValue(int value)
    {
        string output = Convert.ToString(value, 2);
        return encodeValue(output.Length) + output;
    }

    string bitStringtoText(string str)
    {
        string output = "";


        if (str.Length % 8 != 0)
        {
            int temp = (str.Length % 8);
            str = str.PadRight(str.Length + temp, '0');
        }
        for (int i = 0; i + 8 <= str.Length; i += 8)
        {

            //if (segment.Length < 8)
            //
            // segment = segment.PadLeft(8, '0');
            //}

            string segment = str.Substring(i, 8);

            string justNumbers = string.Concat(segment.Where(char.IsDigit));
            //byte temp = Convert.ToByte(justNumbers, 2);
            string temp = Encoding.UTF8.GetString(Regex.Split(justNumbers, "(.{8})").Where(binary => !String.IsNullOrEmpty(binary)).Select(binary => Convert.ToByte(binary, 2)).ToArray());
            //char temp2 = Convert.ToChar(temp);
            output += temp;
        }
        return encoding(output);
    }


    public static string Reverse(string s)
    {
        char[] charArray = s.ToCharArray();
        Array.Reverse(charArray);
        return new string(charArray);
    }


    public string encoding(string toEncode)
    {
        //JavaScriptEncoder scriptEncoder = null;
        byte[] bytes = Encoding.GetEncoding(28591).GetBytes(toEncode);
        string toReturn = System.Convert.ToBase64String(bytes);
        //accessJava();
        return toReturn;
        //return scriptEncoder.Encode(toEncode);
    }
    public static string Base64Encode(string plainText)
    {
        var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
        return System.Convert.ToBase64String(plainTextBytes);
    }

}
