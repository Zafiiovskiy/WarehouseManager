﻿using System.IO;
using System.Windows.Media.Imaging;

namespace WMDesktopUI.Helpers
{
	public static class ConvertHelper
    {
		public static byte[] ImageToByteArray(BitmapImage imageC)
		{
			MemoryStream memStream = new MemoryStream();
			JpegBitmapEncoder encoder = new JpegBitmapEncoder();
			encoder.Frames.Add(BitmapFrame.Create(imageC));
			encoder.Save(memStream);
			return memStream.ToArray();
		}

	}
}
