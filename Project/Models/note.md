
<h2>in database it is of type VARBINARY(max) in SQL</h2>
<h2>import image from PC</h2>
</div>
private byte[] ImageToStream(string fileName)
		{
			MemoryStream stream = new MemoryStream();
		tryagain:
			try
			{
				Bitmap image = new Bitmap(fileName);
				image.Save(stream, System.Drawing.Imaging.ImageFormat.Jpeg);
			}
			catch (Exception ex)
			{
				goto tryagain;
			}

			return stream.ToArray();
		}
</div>

		