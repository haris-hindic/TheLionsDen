namespace WinUI.Helpers
{
    public static class ImageHelper
    {
        public static Image ByteArrayToImage(byte[] byteArrayIn)
        {
            MemoryStream ms = new MemoryStream(byteArrayIn, 0, byteArrayIn.Length);
            ms.Write(byteArrayIn, 0, byteArrayIn.Length);
            return Image.FromStream(ms, true);
        }

        public static byte[] ImageToByteArray(Image image)
        {
            MemoryStream stream = new MemoryStream();
            image?.Save(stream, image.RawFormat);
            return stream.ToArray();
        }
    }
}
