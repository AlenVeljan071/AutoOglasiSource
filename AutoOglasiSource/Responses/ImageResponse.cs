using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace AutoOglasiSource.Responses
{
    public class ImageResponse 
    {
        public int Id { get; set; }
        public string Link { get; set; }
        public ImageSource Image { get; set; }
        public bool IsSelected { get; set; }
    }
  
}
