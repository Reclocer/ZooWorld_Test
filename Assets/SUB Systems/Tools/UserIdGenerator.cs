using System;
using System.Linq;
using UnityEngine;

namespace SUBS.Core.Tools
{
    public static class UserIdGenerator
    {
        private static System.Random _random = new System.Random();

        public static string GetRandomHexNumber(int digits)
        {
            byte[] buffer = new byte[digits / 2];
            _random.NextBytes(buffer);
            string result = String.Concat(buffer.Select(x => x.ToString("X2")).ToArray());

            if (digits % 2 == 0)
                return result;

            return result + _random.Next(16).ToString("X");
        }

        public static string GetUserId()
        {
            if (PlayerPrefs.HasKey("UserID"))
            {
                return CustomObscuredPrefs.GetString("UserID");
            }

            double time = DateTimeExtensions.GetCurrentTimeMilliSec();
            string hex = GetRandomHexNumber(5);
            string userId = string.Format("{0}-{1}", time, hex);
            CustomObscuredPrefs.SetString("UserID", userId);

            return userId;
        }

        public static string GetMatchId()
        {
            double time = DateTimeExtensions.GetCurrentTimeMilliSec();
            string hex = GetRandomHexNumber(10);

            return string.Format("{0}-{1}", time, hex);
        }

        public static string GetUserIdForWindow()
        {
            return "User id : " + GetUserId();
        }
    }
}