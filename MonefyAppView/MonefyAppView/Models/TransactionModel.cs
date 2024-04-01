using GalaSoft.MvvmLight;
using System;

namespace MonefyAppView.Models
{
    public class TransactionsModel : ObservableObject
    {
        private double _price;
        public double Price
        {
            get => _price;
            set => Set(ref _price, value);
        }

        private string _title;
        public string Title
        {
            get => _title;
            set => Set(ref _title, value);
        }

        private int _count;
        public int Count
        {
            get => _count;
            set => Set(ref _count, value);
        }
        public DateTime Date { get; set; }
    }
}
