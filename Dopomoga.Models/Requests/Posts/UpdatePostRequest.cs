using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dopomoga.Models.Requests.Posts
{
    public class UpdatePostRequest
    {
        public string GeorgianTitle { get; set; }
        public string GeorgianDescription { get; set; }
        public string UkrainianTitle { get; set; }
        public string UkrainianDescription { get; set; }
        public byte[] Thumbnail { get; set; }
        public int CategoryId { get; set; }
        public string RedirectUrl { get; set; }
        public bool ShowOnMainMenu { get; set; }
    }
}
