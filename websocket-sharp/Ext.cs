#region MIT License
/**
 * Ext.cs
 *
 * The MIT License
 *
 * Copyright (c) 2010 sta.blockhead
 * 
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 *
 * The above copyright notice and this permission notice shall be included in
 * all copies or substantial portions of the Software.
 * 
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
 * THE SOFTWARE.
 */
#endregion

using System;
using System.Collections.Generic;
using System.Linq;

namespace WebSocketSharp
{
  public static class Ext
  {
    public static bool EqualsWithSaveTo(this int asbyte, char c, List<byte> dist)
    {
      byte b = (byte)asbyte;
      dist.Add(b);
      return b == Convert.ToByte(c);
    }

    public static uint GenerateKey(this Random rand, int space)
    {
      uint max = (uint)(0xffffffff / space);

      int upper16 = (int)((max & 0xffff0000) >> 16);
      int lower16 = (int)(max & 0x0000ffff);

      return ((uint)rand.Next(upper16 + 1) << 16) + (uint)rand.Next(lower16 + 1);
    }

    public static char GeneratePrintableASCIIwithoutSPandNum(this Random rand)
    {
        int ascii = rand.Next(2) == 0 ? rand.Next(33, 48) : rand.Next(58, 127);
        return Convert.ToChar(ascii);
    }

    public static string GenerateSecKey(this Random rand, out uint key)
    {
      int space = rand.Next(1, 13);
      int ascii = rand.Next(1, 13);

      key = rand.GenerateKey(space);

      long mKey = key * space;
      List<char> secKey = new List<char>(mKey.ToString().ToCharArray());

      int i;
      for (int n = 0; n < ascii; n++)
      {
        i = rand.Next(secKey.Count + 1);
        secKey.Insert(i, rand.GeneratePrintableASCIIwithoutSPandNum());
      }

      for (int n = 0; n < space; n++)
      {
        i = rand.Next(1, secKey.Count);
        secKey.Insert(i, ' ');
      }

      return new String(secKey.ToArray());
    }

    public static IEnumerable<int> IndexOf<T>(this T[] array, T val)
    {
      for (int i = 0; i < array.Length; i++)
      {
        if (array[i].Equals(val))
        {
          yield return i;
        }
      }
    }

    public static T[] InitializeWith<T>(this T[] array, T val)
    {
      for (int i = 0; i < array.Length; i++)
      {
        array[i] = val;
      }

      return array;
    }

    public static Byte[] InitializeWithPrintableASCII(this Byte[] bytes, Random rand)
    {
      for (int i = 0; i < bytes.Length; i++)  
      {  
        bytes[i] = (byte)rand.Next(32, 127);
      }  
  
      return bytes;  
    }

    public static void NotEqualsDo(this string a, string b, Action<string, string> action)
    {
      if (a != b)
      {
        action(a, b);
      }
    }
  }
}
