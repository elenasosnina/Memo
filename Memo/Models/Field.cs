using Memo.Infrastructure.Constants;
using Memo.ViewModels.Base;

namespace Memo.Models
{
    internal class Field : ViewModel
    {
        private int i;
        public int I { get => i; set => Set(ref i, value); }
        private int j;
        public int J { get => j; set => Set(ref j, value); }

        public string image = ImageArray.Black;

        private string imagePath = ImageArray.Black;
        public string ImagePath { get => imagePath; set => Set(ref imagePath, value); }
 
        private bool enable = true;
        public bool Enable { get => enable; set => Set(ref enable, value); }

        private bool selected = false;
        public bool Selected { get => selected; set => Set(ref selected, value); }

    }
}
