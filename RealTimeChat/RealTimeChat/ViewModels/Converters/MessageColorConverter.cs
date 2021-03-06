﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace RealTimeChat.ViewModels.Converters
{
    public class MessageColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo language)
        {
            //var CheckOwner = (string)value;

            //if (!string.IsNullOrEmpty(value.ToString()))
            //{

            if (Equals(value, null))
                return "";

            string ValueStr = (string)value;
            var parameterLabel = parameter as Label;

            Color RetValue = new Color();

            if (ValueStr == parameterLabel.Text)
            {
                RetValue = Color.LightGreen;
            }
            else
            {
                RetValue = Color.White;
            }

            return RetValue;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            //if (Equals(value, null))
            //    return null;

            //string hackyBindedKeyValue = (string)value;
            //var toCompare = parameter as Label;

            //if (hackyBindedKeyValue == "End")
            //{
            //    hackyBindedKeyValue = "End";
            //}
            //else
            //{
            //    hackyBindedKeyValue = "Start";
            //}

            return "";
        }
    }
}
